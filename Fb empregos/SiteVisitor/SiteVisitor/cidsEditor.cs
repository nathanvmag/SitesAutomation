using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiteVisitor
{
    public partial class cidsEditor : Form
    {
        public cidsEditor()
        {
            InitializeComponent();
           
            string[] strArray = new string[Properties.Settings.Default.cidades.Count];
            Properties.Settings.Default.cidades.CopyTo(strArray, 0);
            textBox1.Lines = strArray;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Lines.Length!=100)
            {
                Form1.errorbox("Por Favor preencha um total de 100 cidades");
                return;
            }
            Properties.Settings.Default.cidades = new System.Collections.Specialized.StringCollection();

            Properties.Settings.Default.cidades.AddRange(textBox1.Lines);
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
            MessageBox.Show("Cidades salvas com sucesso");
        }
    }
}
