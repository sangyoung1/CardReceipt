namespace WindowsFormsApp1
{
    partial class CardInUp
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtCardNum1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCardNum2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCardNum3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCardNum4 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtLimit = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "카드번호";
            // 
            // txtCardNum1
            // 
            this.txtCardNum1.Location = new System.Drawing.Point(136, 48);
            this.txtCardNum1.MaxLength = 4;
            this.txtCardNum1.Name = "txtCardNum1";
            this.txtCardNum1.Size = new System.Drawing.Size(65, 25);
            this.txtCardNum1.TabIndex = 1;
            this.txtCardNum1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCardNum1.TextChanged += new System.EventHandler(this.txtCardNum1_TextChanged);
            this.txtCardNum1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeypressNum);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(206, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(306, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "-";
            // 
            // txtCardNum2
            // 
            this.txtCardNum2.Location = new System.Drawing.Point(232, 48);
            this.txtCardNum2.MaxLength = 4;
            this.txtCardNum2.Name = "txtCardNum2";
            this.txtCardNum2.Size = new System.Drawing.Size(65, 25);
            this.txtCardNum2.TabIndex = 3;
            this.txtCardNum2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCardNum2.TextChanged += new System.EventHandler(this.txtCardNum2_TextChanged);
            this.txtCardNum2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeypressNum);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(407, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "-";
            // 
            // txtCardNum3
            // 
            this.txtCardNum3.Location = new System.Drawing.Point(332, 49);
            this.txtCardNum3.MaxLength = 4;
            this.txtCardNum3.Name = "txtCardNum3";
            this.txtCardNum3.Size = new System.Drawing.Size(65, 25);
            this.txtCardNum3.TabIndex = 5;
            this.txtCardNum3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCardNum3.TextChanged += new System.EventHandler(this.txtCardNum3_TextChanged);
            this.txtCardNum3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeypressNum);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(548, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 15);
            this.label5.TabIndex = 8;
            // 
            // txtCardNum4
            // 
            this.txtCardNum4.Location = new System.Drawing.Point(434, 49);
            this.txtCardNum4.MaxLength = 4;
            this.txtCardNum4.Name = "txtCardNum4";
            this.txtCardNum4.Size = new System.Drawing.Size(65, 25);
            this.txtCardNum4.TabIndex = 7;
            this.txtCardNum4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCardNum4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeypressNum);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(46, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "카드담당자";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(46, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 10;
            this.label7.Text = "카드한도";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(155, 110);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(122, 25);
            this.txtUser.TabIndex = 11;
            // 
            // txtLimit
            // 
            this.txtLimit.Location = new System.Drawing.Point(155, 169);
            this.txtLimit.Name = "txtLimit";
            this.txtLimit.Size = new System.Drawing.Size(122, 25);
            this.txtLimit.TabIndex = 12;
            this.txtLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeypressNum);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(283, 174);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 15);
            this.label8.TabIndex = 13;
            this.label8.Text = "원";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(424, 218);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 33);
            this.button1.TabIndex = 14;
            this.button1.Text = "닫기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(332, 218);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 33);
            this.button2.TabIndex = 15;
            this.button2.Text = "등록";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CardInUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 263);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtLimit);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCardNum4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCardNum3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCardNum2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCardNum1);
            this.Controls.Add(this.label1);
            this.Name = "CardInUp";
            this.Text = "카드정보관리";
            this.Load += new System.EventHandler(this.CardInUp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCardNum1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCardNum2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCardNum3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCardNum4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtLimit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}