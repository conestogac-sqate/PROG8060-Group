using PROG8060_Group.Models;
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
using System.Windows.Shapes;

namespace UI.MovieManagement
{
    /// <summary>
    /// Interaction logic for UserSettingWindow.xaml
    /// </summary>
    public partial class UserSettingWindow : Window
    {
        public delegate void UserAddedCallback(UserInfo userInfo);

        private UserInfo[] _userList;
        public UserSettingWindow()
        {
            InitializeComponent();
            this.Loaded += UserSettingWindow_Loaded;
        }

        private void UserSettingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            ApiResult ret = APIController.RequestGetAllUsers();
            if (!ret.Success) { MessageBox.Show("Unable to load movie infomation"); return; }

            _userList = (UserInfo[])ret.Data;

            this.dUsers.Items.Clear();
            if (_userList == null) { return; }
            foreach (UserInfo userInfo in _userList)
            {
                this.dUsers.Items.Add(userInfo);
            }
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            bool canCreate = false; bool canUpdate = false; bool canRead = true; bool canDelete = false;
            if (rBtnAdmin.IsChecked ?? false) { canCreate = canUpdate = canRead = canDelete = true; }

            UserInfo userInfo = new UserInfo(txtUsername.Text, txtPassword.Text, txtEmail.Text, canCreate, canUpdate, canRead, canDelete);
            ApiResult ret = APIController.RequestAddUser(userInfo);

            if (!ret.Success) { MessageBox.Show("Unable to Submit new user Infomation"); return; }
            MessageBox.Show("New User Added");
            LoadUsers();
        }

        private void dUsers_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            UserInfo userInfo = (UserInfo)e.Row.Item;
            if (MessageBox.Show("Are you change the role of " + userInfo.Name + " from " + (userInfo.IsAdmin ? "admin to Regular user?" : "regular user to admin"), "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) { e.Cancel = true; return; }
            userInfo.IsAdmin = !userInfo.IsAdmin;
            if (userInfo.IsAdmin) { userInfo.CanCreate = userInfo.CanUpdate = userInfo.CanRead = userInfo.CanDelete = true; }
            else { userInfo.CanRead = true; userInfo.CanCreate = userInfo.CanUpdate = userInfo.CanDelete = false; }
            ApiResult ret = APIController.RequestEditUserRole(userInfo);
            if (!ret.Success) { MessageBox.Show("Unable to edit user role"); e.Cancel = true; return; }
            LoadUsers(); e.Cancel = true;
        }
    }
}
