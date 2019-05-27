

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Remote;

namespace SiteVisitor
{
    public partial class Form1 : Form
    {
        string statustx = "Aguardando início";
        string username;
        IWebDriver driver;
        string firefoxprof;
        List<string> sites;
        string[] cidades2 = new string[] { "SÃO PAULO CAPITAL", "RJ RIO DE JANEIRO", "Brasília", "BAHIA Salvador", "Fortaleza", "Belo Horizonte", "Manaus city city", "Curitiba Cidade", "Recife City", "Goiânia Goias", "Belém Cidade", "Porto Alegre cidade", "Guarulhos centro", "Campinas cidade sp", "São Luís", "Cidade de São Gonçalo", "Maceió City", "Duque de Caxias Rio", "Campo Grande CG MS", "Natal RN", "Teresina City", "São Bernardo do Campo sp", "Nova Iguaçu City", "João Pessoa PB", "Santo André city", "São José dos Campos", "Jaboatão dos Guararapes city", "Osasco são paulo", "ribeirão preto centro cidade", "Uberlândia cidade mg", "Sorocaba centro cidade", "Contagem mg", "Centro Aracaju", "centro Feira de Santana cidade", "centro Cuiabá", "Joinville sc", "Aparecida de Goiânia c", "Juiz de Fora city", "londrina centro", "Ananindeua cidade", "cidade de Porto Velho RO", "RJ CENTRO Niterói", "RJ Belford Roxo CENTRO", "Serra cidade", "Caxias do Sul RS", "Campos dos Goytacazes", "Macapá CITY", "CENTRO DE Florianópolis", "Vila Velha ES", "São João de Meriti", "Mauá city", "São José do Rio Preto cidade", "Mogi das Cruzes centro", "sp santos", "Betim", "sp Diadema centro", "Maringá city", "Jundiaí city", "Campina Grande pb", "cidade Montes Claros", "acre Rio Branco", "cidade Piracicaba", "centro Carapicuíba", "Olinda pernambuco", "cidade Anápolis", "Cariacica ci", "Boa Vista city", "Bauru sp", "Itaquaquecetuba centro", "Caucaia ce", "sp São Vicente", "Vitória city es", "Caruaru - per", "Blumenau city", "franca sp", "ponta grossa cidade", "Canoas rs", "centro petrolina pernambuco", "Pelotas RS",
            "Vitória da Conquista cidade", "Ribeirão das Neves mg", "Uberaba mg", "Paulista per", "Cascavel pr", "Praia Grande sp", "Guarujá sp", "São José dos Pinhais paraná", "Taubaté sp", "Petrópolis rj", "city Limeira", "Suzano sp", "Mossoró rn", "bahia Camaçari", "tocantins Palmas", "Taboão da Serra sp", "mt Várzea Grande", "cidade Santa Maria rs", "Gravataí rs", "centro Governador Valadares", "Sumaré" };

