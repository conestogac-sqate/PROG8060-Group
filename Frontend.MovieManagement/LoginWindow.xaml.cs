using System;
using System.Collections.Generic;
using System.Linq;
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
using Newtonsoft.Json;
using PROG8060_Group.Models;
using RestSharp;

namespace UI.MovieManagement
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public delegate void LoginStatusCallback(UserInfo userInfo, bool isLogin);
        public LoginStatusCallback OnLoginStatusCallback;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (OnLoginStatusCallback == null) { MessageBox.Show("Unable to submit movie due to invalid listener"); return; }

            this.IsEnabled = false;

            string username = txtUsername.Text;
            string password = txtPassword.Text;

            ApiResult ret = APIController.RequestLogin(username, password);
            if(!ret.Success)
            {
                MessageBox.Show("Unable to login");
                this.IsEnabled = true;
                return;
            }
            OnLoginStatusCallback((UserInfo)ret.Data, true);
        }
    }
}
