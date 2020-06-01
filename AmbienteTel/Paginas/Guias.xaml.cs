using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL;
using CefSharp.Wpf;
using DTO;
using MaterialDesignThemes;
using Microsoft.Win32;

namespace AmbienteTel.Paginas
{
    /// <summary>
    /// Interação lógica para Guias.xam
    /// </summary>
    public partial class Guias : Page
    {
        private string _usuario { get; set; }
        private string _senha { get; set; }
        UsuarioBLL _usuarioBLL = new UsuarioBLL();
        SistemaBLL _sistemaBLL = new SistemaBLL();

        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_NOACTIVATE = 0x0010;

        public Guias(string usuario, string senha)
        {
            this._usuario = usuario;
            this._senha = senha;

            InitializeComponent();

            CarregarSistemas();

            InstalarDiscador();

            //CarregarExecutaveis();
        }

        private void InstalarDiscador()
        {
            string regKey = @"SOFTWARE";
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(regKey))
            {
                if (!key.GetSubKeyNames().Any(keyName => keyName.ToString() == "Avaya"))
                {
                    //instalar Avaya Client
                    Process process = new Process();
                    process.StartInfo.FileName = "Avaya_oneX_Communicator_Suite.exe";
                    process.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory() + "\\" + "Discador";                    
                    process.StartInfo.Verb = "runas";
                    process.Start();
                    process.WaitForExit(); 
                }
            }
        }

        private void CarregarExecutaveis()
        {

            //ProcessStartInfo procInfo = new ProcessStartInfo();            
            //procInfo.WindowStyle = ProcessWindowStyle.Minimized;            

            //WindowsFormsHost windowsFormsHost = new WindowsFormsHost();
            //System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();

            //windowsFormsHost.Child = panel;

            //TabItem item = new TabItem
            //{
            //    Header = "NOME do EXE",
            //    Content = windowsFormsHost
            //};

            //TabControl.Items.Add(item);

            //Process proc = new Process();
            //proc.StartInfo = procInfo;
            //proc = Process.Start(Directory.GetCurrentDirectory() + "\\Asimov\\Asimov.exe", null, null, null);          

            //proc.WaitForInputIdle();

            //Thread.Sleep(5000);
            //SetParent(proc.MainWindowHandle, panel.Handle);            

            ////Thread.Sleep(5000);
            //SetWindowPos(proc.MainWindowHandle, panel.Handle, 0, 0, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height, SWP_NOZORDER | SWP_NOACTIVATE);

        }

        private void CarregarSistemas()
        {
            try
            {
                UsuarioDTO _usuarioDTO = new UsuarioDTO();
                _usuarioDTO = _usuarioBLL.ObterUsuario(_usuario, _senha);
                List<SistemaDTO> _listSistemas = new List<SistemaDTO>();
                _listSistemas = _sistemaBLL.ObterSistemas(_usuarioDTO.usuarioGrupo);
                Button buttonSistemas;

                int count = 0;
                foreach (SistemaDTO sistema in _listSistemas)
                {
                    buttonSistemas = new Button();
                    buttonSistemas.Content = sistema.sistemaNome;
                    buttonSistemas.Name = "Button" + sistema.sistemaNome.Replace(" ","_");

                    if (sistema.sistemaExecutavel)
                    {
                        if (sistema.sistemaNome.ToUpper().Equals("ASIMOV"))
                        {
                            //ImageBrush icone = new ImageBrush();
                            //icone.ImageSource = new BitmapImage(new Uri("../Imagens/Asimov2.ico", UriKind.Relative));
                            //buttonSistemas.Background = icone;
                        }

                        buttonSistemas.Click += (sender, EventArgs) => { CarregarExe(sender, EventArgs, sistema); };
                        
                    }
                    else
                    {
                        Uri uri = new Uri(sistema.sistemaCaminho, UriKind.Absolute);
                        buttonSistemas.Click += (sender, EventArgs) => { CarregarUri(sender, EventArgs, uri); };
                    }

                    RowDefinition height = new RowDefinition();
                    height.Height = GridLength.Auto;

                    GridButtons.RowDefinitions.Add(height);

                    Grid.SetRow(buttonSistemas, count);
                    GridButtons.Children.Add(buttonSistemas);

                    count++;

                }               
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //private void CarregarSistema(SistemaDTO site)
        private void CarregarUri(object sender, EventArgs e, Uri uri)
        {
            ChromiumWebBrowser browser = new ChromiumWebBrowser
            {
                Address = uri.AbsoluteUri               
            };

            //WebBrowser browser = new WebBrowser
            //{
            //    Source = new Uri(uri.AbsoluteUri, UriKind.Absolute)
            //};

            TabItem item = new TabItem
            {
                Header = uri.AbsoluteUri,
                Content = browser
            };

            TabControl.AddToSource(item);
            TabControl.SelectedItem = item;

        }

        private void CarregarExe(object sender, EventArgs e, SistemaDTO sistema)
        {
            ProcessStartInfo procInfo = new ProcessStartInfo();
            procInfo.WindowStyle = ProcessWindowStyle.Minimized;

            WindowsFormsHost windowsFormsHost = new WindowsFormsHost();
            System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();

            windowsFormsHost.Child = panel;

            TabItem item = new TabItem
            {
                Header = sistema.sistemaNome,
                Content = windowsFormsHost
            };

            TabControl.AddToSource(item);
            TabControl.SelectedItem = item;


            Process proc = new Process();
            proc.StartInfo = procInfo;
            proc = Process.Start(Directory.GetCurrentDirectory() + sistema.sistemaCaminho, null, null, null);

            proc.WaitForInputIdle();

            Thread.Sleep(5000);
            SetParent(proc.MainWindowHandle, panel.Handle);

            Thread.Sleep(5000);
            SetWindowPos(proc.MainWindowHandle, panel.Handle, 0, -40, (int)TabControl.ActualWidth, (int)TabControl.ActualHeight + 40, SWP_NOZORDER | SWP_NOACTIVATE);

        }        

        private void btnDiscador_Click(object sender, EventArgs e)
        {

            //iniciar e conectar Avaya
            Process avaya = new Process();
            avaya.StartInfo.FileName = "onexcui.exe";            
            avaya.StartInfo.WorkingDirectory = "C:\\Program Files (x86)\\Avaya\\Avaya one-X Communicator";
            avaya.Start();

            //iniciar Toolbar de discagem
            //Process procToolbar = new Process();
            //procToolbar.StartInfo.FileName = "ToolbarCTI.exe";
            //procToolbar.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory() + "\\Toolbar";
            //procToolbar.Start(); 
        }


        [DllImport("USER32.DLL")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        private extern static bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);
                
    }
}
