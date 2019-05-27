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

namespace ManyChat_Boot
{
    public partial class Form1 : Form
    {
        IWebDriver driver;
        string status;
        public Form1()
        {
            InitializeComponent();
            loginbox.Text = Properties.Settings.Default.login;
            senhabox.Text = Properties.Settings.Default.senha;
            dateTimePicker1.Value = DateTime.Now;
            status = "Aguardando início";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread tdr = new Thread(getPagesAsync);
            tdr.Start();
            status = "Iniciando sistema";

        }
        async void getPagesAsync()
        {

            var perfilname = "perfisChrome/" + "perfil";
            ChromeOptions op = new ChromeOptions();
            op.AddArgument("user-data-dir=" + perfilname);
            op.AddArgument("--disable-notifications");
            op.AddArgument("--start-maximized");

            try
            {
                driver = new ChromeDriver("./", op);
            }
            catch
            {
                errobox("Falha ao abrir automatizador, por favor feche todas as instancias abertas");
                return;
            }
            status = "Abrindo Chrome";

            string login = loginbox.Text;
            string pass = senhabox.Text;
            Properties.Settings.Default.login = login;
            Properties.Settings.Default.senha = pass;
            Properties.Settings.Default.Save();
            driver.Navigate().GoToUrl("https://manychat.com/login");
            try
            {
                status = "Logando no ManyChat";

                driver.FindElement(By.Id("agree-checkbox")).Click();
                await Task.Delay(500);
                driver.FindElement(By.Id("sign-in-link")).Click();
                await Task.Delay(1500);
                try
                {
                    driver.FindElement(By.Id("email")).SendKeys(login);
                    driver.FindElement(By.Id("pass")).SendKeys(pass);
                    driver.FindElement(By.Id("loginbutton")).Click();
                }
                catch
                {

                }
                await Task.Delay(6000);
                if (driver.FindElements(By.ClassName("_43rm")).Count == 0)
                {
                    errobox("Senha incorreta ou problema no login, resolva manualmente");
                    return;
                }
               
                driver.FindElements(By.ClassName("_43rm"))[1].Click();
                await Task.Delay(1000);
                driver.FindElement(By.Id("allAssetsInput")).Click();
                driver.FindElements(By.ClassName("_43rm"))[1].Click();
                await Task.Delay(1000);
                driver.FindElements(By.ClassName("_43rm"))[2].Click();
                await Task.Delay(1000);
                driver.FindElement(By.ClassName("_43rl")).Click();
            }
            catch
            {

            }
            await Task.Delay(1000);
            status = "Abrindo Páginas";

            driver.Navigate().GoToUrl("https://manychat.com/profile/dashboard");
            if (driver.FindElements(By.Id("agree-checkbox")).Count > 0)
            {
                errobox("Falha ao logar no ManyChat, por favor proceda manualmente");
                return;
            }
            await Task.Delay(8000);

            var linhas = driver.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
            if (linhas.Count == 0)
            {
                errobox("Essa conta não possui paginas ");
                return;
            }
            status = "Obtendo Páginas";

            for (int i = 0; i < linhas.Count; i++)
            {
                var tds = linhas[i].FindElements(By.TagName("td"));
                if (tds.Count == 5)
                {
                    Console.WriteLine(tds[0].Text);
                    string agetitle = tds[0].Text;
                    CheckBox ck = new CheckBox();
                    ck.Text = agetitle;
                    ck.Location = new Point(10, (ck.Height + 5) * i);
                    BeginInvoke(new Action(() =>
                    {
                        paginas.Controls.Add(ck);

                    }));
                }
                else continue;

            }
            status = "Total de "+linhas.Count+" Páginas";

            MessageBox.Show("Paginas obtidas com sucesso, por favor não feche o navegador do manychat", "Páginas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            BeginInvoke(new Action(() =>
            {
                button3.Enabled = linhas.Count > 0 ? true : false;
                button4.Enabled = linhas.Count > 0 ? true : false;

            }));

        }

