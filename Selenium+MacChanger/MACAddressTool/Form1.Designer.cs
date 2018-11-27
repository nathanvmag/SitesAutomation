namespace MACAddressTool
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
            this.label1 = new System.Windows.Forms.Label();
            this.ActualMacLabel = new System.Windows.Forms.Label();
            this.AdaptersComboBox = new System.Windows.Forms.ComboBox();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.RereadButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.userlabel = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.passlabel = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.statuslabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mac Adress atual:";
            // 
            // ActualMacLabel
            // 
            this.ActualMacLabel.Location = new System.Drawing.Point(161, 64);
            this.ActualMacLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ActualMacLabel.Name = "ActualMacLabel";
            this.ActualMacLabel.Size = new System.Drawing.Size(227, 21);
            this.ActualMacLabel.TabIndex = 3;
            this.ActualMacLabel.Text = "label2";
            this.ActualMacLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AdaptersComboBox
            // 
            this.AdaptersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AdaptersComboBox.FormattingEnabled = true;
            this.AdaptersComboBox.Location = new System.Drawing.Point(164, 13);
            this.AdaptersComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.AdaptersComboBox.Name = "AdaptersComboBox";
            this.AdaptersComboBox.Size = new System.Drawing.Size(312, 24);
            this.AdaptersComboBox.TabIndex = 5;
            this.AdaptersComboBox.SelectedIndexChanged += new System.EventHandler(this.AdaptersComboBox_SelectedIndexChanged);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(127, 104);
            this.UpdateButton.Margin = new System.Windows.Forms.Padding(4);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(201, 46);
            this.UpdateButton.TabIndex = 6;
            this.UpdateButton.Text = "Trocar mac Address";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // RereadButton
            // 
            this.RereadButton.Location = new System.Drawing.Point(396, 59);
            this.RereadButton.Margin = new System.Windows.Forms.Padding(4);
            this.RereadButton.Name = "RereadButton";
            this.RereadButton.Size = new System.Drawing.Size(76, 26);
            this.RereadButton.TabIndex = 8;
            this.RereadButton.Text = "Atualizar";
            this.RereadButton.UseVisualStyleBackColor = true;
            this.RereadButton.Click += new System.EventHandler(this.RereadButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Selecione a conexão:";
            // 
            // userlabel
            // 
            this.userlabel.Location = new System.Drawing.Point(85, 195);
            this.userlabel.Name = "userlabel";
            this.userlabel.Size = new System.Drawing.Size(286, 22);
            this.userlabel.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(172, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Login Adsense:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(172, 229);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "Senha Adsense:";
            // 
            // passlabel
            // 
            this.passlabel.Location = new System.Drawing.Point(85, 249);
            this.passlabel.Name = "passlabel";
            this.passlabel.Size = new System.Drawing.Size(286, 22);
            this.passlabel.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(160, 293);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Iniciar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_ClickAsync);
            // 
            // statuslabel
            // 
            this.statuslabel.Location = new System.Drawing.Point(35, 338);
            this.statuslabel.Name = "statuslabel";
            this.statuslabel.Size = new System.Drawing.Size(382, 23);
            this.statuslabel.TabIndex = 15;
            this.statuslabel.Text = "Status: Aguardando início";
            this.statuslabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 380);
            this.Controls.Add(this.statuslabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.passlabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.userlabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RereadButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.AdaptersComboBox);
            this.Controls.Add(this.ActualMacLabel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Automatizador Adsense";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ActualMacLabel;
        private System.Windows.Forms.ComboBox AdaptersComboBox;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button RereadButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox userlabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox passlabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label statuslabel;
    }
}

