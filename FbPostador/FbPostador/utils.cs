using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FbPostador
{
    class utils
    {
        public static void errobox(string title,string text)
        {
            MessageBox.Show( text, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
