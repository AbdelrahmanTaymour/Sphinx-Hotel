namespace Hotel.Items
{
    partial class frmManageItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManageItems));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbItemTypes = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtFilterBy = new Guna.UI2.WinForms.Guna2TextBox();
            this.cbFilterBy = new Guna.UI2.WinForms.Guna2ComboBox();
            this.bunifuLabel2 = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuLabel1 = new Bunifu.UI.WinForms.BunifuLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsAddNewItemType = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsDeleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEditItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvItemsList = new Bunifu.UI.WinForms.BunifuDataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsShowItemDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsAddNewItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsEditItemType = new System.Windows.Forms.ToolStripMenuItem();
            this.bunifuShadowPanel1 = new Bunifu.UI.WinForms.BunifuShadowPanel();
            this.btnAddNewItem = new Guna.UI2.WinForms.Guna2PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemsList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.bunifuShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddNewItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbItemTypes
            // 
            this.cbItemTypes.BackColor = System.Drawing.Color.Transparent;
            this.cbItemTypes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbItemTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItemTypes.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbItemTypes.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbItemTypes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbItemTypes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbItemTypes.ItemHeight = 30;
            this.cbItemTypes.Location = new System.Drawing.Point(360, 335);
            this.cbItemTypes.Name = "cbItemTypes";
            this.cbItemTypes.Size = new System.Drawing.Size(137, 36);
            this.cbItemTypes.TabIndex = 25;
            this.cbItemTypes.Visible = false;
            this.cbItemTypes.SelectedIndexChanged += new System.EventHandler(this.cbItemTypes_SelectedIndexChanged);
            // 
            // txtFilterBy
            // 
            this.txtFilterBy.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFilterBy.DefaultText = "";
            this.txtFilterBy.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtFilterBy.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtFilterBy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFilterBy.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFilterBy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFilterBy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFilterBy.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFilterBy.Location = new System.Drawing.Point(360, 335);
            this.txtFilterBy.Name = "txtFilterBy";
            this.txtFilterBy.PasswordChar = '\0';
            this.txtFilterBy.PlaceholderText = "";
            this.txtFilterBy.SelectedText = "";
            this.txtFilterBy.Size = new System.Drawing.Size(200, 36);
            this.txtFilterBy.TabIndex = 24;
            this.txtFilterBy.Visible = false;
            this.txtFilterBy.TextChanged += new System.EventHandler(this.txtFilterBy_TextChanged);
            this.txtFilterBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterBy_KeyPress);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.BackColor = System.Drawing.Color.Transparent;
            this.cbFilterBy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbFilterBy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbFilterBy.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.cbFilterBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbFilterBy.ItemHeight = 30;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "Item ID",
            "Item Name",
            "Item Type"});
            this.cbFilterBy.Location = new System.Drawing.Point(144, 335);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(178, 36);
            this.cbFilterBy.StartIndex = 0;
            this.cbFilterBy.TabIndex = 23;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // bunifuLabel2
            // 
            this.bunifuLabel2.AllowParentOverrides = false;
            this.bunifuLabel2.AutoEllipsis = false;
            this.bunifuLabel2.CursorType = null;
            this.bunifuLabel2.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F);
            this.bunifuLabel2.Location = new System.Drawing.Point(46, 341);
            this.bunifuLabel2.Name = "bunifuLabel2";
            this.bunifuLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel2.Size = new System.Drawing.Size(78, 25);
            this.bunifuLabel2.TabIndex = 16;
            this.bunifuLabel2.Text = "Filter By:";
            this.bunifuLabel2.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel2.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuLabel1
            // 
            this.bunifuLabel1.AllowParentOverrides = false;
            this.bunifuLabel1.AutoEllipsis = false;
            this.bunifuLabel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel1.CursorType = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.bunifuLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bunifuLabel1.Location = new System.Drawing.Point(552, 221);
            this.bunifuLabel1.Name = "bunifuLabel1";
            this.bunifuLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel1.Size = new System.Drawing.Size(156, 32);
            this.bunifuLabel1.TabIndex = 15;
            this.bunifuLabel1.Text = "Manage Items";
            this.bunifuLabel1.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel1.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(238, 6);
            // 
            // cmsAddNewItemType
            // 
            this.cmsAddNewItemType.ForeColor = System.Drawing.Color.White;
            this.cmsAddNewItemType.Image = global::Hotel.Properties.Resources.add_item_32;
            this.cmsAddNewItemType.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmsAddNewItemType.Name = "cmsAddNewItemType";
            this.cmsAddNewItemType.Size = new System.Drawing.Size(241, 38);
            this.cmsAddNewItemType.Text = "Add New Item Type";
            this.cmsAddNewItemType.Click += new System.EventHandler(this.cmsAddNewItemType_Click);
            // 
            // cmsDeleteItem
            // 
            this.cmsDeleteItem.ForeColor = System.Drawing.Color.White;
            this.cmsDeleteItem.Image = global::Hotel.Properties.Resources.delete_reservation_40;
            this.cmsDeleteItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmsDeleteItem.Name = "cmsDeleteItem";
            this.cmsDeleteItem.Size = new System.Drawing.Size(241, 38);
            this.cmsDeleteItem.Text = "Delete Item";
            this.cmsDeleteItem.Click += new System.EventHandler(this.cmsDeleteItem_Click);
            // 
            // cmsEditItem
            // 
            this.cmsEditItem.ForeColor = System.Drawing.Color.White;
            this.cmsEditItem.Image = global::Hotel.Properties.Resources.edit_reservation32;
            this.cmsEditItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmsEditItem.Name = "cmsEditItem";
            this.cmsEditItem.Size = new System.Drawing.Size(241, 38);
            this.cmsEditItem.Text = "Edit Item";
            this.cmsEditItem.Click += new System.EventHandler(this.cmsEditItem_Click);
            // 
            // dgvItemsList
            // 
            this.dgvItemsList.AllowCustomTheming = false;
            this.dgvItemsList.AllowUserToAddRows = false;
            this.dgvItemsList.AllowUserToDeleteRows = false;
            this.dgvItemsList.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgvItemsList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItemsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemsList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItemsList.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvItemsList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvItemsList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvItemsList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemsList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvItemsList.ColumnHeadersHeight = 40;
            this.dgvItemsList.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvItemsList.CurrentTheme.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.dgvItemsList.CurrentTheme.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvItemsList.CurrentTheme.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvItemsList.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.dgvItemsList.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvItemsList.CurrentTheme.BackColor = System.Drawing.Color.White;
            this.dgvItemsList.CurrentTheme.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.dgvItemsList.CurrentTheme.HeaderStyle.BackColor = System.Drawing.Color.DodgerBlue;
            this.dgvItemsList.CurrentTheme.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.dgvItemsList.CurrentTheme.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvItemsList.CurrentTheme.HeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            this.dgvItemsList.CurrentTheme.HeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvItemsList.CurrentTheme.Name = null;
            this.dgvItemsList.CurrentTheme.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvItemsList.CurrentTheme.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvItemsList.CurrentTheme.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvItemsList.CurrentTheme.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.dgvItemsList.CurrentTheme.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemsList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvItemsList.EnableHeadersVisualStyles = false;
            this.dgvItemsList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.dgvItemsList.HeaderBackColor = System.Drawing.Color.DodgerBlue;
            this.dgvItemsList.HeaderBgColor = System.Drawing.Color.Empty;
            this.dgvItemsList.HeaderForeColor = System.Drawing.Color.White;
            this.dgvItemsList.Location = new System.Drawing.Point(33, 386);
            this.dgvItemsList.Name = "dgvItemsList";
            this.dgvItemsList.ReadOnly = true;
            this.dgvItemsList.RowHeadersVisible = false;
            this.dgvItemsList.RowTemplate.Height = 40;
            this.dgvItemsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemsList.Size = new System.Drawing.Size(1159, 495);
            this.dgvItemsList.TabIndex = 13;
            this.dgvItemsList.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
            this.dgvItemsList.DoubleClick += new System.EventHandler(this.dgvItemsList_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(96)))));
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsShowItemDetails,
            this.toolStripSeparator2,
            this.cmsAddNewItem,
            this.cmsAddNewItemType,
            this.toolStripSeparator3,
            this.cmsEditItem,
            this.cmsEditItemType,
            this.toolStripSeparator1,
            this.cmsDeleteItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(242, 272);
            // 
            // cmsShowItemDetails
            // 
            this.cmsShowItemDetails.ForeColor = System.Drawing.Color.White;
            this.cmsShowItemDetails.Image = ((System.Drawing.Image)(resources.GetObject("cmsShowItemDetails.Image")));
            this.cmsShowItemDetails.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmsShowItemDetails.Name = "cmsShowItemDetails";
            this.cmsShowItemDetails.Size = new System.Drawing.Size(241, 38);
            this.cmsShowItemDetails.Text = "Show Item Details";
            this.cmsShowItemDetails.Click += new System.EventHandler(this.cmsShowItemDetails_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(238, 6);
            // 
            // cmsAddNewItem
            // 
            this.cmsAddNewItem.ForeColor = System.Drawing.Color.White;
            this.cmsAddNewItem.Image = global::Hotel.Properties.Resources.add_item_32;
            this.cmsAddNewItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmsAddNewItem.Name = "cmsAddNewItem";
            this.cmsAddNewItem.Size = new System.Drawing.Size(241, 38);
            this.cmsAddNewItem.Text = "Add New Item";
            this.cmsAddNewItem.Click += new System.EventHandler(this.cmsAddNewItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(238, 6);
            // 
            // cmsEditItemType
            // 
            this.cmsEditItemType.ForeColor = System.Drawing.Color.White;
            this.cmsEditItemType.Image = global::Hotel.Properties.Resources.edit_reservation32;
            this.cmsEditItemType.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmsEditItemType.Name = "cmsEditItemType";
            this.cmsEditItemType.Size = new System.Drawing.Size(241, 38);
            this.cmsEditItemType.Text = "Edit Item Type";
            this.cmsEditItemType.Click += new System.EventHandler(this.cmsEditItemType_Click);
            // 
            // bunifuShadowPanel1
            // 
            this.bunifuShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuShadowPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(57)))), ((int)(((byte)(109)))));
            this.bunifuShadowPanel1.BorderRadius = 60;
            this.bunifuShadowPanel1.BorderThickness = 1;
            this.bunifuShadowPanel1.Controls.Add(this.btnAddNewItem);
            this.bunifuShadowPanel1.Controls.Add(this.cbItemTypes);
            this.bunifuShadowPanel1.Controls.Add(this.txtFilterBy);
            this.bunifuShadowPanel1.Controls.Add(this.cbFilterBy);
            this.bunifuShadowPanel1.Controls.Add(this.bunifuLabel2);
            this.bunifuShadowPanel1.Controls.Add(this.bunifuLabel1);
            this.bunifuShadowPanel1.Controls.Add(this.pictureBox1);
            this.bunifuShadowPanel1.Controls.Add(this.dgvItemsList);
            this.bunifuShadowPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bunifuShadowPanel1.FillStyle = Bunifu.UI.WinForms.BunifuShadowPanel.FillStyles.Solid;
            this.bunifuShadowPanel1.GradientMode = Bunifu.UI.WinForms.BunifuShadowPanel.GradientModes.Vertical;
            this.bunifuShadowPanel1.Location = new System.Drawing.Point(0, 0);
            this.bunifuShadowPanel1.Name = "bunifuShadowPanel1";
            this.bunifuShadowPanel1.PanelColor = System.Drawing.Color.White;
            this.bunifuShadowPanel1.PanelColor2 = System.Drawing.Color.White;
            this.bunifuShadowPanel1.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(57)))), ((int)(((byte)(109)))));
            this.bunifuShadowPanel1.ShadowDept = 2;
            this.bunifuShadowPanel1.ShadowDepth = 5;
            this.bunifuShadowPanel1.ShadowStyle = Bunifu.UI.WinForms.BunifuShadowPanel.ShadowStyles.ForwardDiagonal;
            this.bunifuShadowPanel1.ShadowTopLeftVisible = false;
            this.bunifuShadowPanel1.Size = new System.Drawing.Size(1245, 937);
            this.bunifuShadowPanel1.Style = Bunifu.UI.WinForms.BunifuShadowPanel.BevelStyles.Flat;
            this.bunifuShadowPanel1.TabIndex = 1;
            // 
            // btnAddNewItem
            // 
            this.btnAddNewItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNewItem.Image")));
            this.btnAddNewItem.ImageRotate = 0F;
            this.btnAddNewItem.Location = new System.Drawing.Point(1103, 324);
            this.btnAddNewItem.Name = "btnAddNewItem";
            this.btnAddNewItem.Size = new System.Drawing.Size(89, 47);
            this.btnAddNewItem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnAddNewItem.TabIndex = 26;
            this.btnAddNewItem.TabStop = false;
            this.btnAddNewItem.Click += new System.EventHandler(this.btnAddNewItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Hotel.Properties.Resources.Menu1_Icon;
            this.pictureBox1.Location = new System.Drawing.Point(518, 57);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(221, 158);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // frmManageItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(57)))), ((int)(((byte)(109)))));
            this.ClientSize = new System.Drawing.Size(1245, 937);
            this.Controls.Add(this.bunifuShadowPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmManageItems";
            this.Text = "Manage Items";
            this.Load += new System.EventHandler(this.frmManageItems_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemsList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.bunifuShadowPanel1.ResumeLayout(false);
            this.bunifuShadowPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddNewItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2PictureBox btnAddNewItem;
        private Guna.UI2.WinForms.Guna2ComboBox cbItemTypes;
        private Guna.UI2.WinForms.Guna2TextBox txtFilterBy;
        private Guna.UI2.WinForms.Guna2ComboBox cbFilterBy;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel2;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cmsAddNewItemType;
        private System.Windows.Forms.ToolStripMenuItem cmsDeleteItem;
        private System.Windows.Forms.ToolStripMenuItem cmsEditItem;
        private Bunifu.UI.WinForms.BunifuDataGridView dgvItemsList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmsShowItemDetails;
        private System.Windows.Forms.ToolStripMenuItem cmsAddNewItem;
        private System.Windows.Forms.ToolStripMenuItem cmsEditItemType;
        private Bunifu.UI.WinForms.BunifuShadowPanel bunifuShadowPanel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}