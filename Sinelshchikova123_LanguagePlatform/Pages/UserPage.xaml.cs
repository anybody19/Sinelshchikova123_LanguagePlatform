using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sinelshchikova123_LanguagePlatform.Pages
{
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            UpdateUsers();
        }

        private void UpdateUsers()
        {
            if (!IsInitialized) return;
            using (var db = new BD_Sinelshchikova123_LanguagePlatformEntities())
            {
                var currentUsers = db.User.ToList();

                if (!string.IsNullOrWhiteSpace(fioFilterTextBox.Text))
                {
                    currentUsers = currentUsers.Where(x => x.FIO.ToLower().Contains(fioFilterTextBox.Text.ToLower())).ToList();
                }

                if (onlyAdminCheckBox.IsChecked == true)
                {
                    currentUsers = currentUsers.Where(x => x.Role == "Admin").ToList();
                }

                ListUser.ItemsSource = (sortComboBox.SelectedIndex == 0)
                    ? currentUsers.OrderBy(x => x.FIO).ToList()
                    : currentUsers.OrderByDescending(x => x.FIO).ToList();
            }
        }

        private void fioFilterTextBox_TextChanged(object sender, TextChangedEventArgs e) => UpdateUsers();
        private void sortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdateUsers();
        private void onlyAdminCheckBox_Checked(object sender, RoutedEventArgs e) => UpdateUsers();
        private void onlyAdminCheckBox_Unchecked(object sender, RoutedEventArgs e) => UpdateUsers();
    }
}