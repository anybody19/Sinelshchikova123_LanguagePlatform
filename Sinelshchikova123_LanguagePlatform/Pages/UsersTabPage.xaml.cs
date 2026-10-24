using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sinelshchikova123_LanguagePlatform.Pages
{
    public partial class UsersTabPage : Page
    {
        public UsersTabPage()
        {
            InitializeComponent();
            DataGridUser.ItemsSource = new BD_Sinelshchikova123_LanguagePlatformEntities().User.ToList();
            this.IsVisibleChanged += (s, e) => {
                if (Visibility == Visibility.Visible)
                    DataGridUser.ItemsSource = new BD_Sinelshchikova123_LanguagePlatformEntities().User.ToList();
            };
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e) => NavigationService?.Navigate(new AddUserPage(null));

        private void ButtonEdit_Click(object sender, RoutedEventArgs e) => NavigationService?.Navigate(new AddUserPage((sender as Button).DataContext as User));

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var usersForRemoving = DataGridUser.SelectedItems.Cast<User>().ToList();
            if (usersForRemoving.Count == 0) return;
            if (MessageBox.Show($"Удалить {usersForRemoving.Count} записей?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (var db = new BD_Sinelshchikova123_LanguagePlatformEntities())
                {
                    db.User.RemoveRange(usersForRemoving);
                    db.SaveChanges();
                }
                DataGridUser.ItemsSource = new BD_Sinelshchikova123_LanguagePlatformEntities().User.ToList();
            }
        }
    }
}