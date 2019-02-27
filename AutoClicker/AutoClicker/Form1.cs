using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoClicker
{
    public partial class Form1 : Form
    {
        int erros = 0, clicadas = 0,nunclicks=0;
        public Form1()
        {
            InitializeComponent();
             loginbox.Text = "gustavobrbs@bol.com.br";
             senhabox.Text = "485800gu";

        }
        // gustavobrbs@bol.com.br
        // 485800gu


        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            string login = loginbox.Text;
            string pass = senhabox.Text;
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(pass))
            {
                try
                {
                    var perfilname = "perfilChrome";
                    ChromeOptions op = new ChromeOptions();
                    op.AddArgument("user-data-dir=" + perfilname);
                    op.AddArgument(@"--incognito");
                   op.AddArgument("--start-maximized");
                    op.AddUserProfilePreference("credentials_enable_service", false);
                    op.AddUserProfilePreference("profile.password_manager_enabled", false);
                    IWebDriver driver = new ChromeDriver("./", op);
                    driver.Manage().Cookies.DeleteAllCookies();
                    driver.Navigate().GoToUrl("https://app.directly.com/login/auth");


                    await Task.Delay(2000);
                    try
                    {
                        driver.FindElement(By.Id("j_username")).SendKeys(login);
                        driver.FindElement(By.Id("j_password")).SendKeys(pass);
                        driver.FindElement(By.XPath("/html/body/div/div/div[1]/div/div/form/div[4]/button")).Click();
                    }
                    catch
                    {

                    }

                      await Task.Delay(1000);


                    if (driver.FindElements(By.ClassName("nova-regular")).Count > 0)
                    {

                        DialogResult dr = MessageBox.Show("Por favor ligue a extensão do auto refresh coloque 5 segundos (Aperte sim quando estiver ligada)", "Ligar Extensão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr == DialogResult.Yes)
                            while (true)
                            {
                              await clicadorAsync(driver);
                                //if (!a) break;

                                await Task.Delay(100);

                            }


                    }
                    else
                    {
                        MessageBox.Show("Por favor preencha com a senha correta", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        driver.Quit();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Falha ao abrir navegador, feche todas as guias por favor", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex);
                }
            }
            else
            {
                MessageBox.Show("Por favor preencha os 2 campos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
        }

        private void Timer1_Tick(object sender, EventArgs e, IWebDriver driver)
        {
            Atualizador(driver);
            Console.WriteLine("ticou");
        }
        private void Timer2_Tick(object sender, EventArgs e, IWebDriver driver)
        {
            Console.WriteLine("ticou2");
            clicadorAsync(driver);

        }
        public async void Atualizador(IWebDriver driver)
        {
            driver.FindElement(By.ClassName("logo-directly")).Click();
        }
        public async         Task<bool>
                    clicadorAsync(IWebDriver driver)
        {
            try
            {
                Console.WriteLine("tentando clicar");

                if (driver.FindElements(By.ClassName("js-claim-question")).Count > 0)
                {
                    Console.WriteLine("Achou");
                    driver.FindElement(By.ClassName("js-claim-question")).Click();
                    clicadas++;
                    Console.WriteLine("clicou " + clicadas);
                    
                }
                else if (driver.FindElements(By.ClassName("VotingButton")).Count > 0&&driver.FindElements(By.ClassName("VotingButton--votedUp")).Count==0)
                {
                    Console.WriteLine("vai vota");
                    driver.FindElements(By.ClassName("VotingButton"))[0].Click();
                    Console.WriteLine("Votou");
                    return false;
                }
                else
                {
                    nunclicks++;
                }

            }
            catch
            {
                Console.WriteLine("tem erros");
                
            }
            await Task.Delay(100);
            return true;
        }

         }
}
