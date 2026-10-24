using System.Windows;
using System.Windows.Controls;

namespace Sinelshchikova123_LanguagePlatform.Pages
{
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void BtnTab1_Click(object sender, RoutedEventArgs e) { NavigationService?.Navigate(new UsersTabPage()); }
        private void BtnTab2_Click(object sender, RoutedEventArgs e) { NavigationService?.Navigate(new LanguageTabPage()); }
        private void BtnTab3_Click(object sender, RoutedEventArgs e) { NavigationService?.Navigate(new ProgressTabPage()); }
        private void BtnTab4_Click(object sender, RoutedEventArgs e) { NavigationService?.Navigate(new DiagrammPage()); }
    }
}