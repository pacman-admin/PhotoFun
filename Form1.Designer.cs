namespace ImageProcessing
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            close();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBlue = new System.Windows.Forms.Button();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.btnNegative = new System.Windows.Forms.Button();
            this.btnLighten = new System.Windows.Forms.Button();
            this.btnFlipLateral = new System.Windows.Forms.Button();
            this.btnDarken = new System.Windows.Forms.Button();
            this.btnRotate180 = new System.Windows.Forms.Button();
            this.btnFlipHorizontal = new System.Windows.Forms.Button();
            this.btnPolarize = new System.Windows.Forms.Button();
            this.btnRed = new System.Windows.Forms.Button();
            this.btnGreen = new System.Windows.Forms.Button();
            this.btnTile = new System.Windows.Forms.Button();
            this.btnPixelate = new System.Windows.Forms.Button();
            this.btnDiagonalSwap = new System.Windows.Forms.Button();
            this.btnSunset = new System.Windows.Forms.Button();
            this.btnGrey = new System.Windows.Forms.Button();
            this.sbHorizontal = new System.Windows.Forms.HScrollBar();
            this.sbVertical = new System.Windows.Forms.VScrollBar();
            this.lblCoordata = new System.Windows.Forms.Label();
            this.btnPolarize2 = new System.Windows.Forms.Button();
            this.nudPixelateAmt = new System.Windows.Forms.NumericUpDown();
            this.lblScale = new System.Windows.Forms.Label();
            this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actualSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBlur = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPixelateAmt)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBlue
            // 
            this.btnBlue.Location = new System.Drawing.Point(10, 89);
            this.btnBlue.Name = "btnBlue";
            this.btnBlue.Size = new System.Drawing.Size(75, 25);
            this.btnBlue.TabIndex = 2;
            this.btnBlue.Text = "Blue";
            this.btnBlue.UseVisualStyleBackColor = true;
            this.btnBlue.Click += new System.EventHandler(this.btnBlue_Click);
            // 
            // picImage
            // 
            this.picImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picImage.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picImage.Location = new System.Drawing.Point(172, 27);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(320, 240);
            this.picImage.TabIndex = 3;
            this.picImage.TabStop = false;
            this.picImage.WaitOnLoad = true;
            this.picImage.MouseEnter += new System.EventHandler(this.picImage_MouseEnter);
            this.picImage.MouseLeave += new System.EventHandler(this.picImage_MouseLeave);
            this.picImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseMove);
            // 
            // btnNegative
            // 
            this.btnNegative.Location = new System.Drawing.Point(10, 120);
            this.btnNegative.Name = "btnNegative";
            this.btnNegative.Size = new System.Drawing.Size(75, 25);
            this.btnNegative.TabIndex = 3;
            this.btnNegative.Text = "Invert";
            this.btnNegative.UseVisualStyleBackColor = true;
            this.btnNegative.Click += new System.EventHandler(this.btnNegative_Click);
            // 
            // btnLighten
            // 
            this.btnLighten.Location = new System.Drawing.Point(10, 151);
            this.btnLighten.Name = "btnLighten";
            this.btnLighten.Size = new System.Drawing.Size(75, 25);
            this.btnLighten.TabIndex = 4;
            this.btnLighten.Text = "Lighten";
            this.btnLighten.UseVisualStyleBackColor = true;
            this.btnLighten.Click += new System.EventHandler(this.btnLighten_Click);
            // 
            // btnFlipLateral
            // 
            this.btnFlipLateral.Location = new System.Drawing.Point(91, 89);
            this.btnFlipLateral.Name = "btnFlipLateral";
            this.btnFlipLateral.Size = new System.Drawing.Size(75, 25);
            this.btnFlipLateral.TabIndex = 12;
            this.btnFlipLateral.Text = "Lateral Flip";
            this.btnFlipLateral.UseVisualStyleBackColor = true;
            this.btnFlipLateral.Click += new System.EventHandler(this.btnFlipLateral_Click);
            // 
            // btnDarken
            // 
            this.btnDarken.Location = new System.Drawing.Point(10, 182);
            this.btnDarken.Name = "btnDarken";
            this.btnDarken.Size = new System.Drawing.Size(75, 25);
            this.btnDarken.TabIndex = 5;
            this.btnDarken.Text = "Darken";
            this.btnDarken.UseVisualStyleBackColor = true;
            this.btnDarken.Click += new System.EventHandler(this.btnDarken_Click);
            // 
            // btnRotate180
            // 
            this.btnRotate180.Location = new System.Drawing.Point(91, 120);
            this.btnRotate180.Name = "btnRotate180";
            this.btnRotate180.Size = new System.Drawing.Size(75, 25);
            this.btnRotate180.TabIndex = 13;
            this.btnRotate180.Text = "Rotate 180";
            this.btnRotate180.UseVisualStyleBackColor = true;
            this.btnRotate180.Click += new System.EventHandler(this.btnRotate180_Click);
            // 
            // btnFlipHorizontal
            // 
            this.btnFlipHorizontal.Location = new System.Drawing.Point(91, 58);
            this.btnFlipHorizontal.Name = "btnFlipHorizontal";
            this.btnFlipHorizontal.Size = new System.Drawing.Size(75, 25);
            this.btnFlipHorizontal.TabIndex = 11;
            this.btnFlipHorizontal.Text = "Horizontal Flip";
            this.btnFlipHorizontal.UseVisualStyleBackColor = true;
            this.btnFlipHorizontal.Click += new System.EventHandler(this.btnFlipHorizontal_Click);
            // 
            // btnPolarize
            // 
            this.btnPolarize.Location = new System.Drawing.Point(91, 27);
            this.btnPolarize.Name = "btnPolarize";
            this.btnPolarize.Size = new System.Drawing.Size(75, 25);
            this.btnPolarize.TabIndex = 10;
            this.btnPolarize.Text = "Polarize";
            this.btnPolarize.UseVisualStyleBackColor = true;
            this.btnPolarize.Click += new System.EventHandler(this.btnPolarize_Click);
            // 
            // btnRed
            // 
            this.btnRed.Location = new System.Drawing.Point(10, 27);
            this.btnRed.Name = "btnRed";
            this.btnRed.Size = new System.Drawing.Size(75, 25);
            this.btnRed.TabIndex = 0;
            this.btnRed.Text = "Red";
            this.btnRed.UseVisualStyleBackColor = true;
            this.btnRed.Click += new System.EventHandler(this.btnRed_Click);
            // 
            // btnGreen
            // 
            this.btnGreen.Location = new System.Drawing.Point(10, 58);
            this.btnGreen.Name = "btnGreen";
            this.btnGreen.Size = new System.Drawing.Size(75, 25);
            this.btnGreen.TabIndex = 1;
            this.btnGreen.Text = "Green";
            this.btnGreen.UseVisualStyleBackColor = true;
            this.btnGreen.Click += new System.EventHandler(this.btnGreen_Click);
            // 
            // btnTile
            // 
            this.btnTile.Location = new System.Drawing.Point(91, 182);
            this.btnTile.Name = "btnTile";
            this.btnTile.Size = new System.Drawing.Size(75, 25);
            this.btnTile.TabIndex = 15;
            this.btnTile.Text = "Tile";
            this.btnTile.UseVisualStyleBackColor = true;
            this.btnTile.Click += new System.EventHandler(this.btnTile_Click);
            // 
            // btnPixelate
            // 
            this.btnPixelate.Location = new System.Drawing.Point(10, 275);
            this.btnPixelate.Name = "btnPixelate";
            this.btnPixelate.Size = new System.Drawing.Size(75, 25);
            this.btnPixelate.TabIndex = 7;
            this.btnPixelate.Text = "Pixelate";
            this.btnPixelate.UseVisualStyleBackColor = true;
            this.btnPixelate.Click += new System.EventHandler(this.btnPixelate_Click);
            // 
            // btnDiagonalSwap
            // 
            this.btnDiagonalSwap.Location = new System.Drawing.Point(91, 151);
            this.btnDiagonalSwap.Name = "btnDiagonalSwap";
            this.btnDiagonalSwap.Size = new System.Drawing.Size(75, 25);
            this.btnDiagonalSwap.TabIndex = 14;
            this.btnDiagonalSwap.Text = "Puzzle";
            this.btnDiagonalSwap.UseVisualStyleBackColor = true;
            this.btnDiagonalSwap.Click += new System.EventHandler(this.btnDiagonalSwap_Click);
            // 
            // btnSunset
            // 
            this.btnSunset.Location = new System.Drawing.Point(10, 213);
            this.btnSunset.Name = "btnSunset";
            this.btnSunset.Size = new System.Drawing.Size(75, 25);
            this.btnSunset.TabIndex = 6;
            this.btnSunset.Text = "Sunset";
            this.btnSunset.UseVisualStyleBackColor = true;
            this.btnSunset.Click += new System.EventHandler(this.btnSunset_Click);
            // 
            // btnGrey
            // 
            this.btnGrey.Location = new System.Drawing.Point(10, 244);
            this.btnGrey.Name = "btnGrey";
            this.btnGrey.Size = new System.Drawing.Size(75, 25);
            this.btnGrey.TabIndex = 9;
            this.btnGrey.Text = "Greyscale";
            this.btnGrey.UseVisualStyleBackColor = true;
            this.btnGrey.Click += new System.EventHandler(this.btnGrey_Click);
            // 
            // sbHorizontal
            // 
            this.sbHorizontal.LargeChange = 1;
            this.sbHorizontal.Location = new System.Drawing.Point(172, 270);
            this.sbHorizontal.Maximum = 0;
            this.sbHorizontal.Name = "sbHorizontal";
            this.sbHorizontal.Size = new System.Drawing.Size(320, 17);
            this.sbHorizontal.TabIndex = 18;
            this.sbHorizontal.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sbHorizontal_Scroll);
            // 
            // sbVertical
            // 
            this.sbVertical.LargeChange = 1;
            this.sbVertical.Location = new System.Drawing.Point(495, 27);
            this.sbVertical.Maximum = 0;
            this.sbVertical.Name = "sbVertical";
            this.sbVertical.Size = new System.Drawing.Size(17, 240);
            this.sbVertical.TabIndex = 17;
            this.sbVertical.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sbVertical_Scroll);
            // 
            // lblCoordata
            // 
            this.lblCoordata.AutoSize = true;
            this.lblCoordata.Location = new System.Drawing.Point(314, 287);
            this.lblCoordata.Name = "lblCoordata";
            this.lblCoordata.Size = new System.Drawing.Size(28, 13);
            this.lblCoordata.TabIndex = 20;
            this.lblCoordata.Text = "0 , 0";
            // 
            // btnPolarize2
            // 
            this.btnPolarize2.Location = new System.Drawing.Point(91, 213);
            this.btnPolarize2.Name = "btnPolarize2";
            this.btnPolarize2.Size = new System.Drawing.Size(75, 25);
            this.btnPolarize2.TabIndex = 16;
            this.btnPolarize2.Text = "Polarize2";
            this.btnPolarize2.UseVisualStyleBackColor = true;
            this.btnPolarize2.Click += new System.EventHandler(this.btnPolarize2_Click);
            // 
            // nudPixelateAmt
            // 
            this.nudPixelateAmt.Location = new System.Drawing.Point(91, 279);
            this.nudPixelateAmt.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudPixelateAmt.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudPixelateAmt.Name = "nudPixelateAmt";
            this.nudPixelateAmt.Size = new System.Drawing.Size(61, 20);
            this.nudPixelateAmt.TabIndex = 8;
            this.nudPixelateAmt.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudPixelateAmt.ValueChanged += new System.EventHandler(this.nudPixelateAmt_ValueChanged);
            // 
            // lblScale
            // 
            this.lblScale.AutoSize = true;
            this.lblScale.Location = new System.Drawing.Point(211, 287);
            this.lblScale.Name = "lblScale";
            this.lblScale.Size = new System.Drawing.Size(33, 13);
            this.lblScale.TabIndex = 19;
            this.lblScale.Text = "100%";
            // 
            // zoomInToolStripMenuItem
            // 
            this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(523, 24);
            this.menuStrip1.TabIndex = 36;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.cropToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // cropToolStripMenuItem
            // 
            this.cropToolStripMenuItem.Name = "cropToolStripMenuItem";
            this.cropToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.cropToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.cropToolStripMenuItem.Text = "Crop";
            this.cropToolStripMenuItem.Click += new System.EventHandler(this.cropToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actualSizeToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // actualSizeToolStripMenuItem
            // 
            this.actualSizeToolStripMenuItem.Name = "actualSizeToolStripMenuItem";
            this.actualSizeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)));
            this.actualSizeToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.actualSizeToolStripMenuItem.Text = "Actual Size";
            this.actualSizeToolStripMenuItem.Click += new System.EventHandler(this.actualSizeToolStripMenuItem_Click);
            // 
            // btnBlur
            // 
            this.btnBlur.Location = new System.Drawing.Point(91, 244);
            this.btnBlur.Name = "btnBlur";
            this.btnBlur.Size = new System.Drawing.Size(75, 25);
            this.btnBlur.TabIndex = 17;
            this.btnBlur.Text = "Blur";
            this.btnBlur.UseVisualStyleBackColor = true;
            this.btnBlur.Click += new System.EventHandler(this.btnBlur_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(523, 311);
            this.Controls.Add(this.btnBlur);
            this.Controls.Add(this.lblScale);
            this.Controls.Add(this.nudPixelateAmt);
            this.Controls.Add(this.btnPolarize2);
            this.Controls.Add(this.lblCoordata);
            this.Controls.Add(this.sbVertical);
            this.Controls.Add(this.sbHorizontal);
            this.Controls.Add(this.btnGrey);
            this.Controls.Add(this.btnSunset);
            this.Controls.Add(this.btnTile);
            this.Controls.Add(this.btnPixelate);
            this.Controls.Add(this.btnDiagonalSwap);
            this.Controls.Add(this.btnRed);
            this.Controls.Add(this.btnGreen);
            this.Controls.Add(this.btnPolarize);
            this.Controls.Add(this.btnRotate180);
            this.Controls.Add(this.btnFlipHorizontal);
            this.Controls.Add(this.btnFlipLateral);
            this.Controls.Add(this.btnDarken);
            this.Controls.Add(this.btnLighten);
            this.Controls.Add(this.btnNegative);
            this.Controls.Add(this.btnBlue);
            this.Controls.Add(this.picImage);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Paint.LMS";
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPixelateAmt)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBlue;
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Button btnNegative;
        private System.Windows.Forms.Button btnLighten;
        private System.Windows.Forms.Button btnFlipLateral;
        private System.Windows.Forms.Button btnDarken;
        private System.Windows.Forms.Button btnRotate180;
        private System.Windows.Forms.Button btnFlipHorizontal;
        private System.Windows.Forms.Button btnPolarize;
        private System.Windows.Forms.Button btnRed;
        private System.Windows.Forms.Button btnGreen;
        private System.Windows.Forms.Button btnTile;
        private System.Windows.Forms.Button btnPixelate;
        private System.Windows.Forms.Button btnDiagonalSwap;
        private System.Windows.Forms.Button btnSunset;
        private System.Windows.Forms.Button btnGrey;
        private System.Windows.Forms.HScrollBar sbHorizontal;
        private System.Windows.Forms.VScrollBar sbVertical;
        private System.Windows.Forms.Label lblCoordata;
        private System.Windows.Forms.Button btnPolarize2;
        private System.Windows.Forms.NumericUpDown nudPixelateAmt;
        private System.Windows.Forms.Label lblScale;
        private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actualSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cropToolStripMenuItem;
        private System.Windows.Forms.Button btnBlur;
    }
}

