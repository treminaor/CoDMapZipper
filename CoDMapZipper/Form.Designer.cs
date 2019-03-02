namespace CoDMapZipper
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.fileToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.openMenuItem = new System.Windows.Forms.MenuItem();
            this.saveMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuRecentFile = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.instructionsMenuItem = new System.Windows.Forms.MenuItem();
            this.checkForUpdatesMenuItem = new System.Windows.Forms.MenuItem();
            this.aboutMenuItem = new System.Windows.Forms.MenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imagesCheck = new System.Windows.Forms.CheckBox();
            this.materialsCheck = new System.Windows.Forms.CheckBox();
            this.xmodelsCheck = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.launcherCfg = new System.Windows.Forms.CheckBox();
            this.prefabsCheck = new System.Windows.Forms.CheckBox();
            this.mapCheck = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.zipCheck = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.excludeOldCheck = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.zonesource = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileToolStripMenuItem,
            this.menuItem7});
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Index = 0;
            this.fileToolStripMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.openMenuItem,
            this.saveMenuItem,
            this.menuItem2,
            this.menuRecentFile,
            this.menuItem3,
            this.exitMenuItem});
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openMenuItem
            // 
            this.openMenuItem.Index = 0;
            this.openMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.openMenuItem.Text = "Open";
            this.openMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Enabled = false;
            this.saveMenuItem.Index = 1;
            this.saveMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.saveMenuItem.Text = "Save Preset";
            this.saveMenuItem.Click += new System.EventHandler(this.savePresetToolStripMenuItem_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 2;
            this.menuItem2.Text = "-";
            // 
            // menuRecentFile
            // 
            this.menuRecentFile.Index = 3;
            this.menuRecentFile.Text = "Recent";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 4;
            this.menuItem3.Text = "-";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Index = 5;
            this.exitMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlQ;
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 1;
            this.menuItem7.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.instructionsMenuItem,
            this.checkForUpdatesMenuItem,
            this.aboutMenuItem});
            this.menuItem7.Text = "Help";
            // 
            // instructionsMenuItem
            // 
            this.instructionsMenuItem.Enabled = false;
            this.instructionsMenuItem.Index = 0;
            this.instructionsMenuItem.Text = "Instructions";
            this.instructionsMenuItem.Click += new System.EventHandler(this.instructionsToolStripMenuItem_Click);
            // 
            // checkForUpdatesMenuItem
            // 
            this.checkForUpdatesMenuItem.Enabled = false;
            this.checkForUpdatesMenuItem.Index = 1;
            this.checkForUpdatesMenuItem.Text = "Check for Updates";
            this.checkForUpdatesMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Index = 2;
            this.aboutMenuItem.Text = "About";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "csv.bmp");
            this.imageList1.Images.SetKeyName(1, "iwi.bmp");
            this.imageList1.Images.SetKeyName(2, "mtl.bmp");
            this.imageList1.Images.SetKeyName(3, "3d.bmp");
            this.imageList1.Images.SetKeyName(4, "61241_weapon_gun_.ico");
            this.imageList1.Images.SetKeyName(5, "Studiomx-Leomx-Music.ico");
            this.imageList1.Images.SetKeyName(6, "fx.bmp");
            this.imageList1.Images.SetKeyName(7, "ai.bmp");
            this.imageList1.Images.SetKeyName(8, "ui.bmp");
            this.imageList1.Images.SetKeyName(9, "ui.bmp");
            this.imageList1.Images.SetKeyName(10, "techset.bmp");
            this.imageList1.Images.SetKeyName(11, "xanim.bmp");
            this.imageList1.Images.SetKeyName(12, "font.bmp");
            this.imageList1.Images.SetKeyName(13, "gsc.bmp");
            this.imageList1.Images.SetKeyName(14, "sound.bmp");
            // 
            // imagesCheck
            // 
            this.imagesCheck.AutoSize = true;
            this.imagesCheck.Checked = true;
            this.imagesCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.imagesCheck.Location = new System.Drawing.Point(114, 18);
            this.imagesCheck.Name = "imagesCheck";
            this.imagesCheck.Size = new System.Drawing.Size(59, 17);
            this.imagesCheck.TabIndex = 0;
            this.imagesCheck.Text = "images";
            this.imagesCheck.UseVisualStyleBackColor = true;
            // 
            // materialsCheck
            // 
            this.materialsCheck.AutoSize = true;
            this.materialsCheck.Checked = true;
            this.materialsCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.materialsCheck.Location = new System.Drawing.Point(114, 41);
            this.materialsCheck.Name = "materialsCheck";
            this.materialsCheck.Size = new System.Drawing.Size(144, 17);
            this.materialsCheck.TabIndex = 1;
            this.materialsCheck.Text = "materials (incl. properties)";
            this.materialsCheck.UseVisualStyleBackColor = true;
            // 
            // xmodelsCheck
            // 
            this.xmodelsCheck.AutoSize = true;
            this.xmodelsCheck.Checked = true;
            this.xmodelsCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.xmodelsCheck.Location = new System.Drawing.Point(114, 64);
            this.xmodelsCheck.Name = "xmodelsCheck";
            this.xmodelsCheck.Size = new System.Drawing.Size(152, 17);
            this.xmodelsCheck.TabIndex = 3;
            this.xmodelsCheck.Text = "xmodels (incl. surfs + parts)";
            this.xmodelsCheck.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.zonesource);
            this.groupBox1.Controls.Add(this.launcherCfg);
            this.groupBox1.Controls.Add(this.prefabsCheck);
            this.groupBox1.Controls.Add(this.mapCheck);
            this.groupBox1.Controls.Add(this.imagesCheck);
            this.groupBox1.Controls.Add(this.xmodelsCheck);
            this.groupBox1.Controls.Add(this.materialsCheck);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 108);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Assets to pack:";
            // 
            // launcherCfg
            // 
            this.launcherCfg.AutoSize = true;
            this.launcherCfg.Checked = true;
            this.launcherCfg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.launcherCfg.Location = new System.Drawing.Point(6, 64);
            this.launcherCfg.Name = "launcherCfg";
            this.launcherCfg.Size = new System.Drawing.Size(103, 17);
            this.launcherCfg.TabIndex = 9;
            this.launcherCfg.Text = "Launcher config";
            this.launcherCfg.UseVisualStyleBackColor = true;
            // 
            // prefabsCheck
            // 
            this.prefabsCheck.AutoSize = true;
            this.prefabsCheck.Checked = true;
            this.prefabsCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.prefabsCheck.Location = new System.Drawing.Point(6, 41);
            this.prefabsCheck.Name = "prefabsCheck";
            this.prefabsCheck.Size = new System.Drawing.Size(61, 17);
            this.prefabsCheck.TabIndex = 8;
            this.prefabsCheck.Text = "prefabs";
            this.prefabsCheck.UseVisualStyleBackColor = true;
            // 
            // mapCheck
            // 
            this.mapCheck.AutoSize = true;
            this.mapCheck.Checked = true;
            this.mapCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mapCheck.Location = new System.Drawing.Point(6, 18);
            this.mapCheck.Name = "mapCheck";
            this.mapCheck.Size = new System.Drawing.Size(70, 17);
            this.mapCheck.TabIndex = 6;
            this.mapCheck.Text = ".map files";
            this.mapCheck.UseVisualStyleBackColor = true;
            this.mapCheck.CheckedChanged += new System.EventHandler(this.mapCheck_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.zipCheck);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.excludeOldCheck);
            this.groupBox2.Location = new System.Drawing.Point(11, 205);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(298, 76);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Packing Options:";
            // 
            // zipCheck
            // 
            this.zipCheck.AutoSize = true;
            this.zipCheck.Checked = true;
            this.zipCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.zipCheck.Location = new System.Drawing.Point(7, 49);
            this.zipCheck.Name = "zipCheck";
            this.zipCheck.Size = new System.Drawing.Size(188, 17);
            this.zipCheck.TabIndex = 2;
            this.zipCheck.Text = "Create ZIP archive of export folder";
            this.zipCheck.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "(files older than 1/1/2013 will not be packed.)";
            // 
            // excludeOldCheck
            // 
            this.excludeOldCheck.AutoSize = true;
            this.excludeOldCheck.Checked = true;
            this.excludeOldCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.excludeOldCheck.Location = new System.Drawing.Point(7, 20);
            this.excludeOldCheck.Name = "excludeOldCheck";
            this.excludeOldCheck.Size = new System.Drawing.Size(176, 17);
            this.excludeOldCheck.TabIndex = 0;
            this.excludeOldCheck.Text = "Exclude Uneeded WaW Assets";
            this.excludeOldCheck.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 287);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(299, 55);
            this.button1.TabIndex = 6;
            this.button1.Text = "Browse for .map to pack...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // zonesource
            // 
            this.zonesource.AutoSize = true;
            this.zonesource.Checked = true;
            this.zonesource.CheckState = System.Windows.Forms.CheckState.Checked;
            this.zonesource.Location = new System.Drawing.Point(6, 87);
            this.zonesource.Name = "zonesource";
            this.zonesource.Size = new System.Drawing.Size(87, 17);
            this.zonesource.TabIndex = 10;
            this.zonesource.Text = "zone_source";
            this.zonesource.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 354);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(339, 413);
            this.Menu = this.mainMenu1;
            this.MinimumSize = new System.Drawing.Size(339, 413);
            this.Name = "Form1";
            this.Text = "CoD Map Zipper";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuItem openMenuItem;
        private System.Windows.Forms.MenuItem saveMenuItem;
        private System.Windows.Forms.MenuItem menuRecentFile;
        private System.Windows.Forms.MenuItem exitMenuItem;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem aboutMenuItem;
        private System.Windows.Forms.MenuItem instructionsMenuItem;
        private System.Windows.Forms.MenuItem checkForUpdatesMenuItem;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox imagesCheck;
        private System.Windows.Forms.CheckBox materialsCheck;
        private System.Windows.Forms.CheckBox xmodelsCheck;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox excludeOldCheck;
        private System.Windows.Forms.CheckBox prefabsCheck;
        private System.Windows.Forms.CheckBox mapCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox zipCheck;
        private System.Windows.Forms.CheckBox launcherCfg;
        private System.Windows.Forms.CheckBox zonesource;
    }
}

