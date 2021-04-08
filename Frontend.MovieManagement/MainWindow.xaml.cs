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
        private AdvanceFilteringSettingWindow _advanceFilteringSettingsWindow;
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
            if (_addEditWindow?.OnMovieActionCallback != null) { _addEditWindow.OnMovieActionCallback -= OnMovieActionCallback; }

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

            if (Config.IsAdmin)
            {
                colDirector.Width = 130;
                colEdit.Visibility = Visibility.Visible;
                colDelete.Visibility = Visibility.Visible;
                btnUser.Click += btnUser_Click;
            }

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

        private void btnEditMovie_Click(object sender, RoutedEventArgs e)
        {
            if (_addEditWindow?.OnMovieActionCallback != null) { _addEditWindow.OnMovieActionCallback -= OnMovieActionCallback; }

            string id = ((Button)sender).CommandParameter?.ToString();
            ApiResult ret = APIController.RequestGetMoviesByIds(id);

            MovieInfo movieInfo = ((MovieInfo[])ret.Data).Length != 1 ? null : ((MovieInfo[])ret.Data)[0];
            if(movieInfo == null) { MessageBox.Show("Unable to get movie infomation."); return; }
            _addEditWindow = new AddEditWindow(AddEditWindow.ActionType.Edit, movieInfo);
            _addEditWindow.OnMovieActionCallback += OnMovieActionCallback;
            _addEditWindow.Show();
        }

        private void btnRemoveMovie_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure deleteing the movie?\nYou cannot recover the movie once it is deleted.", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) { return; }

            string id = ((Button)sender).CommandParameter?.ToString();
            ApiResult ret = APIController.RequestDeleteMovie(id);

            bool success = (bool)ret.Data;
            if (!success) { MessageBox.Show("Unable to delete movie"); return; }
            LoadMovies();
        }

        private void LoadMovies()
        {
            ApiResult ret = APIController.RequestGetAllMovies();
            if (!ret.Success) { MessageBox.Show("Unable to load movie infomation"); return; }

            _movieList = (MovieInfo[])ret.Data;

            this.dMovies.Items.Clear();
            if (_movieList == null) { return; }
            foreach (MovieInfo movieInfo in _movieList)
            {
                this.dMovies.Items.Add(movieInfo);
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string prefix = txtSearch.Text;
            ApiResult ret = APIController.RequestGetMoviesByPrefix(prefix);

            _movieList = (MovieInfo[])ret.Data;
            this.dMovies.Items.Clear();
            if (_movieList == null) { return; }
            foreach (MovieInfo movieInfo in _movieList)
            {
                this.dMovies.Items.Add(movieInfo);
            }
        }

        private void btnSearchAdvance_Click(object sender, RoutedEventArgs e)
        {
            if (_advanceFilteringSettingsWindow?.OnAdvanceFilterCallback != null) { _advanceFilteringSettingsWindow.OnAdvanceFilterCallback -= OnAdvanceFilterCallback; }

            _advanceFilteringSettingsWindow = new AdvanceFilteringSettingWindow();
            _advanceFilteringSettingsWindow.OnAdvanceFilterCallback += OnAdvanceFilterCallback;
            _advanceFilteringSettingsWindow.Show();
        }

        private void OnAdvanceFilterCallback(SearchConfiguration filterSettings)
        {
            _advanceFilteringSettingsWindow.Close();
            ApiResult ret = APIController.RequestGetMoviesAdvance(filterSettings);
            _movieList = (MovieInfo[])ret.Data;
            this.dMovies.Items.Clear();
            if (_movieList == null) { return; }
            foreach (MovieInfo movieInfo in _movieList)
            {
                this.dMovies.Items.Add(movieInfo);
            }
        }
    }
}
