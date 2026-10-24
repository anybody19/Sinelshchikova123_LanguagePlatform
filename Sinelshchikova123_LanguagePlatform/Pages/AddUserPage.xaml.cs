using System;
using System.Windows;
using System.Windows.Controls;

namespace Sinelshchikova123_LanguagePlatform.Pages
{
    public partial class AddUserPage : Page
    {
        private User _currentUser;

        public AddUserPage(User selectedUser)
        {
            InitializeComponent();
            _currentUser = selectedUser ?? new User();
            DataContext = _currentUser;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_currentUser.Login) || string.IsNullOrWhiteSpace(_currentUser.FIO))
            {
                MessageBox.Show("Заполните обязательные поля!");
                return;
            }
            using (var db = new BD_Sinelshchikova123_LanguagePlatformEntities())
            {
                if (_currentUser.ID == 0) db.User.Add(_currentUser);
                db.SaveChanges();
            }
            MessageBox.Show("Сохранено!");
            NavigationService?.GoBack();
        }

        private void ButtonClean_Click(object sender, RoutedEventArgs e)
        {
            TBLogin.Text = ""; TBPass.Text = ""; cmbRole.Text = ""; TBFio.Text = ""; TBPhoto.Text = "";
        }
    }
}