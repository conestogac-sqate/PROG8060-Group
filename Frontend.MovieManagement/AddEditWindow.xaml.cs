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
        private int _movieId = -1;

        public AddEditWindow(ActionType actionType = ActionType.Create, int movieId = -1)
        {
            InitializeComponent();

            this._actionType = actionType;
            this._movieId = movieId;
            if (actionType == ActionType.Create)
            {
                lblHeader.Content = "Add New Movie";
                btnSubmit.Content = "Add MOVIE";
            }
            else
            {
                lblHeader.Content = "Edit The Movie";
                btnSubmit.Content = "EDIT MOVIE";
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (OnMovieActionCallback == null) { MessageBox.Show("Unable to submit movie due to invalid listener"); return; }

            MovieInfo movieInfo = new MovieInfo();
            movieInfo.Title = txtTitle.Text;
            movieInfo.Director = txtDirector.Text;
            movieInfo.Genere = txtGenre.Text;
            movieInfo.Cast = txtCast.Text;
            movieInfo.Year = Convert.ToInt32(txtYear.Text);
            movieInfo.Award = txtAwards.Text;
            movieInfo.IsOnShow = rBtnNotPlaying.IsChecked ?? false;

            ApiResult ret = APIController.RequestAddMovie(movieInfo);
            if (!ret.Success) { MessageBox.Show("Unable to Submit Movie Infomation"); return; }
            if (_actionType == ActionType.Create)
            {
                movieInfo.Id = (int)ret.Data;
            }

            MessageBox.Show("Movie Submit Success!");
            OnMovieActionCallback(ret.Success, movieInfo);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
