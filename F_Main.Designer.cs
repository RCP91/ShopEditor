namespace ShopEditor
{
    partial class F_Main
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_Main));
            this.dgv_main = new System.Windows.Forms.DataGridView();
            this.dgv_cb_category = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgv_ico_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_item_look = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_count_sex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.pn_config = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cb_category = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_pathItemServer = new System.Windows.Forms.TextBox();
            this.tb_pathClient = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_pathServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_autoLoad = new System.Windows.Forms.CheckBox();
            this.cb_autoSave = new System.Windows.Forms.CheckBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_remove = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tb_display = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_main)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.pn_config.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_main
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgv_main.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_main.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(25)))));
            this.dgv_main.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_main.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv_main.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_main.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_cb_category,
            this.dgv_ico_type,
            this.dgv_item_look,
            this.dgv_price,
            this.dgv_count_sex,
            this.dgv_desc,
            this.dgv_title});
            this.dgv_main.Location = new System.Drawing.Point(19, 37);
            this.dgv_main.Margin = new System.Windows.Forms.Padding(10);
            this.dgv_main.Name = "dgv_main";
            this.dgv_main.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_main.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_main.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dgv_main.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_main.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_main.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_main.Size = new System.Drawing.Size(842, 270);
            this.dgv_main.TabIndex = 0;
            // 
            // dgv_cb_category
            // 
            this.dgv_cb_category.HeaderText = "Categories";
            this.dgv_cb_category.Name = "dgv_cb_category";
            this.dgv_cb_category.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dgv_ico_type
            // 
            this.dgv_ico_type.HeaderText = "IcoItem/IcoLook";
            this.dgv_ico_type.MaxInputLength = 25;
            this.dgv_ico_type.Name = "dgv_ico_type";
            this.dgv_ico_type.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ico_type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgv_item_look
            // 
            this.dgv_item_look.HeaderText = "Item/Look";
            this.dgv_item_look.MaxInputLength = 25;
            this.dgv_item_look.Name = "dgv_item_look";
            this.dgv_item_look.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_item_look.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgv_price
            // 
            this.dgv_price.HeaderText = "Price";
            this.dgv_price.MaxInputLength = 11;
            this.dgv_price.Name = "dgv_price";
            this.dgv_price.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_price.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgv_price.ToolTipText = "1";
            this.dgv_price.Width = 60;
            // 
            // dgv_count_sex
            // 
            this.dgv_count_sex.HeaderText = "Count/Sex";
            this.dgv_count_sex.MaxInputLength = 11;
            this.dgv_count_sex.Name = "dgv_count_sex";
            this.dgv_count_sex.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_count_sex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgv_count_sex.Width = 60;
            // 
            // dgv_desc
            // 
            this.dgv_desc.HeaderText = "Description";
            this.dgv_desc.MaxInputLength = 50;
            this.dgv_desc.Name = "dgv_desc";
            this.dgv_desc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_desc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgv_desc.Width = 160;
            // 
            // dgv_title
            // 
            this.dgv_title.HeaderText = "Title";
            this.dgv_title.MaxInputLength = 25;
            this.dgv_title.Name = "dgv_title";
            this.dgv_title.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_title.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgv_title.Width = 160;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.aboutToolStripMenuItem,
            this.toolStripTextBox1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(880, 27);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFileToolStripMenuItem,
            this.saveFileToolStripMenuItem,
            this.configurationToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(61, 23);
            this.toolStripMenuItem1.Text = "Options";
            // 
            // loadFileToolStripMenuItem
            // 
            this.loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
            this.loadFileToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.loadFileToolStripMenuItem.Text = "Load File";
            this.loadFileToolStripMenuItem.Click += new System.EventHandler(this.loadFileToolStripMenuItem_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.saveFileToolStripMenuItem.Text = "Save File";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.configurationToolStripMenuItem.Text = "Configuration";
            this.configurationToolStripMenuItem.Click += new System.EventHandler(this.configurationToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 23);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BackColor = System.Drawing.SystemColors.WindowText;
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.toolStripTextBox1.MaxLength = 30;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(150, 23);
            this.toolStripTextBox1.Text = "Search";
            // 
            // pn_config
            // 
            this.pn_config.BackColor = System.Drawing.Color.DarkGray;
            this.pn_config.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pn_config.Controls.Add(this.panel1);
            this.pn_config.Location = new System.Drawing.Point(24, 30);
            this.pn_config.Name = "pn_config";
            this.pn_config.Size = new System.Drawing.Size(344, 249);
            this.pn_config.TabIndex = 2;
            this.pn_config.Visible = false;
            this.pn_config.MouseLeave += new System.EventHandler(this.pn_config_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.cb_category);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tb_pathItemServer);
            this.panel1.Controls.Add(this.tb_pathClient);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tb_pathServer);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cb_autoLoad);
            this.panel1.Controls.Add(this.cb_autoSave);
            this.panel1.Controls.Add(this.btn_add);
            this.panel1.Controls.Add(this.btn_remove);
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(322, 227);
            this.panel1.TabIndex = 4;
            // 
            // cb_category
            // 
            this.cb_category.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cb_category.FormattingEnabled = true;
            this.cb_category.Location = new System.Drawing.Point(12, 29);
            this.cb_category.Name = "cb_category";
            this.cb_category.Size = new System.Drawing.Size(137, 21);
            this.cb_category.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label5.Location = new System.Drawing.Point(12, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Category";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(12, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Path to load items.srv:";
            // 
            // tb_pathItemServer
            // 
            this.tb_pathItemServer.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tb_pathItemServer.Location = new System.Drawing.Point(12, 109);
            this.tb_pathItemServer.Name = "tb_pathItemServer";
            this.tb_pathItemServer.Size = new System.Drawing.Size(291, 20);
            this.tb_pathItemServer.TabIndex = 12;
            this.tb_pathItemServer.DoubleClick += new System.EventHandler(this.tb_pathItemServer_DoubleClick);
            // 
            // tb_pathClient
            // 
            this.tb_pathClient.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tb_pathClient.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tb_pathClient.Location = new System.Drawing.Point(12, 187);
            this.tb_pathClient.Name = "tb_pathClient";
            this.tb_pathClient.ReadOnly = true;
            this.tb_pathClient.Size = new System.Drawing.Size(291, 20);
            this.tb_pathClient.TabIndex = 11;
            this.tb_pathClient.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tb_pathClient_MouseClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(12, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(211, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Path to save shop_items_client.lua to client";
            // 
            // tb_pathServer
            // 
            this.tb_pathServer.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tb_pathServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tb_pathServer.Location = new System.Drawing.Point(12, 148);
            this.tb_pathServer.Name = "tb_pathServer";
            this.tb_pathServer.ReadOnly = true;
            this.tb_pathServer.Size = new System.Drawing.Size(291, 20);
            this.tb_pathServer.TabIndex = 9;
            this.tb_pathServer.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tb_pathServer_MouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(12, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Path to load/save shop_items.lua server:";
            // 
            // cb_autoLoad
            // 
            this.cb_autoLoad.AutoSize = true;
            this.cb_autoLoad.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.cb_autoLoad.Location = new System.Drawing.Point(191, 52);
            this.cb_autoLoad.Name = "cb_autoLoad";
            this.cb_autoLoad.Size = new System.Drawing.Size(117, 17);
            this.cb_autoLoad.TabIndex = 5;
            this.cb_autoLoad.Text = "Auto Load Last File";
            this.cb_autoLoad.UseVisualStyleBackColor = true;
            this.cb_autoLoad.CheckedChanged += new System.EventHandler(this.cb_autoLoad_CheckedChanged);
            // 
            // cb_autoSave
            // 
            this.cb_autoSave.AutoSize = true;
            this.cb_autoSave.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.cb_autoSave.Location = new System.Drawing.Point(191, 29);
            this.cb_autoSave.Name = "cb_autoSave";
            this.cb_autoSave.Size = new System.Drawing.Size(76, 17);
            this.cb_autoSave.TabIndex = 4;
            this.cb_autoSave.Text = "Auto Save";
            this.cb_autoSave.UseVisualStyleBackColor = true;
            this.cb_autoSave.CheckedChanged += new System.EventHandler(this.cb_autoSave_CheckedChanged);
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(93, 56);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(56, 23);
            this.btn_add.TabIndex = 3;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(12, 56);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_remove.Size = new System.Drawing.Size(57, 23);
            this.btn_remove.TabIndex = 2;
            this.btn_remove.Text = "Remove";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tb_display
            // 
            this.tb_display.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(25)))));
            this.tb_display.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_display.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.tb_display.Location = new System.Drawing.Point(19, 320);
            this.tb_display.Multiline = true;
            this.tb_display.Name = "tb_display";
            this.tb_display.ReadOnly = true;
            this.tb_display.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_display.Size = new System.Drawing.Size(429, 129);
            this.tb_display.TabIndex = 3;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.Cyan;
            this.progressBar1.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.progressBar1.ForeColor = System.Drawing.Color.Cyan;
            this.progressBar1.Location = new System.Drawing.Point(24, 435);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(402, 10);
            this.progressBar1.TabIndex = 4;
            // 
            // F_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(880, 461);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pn_config);
            this.Controls.Add(this.dgv_main);
            this.Controls.Add(this.tb_display);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "F_Main";
            this.Text = "::: Shop Edit Server & Client :::";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_main)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pn_config.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        public System.Windows.Forms.Panel pn_config;
        public System.Windows.Forms.DataGridView dgv_main;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cb_autoLoad;
        private System.Windows.Forms.CheckBox cb_autoSave;
        private System.Windows.Forms.TextBox tb_pathClient;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_pathServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox tb_pathItemServer;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox tb_display;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ComboBox cb_category;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgv_cb_category;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ico_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_item_look;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_count_sex;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_title;
        public System.Windows.Forms.ProgressBar progressBar1;
    }
}

