using AmbienteTel.Paginas;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AmbienteTel
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        Process _vpnProcess;

        public MainWindow()
        {
            InitializeComponent();

            string regKey = @"SOFTWARE";
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(regKey))
            {
                if (key.GetSubKeyNames().Any(keyName => keyName.ToString() == "OpenVPN"))
                {
                    Console.WriteLine("Already installed...");

                    string destino = "C:\\Program Files\\OpenVPN\\config";
                    string folder = Directory.GetCurrentDirectory() + "\\Openvpn\\config";

                    //copiar arquivos de configuração
                    if (!Directory.Exists(destino))
                    {
                        Directory.CreateDirectory(destino);
                    }

                    DirectoryInfo dir = new DirectoryInfo(folder);
                    foreach (FileInfo f in dir.GetFiles())
                    {
                        try
                        {
                            File.Copy(f.FullName, destino + "\\" + f.Name);
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    //iniciar e conectar OpenVPN Client
                    _vpnProcess = new Process();
                    _vpnProcess.StartInfo.FileName = "openvpn-gui.exe";
                    _vpnProcess.StartInfo.Arguments = "--connect tel.ovpn";
                    _vpnProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    _vpnProcess.StartInfo.CreateNoWindow = true;
                    _vpnProcess.StartInfo.WorkingDirectory = "C:\\Program Files\\OpenVPN\\bin";
                    _vpnProcess.Start();
                }
                else
                {
                    //instalar OpenVPN Client
                    Process process = new Process();
                    process.StartInfo.FileName = "openvpn-install-2.4.8-I602-Win10.exe";
                    process.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory() + "\\" + "Openvpn";
                    process.StartInfo.Arguments = "/S";
                    process.StartInfo.Verb = "runas";
                    process.Start();
                    process.WaitForExit();

                    string destino = "C:\\Program Files\\OpenVPN\\config";
                    string folder = Directory.GetCurrentDirectory() + "\\Openvpn\\config";

                    //copiar arquivos de configuração
                    if (!Directory.Exists(destino))
                    {
                        Directory.CreateDirectory(destino);
                    }

                    DirectoryInfo dir = new DirectoryInfo(folder);
                    foreach (FileInfo f in dir.GetFiles())
                    {
                        try
                        {
                            File.Copy(f.FullName, destino + "\\" + f.Name);
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    //iniciar e conectar OpenVPN Client
                    _vpnProcess = new Process();
                    _vpnProcess.StartInfo.FileName = "openvpn-gui.exe";
                    _vpnProcess.StartInfo.Arguments = "--connect tel.ovpn";
                    _vpnProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    _vpnProcess.StartInfo.CreateNoWindow = true;
                    _vpnProcess.StartInfo.WorkingDirectory = "C:\\Program Files\\OpenVPN\\bin";
                    _vpnProcess.Start();
                }
            }

            FramePrincipal.Content = new Login();
        }       

        protected void CloseWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //TODO: Encerrar conexão e fechar OpenVPN
            foreach (var p in Process.GetProcessesByName("openvpn"))
            {                
                p.Kill();
            }
            foreach (var p in Process.GetProcessesByName("ToolbarCTI"))
            {
                p.CloseMainWindow();
            }
            foreach (var p in Process.GetProcessesByName("onexcui"))
            {
                p.Kill();
            }
            Application.Current.Shutdown();
        }
    }
}
