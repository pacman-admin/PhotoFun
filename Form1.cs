using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
/*
 * Copyright (c) 2025 Langdon Staab <code@langdonstaab.ca>
 *
 * Permission to use, copy, modify, and distribute this software for any
 * purpose with or without fee is hereby granted, provided that the above
 * copyright notice and this permission notice appear in all copies.
 *
 * THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
 * WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
 * ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
 * WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
 * ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
 * OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
 */ 
namespace ImageProcessing
{
    public partial class Form1 : Form
    {
        //Image width and height constants.
        private static readonly int WIDTH = 320;
        private static readonly int HEIGHT = 240;

        //Window title text (preceded by filename)
        private static readonly string TITLE_SUFFIX = " - Paint.LMS";

        //The filter used in fileDialogs.
        private static readonly string DIALOG_FILTER = "Image Files (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";

        //The name (and path) of the currently "open" file
        private static string currentFilename = "Untitled";

        //Path to all history files of the current file
        private static string historyPath = "%TEMP%";

        //The count of all edits possible to undo/redo
        private static int editCount = 0;

        //The edit that is currently being displayed to the user
        private static int currentEditCount = 0;

        //Holds the current displayed image data
        private static Bitmap bitmapImage;
        private static Color[,] imageArray = new Color[WIDTH, HEIGHT];

        //Enables/disables all scrollwheel-related functionality
        private static bool canScroll = false;

        /// <summary>
        /// The side length of the square "pixels" that will result if the user clicks the "Pixelate" button.
        /// See <see cref="btnPixelate_Click"/> for one of the uses of this variable.
        /// </summary>
        private static int pixellateAmt = 2;

        //Scale/zoom multiplier
        private static int scaleFactor = 1;

        //Stores current position of the mouse cursor in the picturebox and coordinates of the pixel directly under the mouse cursor.
        private static Point mousePos = new Point(0, 0);
        private static Point pixelPos = new Point(0, 0);
        /// <summary>
        /// Store at which edit the image was saved to the current file.
        /// If the image was saved to an edit that no longer exists,
        /// (i.e. because it was undone and replaced with a different filter),
        /// this variable will be set to -1.
        /// The file cannot be saved (again) if
        /// <see cref="savedAtEditNum"/> equals <see cref="currentEditCount"/>.
        /// 
        /// Also see <seealso cref="hasUnsavedChanges"/> for more info.
        /// </summary>
        private static int savedAtEditNum = 0;
        public Form1()
        {
            InitializeComponent();
            //MessageBox.Show("Hi!");
            MouseWheel += new MouseEventHandler(handleMouseWheel);
            updateWindowTitle();
        }

        //************ Static Methods ************//

        //Determine whether there are unsaved changes to the current file.
        private static bool hasUnsavedChanges()
        {
            return (currentEditCount != savedAtEditNum) && (bitmapImage != null);
        }

        //Return the name of the current file, without the full path
        private static string getFileNameWithoutPath()
        {
            if (string.IsNullOrWhiteSpace(currentFilename))
            {
                return null;
            }
            string[] tempString = currentFilename.Split('\\');
            return tempString[tempString.Length - 1];
        }

        //Calculates scrollbar value and ensures it is within the acceptable range of values
        private static int getScrollNum(int delta, int current, int max)
        {
            if (scaleFactor < 2)
            {
                return 0;
            }
            current += Math.Sign(delta) * scaleFactor * 2;
            if (current > max)
            {
                return max;
            }
            if (current < 0)
            {
                return 0;
            }
            return current;
        }

        //Not my code
        private static void SetArrayFromBitmap()
        {
            if (bitmapImage == null)
            {
                return;
            }
            for (int row = 0; row < WIDTH; row++)
            {
                for (int col = 0; col < HEIGHT; col++)
                {
                    imageArray[row, col] = bitmapImage.GetPixel(row, col);
                }
            }
        }

        //Force a total garbage collection to keep down memory usage and leaks
        private static void invokeGC()
        {
            if (bitmapImage == null)
            {
                return;
            }
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true, true);
            GC.WaitForPendingFinalizers();
        }

