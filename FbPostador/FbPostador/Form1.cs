using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Newtonsoft.Json;
using OpenQA.Selenium.Support.UI;
using System.Globalization;

namespace FbPostador
{
    public partial class Form1 : Form
    {
        List<token> mytokens;
        string status;
        int finishTasks = 0;
        public Form1()
        {
            InitializeComponent();
            tokenlogin.Text = "mon";
            tokenpass.Text = "programador";
            tiger1log.Text = "postador01@bancodempregos.com";
            tiger1pass.Text = "postador";
            logintiger2.Text = "postador02@bancodempregos.com";
            passtiger2.Text= "postador";
            logintiger3.Text = "postador03@bancodempregos.com";
            passtiger3.Text = "postador";
            urltk.Text = "http://bancodempregos.com/token/bbpanel/";
            urltiger.Text = "http://bancodempregos.com/postador/";

            if (File.Exists("mytokens.txt"))
            {
                StreamReader sr = new StreamReader("mytokens.txt");
                mytokens = JsonConvert.DeserializeObject<List<token>>(sr.ReadToEnd());
                sr.Close();
            }
            else mytokens = new List<token>();
        }
        public async void seleniumWork()
        {
            status = "Iniciando sistema";
            IWebDriver driver = new ChromeDriver("./");
            driver.Navigate().GoToUrl(urltk.Text);
            driver.FindElements(By.TagName("input"))[0].SendKeys(tokenlogin.Text);
            driver.FindElements(By.TagName("input"))[1].SendKeys(tokenpass.Text);
            driver.FindElements(By.TagName("input"))[2].Click();
            status = "Logando no sistema de tokens";
            await Task.Delay(4000);
            if(driver.FindElement(By.TagName("body")).GetAttribute("innerHTML")== "senha errada!!")
            {
                utils.errobox("Error", "Senha do receptor de token incorreta");
                status = "falha ao logar no sistema de tokens";
                return;

            }
            status = "Lendo tokens";
            var inputs = driver.FindElements(By.TagName("input"));
            bool newtokens = false;
            for(int i=0;i<inputs.Count;i++)
            {
                string usertoken =inputs[i].GetAttribute("value");
                token tk = new token(usertoken, i % 3);
                if(!mytokens.Contains(tk))
                {
                    Console.WriteLine("adicionou ");
                    mytokens.Add(tk);
                    newtokens = true;
                }
            }
            for (int i = 0; i < mytokens.Count;i++)
            {
                if (!mytokens[i].usada) newtokens = true;
            }
            Console.WriteLine("Possui  " +mytokens.Count+" possui novo :"+ newtokens);
            //Savetokens(mytokens);
            driver.Quit();

            if (newtokens)
            {
                status = "Iniciando TigerPost";
                Thread tr1 = new Thread(()=>dotigerWork(tiger1log.Text, tiger1pass.Text, 0));
                tr1.Start();

                Thread tr2 = new Thread(() => dotigerWork(logintiger2.Text, passtiger2.Text, 1));
                tr2.Start();

                Thread tr3 = new Thread(() => dotigerWork(logintiger3.Text, passtiger3.Text, 2));
                tr3.Start();


            }else
            {
                status = "Terminando processo sem novos tokens aguardando 5 minutos para tentar novamente ";
                await Task.Delay(60000 * 5);
                finishTasks = 3;
            }






        }
        async void  dotigerWork(string login,string pass, int conta)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--incognito");
            options.AddArguments("--start-maximized");

            IWebDriver driver = new ChromeDriver("./", options);
            driver.Navigate().GoToUrl(urltiger.Text);
            string myurl = driver.Url;
            await Task.Delay(2000);
            driver.FindElement(By.XPath("//*[@id='navbar-collapse']/ul/li[2]/a")).Click();
            await Task.Delay(1500);
            driver.FindElement(By.Name("email")).SendKeys(login);
            driver.FindElement(By.Name("password")).SendKeys(pass);
            driver.FindElement(By.Name("password")).SendKeys(OpenQA.Selenium.Keys.Enter);
            await Task.Delay(5000);
            if(driver.FindElements(By.Id("md_checkbox_profile")).Count==0)
            {
                utils.errobox("Error", "Falha ao logar no tiger, Senha incorreta");
                return;
            }
            driver.Navigate().GoToUrl(myurl+"index.php/facebook_accounts");
            List<token> thistoken = new List<token>();
            for(int i=0;i<mytokens.Count;i++)
            {
                if (mytokens[i].conta == conta && !mytokens[i].usada)
                    thistoken.Add(mytokens[i]);
            }

