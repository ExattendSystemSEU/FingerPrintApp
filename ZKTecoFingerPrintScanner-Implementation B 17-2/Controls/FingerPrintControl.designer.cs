﻿namespace Dofe_Re_Entry.UserControls.DeviceController
{
    partial class FingerPrintControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FingerPrintControl));
            this.lblDeviceStatus = new System.Windows.Forms.Label();
            this.btnInit = new System.Windows.Forms.Button();
            this.lblFingerPrintCount = new System.Windows.Forms.Label();
            this.btnEnroll = new System.Windows.Forms.Button();
            this.cmbIdx = new System.Windows.Forms.ComboBox();
            this.label43 = new System.Windows.Forms.Label();
            this.picFPImg = new System.Windows.Forms.PictureBox();
            this.btnVerify = new System.Windows.Forms.Button();
            this.btnIdentify = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFree = new System.Windows.Forms.Button();
            this.btnFree2 = new System.Windows.Forms.Button();
            this.label2_searchResult = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1_CheckSOcialID = new System.Windows.Forms.Button();
            this.textBox1_SocialID = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picFPImg)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDeviceStatus
            // 
            this.lblDeviceStatus.AutoEllipsis = true;
            this.lblDeviceStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDeviceStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(204)))), ((int)(((byte)(240)))));
            this.lblDeviceStatus.Location = new System.Drawing.Point(16, 109);
            this.lblDeviceStatus.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lblDeviceStatus.Name = "lblDeviceStatus";
            this.lblDeviceStatus.Padding = new System.Windows.Forms.Padding(0, 4, 5, 0);
            this.lblDeviceStatus.Size = new System.Drawing.Size(298, 36);
            this.lblDeviceStatus.TabIndex = 782;
            this.lblDeviceStatus.Text = "Disconnected";
            // 
            // btnInit
            // 
            this.btnInit.BackColor = System.Drawing.Color.White;
            this.btnInit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnInit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnInit.ForeColor = System.Drawing.Color.Black;
            this.btnInit.Location = new System.Drawing.Point(6, 19);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(125, 37);
            this.btnInit.TabIndex = 1;
            this.btnInit.Text = "Initialize Device";
            this.btnInit.UseVisualStyleBackColor = false;
            this.btnInit.Click += new System.EventHandler(this.bnInit_Click);
            // 
            // lblFingerPrintCount
            // 
            this.lblFingerPrintCount.AutoSize = true;
            this.lblFingerPrintCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.lblFingerPrintCount.Location = new System.Drawing.Point(235, 263);
            this.lblFingerPrintCount.Name = "lblFingerPrintCount";
            this.lblFingerPrintCount.Size = new System.Drawing.Size(42, 46);
            this.lblFingerPrintCount.TabIndex = 783;
            this.lblFingerPrintCount.Text = "3";
            // 
            // btnEnroll
            // 
            this.btnEnroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(204)))), ((int)(((byte)(240)))));
            this.btnEnroll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnroll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(204)))), ((int)(((byte)(240)))));
            this.btnEnroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnroll.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEnroll.ForeColor = System.Drawing.Color.White;
            this.btnEnroll.Location = new System.Drawing.Point(267, 20);
            this.btnEnroll.Name = "btnEnroll";
            this.btnEnroll.Size = new System.Drawing.Size(138, 36);
            this.btnEnroll.TabIndex = 767;
            this.btnEnroll.Text = "Click To Enroll";
            this.btnEnroll.UseVisualStyleBackColor = false;
            this.btnEnroll.Click += new System.EventHandler(this.bnEnroll_Click);
            // 
            // cmbIdx
            // 
            this.cmbIdx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIdx.FormattingEnabled = true;
            this.cmbIdx.Location = new System.Drawing.Point(342, 227);
            this.cmbIdx.Name = "cmbIdx";
            this.cmbIdx.Size = new System.Drawing.Size(40, 21);
            this.cmbIdx.TabIndex = 11;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Arial", 8.75F, System.Drawing.FontStyle.Bold);
            this.label43.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label43.Location = new System.Drawing.Point(205, 233);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(112, 15);
            this.label43.TabIndex = 758;
            this.label43.Text = "Available Devices :";
            // 
            // picFPImg
            // 
            this.picFPImg.BackColor = System.Drawing.Color.Transparent;
            this.picFPImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFPImg.Location = new System.Drawing.Point(22, 227);
            this.picFPImg.Name = "picFPImg";
            this.picFPImg.Size = new System.Drawing.Size(125, 129);
            this.picFPImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picFPImg.TabIndex = 779;
            this.picFPImg.TabStop = false;
            // 
            // btnVerify
            // 
            this.btnVerify.BackColor = System.Drawing.Color.White;
            this.btnVerify.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerify.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnVerify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerify.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnVerify.ForeColor = System.Drawing.Color.Black;
            this.btnVerify.Location = new System.Drawing.Point(319, 513);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(138, 34);
            this.btnVerify.TabIndex = 775;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = false;
            this.btnVerify.Click += new System.EventHandler(this.bnVerify_Click);
            // 
            // btnIdentify
            // 
            this.btnIdentify.BackColor = System.Drawing.Color.White;
            this.btnIdentify.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIdentify.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnIdentify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIdentify.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnIdentify.ForeColor = System.Drawing.Color.Black;
            this.btnIdentify.Location = new System.Drawing.Point(336, 111);
            this.btnIdentify.Name = "btnIdentify";
            this.btnIdentify.Size = new System.Drawing.Size(98, 34);
            this.btnIdentify.TabIndex = 778;
            this.btnIdentify.Text = "Identify User";
            this.btnIdentify.UseVisualStyleBackColor = false;
            this.btnIdentify.Visible = false;
            this.btnIdentify.Click += new System.EventHandler(this.bnIdentify_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(112)))), ((int)(((byte)(134)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(112)))), ((int)(((byte)(134)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(137, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(124, 36);
            this.btnClose.TabIndex = 777;
            this.btnClose.Text = "Disconnect Device";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.bnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnInit);
            this.groupBox1.Controls.Add(this.btnEnroll);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 8.75F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox1.Location = new System.Drawing.Point(16, 151);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(418, 70);
            this.groupBox1.TabIndex = 785;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // btnFree
            // 
            this.btnFree.BackColor = System.Drawing.Color.White;
            this.btnFree.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFree.Enabled = false;
            this.btnFree.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnFree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFree.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFree.ForeColor = System.Drawing.Color.Black;
            this.btnFree.Location = new System.Drawing.Point(319, 561);
            this.btnFree.Name = "btnFree";
            this.btnFree.Size = new System.Drawing.Size(138, 34);
            this.btnFree.TabIndex = 786;
            this.btnFree.Text = "Free Resources";
            this.btnFree.UseVisualStyleBackColor = false;
            this.btnFree.Click += new System.EventHandler(this.bnFree_Click);
            // 
            // btnFree2
            // 
            this.btnFree2.BackColor = System.Drawing.Color.White;
            this.btnFree2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFree2.Enabled = false;
            this.btnFree2.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnFree2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFree2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFree2.ForeColor = System.Drawing.Color.Black;
            this.btnFree2.Location = new System.Drawing.Point(0, 0);
            this.btnFree2.Name = "btnFree2";
            this.btnFree2.Size = new System.Drawing.Size(138, 34);
            this.btnFree2.TabIndex = 786;
            this.btnFree2.Text = "Free Resources";
            this.btnFree2.UseVisualStyleBackColor = false;
            this.btnFree2.Click += new System.EventHandler(this.bnFree_Click);
            // 
            // label2_searchResult
            // 
            this.label2_searchResult.AutoSize = true;
            this.label2_searchResult.Location = new System.Drawing.Point(19, 71);
            this.label2_searchResult.Name = "label2_searchResult";
            this.label2_searchResult.Size = new System.Drawing.Size(35, 13);
            this.label2_searchResult.TabIndex = 790;
            this.label2_searchResult.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 789;
            this.label1.Text = "label1";
            // 
            // button1_CheckSOcialID
            // 
            this.button1_CheckSOcialID.Location = new System.Drawing.Point(336, 34);
            this.button1_CheckSOcialID.Name = "button1_CheckSOcialID";
            this.button1_CheckSOcialID.Size = new System.Drawing.Size(75, 23);
            this.button1_CheckSOcialID.TabIndex = 788;
            this.button1_CheckSOcialID.Text = "button1";
            this.button1_CheckSOcialID.UseVisualStyleBackColor = true;
            this.button1_CheckSOcialID.Click += new System.EventHandler(this.button1_CheckSOcialID_Click);
            // 
            // textBox1_SocialID
            // 
            this.textBox1_SocialID.Location = new System.Drawing.Point(16, 36);
            this.textBox1_SocialID.Name = "textBox1_SocialID";
            this.textBox1_SocialID.Size = new System.Drawing.Size(223, 20);
            this.textBox1_SocialID.TabIndex = 787;
            // 
            // FingerPrintControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Controls.Add(this.label2_searchResult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1_CheckSOcialID);
            this.Controls.Add(this.textBox1_SocialID);
            this.Controls.Add(this.lblFingerPrintCount);
            this.Controls.Add(this.btnFree);
            this.Controls.Add(this.cmbIdx);
            this.Controls.Add(this.label43);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.btnIdentify);
            this.Controls.Add(this.lblDeviceStatus);
            this.Controls.Add(this.picFPImg);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "FingerPrintControl";
            this.Size = new System.Drawing.Size(700, 700);
            this.Load += new System.EventHandler(this.FingerPrintControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picFPImg)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Button btnEnroll;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnIdentify;
        private System.Windows.Forms.PictureBox picFPImg;
        private System.Windows.Forms.Label lblDeviceStatus;
        private System.Windows.Forms.Label lblFingerPrintCount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFree;
        private System.Windows.Forms.Button btnFree2;
        public System.Windows.Forms.ComboBox cmbIdx;
        private System.Windows.Forms.Label label2_searchResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1_CheckSOcialID;
        private System.Windows.Forms.TextBox textBox1_SocialID;
    }
}
