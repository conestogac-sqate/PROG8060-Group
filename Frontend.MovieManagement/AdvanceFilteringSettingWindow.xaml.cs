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
    /// Interaction logic for AdvanceFilteringSettingWindow.xaml
    /// </summary>
    public partial class AdvanceFilteringSettingWindow : Window
    {
        public delegate void AdvanceFilterCallback(SearchConfiguration filterSettings);
        public AdvanceFilterCallback OnAdvanceFilterCallback;

        public AdvanceFilteringSettingWindow()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchConfiguration filterSettings = new SearchConfiguration();
            filterSettings.Title = txtTitle.Text;
            filterSettings.Director = txtDirector.Text;
            filterSettings.Genre = txtGenre.Text;
            filterSettings.Cast = txtCast.Text;
            filterSettings.Year = string.IsNullOrEmpty(txtYear.Text) ? -1 : Convert.ToInt32(txtYear.Text);
            filterSettings.Award = txtAward.Text;
            filterSettings.IsOnShow = rBtnClearAll.IsChecked ?? true ? SearchConfiguration.OnShow.UNKNOWN : (rBtnNowPlaying.IsChecked ?? true ? SearchConfiguration.OnShow.YES : SearchConfiguration.OnShow.NO);

            if (OnAdvanceFilterCallback == null) { MessageBox.Show("Unable to do advance search"); return; }
            OnAdvanceFilterCallback(filterSettings);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
