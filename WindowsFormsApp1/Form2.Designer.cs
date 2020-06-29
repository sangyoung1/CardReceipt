namespace WindowsFormsApp1
{
    partial class Form2
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.cboCode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.clbCard = new System.Windows.Forms.CheckedListBox();
            this.clbName = new System.Windows.Forms.CheckedListBox();
            this.cboManager = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLimit = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnLimit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(408, 483);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "등록";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(308, 483);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 42);
            this.button2.TabIndex = 1;
            this.button2.Text = "닫기";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "프로젝트 코드";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(158, 83);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(135, 25);
            this.txtName.TabIndex = 3;
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "yyyy-MM-dd";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(158, 171);
            this.dtpStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(135, 25);
            this.dtpStart.TabIndex = 4;
            // 
            // cboCode
            // 
            this.cboCode.FormattingEnabled = true;
            this.cboCode.Location = new System.Drawing.Point(158, 31);
            this.cboCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboCode.Name = "cboCode";
            this.cboCode.Size = new System.Drawing.Size(146, 23);
            this.cboCode.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "프로젝트명";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "프로젝트 기간";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(333, 171);
            this.dtpEnd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(143, 25);
            this.dtpEnd.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(305, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "~";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 262);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "카드";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 368);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "투입인력";
            // 
            // clbCard
            // 
            this.clbCard.FormattingEnabled = true;
            this.clbCard.Location = new System.Drawing.Point(158, 256);
            this.clbCard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clbCard.Name = "clbCard";
            this.clbCard.Size = new System.Drawing.Size(187, 84);
            this.clbCard.TabIndex = 13;
            // 
            // clbName
            // 
            this.clbName.FormattingEnabled = true;
            this.clbName.Location = new System.Drawing.Point(158, 361);
            this.clbName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clbName.Name = "clbName";
            this.clbName.Size = new System.Drawing.Size(187, 104);
            this.clbName.TabIndex = 14;
            // 
            // cboManager
            // 
            this.cboManager.FormattingEnabled = true;
            this.cboManager.Location = new System.Drawing.Point(158, 126);
            this.cboManager.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboManager.Name = "cboManager";
            this.cboManager.Size = new System.Drawing.Size(135, 23);
            this.cboManager.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 15);
            this.label7.TabIndex = 15;
            this.label7.Text = "프로젝트 담당자";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 223);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 15);
            this.label8.TabIndex = 18;
            this.label8.Text = "프로젝트 한도";
            // 
            // txtLimit
            // 
            this.txtLimit.Location = new System.Drawing.Point(158, 216);
            this.txtLimit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLimit.Name = "txtLimit";
            this.txtLimit.Size = new System.Drawing.Size(135, 25);
            this.txtLimit.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(299, 223);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 15);
            this.label9.TabIndex = 19;
            this.label9.Text = "원";
            // 
            // btnLimit
            // 
            this.btnLimit.Location = new System.Drawing.Point(327, 216);
            this.btnLimit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLimit.Name = "btnLimit";
            this.btnLimit.Size = new System.Drawing.Size(75, 27);
            this.btnLimit.TabIndex = 20;
            this.btnLimit.Text = "금액변경";
            this.btnLimit.UseVisualStyleBackColor = true;
            this.btnLimit.Click += new System.EventHandler(this.btnLimit_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 536);
            this.Controls.Add(this.btnLimit);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtLimit);
            this.Controls.Add(this.cboManager);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.clbName);
            this.Controls.Add(this.clbCard);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboCode);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form2";
            this.Text = "기준정보관리";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.ComboBox cboCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckedListBox clbCard;
        private System.Windows.Forms.CheckedListBox clbName;
        private System.Windows.Forms.ComboBox cboManager;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtLimit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnLimit;
    }
}