        //Average the RGB values of every Color in imageArray
        private static void setToGreyscale()
        {
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    byte r = imageArray[i, j].R;
                    byte g = imageArray[i, j].G;
                    byte b = imageArray[i, j].B;
                    int grey = (r + g + b) / 3;
                    imageArray[i, j] = Color.FromArgb(grey, grey, grey);
                }
            }
        }

        //Flip imageArray leftside-right
        private static void lateralFlip()
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < (WIDTH / 2); j++)
                {
                    Color temp = imageArray[j, i];
                    imageArray[j, i] = imageArray[WIDTH - j - 1, i];
                    imageArray[WIDTH - j - 1, i] = temp;
                }
            }
        }

        //Flip imageArray upside-down
        private static void horizontalFlip()
        {
            for (int i = 0; i < (HEIGHT / 2); i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    Color temp = imageArray[j, i];
                    imageArray[j, i] = imageArray[j, HEIGHT - i - 1];
                    imageArray[j, HEIGHT - i - 1] = temp;
                }
            }
        }

        //Save bitmapImage to the file that was opened initially.
        private static void save()
        {
            save(false);
        }
        //Save the image to an image file, either to a temporary history file or to the current filename.
        private static void save(bool toTemp)
        {
            if (bitmapImage == null)
            {
                return;
            }
            string filename = toTemp ? getHistoryFilePath() : currentFilename;
            if (scaleFactor < 2)
            {
                bitmapImage.Save(filename);
            }
            else
            {
                getBitmap(0, 0, 1).Save(filename);
            }
            if (!toTemp)
            {
                savedAtEditNum = currentEditCount;
            }
            Console.WriteLine("Changes written to: " + filename);
        }

        //Calculates a directory path for history files based on the filename
        private static string getHistoryFilePath()
        {
            if (bitmapImage == null)
            {
                return null;
            }
            return historyPath + "\\" + currentEditCount + getFileNameWithoutPath();
        }

        //Create a bitmap from the contents of imageArray, scaled to the scale argument and offset by the offset arguments in pixels
        private static Bitmap getBitmap(int xOffset, int yOffset, int scale)
        {
            Bitmap b = new Bitmap(WIDTH, HEIGHT);
            for (int row = 0; row < WIDTH; row++)
            {
                for (int col = 0; col < HEIGHT; col++)
                {
                    b.SetPixel(row, col, imageArray[(row & -scale) / scale + xOffset, (col & -scale) / scale + yOffset]);
                }
            }
            return b;
        }

        //Update the image seen by the user with the return value of getBitmap()
        private void setBitmapFromArray()
        {
            if (bitmapImage == null)
            {
                return;
            }
            //Console.WriteLine("Scale: " + scaleFactor + "x");
            bitmapImage = getBitmap(sbHorizontal.Value, sbVertical.Value, scaleFactor);
            picImage.Image = bitmapImage;
            invokeGC();
        }

        /*
         * Handle scroll wheel input
         * If Ctrl is pressed, zoom
         * If Shift is pressed, scroll horizontally
         * Otherwise, scroll vertically
         */
        private void handleMouseWheel(object sender, MouseEventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            if (e.Delta == 0)
            {
                return;
            }
            if (canScroll)
            {
                if (ModifierKeys == Keys.Control)
                {
                    if (e.Delta < 0)
                    {
                        if (scaleFactor > 4)
                        {
                            return;
                        }
                        scaleFactor = scaleFactor << 1;
                    }
                    else
                    {
                        if (scaleFactor < 2)
                        {
                            return;
                        }
                        scaleFactor = scaleFactor >> 1;
                    }
                    showScaleChange();
                    sbHorizontal.Value = (int)(pixelPos.X * (double)sbHorizontal.Maximum / WIDTH);
                    sbVertical.Value = (int)(pixelPos.Y * (double)sbVertical.Maximum / HEIGHT);
                }
                else if (ModifierKeys == Keys.Shift)
                {
                    if (scaleFactor < 2)
                    {
                        return;
                    }
                    sbHorizontal.Value = getScrollNum(e.Delta, sbHorizontal.Value, sbHorizontal.Maximum);
                }
                else
                {
                    if (scaleFactor < 2)
                    {
                        return;
                    }
                    sbVertical.Value = getScrollNum(e.Delta, sbVertical.Value, sbVertical.Maximum);
                }
                showMousePixelPos();
                setBitmapFromArray();
            }
        }

        //Keep only the red channel
        private void btnRed_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    Color colour = imageArray[i, j];
                    imageArray[i, j] = Color.FromArgb(colour.R, 0, 0);
                }
            }
            setBitmapFromArray();
            showUnsavedChanges();
        }

        //Keep only the green channel
        private void btnGreen_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    Color colour = imageArray[i, j];
                    imageArray[i, j] = Color.FromArgb(0, colour.G, 0);
                }
            }
            setBitmapFromArray();
            showUnsavedChanges();
        }

        //Keep only the blue channel
        private void btnBlue_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    Color colour = imageArray[i, j];
                    imageArray[i, j] = Color.FromArgb(0, 0, colour.B);
                }
            }
            setBitmapFromArray();
            showUnsavedChanges();
        }

        //Redo an operation that was undone
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentEditCount == editCount)
            {
                return;
            }
            if (editCount > currentEditCount)
            {
                currentEditCount++;
                Console.WriteLine(currentEditCount + " | " + editCount);
                loadFromHistory();
                updateWindowTitle();
            }
        }

        //Set the image to the zoomed version
        private void cropToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            SetArrayFromBitmap();
            scaleFactor = 1;
            showScaleChange();
            showMousePixelPos();
            showUnsavedChanges();
        }

        //Blur the image by averaging the colour of a pixel with adjacent pixels
        private void btnBlur_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            Color[,] temp = new Color[WIDTH, HEIGHT];
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    ColourAverager c = new ColourAverager(imageArray[i, j]);
                    for (int k = Math.Max(0, i - 1); k < Math.Min(WIDTH, i + 2); k++)
                    {
                        for (int l = Math.Max(0, j - 1); l < Math.Min(HEIGHT, j + 2); l++)
                        {
                            c.addVal(imageArray[k, l]);
                        }
                    }
                    temp[i, j] = c.getAvg();
                }
            }
            imageArray = temp;
            setBitmapFromArray();
            showUnsavedChanges();
        }

        //Invert the colors of all the pixels
        private void btnNegative_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    int r = imageArray[i, j].R ^ 255;
                    int g = imageArray[i, j].G ^ 255;
                    int b = imageArray[i, j].B ^ 255;
                    imageArray[i, j] = Color.FromArgb(r, g, b);
                }
            }
            setBitmapFromArray();
            showUnsavedChanges();
        }

        //Lighten the image by 125% (not gamma corrected) and add 1
        private void btnLighten_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    int r = (imageArray[i, j].R + 1) * 5 / 4;
                    int g = (imageArray[i, j].G + 1) * 5 / 4;
                    int b = (imageArray[i, j].B + 1) * 5 / 4;
                    if ((++r & 256) == 256)
                    {
                        r = 255;
                    }
                    if ((++g & 256) == 256)
                    {
                        g = 255;
                    }
                    if ((++b & 256) == 256)
                    {
                        b = 255;
                    }
                    imageArray[i, j] = Color.FromArgb(r, g, b);
                }
            }
            setBitmapFromArray();
            showUnsavedChanges();
        }

        //Darken the image to 80%
        private void btnDarken_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    int r = imageArray[i, j].R / 5 * 4;
                    int g = imageArray[i, j].G / 5 * 4;
                    int b = imageArray[i, j].B / 5 * 4;
                    imageArray[i, j] = Color.FromArgb(r, g, b);
                }
            }
            setBitmapFromArray();
            showUnsavedChanges();
        }

        //Flip leftside-right
        private void btnFlipLateral_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            lateralFlip();
            setBitmapFromArray();
            showUnsavedChanges();
        }

        //Flip upside-down
        private void btnFlipHorizontal_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            horizontalFlip();
            setBitmapFromArray();
            showUnsavedChanges();
        }

        //Rotate 180 degrees
        private void btnRotate180_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            lateralFlip();
            horizontalFlip();
            setBitmapFromArray();
            showUnsavedChanges();
        }

        /*
         * Find the average red, green, and blue values for all pixels in the image
         * All pixels with a channel higher than the average for that channel are set to 255
         * Any pixel with a channel that has a value lower than the average has that channel set to 0
         */
        private void btnPolarize_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            int avgR = 0;
            int avgG = 0;
            int avgB = 0;
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    avgR += imageArray[i, j].R;
                    avgG += imageArray[i, j].G;
                    avgB += imageArray[i, j].B;
                }
            }
            avgR /= WIDTH * HEIGHT;
            avgG /= WIDTH * HEIGHT;
            avgB /= WIDTH * HEIGHT;
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    int r = imageArray[i, j].R > avgR ? 255 : 0;
                    int g = imageArray[i, j].G > avgG ? 255 : 0;
                    int b = imageArray[i, j].B > avgB ? 255 : 0;
                    imageArray[i, j] = Color.FromArgb(r, g, b);
                }
            }
            setBitmapFromArray();
            showUnsavedChanges();
        }

        //Set each pixel to the average of its red, green, and blue value.
        private void btnGrey_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            setToGreyscale();
            setBitmapFromArray();
            showUnsavedChanges();
        }

        //Give the image a sunset effect
        private void btnSunset_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    double r = imageArray[i, j].R;
                    int g = imageArray[i, j].G;
                    int b = imageArray[i, j].B;
                    r += Math.Max(0d, HEIGHT * .7 - j) * 1.1;
                    if (r > 255d)
                    {
                        r = 255d;
                    }
                    imageArray[i, j] = Color.FromArgb((int)r, g, b);
                }
            }
            setBitmapFromArray();
            showUnsavedChanges();
        }

        //Swap the bottom right and top left corners
        private void btnDiagonalSwap_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            for (int i = 0; i < (HEIGHT / 2); i++)
            {
                for (int j = 0; j < WIDTH / 2; j++)
                {
                    Color temp = imageArray[j, i];
                    imageArray[j, i] = imageArray[j + WIDTH / 2, i + HEIGHT / 2];
                    imageArray[j + WIDTH / 2, i + HEIGHT / 2] = temp;
                }
            }
            setBitmapFromArray();
            showUnsavedChanges();
        }

        //Pixelate the image
        private void btnPixelate_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            int pixelationMask = -(int)nudPixelateAmt.Value;
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    imageArray[i, j] = imageArray[i & pixelationMask, j & pixelationMask];
                }
            }
            setBitmapFromArray();
            showUnsavedChanges();
        }

        //Set the image to 4 identical tiles flipped interestingly
        private void btnTile_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            Color[,] temp = new Color[WIDTH, HEIGHT];
            for (int i = 0; i < HEIGHT / 2; i++)
            {
                for (int j = 0; j < WIDTH / 2; j++)
                {
                    temp[j, i] = imageArray[2 * j, 2 * i];
                    temp[WIDTH - j - 1, i] = imageArray[2 * j, 2 * i];
                    temp[WIDTH - j - 1, HEIGHT - i - 1] = imageArray[2 * j, 2 * i];
                    temp[j, HEIGHT - i - 1] = imageArray[2 * j, 2 * i];
                }
            }
            imageArray = temp;
            setBitmapFromArray();
            showUnsavedChanges();
        }


        //Handle physical dragging of scrollbars
        private void sbHorizontal_Scroll(object sender, ScrollEventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            setBitmapFromArray();
        }
        private void sbVertical_Scroll(object sender, ScrollEventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            setBitmapFromArray();
        }


        //Handle moving of the mouse pointer within picImage.
        private void picImage_MouseMove(object sender, MouseEventArgs e)
        {
            mousePos.X = e.X;
            mousePos.Y = e.Y;
            showMousePixelPos();
        }

        //When the mouse pointer enters the pictureBox, allow the user to scroll via the scrollwheel
        private void picImage_MouseEnter(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            canScroll = true;
        }

        /*
         * An alternate polarization filter performed via binary logic
         * Any value 128 or higher is set to 255
         * Otherwise it is set to 0
         */
        private void btnPolarize2_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    //Shift bits left so anything 128 or greater becomes 1
                    int r = imageArray[i, j].R >> 7;
                    //Multiply that by 255 so the value is either 0 or 255
                    r *= 255;
                    int g = imageArray[i, j].G >> 7;
                    g *= 255;
                    int b = imageArray[i, j].B >> 7;
                    b *= 255;
                    /*
                     * Alternate method (I kept it because it is cool)
                    byte r = imageArray[i, j].R;
                    byte g = imageArray[i, j].G;
                    byte b = imageArray[i, j].B;
                    r &= 128;
                    g &= 128;
                    b &= 128;
                    if (r == 128)
                    {
                        r = 255;
                    }
                    if (g == 128)
                    {
                        g = 255;
                    }
                    if (b == 128)
                    {
                        b = 255;
                    }*/
                    imageArray[i, j] = Color.FromArgb(r, g, b);
                }
            }
            setBitmapFromArray();
            showUnsavedChanges();
        }

        /*
         * When the pixelation setting is changed, set the value of the NUD to a power of 2
         * Only powers of 2 are possible ile due to the way the binary logic works.
         */
        private void nudPixelateAmt_ValueChanged(object sender, EventArgs e)
        {
            if (nudPixelateAmt.Value > pixellateAmt)
            {
                pixellateAmt = pixellateAmt << 1;
                nudPixelateAmt.Value = pixellateAmt;
            }
            if (nudPixelateAmt.Value < pixellateAmt)
            {
                pixellateAmt = pixellateAmt >> 1;
                nudPixelateAmt.Value = pixellateAmt;
            }
        }

        //Show the image at its actual size (100% zoom)
        private void actualSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            scaleFactor = 1;
            showScaleChange();
            showMousePixelPos();
        }

        //Load a past (of "future") edit from the next/previous history file that was saved
        private void loadFromHistory()
        {
            Image img = Image.FromFile(getHistoryFilePath());
            bitmapImage = new Bitmap(img, WIDTH, HEIGHT);
            SetArrayFromBitmap();
            setBitmapFromArray();
        }

        //Update the scale display label and the scrollbar maximum to what they should be
        private void showScaleChange()
        {
            lblScale.Text = scaleFactor + "00%";
            sbHorizontal.Maximum = (WIDTH - (WIDTH / scaleFactor));
            sbVertical.Maximum = (HEIGHT - (HEIGHT / scaleFactor));
        }

        //Record that there are unsaved changes and update the window title
        private void showUnsavedChanges()
        {
            if (savedAtEditNum > currentEditCount)
            {
                savedAtEditNum = -1;
            }
            editCount = ++currentEditCount;
            updateWindowTitle();
            save(true);
        }

        /*
         * Show coordinates of the pixel directly below the mouse
         * This method does not just display the current position of the mouse in the PictureBox.
         * The pixel coordinates are calculated using the scale, mousebox, and scrollbar offset.
         * Inspired by the identical feature in Paint.NET
         */
        private void showMousePixelPos()
        {
            pixelPos.X = (int)(mousePos.X / (double)scaleFactor + sbHorizontal.Value);
            pixelPos.Y = (int)(mousePos.Y / (double)scaleFactor + sbVertical.Value);
            lblCoordata.Text = pixelPos.X + 1 + ", " + pixelPos.Y + 1;
        }

        /*
         * Update the window title
         * 
         * Display the current filename, preceded by an asterisk if there are unsaved changes,
         * followed by the value of the TITLE_SUFFIX constant.
         */
        private void updateWindowTitle()
        {
            Text = (hasUnsavedChanges() ? "*" : "") + currentFilename + TITLE_SUFFIX;
        }


        //Open file menu item (Ctrl+O)
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (hasUnsavedChanges())
                {
                    if (MessageBox.Show("Do you want to discard your unsaved changes and open a new file?", "Discard unsaved edits?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        openFile();
                    }
                    return;
                }
                openFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Disable the ability to scroll when the mouse cursor leaves the Picturebox.
        private void picImage_MouseLeave(object sender, EventArgs e)
        {
            canScroll = false;
        }

        /*
         * Open a new image file from the selected file in an OpenFileDialog
         * This method has no error handling or overwrite protection
         * The caller must ensure errors are handled and all unsaved changes have been written.
         */
        private void openFile()
        {
            Stream stream;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = DIALOG_FILTER;
            dialog.FilterIndex = 2;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if ((stream = dialog.OpenFile()) != null)
                {
                    Image img = Image.FromStream(stream);
                    stream.Close();
                    bitmapImage = new Bitmap(img, WIDTH, HEIGHT);
                    currentFilename = dialog.FileName;
                    picImage.Image = bitmapImage;
                    updateWindowTitle();
                    SetArrayFromBitmap();
                    historyPath = Directory.CreateDirectory("LMSPaint_" + getFileNameWithoutPath()).ToString();
                    Console.WriteLine(Path.GetFullPath(historyPath));
                    save(true);
                }
            }
        }

        /*************** SAVING TO FILES ***************/

        //"Save" menu item (save as current file name or open save as dialog if this is a new file)(Ctrl+S)
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToCurrentFile();
        }

        //Close the current file and ask the user to save any unsaved changes, if there are any.
        private void close()
        {
            if (hasUnsavedChanges())
            {
                if (MessageBox.Show("Do you want to save your edits before closing?", "Save", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    saveToCurrentFile();
                }
            }
            currentFilename = "Untitled";
            savedAtEditNum = 0;
            currentEditCount = 0;
            editCount = 0;
            bitmapImage = null;
            picImage.Image = null;
            updateWindowTitle();
            //Delete history directory and all its contents.
            //The fact that any program can delete all your files without so much as a prompt is very concerning.
            //This is why ransomware attacks are so common and so easy.

            // Source - https://stackoverflow.com/a/1469790
            // Posted by RameshVel, modified by community. See post 'Timeline' for change history
            // Retrieved 2025-11-12, License - CC BY-SA 4.0
            string strCmdText;
            strCmdText = "/C rmdir /s /q " + historyPath;
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            historyPath = "%TEMP%";
        }


        /*
         * Save as the current file format.
         * Calls showSaveDialog() if an exception is thrown or the AddressBook is new.
         */
        private void saveToCurrentFile()
        {
            if (bitmapImage == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(currentFilename))
            {
                showSaveDialog();
                return;
            }
            try
            {
                save();
                updateWindowTitle();
            }
            catch
            {
                showSaveDialog();
            }
        }

        /*
         * Show a save dialog to the user;
         * If the user clicks the [Save] button,
         * set currentFilename to the path of the file chosen by the user
         * and call saveToCurrentFile().
         */
        private bool showSaveDialog()
        {
            if (bitmapImage == null)
            {
                return false;
            }
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = DIALOG_FILTER;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                currentFilename = dialog.FileName;
                saveToCurrentFile();
                updateWindowTitle();
                return true;
            }
            return false;
        }

        //Close the current file; the user is prompted to save unsaved changes first, if any.
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            close();
        }

        //Save to a different file than the one that was opened
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            showSaveDialog();
        }

        //Undo the last edit, or edits prior
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
            {
                return;
            }
            if (currentEditCount == 0)
            {
                return;
            }
            if (currentEditCount < 0)
            {
                currentEditCount = 0;
                return;
            }
            currentEditCount--;
            Console.WriteLine(currentEditCount + " | " + editCount);
            loadFromHistory();
            updateWindowTitle();
        }

        //Class to facilitate averaging of colours for blurring purposes.
        private class ColourAverager
        {
            private double r, g, b;
            private int count;

            /// <summary>
            /// Create a new colour averaging helper and add one colour to the average
            /// </summary>
            /// <param name="intial">The first colour in the average</param>
            internal ColourAverager(Color intial)
            {
                r = intial.R; g = intial.G; b = intial.B;
                count = 1;
            }

            /// <summary>
            /// Add another colour to the average
            /// </summary>
            /// <param name="color">The colour to add to the average</param>
            internal void addVal(Color color)
            {
                r += color.R;
                g += color.G;
                b += color.B;
                count++;
            }

            /// <summary>
            /// Calculate the average colour of all the colour values passed to this instance since creation.
            /// Both values added in the constuctor and via <see cref="addVal(Color)"/> are included in the final average.
            /// </summary>
            /// <returns>The average colour of all the colours passed to this instance.</returns>
            internal Color getAvg()
            {
                r /= count; g /= count; b /= count;
                return Color.FromArgb((int)r, (int)g, (int)b);
            }
        }
    }
}