        public void errobox(string text)
        {
            MessageBox.Show(text, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Directory.Exists("perfisChrome"))
            {
                //Directory.Delete("perfisChrome", true);
                System.IO.DirectoryInfo di = new DirectoryInfo("perfisChrome");
                try
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                    MessageBox.Show("Sucesso ao remover", "Sucesso");
                }
                catch
                {
                    errobox("Falha ao remover arquivos do chrome, por favor remova a pasta perfisChrome manualmente");
                }
            }

        }

        private async void button3_ClickAsync(object sender, EventArgs e)
        {
            status = "Iniciando Broadcasting";
             button3.Enabled = false; button4.Enabled= false;
            int sucess = 0;
            if (string.IsNullOrEmpty( textBox1.Text))
            {
                errobox("Por favor preencha alguma mensagem no campo de broadcast");
                button3.Enabled = true; button4.Enabled= true;
                return;
            }
            List<string> paginastowork = new List<string>();

            foreach (CheckBox ck in paginas.Controls)
            {
                
                if (ck.Checked) paginastowork.Add(ck.Text);
            }
            if (paginastowork.Count == 0)
            {
                errobox("Por favor selecione ao menos uma página");
                button3.Enabled = true; button4.Enabled= true;
                return;
            }
            driver.Navigate().GoToUrl("https://manychat.com/profile/dashboard");
            await Task.Delay(2000);

            var linhas = driver.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
            if (linhas.Count == 0)
            {
                errobox("Essa conta não possui paginas ");
                button3.Enabled = true; button4.Enabled= true;
                return;
            }
            for(int j=0;j<paginastowork.Count;j++)
            {

                linhas = driver.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));

                for (int i = 0; i < linhas.Count; i++)
                {
                    status = "Configurando broadcast " + j + "/" + paginastowork.Count;

                    var tds = linhas[i].FindElements(By.TagName("td"));
                    if (tds.Count == 5)
                    {
                        if (paginastowork[j] == (tds[0].Text))
                        {
                            tds[0].Click();
                            await Task.Delay(2000);
                            driver.Navigate().GoToUrl(driver.Url.Replace("dashboard", "posting"));
                            await Task.Delay(1000);
                            driver.FindElement(By.Id("broadcast-new-btn")).Click();
                            await Task.Delay(5000);
                            if (textBox1.Text.Contains("[nome]"))
                            {
                                var texts = textBox1.Text.Split(new string[] { "[nome]" }, StringSplitOptions.None);
                                sendwithemojiAsync(driver.FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")), texts[0]);

                                //driver.FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")).SendKeys(texts[0]);
                                driver.FindElements(By.ClassName("oNEDujgXftj5JQaAcw3dz"))[1].Click();
                                await Task.Delay(1000);
                                driver.FindElement(By.ClassName("_3MQRFVhIng49OXcYE8n0Zh")).Click();
                                driver.SwitchTo().ActiveElement().SendKeys(OpenQA.Selenium.Keys.Enter);
                                await Task.Delay(500);
                                driver.SwitchTo().ActiveElement().SendKeys(OpenQA.Selenium.Keys.Enter);
                                //driver.FindElement(By.TagName("body")).Click();
                                await Task.Delay(1000);
                                driver.FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")).SendKeys(" ");
                                await Task.Delay(400);
                                sendwithemojiAsync(driver.FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")), texts[1]);
                               // driver.FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")).SendKeys(texts[1]);
                                driver.FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")).Click();


                            }
                            else

                            {
                                sendwithemojiAsync(driver.FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")), textBox1.Text);

                               
                                //.SendKeys(textBox1.Text);

                                //errobox("jdjsadjsa");

                            }
                            await Task.Delay(1000);
                            if (usabt.Checked)
                            {
                                await Task.Delay(1000);
                                driver.FindElement(By.ClassName("_3TfGXj_Jjsn7ufMd9i-csU")).Click();
                                await Task.Delay(1000);
                                //driver.FindElement(By.ClassName("_1ja2CXAOr_MjmGYkvZN_mb")).FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")).SendKeys(buttontext.Text);
                                sendwithemojiAsync(driver.FindElement(By.ClassName("_1ja2CXAOr_MjmGYkvZN_mb")).FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")), buttontext.Text);

                                await Task.Delay(200);
                                driver.FindElement(By.ClassName("i-Builder-URL")).Click();
                                await Task.Delay(600);
                                //driver.FindElements(By.ClassName("_1ja2CXAOr_MjmGYkvZN_mb"))[1].FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")).SendKeys(btlink.Text);
                                sendwithemojiAsync(driver.FindElements(By.ClassName("_1ja2CXAOr_MjmGYkvZN_mb"))[1].FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")), btlink.Text);

                                await Task.Delay(1000);
                                driver.FindElement(By.ClassName("zOrnvIvPWd0MSmO4ZrEOL")).Click();
                            }

                            driver.FindElement(By.Id("broadcast-go-next-btn")).Click();
                            await Task.Delay(2000);
                            if (agendadobox.Checked)
                            {
                                // driver.FindElement(By.XPath("//*[@data-test-id='postingSettings-scheduleBroadcast-radio ']")).Click();
                                driver.FindElements(By.Name("type"))[1].Click();
                                await Task.Delay(500);
                                driver.FindElements(By.ClassName("_src_components_forms_TextInput_TextInput_module__input"))[1].SendKeys(dateTimePicker1.Value.ToString(dateTimePicker1.CustomFormat));

                            }
                            driver.FindElement(By.Id("broadcast-publish-btn")).Click();
                            await Task.Delay(2000);
                            if (driver.FindElements(By.ClassName("i-check")).Count > 0)
                            {
                                sucess++;
                            }
                            break;
                        }
                    }
                    else continue;

                }
                driver.Navigate().GoToUrl("https://manychat.com/profile/dashboard");
                await Task.Delay(4000);
            }
            status = "Configuração de broadcast finalizada com " + sucess + " Sucessos";
            button3.Enabled = true; button4.Enabled= true;
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = agendadobox.Checked;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            statuslabel.Text = "Status: " + status;
        }

        async void sendwithemojiAsync(IWebElement elemnt,string text)
        {

            elemnt.SendKeys(OpenQA.Selenium.Keys.Space);
             //elemnt.Click();
            if(text.Contains("<br>"))
            {
                var splited = text.Split(new string[] { "<br>" }, StringSplitOptions.None);
                foreach(string s in splited)
                {
                    SendKeys.Send(s);
                    elemnt.SendKeys(Environment.NewLine);
                }
            }else
            SendKeys.Send(text);
            await Task.Delay(500);
        }

        private async void button4_ClickAsync(object sender, EventArgs e)
        {
            status = "Iniciando Default Reply";
            button3.Enabled = false; button4.Enabled = false;
            int sucess = 0;
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                errobox("Por favor preencha alguma mensagem no campo de Default Reply");
                button3.Enabled = true; button4.Enabled = true;
                return;
            }
            List<string> paginastowork = new List<string>();

            foreach (CheckBox ck in paginas.Controls)
            {

                if (ck.Checked) paginastowork.Add(ck.Text);
            }
            if (paginastowork.Count == 0)
            {
                errobox("Por favor selecione ao menos uma página");
                button3.Enabled = true; button4.Enabled = true;
                return;
            }
            driver.Navigate().GoToUrl("https://manychat.com/profile/dashboard");
            await Task.Delay(2000);

            var linhas = driver.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
            if (linhas.Count == 0)
            {
                errobox("Essa conta não possui paginas ");
                button3.Enabled = true; button4.Enabled = true;
                return;
            }
            for (int j = 0; j < paginastowork.Count; j++)
            {

                linhas = driver.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));

                for (int i = 0; i < linhas.Count; i++)
                {
                    status = "Configurando Default Reply " + j + "/" + paginastowork.Count;

                    var tds = linhas[i].FindElements(By.TagName("td"));
                    if (tds.Count == 5)
                    {
                        if (paginastowork[j] == (tds[0].Text))
                        {
                            tds[0].Click();
                            await Task.Delay(2000);
                            driver.Navigate().GoToUrl(driver.Url.Replace("dashboard", "automation/default/edit"));
                            await Task.Delay(2000);
                            //driver.FindElement(By.Id("broadcast-new-btn")).Click();
                            //await Task.Delay(2000);

                            /* driver.FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")).SendKeys(OpenQA.Selenium.Keys.Control+ "A");
                             driver.FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")).SendKeys(OpenQA.Selenium.Keys.Backspace);
                             */
                            try
                            {
                                driver.FindElement(By.ClassName("_3fi7eNi-OjLJ4cz56PDxkj")).Click();
                                await Task.Delay(500);
                                driver.FindElement(By.ClassName("_1d8udd8wg72QrVzxpbqVLV")).Click();
                                await Task.Delay(300);
                            }
                            catch
                            {

                            }
                            await Task.Delay(2000);
                            driver.FindElement(By.ClassName("i-Text")).Click();


                            if (textBox1.Text.Contains("[nome]"))
                            {
                                var texts = textBox1.Text.Split(new string[] { "[nome]" }, StringSplitOptions.None);

                                sendwithemojiAsync(driver.FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")), texts[0]);
                                //driver.FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")).SendKeys(texts[0]);
                                await Task.Delay(2000);
                                driver.FindElements(By.ClassName("oNEDujgXftj5JQaAcw3dz"))[1].Click();
                                await Task.Delay(1000);
                                driver.FindElement(By.ClassName("_3MQRFVhIng49OXcYE8n0Zh")).Click();
                                driver.SwitchTo().ActiveElement().SendKeys(OpenQA.Selenium.Keys.Enter);
                                await Task.Delay(500);
                                driver.SwitchTo().ActiveElement().SendKeys(OpenQA.Selenium.Keys.Enter);
                                //driver.FindElement(By.TagName("body")).Click();

                                await Task.Delay(1000);
                                driver.FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")).SendKeys(" ");
                                await Task.Delay(400);

                                sendwithemojiAsync(driver.FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")), texts[1]);

                                //driver.FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")).SendKeys(texts[1]);
                                // driver.FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")).Click();


                            }
                            else
                                sendwithemojiAsync(driver.FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")), textBox1.Text);

                            //driver.FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")).SendKeys(textBox1.Text);



                            await Task.Delay(2000);
                           // errobox("eorooer");
                            
                            if (usabt.Checked)
                            {
                                await Task.Delay(3000);
                                driver.FindElement(By.ClassName("_3TfGXj_Jjsn7ufMd9i-csU")).Click();
                                await Task.Delay(1000);
                                //driver.FindElement(By.ClassName("_1ja2CXAOr_MjmGYkvZN_mb")).FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")).SendKeys(buttontext.Text);
                                sendwithemojiAsync(driver.FindElement(By.ClassName("_1ja2CXAOr_MjmGYkvZN_mb")).FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")), buttontext.Text);
                                await Task.Delay(200);
                                driver.FindElement(By.ClassName("i-Builder-URL")).Click();
                                await Task.Delay(600);
                                sendwithemojiAsync(driver.FindElements(By.ClassName("_1ja2CXAOr_MjmGYkvZN_mb"))[1].FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")), btlink.Text);
                                //driver.FindElements(By.ClassName("_1ja2CXAOr_MjmGYkvZN_mb"))[1].FindElement(By.ClassName("_2WwE44lGQuscM1N3FqClhO")).SendKeys(btlink.Text);
                                await Task.Delay(1000);
                                driver.FindElement(By.ClassName("zOrnvIvPWd0MSmO4ZrEOL")).Click();
                            }
                            ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementById('dr-enabled-toggle').checked=document.getElementById('dr-enabled-toggle').checked?true:true;document.getElementsByClassName('_3Arx-EN9DiACIX6a6QHjO0')[2].click();");
                            await Task.Delay(5000);

                            // driver.FindElement(By.ClassName("_13Arx-EN9DiACIX6a6QHjO0")).Click();

                            /*

                            driver.FindElement(By.Id("broadcast-go-next-btn")).Click();
                            await Task.Delay(2000);
                            if (agendadobox.Checked)
                            {
                                // driver.FindElement(By.XPath("//*[@data-test-id='postingSettings-scheduleBroadcast-radio ']")).Click();
                                driver.FindElements(By.Name("type"))[1].Click();
                                await Task.Delay(500);
                                driver.FindElements(By.ClassName("_src_components_forms_TextInput_TextInput_module__input"))[1].SendKeys(dateTimePicker1.Value.ToString(dateTimePicker1.CustomFormat));

                            }
                            driver.FindElement(By.Id("broadcast-publish-btn")).Click();
                            await Task.Delay(2000);
                            if (driver.FindElements(By.ClassName("i-check")).Count > 0)
                            {
                                sucess++;
                            }*/
                            sucess++;
                            break;
                        }
                    }
                    else continue;

                }
                driver.Navigate().GoToUrl("https://manychat.com/profile/dashboard");
                await Task.Delay(4000);
            }
            status = "Configuração de Default Reply finalizada com " + sucess + " Sucessos";
            button3.Enabled = true; button4.Enabled = true;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (CheckBox ck in paginas.Controls)
            {
                ck.Checked = true;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}