using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FbPostador
{
    class token
    {
        public string tokenn;
        public bool usada;
        public int conta;
        public token (string tk,int ct)
        {
            tokenn = tk;
            usada = false;
            conta = ct;
        }
        public void setusado()
        {

            usada = true;
        }
        public override bool Equals(object obj)
        {
            return tokenn == ((token)obj).tokenn;
        }
        

    }
}
