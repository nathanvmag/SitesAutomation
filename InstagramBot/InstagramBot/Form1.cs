using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstagramBot
{
    public partial class Form1 : Form
    {
        IWebDriver driver;
        string status = "";
        public Form1()
        {
            InitializeComponent();
            status = "Aguardando início";
            loginfield.Text = "mrobertomec";
            passfield.Text = "24841976";

        }
        async Task<IWebDriver> loginInstaAsync(IWebDriver driver, string login, string pass)
        {
            status = "Logando no instagram";

            driver.Navigate().GoToUrl("https://www.instagram.com/accounts/login/");
            await Task.Delay(2000);
            var elements = driver.FindElements(By.CssSelector("._2hvTZ.pexuQ.zyHYP"));
            elements[0].SendKeys(login);
            elements[1].SendKeys(pass);
            await Task.Delay(1000);
            elements[1].SendKeys(OpenQA.Selenium.Keys.Enter);
            await Task.Delay(10000);
            try
            {
                try
                {
                    driver.FindElement(By.CssSelector(".aOOlW.HoLwm")).Click();
                }
                catch
                {

                }
                driver.FindElement(By.XPath("//*[@id='react-root']/section/nav/div[2]/div/div/div[3]/div/div[3]/a/span")).Click();
                return driver;
            }
            catch (Exception ex)
            {
                status = "Falha ao logar no instagram";
                Console.WriteLine(ex);
                return null;
            }
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            if (checkboxes())
            {
                status = "Iniciando";

                ChromeOptions op = new ChromeOptions();
                op.AddArgument("--start-maximized");
                op.AddArgument("--incognito");
                IWebDriver driver = new ChromeDriver("./", op);

                IWebDriver driver2 = await loginInstaAsync(driver, loginfield.Text, passfield.Text);
                if (driver2 == null)
                {
                    //driver.Quit();
                    MessageBox.Show("Falha ao acessar o instagram", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                driver = driver2;
                Console.WriteLine("muito be");
                await Task.Delay(3000);

                //COMECA SEGUIDORES
                IWebElement segs = driver.FindElement(By.XPath("//*[@id='react-root']/section/main/div/header/section/ul/li[2]/a"));
                string title = driver.FindElement(By.XPath("//*[@id='react-root']/section/main/div/header/section/ul/li[2]/a/span")).GetAttribute("title");
                title = title.Replace(".", "").Replace(".", "").Replace(".", "").Replace(".", "").Replace(".", "");
                int seguidores = int.Parse(title);
                IJavaScriptExecutor javascript = (IJavaScriptExecutor)driver;
                int count2 = 0;
                segs.Click();
                Console.WriteLine("Tenho " + seguidores);

                await Task.Delay(5000);
                javascript.ExecuteScript("document.getElementsByClassName('isgrP')[0].scrollTo(0,document.getElementsByClassName('isgrP')[0].scrollHeight/2.5)");
                await Task.Delay(3000);
                int howmuhc = driver.FindElements(By.ClassName("wo9IH")).Count;
                Console.WriteLine("quanto tem ai " + howmuhc);
                status = "Obtendo seguidores";

                while (howmuhc == 0)
                {
                    try
                    {
                        IJavaScriptExecutor javascript2 = (IJavaScriptExecutor)driver;
                        javascript2.ExecuteScript("document.getElementsByClassName('isgrP')[0].scrollTo(0," + count2 + ")");
                        count2 += 40;
                        howmuhc = driver.FindElements(By.ClassName("wo9IH")).Count;

                        await Task.Delay(1000);
                    }
                    catch
                    {

                    }
                }
                while (howmuhc + 10 < seguidores)
                {
                    try
                    {
                        javascript.ExecuteScript("document.getElementsByClassName('isgrP')[0].scrollTo(0,document.getElementsByClassName('isgrP')[0].scrollHeight)");
                        howmuhc = driver.FindElements(By.ClassName("wo9IH")).Count;
                        await Task.Delay(1000);
                        status = "Obtendo seguidores " + howmuhc + "/" + seguidores;
                    }
                    catch
                    {

                    }
                }
                List<string> Tolike = new List<string>();
                var listelements = driver.FindElements(By.ClassName("wo9IH"));
                for (var i = 0; i < listelements.Count; i++)
                {
                    Tolike.Add(listelements[i].FindElement(By.TagName("a")).GetAttribute("href"));
                }
                driver.FindElement(By.CssSelector(".glyphsSpriteX__outline__24__grey_9.u-__7")).Click();
                await Task.Delay(2000);
                //COMEÇA QUANTOS EU SIGO


                var sigs = driver.FindElement(By.XPath("//*[@id='react-root']/section/main/div/header/section/ul/li[3]/a"));
                title = driver.FindElement(By.XPath("//*[@id='react-root']/section/main/div/header/section/ul/li[3]/a/span")).GetAttribute("title");
                if (title == "") title = driver.FindElement(By.XPath("//*[@id='react-root']/section/main/div/header/section/ul/li[3]/a/span")).Text;
                title = title.Replace(".", "").Replace(".", "").Replace(".", "").Replace(".", "").Replace(".", "");
                Console.WriteLine(title);
                int qntsigo = int.Parse(title);

                sigs.Click();
                await Task.Delay(2000);
                javascript.ExecuteScript("document.getElementsByClassName('isgrP')[0].scrollTo(0,document.getElementsByClassName('isgrP')[0].scrollHeight/2.5)");
                await Task.Delay(2000);
                int howmuch2 = driver.FindElements(By.ClassName("wo9IH")).Count;
                status = "Obtendo quem eu sigo";
                count2 = 0;
                while (howmuch2 == 0)
                {
                    try
                    {
                        IJavaScriptExecutor javascript2 = (IJavaScriptExecutor)driver;
                        javascript2.ExecuteScript("document.getElementsByClassName('isgrP')[0].scrollTo(0," + count2 + ")");
                        count2 += 40;
                        howmuch2 = driver.FindElements(By.ClassName("wo9IH")).Count;

                        await Task.Delay(1000);
                    }
                    catch
                    {

                    }

                }

                while (howmuch2 + 10 < qntsigo)
                {
                    try
                    {
                        javascript.ExecuteScript("document.getElementsByClassName('isgrP')[0].scrollTo(0,document.getElementsByClassName('isgrP')[0].scrollHeight)");
                        howmuch2 = driver.FindElements(By.ClassName("wo9IH")).Count;
                        await Task.Delay(1000);
                        status = "Obtendo quem eu sigo " + howmuch2 + "/" + qntsigo;
                    }
                    catch
                    {

                    }

                }
                listelements = driver.FindElements(By.ClassName("wo9IH"));

                for (var i = 0; i < listelements.Count; i++)
                {
                    try
                    {
                        if (!Tolike.Contains(listelements[i].FindElement(By.TagName("a")).GetAttribute("href")))
                            Tolike.Add(listelements[i].FindElement(By.TagName("a")).GetAttribute("href"));
                    }
                    catch { }

                }
                driver.FindElement(By.CssSelector(".glyphsSpriteX__outline__24__grey_9.u-__7")).Click();
                await Task.Delay(2000);

                Console.WriteLine("Eu sigo " + qntsigo + " e tenho " + seguidores + " possui " + Tolike.Count + " para das like");
                int sucess = 0;

                for (int i = 0; i < Tolike.Count; i++)
                {

                    try
                    {
                        if (await likePhotoAsync(Tolike[i], driver))
                        {
                            Console.WriteLine("dei like");
                            sucess++;
                            await Task.Delay(1500);

                        }
                    }
                    catch
                    {

                    }
                    status = "Curtindo fotos " + i + "/" + Tolike.Count;
                }
                driver.Quit();
                status = "Tarefa finalizada";
                MessageBox.Show("Foram Curtidas com sucesso " + sucess + " fotos e falharam " + (Tolike.Count - sucess), "Final", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("Termineii");


            }
        }
        bool checkboxes()
        {
            if (!string.IsNullOrEmpty(loginfield.Text) && !string.IsNullOrEmpty(passfield.Text))
            {
                return true;
            }
            MessageBox.Show("Por favor preencha os campos corretamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        async Task<bool> likePhotoAsync(string userlink, IWebDriver driver)
        {
            driver.Navigate().GoToUrl(userlink);
            await Task.Delay(2000);
            try
            {
                int loopcont = 0;
                while (true)
                {
                    driver.FindElements(By.CssSelector(".v1Nh3.kIKUG._bz0w"))[loopcont].FindElement(By.TagName("a")).Click();
                    await Task.Delay(3000);
                    string btvalue = driver.FindElement(By.XPath("/html/body/div[3]/div/div[2]/div/article/div[2]/section[1]/span[1]/button/span")).GetAttribute("aria-label");
                    if (btvalue == "Curtir")
                    {
                        driver.FindElement(By.CssSelector(".dCJp8.afkep.coreSpriteHeartOpen._0mzm-")).Click();
                        return true;
                    }
                    loopcont++;
                    driver.FindElement(By.ClassName("ckWGn")).Click();
                    await Task.Delay(500);
                    continue;
                }
            }
            catch (Exception ex)

            {
                Console.WriteLine(ex);
                return false;
            }
        }
        async Task<bool> comentFotoAsync(string userlink,string message, IWebDriver driver)
        {
            driver.Navigate().GoToUrl(userlink);
            await Task.Delay(2000);
            
                int loopcont = 0;
                while (true)
                {
                    driver.FindElements(By.CssSelector(".v1Nh3.kIKUG._bz0w"))[loopcont].FindElement(By.TagName("a")).Click();
                    await Task.Delay(3000);
                    var coments = driver.FindElements(By.ClassName("gElp9"));
                    for (var i=0;i<coments.Count;i++)
                    {
                        Console.WriteLine(coments[i].FindElement(By.ClassName("_6lAjh")).Text);
                        if(coments[i].FindElement(By.ClassName("_6lAjh")).Text==loginfield.Text)
                        {
                            Console.WriteLine("pulou");
                            continue;
                        }
                    }
                    var element = driver.FindElement(By.CssSelector("PdwC2._6oveC.Z_y-9"));
                    element.FindElements(By.ClassName("Ypffh"))[0].SendKeys(message);
                  //  driver.FindElement(By.XPath("/html/body/div[3]/div/div[2]/div/article/div[2]/section[3]/div/form/textarea")).SendKeys(message);
                    //driver.FindElement(By.XPath("/html/body/div[3]/div/div[2]/div/article/div[2]/section[3]/div/form/textarea")).SendKeys(OpenQA.Selenium.Keys.Enter);

                await Task.Delay(3000);
                    Console.WriteLine("CanEnableIme");
                    return true;

                }
            
         }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = status;
        }

        private async void button2_ClickAsync(object sender, EventArgs e)
        {
            if (checkboxes() && !string.IsNullOrEmpty(seguidoresLogin.Text))
            {
                status = "Iniciando";

                ChromeOptions op = new ChromeOptions();
                op.AddArgument("--start-maximized");
                op.AddArgument("--incognito");
                IWebDriver driver = new ChromeDriver("./", op);
                IWebDriver driver2 = await loginInstaAsync(driver, loginfield.Text, passfield.Text);
                if (driver2 == null)
                {
                    //driver.Quit();
                    MessageBox.Show("Falha ao acessar o instagram", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                driver = driver2;

                Console.WriteLine("muito be");
                await Task.Delay(5000);
                try
                {
                    status = "Abrindo conta " + seguidoresLogin.Text;
                    driver.Navigate().GoToUrl("https://www.instagram.com/" + seguidoresLogin.Text);
                    await Task.Delay(10000);
                    IWebElement segs = driver.FindElement(By.XPath("//*[@id='react-root']/section/main/div/header/section/ul/li[2]/a"));
                    string title = driver.FindElement(By.XPath("//*[@id='react-root']/section/main/div/header/section/ul/li[2]/a/span")).GetAttribute("title");
                    title = title.Replace(".", "").Replace(".", "").Replace(".", "").Replace(".", "").Replace(".", "");
                    int seguidores = int.Parse(title);
                    await Task.Delay(2000);//int.Parse( driver.FindElement(By.XPath("//*[@id='react-root']/section/main/div/header/section/ul/li[2]/a/span")).GetAttribute("title"));//int.Parse(segs.Text.Split(' ')[0]);
                    segs.Click();
                    await Task.Delay(5000);
                    Console.WriteLine("Tenho " + seguidores);
                    status = "Obtendo seguidores";
                    IJavaScriptExecutor javascript = (IJavaScriptExecutor)driver;
                    javascript.ExecuteScript("document.getElementsByClassName('isgrP')[0].scrollTo(0,document.getElementsByClassName('isgrP')[0].scrollHeight/2.5)");
                    await Task.Delay(10000);
                    int howmuhc = driver.FindElements(By.ClassName("wo9IH")).Count;
                    Console.WriteLine("quanto tem ai " + howmuhc);
                    int count2 = 0;
                    while (howmuhc == 0)
                    {
                        try
                        {
                            IJavaScriptExecutor javascript2 = (IJavaScriptExecutor)driver;
                            javascript2.ExecuteScript("document.getElementsByClassName('isgrP')[0].scrollTo(0," + count2 + ")");
                            count2 += 40;
                            howmuhc = driver.FindElements(By.ClassName("wo9IH")).Count;

                            await Task.Delay(500);
                        }
                        catch
                        {

                        }

                    }

                    while (howmuhc != seguidores)
                    {
                        try
                        {
                            javascript.ExecuteScript("document.getElementsByClassName('isgrP')[0].scrollTo(0,document.getElementsByClassName('isgrP')[0].scrollHeight)");
                            howmuhc = driver.FindElements(By.ClassName("wo9IH")).Count;
                            await Task.Delay(500);
                            status = "Obtendo seguidores " + howmuhc + "/" + seguidores;
                        }
                        catch
                        {

                        }

                    }
                    var listelements = driver.FindElements(By.ClassName("wo9IH"));
                    Console.WriteLine(listelements.Count);
                    Random rnd = new Random();
                    await Task.Delay(5000);

                    listelements = driver.FindElements(By.ClassName("wo9IH"));

                    for (var i = 0; i < listelements.Count; i++)
                    {
                        status = "Seguindo " + i + "/" + listelements.Count;
                        try
                        {
                            var ele = listelements[i].FindElement(By.TagName("button"));
                            if (ele.GetAttribute("innerHTML") == "Seguir")
                            {

                                ele.Click();

                            }
                            else
                            {
                                status = "Pulando pois ja sigo";
                                continue;
                            }
                            int rand = rnd.Next(7000, 10000);
                            status = "Aguarando " + (rand / 1000) + " Segundos " + i + "/" + listelements.Count;
                            await Task.Delay(rand);
                        }
                        catch
                        {

                        }
                    }
                    driver.Quit();
                    MessageBox.Show("Tarefa finalizada com sucesso", "Finalizada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Falha ao seguir do instagram escolhido, digite uma conta válida " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    driver.Quit();
                }
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private async void button3_ClickAsync(object sender, EventArgs e)
        {
            if (checkboxes() && !string.IsNullOrEmpty(seguidoresLogin.Text))
            {
                status = "Iniciando";

                ChromeOptions op = new ChromeOptions();
                op.AddArgument("--start-maximized");
                op.AddArgument("--incognito");
                IWebDriver driver = new ChromeDriver("./", op);
                IWebDriver driver2 = await loginInstaAsync(driver, loginfield.Text, passfield.Text);
                if (driver2 == null)
                {
                   // driver.Quit();
                    MessageBox.Show("Falha ao acessar o instagram", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                driver = driver2;
                Console.WriteLine("muito be");
                await Task.Delay(5000);
                try
                {
                    status = "Abrindo conta " + seguidoresLogin.Text;
                    driver.Navigate().GoToUrl("https://www.instagram.com/" + seguidoresLogin.Text);
                    await Task.Delay(10000);
                    string title = "";

                    var sigs = driver.FindElement(By.XPath("//*[@id='react-root']/section/main/div/header/section/ul/li[3]/a"));
                    title = driver.FindElement(By.XPath("//*[@id='react-root']/section/main/div/header/section/ul/li[3]/a/span")).GetAttribute("title");
                    if (title == "") title = driver.FindElement(By.XPath("//*[@id='react-root']/section/main/div/header/section/ul/li[3]/a/span")).Text;
                    title = title.Replace(".", "").Replace(".", "").Replace(".", "").Replace(".", "").Replace(".", "");
                    Console.WriteLine(title);
                    int qntsigo = int.Parse(title);

                    sigs.Click();
                    await Task.Delay(2000);
                    IJavaScriptExecutor javascript = (IJavaScriptExecutor)driver;

                    javascript.ExecuteScript("document.getElementsByClassName('isgrP')[0].scrollTo(0,document.getElementsByClassName('isgrP')[0].scrollHeight/2.5)");
                    await Task.Delay(2000);
                    int howmuch2 = driver.FindElements(By.ClassName("wo9IH")).Count;
                    status = "Obtendo quem eu sigo";
                    int count2 = 0;
                    while (howmuch2 == 0)
                    {
                        try
                        {
                            IJavaScriptExecutor javascript2 = (IJavaScriptExecutor)driver;
                            javascript2.ExecuteScript("document.getElementsByClassName('isgrP')[0].scrollTo(0," + count2 + ")");
                            count2 += 40;
                            howmuch2 = driver.FindElements(By.ClassName("wo9IH")).Count;

                            await Task.Delay(1000);
                        }
                        catch
                        {

                        }

                    }

                    while (howmuch2 != qntsigo)
                    {
                        try
                        {
                            javascript.ExecuteScript("document.getElementsByClassName('isgrP')[0].scrollTo(0,document.getElementsByClassName('isgrP')[0].scrollHeight)");
                            howmuch2 = driver.FindElements(By.ClassName("wo9IH")).Count;
                            await Task.Delay(1000);
                            status = "Obtendo quem eu sigo " + howmuch2 + "/" + qntsigo;
                        }
                        catch
                        {

                        }

                    }
                    var listelements = driver.FindElements(By.ClassName("wo9IH"));
                    Console.WriteLine(listelements.Count);
                    Random rnd = new Random();
                    await Task.Delay(5000);

                    listelements = driver.FindElements(By.ClassName("wo9IH"));

                    for (var i = 0; i < listelements.Count; i++)
                    {
                        status = "Seguindo " + i + "/" + listelements.Count;
                        try
                        {
                            var ele = listelements[i].FindElement(By.TagName("button"));
                            if (ele.GetAttribute("innerHTML") == "Seguir")
                            {

                                ele.Click();

                            }
                            else
                            {
                                status = "Pulando pois ja sigo";
                                continue;
                            }
                            int rand = rnd.Next(7000, 10000);
                            status = "Aguarando " + (rand / 1000) + " Segundos " + i + "/" + listelements.Count;
                            await Task.Delay(rand);
                        }
                        catch
                        {

                        }
                    }
                    driver.Quit();
                    MessageBox.Show("Tarefa finalizada com sucesso", "Finalizada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Falha ao seguir do instagram escolhido, digite uma conta válida " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    driver.Quit();
                }

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            passfield.PasswordChar = checkBox1.Checked ? '\0' : '*';
            passfield.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private async void button6_ClickAsync(object sender, EventArgs e)
        {
            startcomentsAsync(2);
        }
        async void startcomentsAsync(int tipe)
        {
            if (checkboxes() && !string.IsNullOrEmpty(textBox1.Text))
            {
                status = "Iniciando";
                string de = seguidoresLogin.Text;
                string coment = textBox1.Text;

                ChromeOptions op = new ChromeOptions();
                op.AddArgument("--start-maximized");
                op.AddArgument("--incognito");
                IWebDriver driver = new ChromeDriver("./", op);

                IWebDriver driver2 = await loginInstaAsync(driver, loginfield.Text, passfield.Text);
                if (driver2 == null)
                {
                    // driver.Quit();
                    MessageBox.Show("Falha ao acessar o instagram", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                driver = driver2;
                Console.WriteLine("muito be");
                await Task.Delay(3000);
                await comentFotoAsync("https://www.instagram.com/ivetesangalo", "Ola ivete", driver);
                /*
                if(!string.IsNullOrEmpty(de))
                {
                    status = "Abrindo conta " + seguidoresLogin.Text;
                    driver.Navigate().GoToUrl("https://www.instagram.com/" + de);
                    await Task.Delay(10000);
                    
                    {
                        IWebElement segs2 = driver.FindElement(By.XPath("//*[@id='react-root']/section/main/div/header/section/ul/li[2]/a"));
                        string title2 = driver.FindElement(By.XPath("//*[@id='react-root']/section/main/div/header/section/ul/li[2]/a/span")).GetAttribute("title");
                        title2 = title2.Replace(".", "").Replace(".", "").Replace(".", "").Replace(".", "").Replace(".", "");

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Falha ao comentar do instagram escolhido, digite uma conta válida " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        driver.Quit();
                        return;
                    }

                }
                List<string> Tolike = new List<string>();
                IWebElement segs = driver.FindElement(By.XPath("//*[@id='react-root']/section/main/div/header/section/ul/li[2]/a"));
                string title = driver.FindElement(By.XPath("//*[@id='react-root']/section/main/div/header/section/ul/li[2]/a/span")).GetAttribute("title");
                title = title.Replace(".", "").Replace(".", "").Replace(".", "").Replace(".", "").Replace(".", "");
                IJavaScriptExecutor javascript = (IJavaScriptExecutor)driver;
                int count2 = 0;
                int seguidores = int.Parse(title);

                if (tipe == 0 || tipe == 2)
                {

                    //COMECA SEGUIDORES
                    
                    segs.Click();
                    Console.WriteLine("Tenho " + seguidores);

                    await Task.Delay(5000);
                    javascript.ExecuteScript("document.getElementsByClassName('isgrP')[0].scrollTo(0,document.getElementsByClassName('isgrP')[0].scrollHeight/2.5)");
                    await Task.Delay(3000);
                    int howmuhc = driver.FindElements(By.ClassName("wo9IH")).Count;
                    Console.WriteLine("quanto tem ai " + howmuhc);
                    status = "Obtendo seguidores";

                    while (howmuhc == 0)
                    {
                        try
                        {
                            IJavaScriptExecutor javascript2 = (IJavaScriptExecutor)driver;
                            javascript2.ExecuteScript("document.getElementsByClassName('isgrP')[0].scrollTo(0," + count2 + ")");
                            count2 += 40;
                            howmuhc = driver.FindElements(By.ClassName("wo9IH")).Count;

                            await Task.Delay(1000);
                        }
                        catch
                        {

                        }
                    }
                    while (howmuhc + 10 < seguidores)
                    {
                        try
                        {
                            javascript.ExecuteScript("document.getElementsByClassName('isgrP')[0].scrollTo(0,document.getElementsByClassName('isgrP')[0].scrollHeight)");
                            howmuhc = driver.FindElements(By.ClassName("wo9IH")).Count;
                            await Task.Delay(1000);
                            status = "Obtendo seguidores " + howmuhc + "/" + seguidores;
                        }
                        catch
                        {

                        }
                    }
                    var listelements = driver.FindElements(By.ClassName("wo9IH"));
                    for (var i = 0; i < listelements.Count; i++)
                    {
                        Tolike.Add(listelements[i].FindElement(By.TagName("a")).GetAttribute("href"));
                    }
                    driver.FindElement(By.CssSelector(".glyphsSpriteX__outline__24__grey_9.u-__7")).Click();
                    await Task.Delay(2000);
                }
                //COMEÇA QUANTOS EU SIGO
                int qntsigo =0;
                if (tipe == 1 || tipe == 2)
                {
                    var sigs = driver.FindElement(By.XPath("//*[@id='react-root']/section/main/div/header/section/ul/li[3]/a"));
                    title = driver.FindElement(By.XPath("//*[@id='react-root']/section/main/div/header/section/ul/li[3]/a/span")).GetAttribute("title");
                    if (title == "") title = driver.FindElement(By.XPath("//*[@id='react-root']/section/main/div/header/section/ul/li[3]/a/span")).Text;
                    title = title.Replace(".", "").Replace(".", "").Replace(".", "").Replace(".", "").Replace(".", "");
                    Console.WriteLine(title);
                     qntsigo = int.Parse(title);

                    sigs.Click();
                    await Task.Delay(2000);
                    javascript.ExecuteScript("document.getElementsByClassName('isgrP')[0].scrollTo(0,document.getElementsByClassName('isgrP')[0].scrollHeight/2.5)");
                    await Task.Delay(2000);
                    int howmuch2 = driver.FindElements(By.ClassName("wo9IH")).Count;
                    status = "Obtendo quem eu sigo";
                    count2 = 0;
                    while (howmuch2 == 0)
                    {
                        try
                        {
                            IJavaScriptExecutor javascript2 = (IJavaScriptExecutor)driver;
                            javascript2.ExecuteScript("document.getElementsByClassName('isgrP')[0].scrollTo(0," + count2 + ")");
                            count2 += 40;
                            howmuch2 = driver.FindElements(By.ClassName("wo9IH")).Count;

                            await Task.Delay(1000);
                        }
                        catch
                        {

                        }

                    }

                    while (howmuch2 + 10 < qntsigo)
                    {
                        try
                        {
                            javascript.ExecuteScript("document.getElementsByClassName('isgrP')[0].scrollTo(0,document.getElementsByClassName('isgrP')[0].scrollHeight)");
                            howmuch2 = driver.FindElements(By.ClassName("wo9IH")).Count;
                            await Task.Delay(1000);
                            status = "Obtendo quem eu sigo " + howmuch2 + "/" + qntsigo;
                        }
                        catch
                        {

                        }

                    }

                    var listelements = driver.FindElements(By.ClassName("wo9IH"));

                    for (var i = 0; i < listelements.Count; i++)
                    {
                        try
                        {
                            if (!Tolike.Contains(listelements[i].FindElement(By.TagName("a")).GetAttribute("href")))
                                Tolike.Add(listelements[i].FindElement(By.TagName("a")).GetAttribute("href"));
                        }
                        catch { }

                    }
                    driver.FindElement(By.CssSelector(".glyphsSpriteX__outline__24__grey_9.u-__7")).Click();
                    await Task.Delay(2000);
                }
                Console.WriteLine("Eu sigo " + qntsigo + " e tenho " + seguidores + " possui " + Tolike.Count + " para das like");
                int sucess = 0;

                for (int i = 0; i < Tolike.Count; i++)
                {
                    if(await comentFotoAsync(Tolike[i],coment,driver))
                    {
                        Console.WriteLine("comentei");
                        sucess++;
                        await Task.Delay(1500);

                    }

                    /*            try
                                        {

                                                if (await likePhotoAsync(Tolike[i], driver))
                                            {
                                                Console.WriteLine("dei like");
                                                sucess++;
                                                await Task.Delay(1500);

                                            }
                                        }
                                        catch
                                        {

                                        }
                    
                    status = "Curtindo fotos " + i + "/" + Tolike.Count;
                }


                driver.Quit();
                status = "Tarefa finalizada";
                MessageBox.Show("Foram Comentados com sucesso " + sucess + " fotos e falharam " + (Tolike.Count - sucess), "Final", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("Termineii");
            }
            else
            {
                MessageBox.Show("Preencha todos os campos por favor ","erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }*/

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            startcomentsAsync(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            startcomentsAsync(1);
        }
    }

}
