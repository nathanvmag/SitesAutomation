namespace ManyChat_Boot
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
            this.loginbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.senhabox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.paginas = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.agendadobox = new System.Windows.Forms.CheckBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statuslabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.buttontext = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btlink = new System.Windows.Forms.TextBox();
            this.usabt = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // loginbox
            // 
            this.loginbox.Location = new System.Drawing.Point(65, 52);
            this.loginbox.Name = "loginbox";
            this.loginbox.Size = new System.Drawing.Size(195, 22);
            this.loginbox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Login:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Senha:";
            // 
            // senhabox
            // 
            this.senhabox.Location = new System.Drawing.Point(65, 83);
            this.senhabox.Name = "senhabox";
            this.senhabox.Size = new System.Drawing.Size(195, 22);
            this.senhabox.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(105, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Obter Páginas";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(295, 46);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 59);
            this.button2.TabIndex = 5;
            this.button2.Text = "Remover dados do Chrome ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // paginas
            // 
            this.paginas.BackColor = System.Drawing.SystemColors.ControlLight;
            this.paginas.Location = new System.Drawing.Point(31, 199);
            this.paginas.Name = "paginas";
            this.paginas.Size = new System.Drawing.Size(304, 197);
            this.paginas.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Minhas Páginas:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(495, 64);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(244, 197);
            this.textBox1.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(545, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Texto para Broadcast:";
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(557, 294);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(116, 45);
            this.button3.TabIndex = 9;
            this.button3.Text = "Enviar Broadcast";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_ClickAsync);
            // 
            // agendadobox
            // 
            this.agendadobox.AutoSize = true;
            this.agendadobox.Location = new System.Drawing.Point(495, 352);
            this.agendadobox.Name = "agendadobox";
            this.agendadobox.Size = new System.Drawing.Size(139, 21);
            this.agendadobox.TabIndex = 10;
            this.agendadobox.Text = "Enviar Agendado";
            this.agendadobox.UseVisualStyleBackColor = true;
            this.agendadobox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(495, 380);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker1.TabIndex = 11;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statuslabel
            // 
            this.statuslabel.AutoSize = true;
            this.statuslabel.Location = new System.Drawing.Point(377, 421);
            this.statuslabel.Name = "statuslabel";
            this.statuslabel.Size = new System.Drawing.Size(52, 17);
            this.statuslabel.TabIndex = 12;
            this.statuslabel.Text = "Status:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(495, 268);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(228, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Use [nome] para função FirstName";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(805, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Texto para botão:";
            // 
            // buttontext
            // 
            this.buttontext.Location = new System.Drawing.Point(755, 64);
            this.buttontext.Multiline = true;
            this.buttontext.Name = "buttontext";
            this.buttontext.Size = new System.Drawing.Size(244, 41);
            this.buttontext.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(805, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 17);
            this.label7.TabIndex = 17;
            this.label7.Text = "Link para botão:";
            // 
            // btlink
            // 
            this.btlink.Location = new System.Drawing.Point(755, 139);
            this.btlink.Name = "btlink";
            this.btlink.Size = new System.Drawing.Size(244, 22);
            this.btlink.TabIndex = 16;
            // 
            // usabt
            // 
            this.usabt.AutoSize = true;
            this.usabt.Location = new System.Drawing.Point(808, 167);
            this.usabt.Name = "usabt";
            this.usabt.Size = new System.Drawing.Size(113, 21);
            this.usabt.TabIndex = 18;
            this.usabt.Text = "Utilizar botão";
            this.usabt.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 450);
            this.Controls.Add(this.usabt);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btlink);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttontext);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.statuslabel);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.agendadobox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.paginas);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.senhabox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loginbox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "ManyChat Bot";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox loginbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox senhabox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel paginas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox agendadobox;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label statuslabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox buttontext;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox btlink;
        private System.Windows.Forms.CheckBox usabt;
    }
}

