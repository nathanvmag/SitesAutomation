using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;

namespace SiteAutomation
{
    public partial class Form1 : Form
    {
        bool logadowordpress = false;
        int sucessos, falhas;
        string status = "Aguardando Ínicio";
        public Form1()
        {
            InitializeComponent();
            sitename.Text = "https://www.dicadecursosudemy.online/wp-admin/";
            username.Text = "systemads@tutanota.com";
            wordpass.Text = "tech123";
            blogcat1.Text = "Cancer";
            blogcat2.Text = "Brain Cancer";        
            
            }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            ChromeOptions op = new ChromeOptions();
            op.AddArgument("--start-maximized");
            IWebDriver wordpress = new ChromeDriver("./",op);
            IWebDriver translate= null;
            if (tradu.Checked)
            {

                translate = new ChromeDriver("./", op);
                translate.Navigate().GoToUrl("https://translate.google.com/#view=home&op=translate&sl=auto&tl=en");
                await Task.Delay(5000);
                
                IJavaScriptExecutor js = (IJavaScriptExecutor)translate;
                    var a = "https://translate.google.com/#view=home&op=translate&sl=auto&tl="+(string.IsNullOrEmpty(sigla.Text) ? "pt" : sigla.Text);
                js.ExecuteScript("location.href='"+a+"' " );
/*
                string g = await traduzir2Async(translate, "Hello World");
                Console.WriteLine(g);*/
            }
            if (!string.IsNullOrEmpty(username.Text)&&!string.IsNullOrEmpty(sitename.Text)&&!string.IsNullOrEmpty(wordpass.Text))
                        {
                            try
                            {
                                status = "Logando no WordPress";
                                wordpress.Navigate().GoToUrl(sitename.Text);
                                await Task.Delay(500);
                                wordpress.FindElement(By.Id("user_login")).SendKeys(username.Text);
                                wordpress.FindElement(By.Id("user_pass")).SendKeys(wordpass.Text);                    
                                wordpress.FindElement(By.Id("wp-submit")).Click();
                                await Task.Delay(2000);
                                wordpress.FindElement(By.XPath("//*[@id='menu-posts']/a/div[3]")).Click();
                                await Task.Delay(1000);
                                wordpress.FindElement(By.XPath("//*[@id='menu-posts']/ul/li[3]/a")).Click();
                                logadowordpress = true;
                                status = "Sucesso ao logar no WordPress";
                            }
                                catch
                            {
                                MessageBox.Show("Erro ao realizar a automação do login do Wordpress", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else MessageBox.Show("Por favor preencha todos os campos do Wordpress", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
           

            if (logadowordpress)
            {
                if (!String.IsNullOrEmpty(blogcat1.Text) && !string.IsNullOrEmpty(blogcat2.Text))
                {
                    IWebDriver driver = new ChromeDriver("./", op);
                    driver.Navigate().GoToUrl("http://ezinearticles.com");
                    try
                    {
                        driver.FindElement(By.Id("categories"));
                    }
                    catch
                    {
                        MessageBox.Show("Por favor Preencha o captcha para começar a automação, só de OK após ter preenchido o capctha e avançado!","Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);


                    }
                    bool search = true;
                    int controller = 1;

                    while (search)
                    {
                        try
                        {
                            IWebElement element = driver.FindElement(By.XPath("//*[@id='categories']/ul/li[" + controller + "]/a"));
                            string innerHTML = element.GetAttribute("innerHTML");
                            status = "Procurando artigo";
                            if (innerHTML == blogcat1.Text)
                            {

                                element.Click();
                                int controller2 = 1;
                                while (search)
                                {
                                    try
                                    {
                                        IWebElement element2 = driver.FindElement(By.XPath("//*[@id='categories']/ul/li[" + controller + "]/div/li[" + controller2 + "]/a"));
                                        string elemnt2html = element2.GetAttribute("innerHTML");
                                        Console.WriteLine("to no for "+elemnt2html +"  "+ blogcat2.Text);
                                        if (elemnt2html == blogcat2.Text)
                                        {
                                            try {
                                                element2.Click();
                                                await Task.Delay(1500);
                                                search = false;
                                                List<string> urls = new List<string>();
                                                for (int i = (int)numericUpDown1.Value; i <= numericUpDown2.Value; i++)
                                                {
                                                    urls.Add(driver.Url + "&page=" + i);
                                                }
                                                int pageind = (int)numericUpDown1.Value;
       
                                                foreach (string nowurl in urls) {
                                                    driver.Navigate().GoToUrl(nowurl);

                                                    try
                                                    {
                                                        driver.FindElement(By.Id("category-title"));
                                                    }
                                                    catch
                                                    {
                                                        MessageBox.Show("Por favor Preencha o captcha para começar a automação, só de OK após ter preenchido o capctha e avançado!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                                    }
                                                    var post = driver.FindElements(By.ClassName("article-title-link"));
                                                    for (int z = 0; z < post.Count(); z++)
                                                    {
                                                        var post2 = driver.FindElements(By.ClassName("article-title-link"));
                                                        post2[z].Click();

                                                        await Task.Delay(2000);
                                                        try
                                                        {
                                                            driver.FindElement(By.Id("page-inner"));
                                                        }
                                                        catch
                                                        {
                                                            MessageBox.Show("Por favor Preencha o captcha para começar a automação, só de OK após ter preenchido o capctha e avançado!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                                        }
                                                        string title = driver.FindElements(By.TagName("h1"))[0].Text;//GetAttribute("innerHTML");
                                                        List<string> texto = new List<string>();
                                                        var Ielments = driver.FindElement(By.Id("article-content")).FindElements(By.TagName("p"));
                                                        texto.Add(title);
                                                        foreach (IWebElement iele in Ielments)
                                                        {
                                                            texto.Add(iele.GetAttribute("innerHTML").Replace("<strong>","").Replace("</strong>","")); // GetAttribute("innerHTML"));
                                                        }
                                                        List<string> traduzidas = new List<string>();
                                                        status = ("traduzindo o post "+z+"/"+post.Count+Environment.NewLine+" Página "+pageind+"/"+numericUpDown2.Value);
                                                        try
                                                        {
                                                            foreach (string s in texto)
                                                        {
                                                                if (tradu.Checked && translate != null)
                                                                {
                                                                    string tradu = await traduzir2Async(translate, s);
                                                                    traduzidas.Add(tradu);
                                                                }
                                                                else traduzidas.Add(s);
                                                        };
                                                        }
                                                        catch (traduzirExeceptiob tx)
                                                        {
                                                            falhas++;
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Console.WriteLine("falha " + ex.ToString());
                                                        }
                                                        foreach (string s in traduzidas)
                                                        {
                                                            Console.WriteLine(s);
                                                        };
                                                            status = "Efetuando Post no Wordpress " + z + "/" + post.Count + Environment.NewLine + " Página " + pageind + "/" + numericUpDown2.Value;
                                                            wordpress.FindElement(By.XPath("//*[@id='menu-posts']/a/div[3]")).Click();
                                                            await Task.Delay(1000);
                                                            wordpress.FindElement(By.XPath("//*[@id='menu-posts']/ul/li[3]/a")).Click();
                                                            wordpress.FindElement(By.Id("title")).SendKeys(traduzidas[0]);
                                                            wordpress.FindElement(By.Id("title")).SendKeys(OpenQA.Selenium.Keys.Tab);

                                                            await Task.Delay(500);

                                                            // Console.WriteLine(wordpress.FindElements(By.TagName("iframe"))[0].GetAttribute("innerHTML"));
                                                            //  wordpress.SwitchTo().Frame(wordpress.SwitchTo().ActiveElement());
                                                            IWebElement ele = wordpress.SwitchTo().ActiveElement();  //wordpress.FindElement(By.Id("tinymce"));
                                                            for (int i = 1; i < traduzidas.Count(); i++)
                                                            {
                                                                ele.SendKeys(traduzidas[i]);
                                                                ele.SendKeys(OpenQA.Selenium.Keys.Enter);
                                                            }

                                                            //wordpress.SwitchTo().DefaultContent();
                                                        
                                                            await Task.Delay(500);
                                                            IJavaScriptExecutor js = (IJavaScriptExecutor)wordpress;
                                                            js.ExecuteScript("document.getElementById('publish').click()");

                                                       
                                                        status = ("Artigo finalizado esperando 1 segundos " + z + "/" + post.Count + Environment.NewLine + " Página " + pageind + "/" + numericUpDown2.Value);
                                                        sucessos++;
                                                        driver.Navigate().Back();
                                                        await Task.Delay(1000);
                                                    }
                                                }
                                                status = ("Terminei uma página esperando 10 segundos"+Environment.NewLine + " Página " + pageind + "/" + numericUpDown2.Value);
                                                pageind++;
                                                await Task.Delay(10000);
                                            }
                                            catch(Exception ex)
                                            {
                                                Console.WriteLine(ex);
                                            }
                                            }
                                        
                                        controller2++;

                                    }
                                    catch(Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                        MessageBox.Show("Não foi encontrado a Segunda categoria com este nome ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    }
                                }
                                break;
                            }
                            controller++;
                        }
                        catch
                        {
                            MessageBox.Show("Não foi encontrado a primeira categoria com este nome ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                    try
                    {
                        if (driver != null) driver.Quit();
                        
                    }
                    catch
                    {

                    }
                    
                    MessageBox.Show("Terminou automação com "+sucessos+" SUCESSOS  e "+falhas+" FALHAS","Finalizado",MessageBoxButtons.OK,MessageBoxIcon.Information);

                }
                else MessageBox.Show("Por favor preencha todos os campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show("Você não está logado no sistema do WORDPRESS", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            try
            {
                if (wordpress != null) wordpress.Quit();
                if (translate != null) translate.Quit();
            }
            catch
            {

            }

        }
        
        public async Task<string> traduzir2Async(IWebDriver translate,string tx)
        {
            try
            {
                if (!translate.Url.Contains("https://translate.google.com"))
                    translate.Navigate().GoToUrl("https://translate.google.com");
                translate.FindElement(By.Id("source")).Clear();
                translate.FindElement(By.Id("source")).SendKeys(tx);
                translate.FindElement(By.Id("source")).SendKeys(OpenQA.Selenium.Keys.Enter);
                await Task.Delay(4000);
                var tradu = translate.FindElement(By.ClassName("translation")).Text; // translate.FindElement(By.XPath(" / html/body/div[2]/div[1]/div[2]/div[1]/div[1]/div[2]/div[2]/div[1]/div[2]/div/span[1]")).Text;
                if (string.IsNullOrEmpty(tradu)) throw new traduzirExeceptiob();

                //Console.WriteLine(tradu);
                return tradu;
            }

            catch (Exception ex )
            {
                throw ex;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label13.Text = String.Format("Status : {0}", status);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > numericUpDown2.Value)
            {
                var temp = numericUpDown2.Value;
                numericUpDown2.Value = numericUpDown1.Value;
                numericUpDown1.Value = temp;
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > numericUpDown2.Value)
            {
                var temp = numericUpDown2.Value;
                numericUpDown2.Value = numericUpDown1.Value;
                numericUpDown1.Value = temp;
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
    public class traduzirExeceptiob :Exception
    {

    }
}
