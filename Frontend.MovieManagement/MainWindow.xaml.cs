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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.MovieManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LoginWindow _loginWindow;
        private AddEditWindow _addEditWindow;
        private UserSettingWindow _userSettingWindow;
        private MovieInfo[] _movieList;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _loginWindow = new LoginWindow();
            _loginWindow.OnLoginStatusCallback += OnLoginStatusCallback;
            _loginWindow.Show();
        }

        private void btnAddMovie_Click(object sender, RoutedEventArgs e)
        {
            if (_addEditWindow != null) { _addEditWindow.OnMovieActionCallback -= OnMovieActionCallback; }

            _addEditWindow = new AddEditWindow();
            _addEditWindow.OnMovieActionCallback += OnMovieActionCallback;
            _addEditWindow.Show();
        }

        private void OnLoginStatusCallback(UserInfo userInfo, bool isLogin)
        {
            _loginWindow.Close();
            btnUser.Content = Config.Username = userInfo.Name;
            Config.IsAdmin = userInfo.CanCreate && userInfo.CanRead && userInfo.CanUpdate && userInfo.CanDelete;
            txtSearch.IsEnabled = dMovies.IsEnabled = btnUser.IsEnabled = btnAddMovie.IsEnabled = true;
            LoadMovies();
        }

        private void OnMovieActionCallback(bool success, MovieInfo movieInfo)
        {
            if (_addEditWindow != null) { _addEditWindow.Close(); }
            LoadMovies();
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            if (_userSettingWindow != null) { }

            _userSettingWindow = new UserSettingWindow();
            _userSettingWindow.Show();
        }

        private void LoadMovies()
        {
            ApiResult ret = APIController.RequestGetAllMovies();
            if (!ret.Success) { MessageBox.Show("Unable to load movie infomation"); return; }

            _movieList = (MovieInfo[])ret.Data;
            foreach (MovieInfo movieInfo in _movieList)
            {
                this.dMovies.Items.Add(movieInfo);
            }
        }
    }
}
