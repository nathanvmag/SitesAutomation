namespace FbPostador
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
            this.tokenlogin = new System.Windows.Forms.TextBox();
            this.tokenpass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tiger1pass = new System.Windows.Forms.TextBox();
            this.tiger1log = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.passtiger2 = new System.Windows.Forms.TextBox();
            this.logintiger2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.passtiger3 = new System.Windows.Forms.TextBox();
            this.logintiger3 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.statustx = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.importar = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.urltiger = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.urltk = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tokenlogin
            // 
            this.tokenlogin.Location = new System.Drawing.Point(129, 71);
            this.tokenlogin.Name = "tokenlogin";
            this.tokenlogin.Size = new System.Drawing.Size(164, 22);
            this.tokenlogin.TabIndex = 0;
            // 
            // tokenpass
            // 
            this.tokenpass.Location = new System.Drawing.Point(129, 99);
            this.tokenpass.Name = "tokenpass";
            this.tokenpass.Size = new System.Drawing.Size(164, 22);
            this.tokenpass.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(23, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Login Token:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(23, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Senha Token:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(579, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Iniciar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 251);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "Senha Tiger1:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "Login Tiger1:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tiger1pass
            // 
            this.tiger1pass.Location = new System.Drawing.Point(117, 251);
            this.tiger1pass.Name = "tiger1pass";
            this.tiger1pass.Size = new System.Drawing.Size(164, 22);
            this.tiger1pass.TabIndex = 6;
            // 
            // tiger1log
            // 
            this.tiger1log.Location = new System.Drawing.Point(117, 223);
            this.tiger1log.Name = "tiger1log";
            this.tiger1log.Size = new System.Drawing.Size(164, 22);
            this.tiger1log.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(340, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 29);
            this.label5.TabIndex = 9;
            this.label5.Text = "Contas TigerPost";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(296, 250);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 23);
            this.label6.TabIndex = 13;
            this.label6.Text = "Senha Tiger2:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(296, 221);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 23);
            this.label7.TabIndex = 12;
            this.label7.Text = "Login Tiger2:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // passtiger2
            // 
            this.passtiger2.Location = new System.Drawing.Point(402, 250);
            this.passtiger2.Name = "passtiger2";
            this.passtiger2.Size = new System.Drawing.Size(164, 22);
            this.passtiger2.TabIndex = 11;
            // 
            // logintiger2
            // 
            this.logintiger2.Location = new System.Drawing.Point(402, 222);
            this.logintiger2.Name = "logintiger2";
            this.logintiger2.Size = new System.Drawing.Size(164, 22);
            this.logintiger2.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(576, 252);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 23);
            this.label8.TabIndex = 17;
            this.label8.Text = "Senha Tiger3:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(576, 223);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 23);
            this.label9.TabIndex = 16;
            this.label9.Text = "Login Tiger3:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // passtiger3
            // 
            this.passtiger3.Location = new System.Drawing.Point(682, 252);
            this.passtiger3.Name = "passtiger3";
            this.passtiger3.Size = new System.Drawing.Size(164, 22);
            this.passtiger3.TabIndex = 15;
            // 
            // logintiger3
            // 
            this.logintiger3.Location = new System.Drawing.Point(682, 224);
            this.logintiger3.Name = "logintiger3";
            this.logintiger3.Size = new System.Drawing.Size(164, 22);
            this.logintiger3.TabIndex = 14;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(678, 72);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "Parar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // statustx
            // 
            this.statustx.AutoSize = true;
            this.statustx.Location = new System.Drawing.Point(579, 104);
            this.statustx.Name = "statustx";
            this.statustx.Size = new System.Drawing.Size(52, 17);
            this.statustx.TabIndex = 19;
            this.statustx.Text = "Status:";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // importar
            // 
            this.importar.Location = new System.Drawing.Point(771, 43);
            this.importar.Name = "importar";
            this.importar.Size = new System.Drawing.Size(75, 51);
            this.importar.TabIndex = 20;
            this.importar.Text = "Importar Tokens";
            this.importar.UseVisualStyleBackColor = true;
            this.importar.Click += new System.EventHandler(this.importar_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // urltiger
            // 
            this.urltiger.Location = new System.Drawing.Point(414, 184);
            this.urltiger.Name = "urltiger";
            this.urltiger.Size = new System.Drawing.Size(191, 22);
            this.urltiger.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(308, 184);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 17);
            this.label10.TabIndex = 22;
            this.label10.Text = "Site Tigerpost:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(36, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 17);
            this.label11.TabIndex = 24;
            this.label11.Text = "Site Tokens:";
            // 
            // urltk
            // 
            this.urltk.Location = new System.Drawing.Point(129, 30);
            this.urltk.Name = "urltk";
            this.urltk.Size = new System.Drawing.Size(191, 22);
            this.urltk.TabIndex = 23;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 291);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.urltk);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.urltiger);
            this.Controls.Add(this.importar);
            this.Controls.Add(this.statustx);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.passtiger3);
            this.Controls.Add(this.logintiger3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.passtiger2);
            this.Controls.Add(this.logintiger2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tiger1pass);
            this.Controls.Add(this.tiger1log);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tokenpass);
            this.Controls.Add(this.tokenlogin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Fb Postador";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tokenlogin;
        private System.Windows.Forms.TextBox tokenpass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tiger1pass;
        private System.Windows.Forms.TextBox tiger1log;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox passtiger2;
        private System.Windows.Forms.TextBox logintiger2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox passtiger3;
        private System.Windows.Forms.TextBox logintiger3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label statustx;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button importar;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox urltiger;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox urltk;
    }
}

