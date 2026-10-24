using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sinelshchikova123_LanguagePlatform.Pages
{
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
            comboBxRole.SelectedIndex = 0;
        }

        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtbxLog.Text) || string.IsNullOrEmpty(txtbxFIO.Text) || string.IsNullOrEmpty(passBxFrst.Password) || string.IsNullOrEmpty(passBxScnd.Password))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            if (passBxFrst.Password != passBxScnd.Password)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }

            using (var db = new BD_Sinelshchikova123_LanguagePlatformEntities())
            {
                var existingUser = db.User.AsNoTracking().FirstOrDefault(u => u.Login == txtbxLog.Text);
                if (existingUser != null)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!");
                    return;
                }

                User newUser = new User
                {
                    Login = txtbxLog.Text,
                    Password = AuthPage.GetHash(passBxFrst.Password),
                    Role = (comboBxRole.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    FIO = txtbxFIO.Text
                };

                db.User.Add(newUser);

                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Вы успешно зарегистрированы!");
                    NavigationService?.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ОШИБКА БАЗЫ ДАННЫХ: " + ex.Message + "\n\n" + ex.InnerException?.Message);
                }
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}