﻿namespace Hotel.ItemTypes
{
    partial class frmAddEditItemType
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label22 = new System.Windows.Forms.Label();
            this.lblItemTypeID = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnSave = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnClose = new Guna.UI2.WinForms.Guna2GradientButton();
            this.txtItemTypeName = new Guna.UI2.WinForms.Guna2TextBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(57)))), ((int)(((byte)(109)))));
            this.lblTitle.Location = new System.Drawing.Point(6, 7);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(613, 59);
            this.lblTitle.TabIndex = 212;
            this.lblTitle.Text = "Show Person Info";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(52, 99);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(107, 21);
            this.label22.TabIndex = 214;
            this.label22.Text = "Item Type ID:";
            // 
            // lblItemTypeID
            // 
            this.lblItemTypeID.AutoSize = true;
            this.lblItemTypeID.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemTypeID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblItemTypeID.Location = new System.Drawing.Point(207, 99);
            this.lblItemTypeID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItemTypeID.Name = "lblItemTypeID";
            this.lblItemTypeID.Size = new System.Drawing.Size(41, 21);
            this.lblItemTypeID.TabIndex = 215;
            this.lblItemTypeID.Text = "N\\A";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Hotel.Properties.Resources.id;
            this.pictureBox1.Location = new System.Drawing.Point(166, 96);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 216;
            this.pictureBox1.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BorderRadius = 17;
            this.btnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(57)))), ((int)(((byte)(109)))));
            this.btnSave.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(116)))), ((int)(((byte)(212)))));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Image = global::Hotel.Properties.Resources.save;
            this.btnSave.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnSave.ImageSize = new System.Drawing.Size(25, 25);
            this.btnSave.Location = new System.Drawing.Point(480, 231);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.btnSave.Size = new System.Drawing.Size(139, 44);
            this.btnSave.TabIndex = 217;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BorderRadius = 17;
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(57)))), ((int)(((byte)(109)))));
            this.btnClose.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(116)))), ((int)(((byte)(212)))));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::Hotel.Properties.Resources.close_48;
            this.btnClose.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnClose.ImageSize = new System.Drawing.Size(25, 25);
            this.btnClose.Location = new System.Drawing.Point(324, 231);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.btnClose.Size = new System.Drawing.Size(139, 44);
            this.btnClose.TabIndex = 218;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtItemTypeName
            // 
            this.txtItemTypeName.AutoRoundedCorners = true;
            this.txtItemTypeName.BorderColor = System.Drawing.Color.Gray;
            this.txtItemTypeName.BorderRadius = 17;
            this.txtItemTypeName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtItemTypeName.DefaultText = "";
            this.txtItemTypeName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtItemTypeName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtItemTypeName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtItemTypeName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtItemTypeName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(41)))), ((int)(((byte)(123)))));
            this.txtItemTypeName.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemTypeName.ForeColor = System.Drawing.Color.Black;
            this.txtItemTypeName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(41)))), ((int)(((byte)(123)))));
            this.txtItemTypeName.Location = new System.Drawing.Point(211, 150);
            this.txtItemTypeName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtItemTypeName.Name = "txtItemTypeName";
            this.txtItemTypeName.PasswordChar = '\0';
            this.txtItemTypeName.PlaceholderText = "";
            this.txtItemTypeName.SelectedText = "";
            this.txtItemTypeName.Size = new System.Drawing.Size(244, 36);
            this.txtItemTypeName.TabIndex = 221;
            this.txtItemTypeName.TextChanged += new System.EventHandler(this.txtItemTypeName_TextChanged);
            this.txtItemTypeName.Validating += new System.ComponentModel.CancelEventHandler(this.txtItemTypeName_Validating);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::Hotel.Properties.Resources.national_number;
            this.pictureBox5.Location = new System.Drawing.Point(169, 153);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(31, 26);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 220;
            this.pictureBox5.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(33, 155);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 21);
            this.label6.TabIndex = 219;
            this.label6.Text = "Item Type Name:";
            // 
            // frmAddEditItemType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(622, 283);
            this.Controls.Add(this.txtItemTypeName);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.lblItemTypeID);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAddEditItemType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add/Edit Item Type";
            this.Load += new System.EventHandler(this.frmAddEditItemType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2GradientButton btnSave;
        private Guna.UI2.WinForms.Guna2GradientButton btnClose;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblItemTypeID;
        private Guna.UI2.WinForms.Guna2TextBox txtItemTypeName;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label6;
    }
}