using PROG8060_Group.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        public delegate void MovieActionCallback(bool success, MovieInfo movieInfo);
        public MovieActionCallback OnMovieActionCallback;

        public enum ActionType { Create = 0, Edit = 1 }
        private ActionType _actionType;
        private MovieInfo _movieInfo = null;

        public AddEditWindow(ActionType actionType = ActionType.Create, MovieInfo movieInfo = null)
        {
            InitializeComponent();

            this._actionType = actionType;
            this._movieInfo = movieInfo == null ? new MovieInfo() : movieInfo;
            if (actionType == ActionType.Create)
            {
                lblHeader.Content = "Add New Movie";
                btnSubmit.Content = "ADD MOVIE";
                this.Title = "Add Movie";
            }
            else
            {
                lblHeader.Content = "Edit The Movie";
                btnSubmit.Content = "EDIT MOVIE";
                this.Title = "Edit Movie";
                if (movieInfo == null) { MessageBox.Show("Unable to load movie info."); return; }
                txtTitle.Text = movieInfo.Title;
                txtDirector.Text = movieInfo.Director;
                txtGenre.Text = movieInfo.Genere;
                txtCast.Text = movieInfo.Cast;
                txtYear.Text = movieInfo.Year.ToString();
                txtAwards.Text = movieInfo.Award;
                rBtnNowPlaying.IsChecked = movieInfo.IsOnShow;
                rBtnNotPlaying.IsChecked = !rBtnNowPlaying.IsChecked;
            }   
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (OnMovieActionCallback == null) { MessageBox.Show("Unable to submit movie due to invalid listener"); return; }
            if(string.IsNullOrEmpty(txtTitle.Text) ||
               string.IsNullOrEmpty(txtDirector.Text) ||
               string.IsNullOrEmpty(txtGenre.Text) ||
               string.IsNullOrEmpty(txtYear.Text))
            {
                MessageBox.Show("Invalid Input. Please make sure you have input \"Title\", \"Director\", \"Genre\", \"Cast\", \"Year\", \"Awards\"");
                return;
            }

            _movieInfo.Title = txtTitle.Text;
            _movieInfo.Director = txtDirector.Text;
            _movieInfo.Genere = txtGenre.Text;
            _movieInfo.Cast = txtCast.Text;
            _movieInfo.Year = Convert.ToInt32(txtYear.Text);
            _movieInfo.Award = txtAwards.Text;
            _movieInfo.IsOnShow = !rBtnNotPlaying.IsChecked ?? false;

            ApiResult ret = null;

            if(_actionType == ActionType.Create)
            {
                ret = APIController.RequestAddMovie(_movieInfo);
            }
            else
            {
                ret = APIController.RequestEditMovie(_movieInfo);
            }
            if (!ret.Success) { MessageBox.Show("Unable to Submit Movie Infomation. Please make sure you have input \"Title\", \"Director\", \"Genre\", \"Cast\", \"Year\", \"Awards\""); return; }
            if (_actionType == ActionType.Create)
            {
                _movieInfo.Id = (int)ret.Data;
            }

            MessageBox.Show("Movie Submit Success!");
            OnMovieActionCallback(ret.Success, _movieInfo);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
