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
        public Form1()
        {
            InitializeComponent();
            sites = new List<string>();
            username = System.Environment.GetEnvironmentVariable("USERPROFILE");

            Console.WriteLine(username);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "Status :" + statustx;
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    try
                    {
                        sites.Add(line);
                    }
                    catch
                    {

                    }
                }
                sr.Close();
                statustx = "Sites carregados com um total de " + sites.Count;
                if (radioButton1.Checked)
                {
                    string perfilname = string.IsNullOrEmpty(textBox1.Text) ? "padrao" : textBox1.Text;
                    perfilname = "perfisChrome/" + perfilname;
                    ChromeOptions op = new ChromeOptions();
                    op.AddArgument("user-data-dir="+perfilname);
                    driver = new ChromeDriver("./",op);
                }
                else
                {

                    /* DesiredCapabilities capability = new DesiredCapabilities();
                     capability.SetCapability("browserName", "opera");

                     driver = new ChromeDriver(capability);
                     */
                    /*string pathToCurrentUserProfiles = Environment.ExpandEnvironmentVariables("%APPDATA%") + @"\Mozilla\Firefox\Profiles"; // Path to profile
                    string[] pathsToProfiles = Directory.GetDirectories(pathToCurrentUserProfiles, "*.default", SearchOption.TopDirectoryOnly);
                    if (pathsToProfiles.Length != 0)
                    {
                        FirefoxProfile profile = new FirefoxProfile(pathsToProfiles[0]);
                        FirefoxOptions fo = new FirefoxOptions();
                        fo.Profile = profile;
                        profile.SetPreference("browser.tabs.loadInBackground", false); // set preferences you need
                        driver = new FirefoxDriver("./", fo);
                    }
                    else
                    {
                        driver = new FirefoxDriver("./");
                    }
                    */
                    if (!Directory.Exists("perfisfirefox/padrao")) Directory.CreateDirectory("perfisfirefox/padrao");
                    FirefoxProfile profile = new FirefoxProfile("perfisfirefox/padrao"); //new File("D:\\Selenium Profile"));
                    FirefoxOptions op = new FirefoxOptions();
                    op.Profile = profile;
                    driver = new FirefoxDriver(op);
                    Console.WriteLine("hey ia");
                    var dir = ((RemoteWebDriver)driver).Capabilities.GetCapability("moz:profile").ToString();
                    Console.WriteLine("path "+dir);
                    firefoxprof = dir;



                }
                Random rnd = new Random();
                for (var i = 0; i < sites.Count(); i++)
                {
                    statustx = "Navengando no site " + sites[i]+" "+i+"/"+sites.Count;
                    if (!sites[i].Contains("https://") && !sites[i].Contains("http://"))
                        sites[i] = "https://" + sites[i];
                    driver.Navigate().GoToUrl(sites[i]);
                    await Task.Delay(2000);

                    int rand = rnd.Next(10,20);
                    statustx = "Abrindo " + rand + " Paginas aleatorias";
                    for(var x=0;x<rand;x++)
                    {
                        statustx = "Abrindo pagina " + x + "/" + rand;
                        var elem= driver.FindElements(By.TagName("a"));
                        while(true)
                        {
                            try
                            {
                                await Task.Delay(300);
                                int rnd2 = rnd.Next(elem.Count);
                                if (elem[rnd2].GetAttribute("href").Contains(sites[i].Replace("https://","").Replace("http;//","")))
                                {
                                    driver.Navigate().GoToUrl(elem[rnd2].GetAttribute("href"));
                                    break;
                                }
                            }
                            catch
                            {

                            }

                        }
                        int waittime = rnd.Next(30000, 120000);
                        statustx = "Aguardando " + waittime / 1000 + " Segundos para próximo clique "+x+"/"+rand;
                        await Task.Delay(waittime);
                      
                    }

                    await Task.Delay(5000);

                }

                  
            }
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
