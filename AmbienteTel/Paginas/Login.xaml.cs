using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using AmbienteTel.Modais;
using BLL;
using MaterialDesignThemes.Wpf;

namespace AmbienteTel.Paginas
{
    /// <summary>
    /// Interação lógica para Login.xam
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnAcessar_Click(object sender, RoutedEventArgs e)
        {
            UsuarioBLL _usuarioBLL = new UsuarioBLL();

            txtlogin.IsEnabled = false;
            txtSenha.IsEnabled = false;
            btnAcessar.IsEnabled = false;
            btnAcessar.Content = "Carregando...";

            string login = txtlogin.Text;
            string senha = txtSenha.Password;


            if (_usuarioBLL.VerificarLogin(login, senha))
            {
                NavigationService ns = NavigationService.GetNavigationService(this);
                ns.Content = new Guias(login, senha);
            }
            else
            {
                Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(async () =>
                {
                    Notificacao msg = new Notificacao();
                    msg.Mensagem = "Usuário ou senha inválidos.";

                    await DialogHost.Show(msg, "ModalAlert");

                    btnAcessar.IsEnabled = true;
                    txtlogin.IsEnabled = true;
                    txtSenha.IsEnabled = true;
                    btnAcessar.Content = "Acessar";
                }));
                return;

            }

        }


    }
}