        public Form1()
        {
            InitializeComponent();
            sites = new List<string>();
            username = System.Environment.GetEnvironmentVariable("USERPROFILE");
            Console.WriteLine(username);
            if(Properties.Settings.Default.usuarios==null)
            {
                Properties.Settings.Default.usuarios = new System.Collections.Specialized.StringCollection();
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();

            }
            if(Properties.Settings.Default.cidades==null)
            {
                Properties.Settings.Default.cidades = new System.Collections.Specialized.StringCollection();
                Properties.Settings.Default.cidades.AddRange(cidades2);
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
                Console.WriteLine(Properties.Settings.Default.cidades.Count);

            }
            List<string> localizas = new List<string>();
            for(int i=0;i<10;i++)
            {
                localizas.Add((i*10 + 1) + " - " + (i*10 + 10));
            }
            comboBox2.Items.AddRange(localizas.ToArray());
            comboBox2.SelectedIndex = 0;
            Console.WriteLine(Properties.Settings.Default.usuarios.Count);
            fillcombobox();


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "Status :" + statustx;
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            if (!File.Exists(imgpath.Text))
            {
                errorbox("Por favor preencha uma imagem válida");
                return;
            }
         
            if (string.IsNullOrEmpty(linkpage.Text))
            {
                errorbox("Por favor preencha o link da página");
                return;
            }
            loadBrowser();
            for (int j = comboBox2.SelectedIndex * 10; j < comboBox2.SelectedIndex * 10 + 10; j++)
            {
                try {

                    Console.WriteLine(j + "  " + Properties.Settings.Default.cidades[j].Trim());
                    statustx = "Abrindo a página aguarde";
                    driver.Navigate().GoToUrl(linkpage.Text);
                    await Task.Delay(2000);
                    try
                    {
                        driver.FindElement(By.Id("email"));
                        driver.FindElement(By.Id("pass"));
                        driver.Quit();
                        errorbox("Por favor entre primeiro manualmente e logue na conta do facebook");
                    }
                    catch
                    {

                    }
                    try
                    {
                        driver.FindElement(By.ClassName("fbReactComposerAttachmentSelector_JOB_SEARCH")).Click();
                    }
                    catch
                    {
                        
                        await Task.Delay(3000);
                        ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByClassName('navigationFocus')[1].click()");
                        await Task.Delay(3000);
                        /*
                        ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByClassName('_4a0a img sp_bLl21QwX_K-_1_5x sx_b41085')[0].parentElement.click()");
                        await Task.Delay(1000);*/
                        for (int z = 0; z < 22; z++)
                        {
                            SendKeys.Send("{TAB}");
                            await Task.Delay(200);
                        }
                        SendKeys.Send("{Enter}");

                    }

                    await Task.Delay(3000);
                    driver.FindElement(By.Id("job_composer_job_title")).SendKeys(titlebox.Text);
                    driver.FindElement(By.Id("job_composer_job_title")).SendKeys(OpenQA.Selenium.Keys.Tab);


                    await Task.Delay(1000);
                    //driver.FindElements(By.ClassName("_55r1"))[1].FindElement(By.TagName("input")).SendKeys(cidades[j]);
                    SendKeys.Send(Properties.Settings.Default.cidades[j]);
                    await Task.Delay(5000);
                    SendKeys.Send("{DOWN}");
                    await Task.Delay(1000);

                    SendKeys.Send("{Enter}");

                    //var ele = driver.FindElements(By.ClassName("uiContextualLayerPositioner "))[0].FindElements(By.TagName("li"));
                    /* var splitedcity = cidades[j].ToLower().Split(' ');
                     for (int i = 0; i < ele.Count; i++)
                     {
                         bool a = false;
                         for (int z = 0; z < splitedcity.Length; z++)
                         {

                             if (ele[i].Text.ToLower().Contains(splitedcity[z].ToLower()))
                             {
                                 ele[i].Click();
                                 a = true;
                                 break;
                             }
                         }
                         if (a) break;
                     }*/
                    driver.FindElements(By.ClassName("_58al"))[4].SendKeys(minprice.Text);
                    driver.FindElements(By.ClassName("_58al"))[5].SendKeys(maxprice.Text);
                    driver.FindElement(By.ClassName("_75lr")).FindElement(By.TagName("a")).Click();
                    await Task.Delay(1000);
                    driver.FindElements(By.ClassName("_54nc"))[3].Click();
                    await Task.Delay(1000);
                    driver.FindElement(By.Id("Job Description")).SendKeys(descJob.Text);
                    await Task.Delay(1000);
                    // driver.FindElements(By.ClassName("_5f0v"))[1].Click();
                    // ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByClassName('_3-8_ img sp_VoW_v2X_nWe_1_5x sx_aa9b33')[1].parentElement.getElementsByTagName('input')[0].click()");
                    for (int z = 0; z < 5; z++)
                    {
                        SendKeys.Send("{TAB}");
                        await Task.Delay(200);
                    }
                    SendKeys.Send("{Enter}");

                    await Task.Delay(200);
                    SendKeys.Send(imgpath.Text);
                    await Task.Delay(500);
                    SendKeys.Send("{Enter}");
                    await Task.Delay(8000);
                    try
                    {
                        var elements = driver.FindElements(By.ClassName("selected"));
                        for (int z = 0; z < elements.Count; z++)
                        {
                            if (elements[z].Text == "Salvar")
                            {
                                elements[z].Click();
                            }
                        }
                        await Task.Delay(6000);
                    }
                    catch
                    {

                    }
                ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByClassName('_70o5')[1].getElementsByTagName('button')[0].click();");
                    // driver.FindElements(By.ClassName("_70o5"))[1].FindElement(By.TagName("button")).Click();
                    await Task.Delay(10000);

                }
                catch( Exception ex)
                {
                    errorbox("Erro ao efetuar post nessa cidade, pressine ok para tentar com a próxima... "+Environment.NewLine+ex.Message);
                }
                }
            MessageBox.Show("Sucesso ao efetuar postagens", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            driver.Quit();
            driver = null;
            

        }
        void loadBrowser()
        {
            if (driver != null)
            {
                errorbox("Por favor feche o browser ja aberto ele será fechado");
                driver.Quit();
            }
          
                string perfilname = "";
                try
                {
                    perfilname = comboBox1.SelectedItem.ToString();
                }
                catch (Exception ex)
                {
                    if (string.IsNullOrEmpty(perfilname))
                    {
                        errorbox("Selecione um perfil ao menos por favor");
                        return;
                    }
                }
                statustx = "Abrindo navegador no perfil  " + perfilname;
                perfilname = "perfisChrome/" + perfilname;
                ChromeOptions op = new ChromeOptions();
                op.AddArgument("--disable-notifications");
                op.AddArgument("--start-maximized");
                op.AddArgument("--disable-geolocation");

             op.AddArgument("user-data-dir=" + perfilname);
                driver = new ChromeDriver("./", op);
            
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(driver!=null)
            try
            {
                
                driver.Quit();

            }
            catch
            {

            }

        }
        public static void MoveDirectory(string source, string target)
        {
            var sourcePath = source.TrimEnd('\\', ' ');
            var targetPath = target.TrimEnd('\\', ' ');
            var files = Directory.EnumerateFiles(sourcePath, "*", SearchOption.AllDirectories)
                                 .GroupBy(s => Path.GetDirectoryName(s));
            foreach (var folder in files)
            {
                var targetFolder = folder.Key.Replace(sourcePath, targetPath);
                Directory.CreateDirectory(targetFolder);
                foreach (var file in folder)
                {
                    var targetFile = Path.Combine(targetFolder, Path.GetFileName(file));
                    if (File.Exists(targetFile)) File.Delete(targetFile);
                    File.Move(file, targetFile);
                }
            }
            Directory.Delete(source, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.usuarios.Contains(textBox1.Text))
            {
                Properties.Settings.Default.usuarios.Add(textBox1.Text);
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
                fillcombobox();
            }
            else errorbox("Por favor preencha um usuário que não exista");
        }
        void fillcombobox()
        {
            comboBox1.Items.Clear();
            string[] strArray = Properties.Settings.Default.usuarios.Cast<string>().ToArray<string>();
         //   Properties.Settings.Default.usuarios.CopyTo(strAray, 0);
            comboBox1.Items.AddRange(strArray);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        public  static void errorbox(string text)
        {
            MessageBox.Show(text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadBrowser();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Tem certeza que deseja apagar os perfis ?", "Remover", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Properties.Settings.Default.usuarios = new System.Collections.Specialized.StringCollection();
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
                Directory.Delete("perfisChrome/", true);

                fillcombobox();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                imgpath.Text = openFileDialog1.FileName;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cidsEditor edit = new cidsEditor();
            edit.StartPosition = FormStartPosition.CenterScreen;
            edit.Visible = true;
        }
    }
    class CopyDir
    {
        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())

            {
                try
                {
                    Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                    fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
                }
                catch
                {

                }
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                try
                {
                    DirectoryInfo nextTargetSubDir =
                        target.CreateSubdirectory(diSourceSubDir.Name);
                    CopyAll(diSourceSubDir, nextTargetSubDir);
                }
                catch
                {

                }
            }
        }

       

        // Output will vary based on the contents of the source directory.
    }
}
