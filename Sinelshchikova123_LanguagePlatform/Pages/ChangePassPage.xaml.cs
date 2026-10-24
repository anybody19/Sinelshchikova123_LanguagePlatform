using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sinelshchikova123_LanguagePlatform.Pages
{
    public partial class ChangePassPage : Page
    {
        public ChangePassPage()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentPasswordBox.Password) || string.IsNullOrEmpty(NewPasswordBox.Password) || string.IsNullOrEmpty(ConfirmPasswordBox.Password) || string.IsNullOrEmpty(TbLogin.Text))
            {
                MessageBox.Show("Все поля обязательны к заполнению!");
                return;
            }

            if (NewPasswordBox.Password != ConfirmPasswordBox.Password)
            {
                MessageBox.Show("Новые пароли не совпадают!");
                return;
            }

            string hashedOld = AuthPage.GetHash(CurrentPasswordBox.Password);

            using (var db = new BD_Sinelshchikova123_LanguagePlatformEntities())
            {
                var user = db.User.FirstOrDefault(u => u.Login == TbLogin.Text && u.Password == hashedOld);
                if (user == null)
                {
                    MessageBox.Show("Текущий логин/пароль неверный!");
                    return;
                }

                user.Password = AuthPage.GetHash(NewPasswordBox.Password);
                db.SaveChanges();
            }

            MessageBox.Show("Пароль успешно изменен!");
            NavigationService?.Navigate(new AuthPage());
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}