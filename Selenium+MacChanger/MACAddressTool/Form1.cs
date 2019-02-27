using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Security.Permissions;
using System.Management;
using Microsoft.Win32;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.IO;

namespace MACAddressTool
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Represents a Windows network interface. Wrapper around the .NET API for network
        /// interfaces, as well as for the unmanaged device.
        /// </summary>
        public class Adapter
        {
            public ManagementObject adapter;
            public string adaptername;
            public string customname;
            public int devnum;
            public string newMacadd;

            public Adapter(ManagementObject a, string aname, string cname, int n)
            {
                this.adapter = a;
                this.adaptername = aname;
                this.customname = cname;
                this.devnum = n;
            }

            public Adapter(NetworkInterface i) : this(i.Description) { }

            public Adapter(string aname)
            {
                this.adaptername = aname;

                var searcher = new ManagementObjectSearcher("select * from win32_networkadapter where Name='" + adaptername + "'");
                var found = searcher.Get();
                this.adapter = found.Cast<ManagementObject>().FirstOrDefault();

                // Extract adapter number; this should correspond to the keys under
                // HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Control\Class\{4d36e972-e325-11ce-bfc1-08002be10318}
                try
                {
                    var match = Regex.Match(adapter.Path.RelativePath, "\\\"(\\d+)\\\"$");
                    this.devnum = int.Parse(match.Groups[1].Value);
                }
                catch
                {
                    return;
                }

                // Find the name the user gave to it in "Network Adapters"
                this.customname = NetworkInterface.GetAllNetworkInterfaces().Where(
                    i => i.Description == adaptername
                ).Select(
                    i => " (" + i.Name + ")"
                ).FirstOrDefault();
            }

            /// <summary>
            /// Get the .NET managed adapter.
            /// </summary>
            public NetworkInterface ManagedAdapter
            {
                get
                {
                    return NetworkInterface.GetAllNetworkInterfaces().Where(
                        nic => nic.Description == this.adaptername
                    ).FirstOrDefault();
                }
            }

            /// <summary>
            /// Get the MAC address as reported by the adapter.
            /// </summary>
            public string Mac
            {
                get
                {
                    try
                    {
                        return BitConverter.ToString(this.ManagedAdapter.GetPhysicalAddress().GetAddressBytes()).Replace("-", "").ToUpper();
                    }
                    catch { return null; }
                }
            }

            /// <summary>
            /// Get the registry key associated to this adapter.
            /// </summary>
            public string RegistryKey
            {
                get
                {
                    return String.Format(@"SYSTEM\ControlSet001\Control\Class\{{4D36E972-E325-11CE-BFC1-08002BE10318}}\{0:D4}", this.devnum);
                }
            }

            /// <summary>
            /// Get the NetworkAddress registry value of this adapter.
            /// </summary>
            public string RegistryMac
            {
                get
                {
                    try
                    {
                        using (RegistryKey regkey = Registry.LocalMachine.OpenSubKey(this.RegistryKey, false))
                        {
                            return regkey.GetValue("NetworkAddress").ToString();
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }
            }

            /// <summary>
            /// Sets the NetworkAddress registry value of this adapter.
            /// </summary>
            /// <param name="value">The value. Should be EITHER a string of 12 hexadecimal digits, uppercase, without dashes, dots or anything else, OR an empty string (clears the registry value).</param>
            /// <returns>true if successful, false otherwise</returns>
            public bool SetRegistryMac(string value)
            {
                bool shouldReenable = false;

                try
                {
                    // If the value is not the empty string, we want to set NetworkAddress to it,
                    // so it had better be valid
                    if (value.Length > 0 && !Adapter.IsValidMac(value, false))
                        throw new Exception(value + " is not a valid mac address");

                    using (RegistryKey regkey = Registry.LocalMachine.OpenSubKey(this.RegistryKey, true))
                    {
                        if (regkey == null)
                            throw new Exception("Failed to open the registry key");

                        // Sanity check
                        if (regkey.GetValue("AdapterModel") as string != this.adaptername
                            && regkey.GetValue("DriverDesc") as string != this.adaptername)
                            throw new Exception("Adapter not found in registry");

                        // Ask if we really want to do this
                        /* string question = value.Length > 0 ?
                             "Changing MAC-adress of adapter {0} from {1} to {2}. Proceed?" :
                             "Clearing custom MAC-address of adapter {0}. Proceed?";
                         DialogResult proceed = MessageBox.Show(
                             String.Format(question, this.ToString(), this.Mac, value),
                             "Change MAC-address?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                         if (proceed != DialogResult.Yes)
                             return false;*/

                        // Attempt to disable the adepter
                        var result = (uint)adapter.InvokeMethod("Disable", null);
                        if (result != 0)
                            throw new Exception("Failed to disable network adapter.");

                        // If we're here the adapter has been disabled, so we set the flag that will re-enable it in the finally block
                        shouldReenable = true;

                        // If we're here everything is OK; update or clear the registry value
                        if (value.Length > 0)
                            regkey.SetValue("NetworkAddress", value, RegistryValueKind.String);
                        else
                            regkey.DeleteValue("NetworkAddress");


                        return true;
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }

                finally
                {
                    if (shouldReenable)
                    {
                        uint result = (uint)adapter.InvokeMethod("Enable", null);
                        if (result != 0)
                            MessageBox.Show("Failed to re-enable network adapter.");
                    }
                }
            }

            public override string ToString()
            {
                return this.adaptername + this.customname;
            }

            /// <summary>
            /// Get a random (locally administered) MAC address.
            /// </summary>
            /// <returns>A MAC address having 01 as the least significant bits of the first byte, but otherwise random.</returns>
            public static string GetNewMac()
            {
                System.Random r = new System.Random();

                byte[] bytes = new byte[6];
                r.NextBytes(bytes);

                // Set second bit to 1
                bytes[0] = (byte)(bytes[0] | 0x02);
                // Set first bit to 0
                bytes[0] = (byte)(bytes[0] & 0xfe);

                return MacToString(bytes);
            }

            /// <summary>
            /// Verifies that a given string is a valid MAC address.
            /// </summary>
            /// <param name="mac">The string.</param>
            /// <param name="actual">false if the address is a locally administered address, true otherwise.</param>
            /// <returns>true if the string is a valid MAC address, false otherwise.</returns>
            public static bool IsValidMac(string mac, bool actual)
            {
                // 6 bytes == 12 hex characters (without dashes/dots/anything else)
                if (mac.Length != 12)
                    return false;

                // Should be uppercase
                if (mac != mac.ToUpper())
                    return false;

                // Should not contain anything other than hexadecimal digits
                if (!Regex.IsMatch(mac, "^[0-9A-F]*$"))
                    return false;

                if (actual)
                    return true;

                // If we're here, then the second character should be a 2, 6, A or E
                char c = mac[1];
                return (c == '2' || c == '6' || c == 'A' || c == 'E');
            }

            /// <summary>
            /// Verifies that a given MAC address is valid.
            /// </summary>
            /// <param name="mac">The address.</param>
            /// <param name="actual">false if the address is a locally administered address, true otherwise.</param>
            /// <returns>true if valid, false otherwise.</returns>
            public static bool IsValidMac(byte[] bytes, bool actual)
            {
                return IsValidMac(Adapter.MacToString(bytes), actual);
            }

            /// <summary>
            /// Converts a byte array of length 6 to a MAC address (i.e. string of hexadecimal digits).
            /// </summary>
            /// <param name="bytes">The bytes to convert.</param>
            /// <returns>The MAC address.</returns>
            public static string MacToString(byte[] bytes)
            {
                return BitConverter.ToString(bytes).Replace("-", "").ToUpper();
            }
        }
        public string newMacadd;
        public Form1()
        {
            InitializeComponent();
            /*userlabel.Text = "eli-lsp-90@outlook.com";
            passlabel.Text = "xkzj4k32321";
            userlabel.Text = "nathanvmag@gmail.com";
            passlabel.Text = "24841976";*/
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /* Windows generally seems to add a number of non-physical devices, of which
             * we would not want to change the address. Most of them have an impossible
             * MAC address. */
            foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces().Where(
                    a => Adapter.IsValidMac(a.GetPhysicalAddress().GetAddressBytes(), true)
                ).OrderByDescending(a => a.Speed))
            {
                AdaptersComboBox.Items.Add(new Adapter(adapter));
            }

            AdaptersComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Update the UI to show the current addresses.
        /// </summary>
        private void UpdateAddresses()
        {
            Adapter a = AdaptersComboBox.SelectedItem as Adapter;
            newMacadd = a.RegistryMac;
            this.ActualMacLabel.Text = a.Mac;
        }

        private void AdaptersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAddresses();
        }

        private void RandomButton_Click(object sender, EventArgs e)
        {
            newMacadd = Adapter.GetNewMac();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            newMacadd = Adapter.GetNewMac();

            if (!Adapter.IsValidMac(newMacadd, false))
            {
                MessageBox.Show("Entered MAC-address is not valid; will not update.", "Invalid MAC-address specified", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SetRegistryMac(newMacadd);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            SetRegistryMac("");
        }

        /// <summary>
        /// Set the address of the selected adapter to the given value and update the UI.
        /// </summary>
        /// <param name="mac">The MAC address to set.</param>
        private void SetRegistryMac(string mac)
        {
            Adapter a = AdaptersComboBox.SelectedItem as Adapter;

            if (a.SetRegistryMac(mac))
            {
                System.Threading.Thread.Sleep(100);
                UpdateAddresses();
                //MessageBox.Show("Done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RereadButton_Click(object sender, EventArgs e)
        {
            UpdateAddresses();
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            string login = userlabel.Text;
            string pass = passlabel.Text;

            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(pass))
            {
                DialogResult dr = MessageBox.Show("Você tem certeza que selecionou a conexão correta ? Caso não tenha a troca do mac adress irá falhar"
                    , "Duvida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    statuslabel.Text = "Status: Trocando Mac address";
                    UpdateButton_Click(sender, e);
                    statuslabel.Text = "Status: Aguardando 1 minutos para configurar Mac Address";
                    await Task.Delay(60000);
                    automateAsync(login, pass);

                }

            }
            else MessageBox.Show("Por favor preencha todos os campos", "Error", MessageBoxButtons.OK);
        }
        async         Task
automateAsync(string login, string pass)
        {
            ChromeOptions op = new ChromeOptions();
            op.AddArgument("--start-maximized");
            op.AddArgument("--incognito");
            IWebDriver driver = new ChromeDriver("./", op);
            statuslabel.Text = "Status: Logando no Adsense";

            try

            {
                driver.Navigate().GoToUrl("https://www.google.com/intl/pt-BR_br/adsense/start/#/?modal_active=none");
                await Task.Delay(1000);
                driver.FindElement(By.XPath("/html/body/header/nav/div/a[1]")).Click();
                await Task.Delay(1500);
                driver.FindElement(By.Id("identifierId")).SendKeys(login);
                driver.FindElement(By.Id("identifierId")).SendKeys(OpenQA.Selenium.Keys.Enter);
                await Task.Delay(5000);
                driver.FindElement(By.XPath("//*[@id='password']/div[1]/div/div[1]/input")).SendKeys(pass);
                driver.FindElement(By.XPath("//*[@id='password']/div[1]/div/div[1]/input")).SendKeys(OpenQA.Selenium.Keys.Enter);
                await Task.Delay(5000);

                driver.FindElement(By.Id("bruschetta_container")).GetAttribute("innerHTML");
                //driver.FindElement(By.XPath("//*[@id='as-menu-toggle']/div/material-icon/i")).Click();
                //await Task.Delay(300);
                //driver.FindElement(By.XPath("//*[@id='my-sites-section-header']/div/div")).Click();
                driver.Navigate().GoToUrl(driver.Url.Replace("home", "sites/my-sites"));
                statuslabel.Text = "Status: Obtendo informações";
                await Task.Delay(5000);
                bool work = true;
                int indx = 1;
                string resultpath = "./resultados/" + DateTime.Now.ToString("dd.MM.yyyy") + "/";
                if (!Directory.Exists(resultpath)) Directory.CreateDirectory(resultpath);
                StreamWriter writer = new StreamWriter(resultpath + login + ".txt");
                string finaltx = "Exibindo sites da conta " + login + Environment.NewLine;
                while (work)
                {
                    try
                    {
                        statuslabel.Text = "Status: Lendo site"+indx;

                        string site =
                            driver.FindElement(
                                By.XPath("//*[@id='sitemanagement_container']/my-sites/as-exception-handler/manage-sites-brita/overview/sites-list-brita/div/div[1]/div/material-expansionpanel-set/url-tree-brita[" + indx + "]/material-expansionpanel/div/header/div/div[1]/div/span"))
                                .Text;
                        string status = driver.FindElement(
                                By.XPath("//*[@id='sitemanagement_container']/my-sites/as-exception-handler/manage-sites-brita/overview/sites-list-brita/div/div[1]/div/material-expansionpanel-set/url-tree-brita[" + indx + "]/material-expansionpanel/div/header/div/div[2]/div/div"))
                                .Text.Replace("info_outline", "");
                        Console.WriteLine("O site " + site + " Esta em " + status);
                        finaltx += "O site " + site + " está em " + status + Environment.NewLine;
                        indx++;
                    }
                    catch (NoSuchElementException no)
                    {
                        Console.WriteLine("Acabou para este login");
                        break;
                    }
                    catch (Exception e)
                    {
                        finaltx += "Falha ao obter status " + e + Environment.NewLine;
                        Console.WriteLine(e.ToString());
                        break;
                    }
                }
                writer.Write(finaltx);
                writer.Close();
                driver.Quit();

            }
            catch (Exception e)
            {
                string resultpath = "./resultados/" + DateTime.Now.ToString("dd.MM.yyyy") + "/";
                if (!Directory.Exists(resultpath)) Directory.CreateDirectory(resultpath);
                StreamWriter writer = new StreamWriter(resultpath + login + ".txt");
                Console.WriteLine("Falha ao logar no adsense " + e.ToString());
                writer.Write("Falha ao logar no servidor do adsense "+Environment.NewLine+" "+e.ToString());
                writer.Close();
                driver.Quit();


                //  MessageBox.Show("Falha ao logar no adsense tente novamente depois", "Error", MessageBoxButtons.OK);
            }



            statuslabel.Text = "Status: Tarefa finalizada";

        }

        private async void button2_ClickAsync(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Você tem certeza que selecionou a conexão correta ? Caso não tenha a troca do mac adress irá falhar"
                       , "Duvida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                List<conta> contas = new List<conta>();
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Console.WriteLine(openFileDialog1.FileName);
                    StreamReader sr = new StreamReader(openFileDialog1.FileName);
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        try
                        {
                            string[] splited = line.Split(' ');
                            contas.Add(new conta(splited[0], splited[1]));
                        }
                        catch
                        {

                        }
                    }
                    sr.Close();
                    Console.WriteLine("automatizar " + contas.Count);
                    statuslabel.Text = "Status: Iniciando automatização de " + contas.Count + " contas";
                    foreach (conta c in contas)
                    {
                        userlabel.Text = c.user;
                        passlabel.Text = c.pass;
                        statuslabel.Text = "Status: Trocando Mac address";
                        UpdateButton_Click(sender, e);
                        statuslabel.Text = "Status: Aguardando 1 minutos para configurar Mac Address";
                        await Task.Delay(60000);
                        await automateAsync(c.user, c.pass);
                        statuslabel.Text = "Status: Fim de uma automação";


                    }
                    statuslabel.Text = "Status: Fim da tarefa em lotes";

                }
            }
        }
    }
    class conta
    {
        public string user, pass;
        public conta(string u, string p)
        {
            user = u;
            pass = p;
        }
    }
}