            if (thistoken.Count > 0)
            {
                await Task.Delay(2000);
                int howmuch =  thistoken.Count;
                for (int i = 0; i <howmuch ; i++) //histoken.Count;i++)
                {
                    driver.Navigate().GoToUrl(myurl+"index.php/facebook_accounts/update");
                    driver.FindElement(By.TagName("textarea")).SendKeys(thistoken[i].tokenn);
                    driver.FindElement(By.ClassName("btnFBAccountUpdate")).Click();
                    await Task.Delay(5000);
                }
                driver.Navigate().GoToUrl(myurl+"index.php/post");

                new SelectElement(driver.FindElement(By.ClassName("getSavePost"))).SelectByIndex(1);
                IJavaScriptExecutor javascript = (IJavaScriptExecutor)driver;
                javascript.ExecuteScript("document.getElementById('md_checkbox_profile').click()");
                await Task.Delay(1000);
                javascript.ExecuteScript("document.getElementById('md_checkbox_page').click()");
                await Task.Delay(1000);
                javascript.ExecuteScript("document.getElementById('md_checkbox_all').click()");
                Console.WriteLine("Passou aqui");
                driver.FindElement(By.ClassName("btnPostnow")).Click();
                status = "Efetuando posts tiger";
                await Task.Delay(10000);
                string time = driver.FindElement(By.XPath("/html/body/section[2]/div/form/div/div[1]/div/div[2]/div[2]/div/b/span")).GetAttribute("innerHTML");
                Console.WriteLine(time);
                var milisecs = 0;
                if (time != "--:--")
                {
                    DateTime dt = DateTime.ParseExact(time, "HH:mm:ss", CultureInfo.InvariantCulture);
                    var horas = dt.Hour;
                    var minutes = dt.Minute + 1;
                    var seconds = dt.Second;
                    milisecs = horas * 3600000 + minutes * 60000 + seconds * 1000;
                }else milisecs= thistoken.Count*10000;
                

                Console.WriteLine("Vou esperar " + milisecs);

                await Task.Delay(milisecs);
                List<string> usererros = new List<string>();
                var posterros = driver.FindElements(By.ClassName("post-error"));
                for(int i=0;i<posterros.Count;i++)
                {
                    if(posterros[i].FindElements(By.TagName("td"))[6].Text.Contains("validating access"))
                    {
                        if(!usererros.Contains(posterros[i].FindElements(By.TagName("td"))[1].Text))
                        usererros.Add(posterros[i].FindElements(By.TagName("td"))[1].Text);

                    }
                }
                foreach (string s in usererros)
                {
                    Console.WriteLine("usuarios bloqueados " + s);
                }
                driver.Navigate().GoToUrl(myurl+"index.php/facebook_accounts");
                await Task.Delay(2000);

                foreach(string s in usererros)
                {
                    if(driver.FindElements(By.ClassName("pending")).Count>0)
                    {
                        var users = driver.FindElements(By.ClassName("pending"));
                        for(var i=0;i<users.Count;i++)
                        {
                           if( users[i].FindElements(By.TagName("td"))[3].Text == s)
                            {
                                users[i].FindElements(By.TagName("td"))[8].FindElement(By.TagName("button")).Click();
                                await Task.Delay(1000);
                                driver.FindElement(By.ClassName("sa-button-container")).FindElement(By.ClassName("confirm")).Click();
                                
                            }
                        }
                    }
                    await Task.Delay(10000);
                }
                javascript.ExecuteScript("document.getElementById('md_checkbox_211').click()");
                await Task.Delay(1500);
                javascript.ExecuteScript("document.getElementsByClassName('btnActionModule')[1].click()");
                await Task.Delay(5000);
                for (int i = 0; i < howmuch; i++)
                {
                    for (int j = 0; j < mytokens.Count; j++)
                    {
                        if (mytokens[j].Equals(thistoken[i]))
                        {
                            mytokens[j].setusado();
                            Console.WriteLine("achou igual " + j);
                        }

                    }
                }
                Savetokens(mytokens);
            }
            finishTasks++;
            status = "Finalizado tiger post " + conta;
            driver.Quit();
            return; 

        }
        void Savetokens(List<token> mytks)
        {
            if (File.Exists("mytokens.txt")) File.Delete("mytokens.txt");
            StreamWriter sw = new StreamWriter("mytokens.txt");
            sw.Write(JsonConvert.SerializeObject(mytks));
            sw.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(seleniumWork);
            t.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            statustx.Text = "Status :" + status;
            if(finishTasks==3)
            {
                finishTasks = 0;
                button1_Click(sender,e);

            }
                }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Vôce tem certeza que deseja fechar o programa ?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dr==DialogResult.Yes)
            {
                Savetokens(mytokens);
                Environment.Exit(Environment.ExitCode);

            }
        }

        private void importar_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                string line = "";

                if (mytokens==null)
                {
                    if (File.Exists("mytokens.txt"))
                    {
                        StreamReader srx = new StreamReader("mytokens.txt");
                        mytokens = JsonConvert.DeserializeObject<List<token>>(srx.ReadToEnd());
                        srx.Close();
                    }
                    else mytokens = new List<token>();

                }
                int count = 0,much=0;
                while ((line = sr.ReadLine()) != null)
                {
                    //   System.Console.WriteLine(line);
                    token ttk = new token(line, count % 3);
                    if (!mytokens.Contains(ttk))
                    {
                        mytokens.Add(ttk);
                        much++;
                    }

                    count++;
                  
                }
                sr.Close();
                MessageBox.Show("Foram importados " + much + " tokens com sucesso ");
                Savetokens(mytokens);

            }
        }
    }
}
