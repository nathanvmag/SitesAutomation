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
            /* loginbox.Text = "gaboagui@gmail.com";
             senhabox.Text = "Cerasj10!";*/

        }
        // gustavobrbs@bol.com.br
        // 485800gu


        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            string login = loginbox.Text;
            string pass = senhabox.Text;
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(pass))
            {

                IWebDriver driver = new ChromeDriver("./");
                driver.Navigate().GoToUrl("https://app.directly.com/login/auth");

                await Task.Delay(2000);
                driver.FindElement(By.Id("j_username")).SendKeys(login);
                driver.FindElement(By.Id("j_password")).SendKeys(pass);
                driver.FindElement(By.XPath("/html/body/div/div/div[1]/div/div/form/div[4]/button")).Click();
                await Task.Delay(7000);

                if (driver.FindElements(By.ClassName("nova-regular")).Count > 0)
                {
                    try
                    {
                        while (driver.FindElements(By.ClassName("task-skip-submit")).Count > 0)
                        {

                            if (driver.FindElements(By.ClassName("VotingButton--upvote")).Count > 0)
                            {
                                var elemnt = driver.FindElements(By.ClassName("VotingButton--upvote"))[driver.FindElements(By.ClassName("VotingButton--upvote")).Count - 1];

                                elemnt.Click();
                                Console.WriteLine("Clicou no voto "+ elemnt.Text);
                                await Task.Delay(2000);
                                if (driver.FindElements(By.ClassName("solution-choose-btn")).Count > 0)
                                {
                                    var ele = driver.FindElements(By.ClassName("solution-choose-btn"))[driver.FindElements(By.ClassName("solution-choose-btn")).Count - 1];
                                    if (ele.Text == "Choose as best")
                                        ele.Click();
                                    else Console.WriteLine("nao clicou pq " + ele.Text);
                                    await Task.Delay(2000);
                                }
                            }
                            await Task.Delay(7000);
                            try
                            {
                                driver.FindElements(By.ClassName("task-skip-submit"))[0].Click();
                            }
                            catch (Exception xp)
                            {
                                driver.Navigate().GoToUrl(driver.Url);
                                Console.WriteLine("erro na exepciton " + xp);
                            }
                            await Task.Delay(8000);

                        }
                        MessageBox.Show("Tarefa finalizada com sucesso", "Parabéns", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro ao automatizar "+ex, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    /*
                        while (true)
                        {
                            for (var i = 0; i < 20; i++)
                            {
                               bool a= await clicadorAsync(driver);
                                if (!a) break;
                            }
                            Console.WriteLine(nunclicks);
                             Atualizador(driver);

                        }*/


                }
                else
                {
                    MessageBox.Show("Por favor preencha com a senha correta", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    driver.Quit();
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

                if (driver.FindElements(By.ClassName("js-claim-question")).Count > 0)
                {
                    Console.WriteLine("Achou");
                    driver.FindElement(By.ClassName("js-claim-question")).Click();
                    clicadas++;
                    Console.WriteLine("clicou " + clicadas);
                    
                }
                else if (driver.FindElements(By.ClassName("VotingButton")).Count > 0)
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
