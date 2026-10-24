using Sinelshchikova123_LanguagePlatform;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sinelshchikova123_LanguagePlatform.Pages
{
    public partial class AuthPage : Page
    {
        private int failedAttempts = 0;

        public AuthPage()
        {
            InitializeComponent();
        }

        public static string GetHash(string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }

        private void ButtonEnter_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLogin.Text) || string.IsNullOrEmpty(PasswordBox.Password))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }

            string hashedPassword = GetHash(PasswordBox.Password);

            using (var db = new BD_Sinelshchikova123_LanguagePlatformEntities())
            {
                var user = db.User.AsNoTracking().FirstOrDefault(u => u.Login == TextBoxLogin.Text && u.Password == hashedPassword);

                if (user == null)
                {
                    MessageBox.Show("Пользователь с такими данными не найден!");
                    failedAttempts++;

                    if (failedAttempts >= 3 && CaptchaGrid.Visibility != Visibility.Visible)
                    {
                        CaptchaSwitch();
                        CaptchaChange();
                    }
                    return;
                }

                MessageBox.Show($"Добро пожаловать, {user.FIO}!");

                // ВОТ ЗДЕСЬ ДОЛЖЕН БЫТЬ ПЕРЕХОД 
                if (user.Role == "Admin")
                {
                    NavigationService?.Navigate(new AdminPage());
                }
                else
                {
                    NavigationService?.Navigate(new UserPage());
                }
                
            }
        }

        private void CaptchaSwitch()
        {
            if (CaptchaGrid.Visibility == Visibility.Collapsed)
            {
                CaptchaGrid.Visibility = Visibility.Visible;
                ButtonSubmitCaptcha.Visibility = Visibility.Visible;
                ButtonEnter.Visibility = Visibility.Collapsed;
                ButtonReg.Visibility = Visibility.Collapsed;
                ButtonChangePassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                CaptchaGrid.Visibility = Visibility.Collapsed;
                ButtonSubmitCaptcha.Visibility = Visibility.Collapsed;
                ButtonEnter.Visibility = Visibility.Visible;
                ButtonReg.Visibility = Visibility.Visible;
                ButtonChangePassword.Visibility = Visibility.Visible;
                CaptchaInput.Clear();
            }
        }

        private void CaptchaChange()
        {
            string allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z,1,2,3,4,5,6,7,8,9,0";
            char[] a = { ',' };
            string[] ar = allowchar.Split(a);
            string pwd = "";
            Random r = new Random();

            for (int i = 0; i < 5; i++)
            {
                pwd += ar[r.Next(0, ar.Length)];
            }
            CaptchaText.Text = pwd;
        }

        private void ButtonSubmitCaptcha_Click(object sender, RoutedEventArgs e)
        {
            if (CaptchaInput.Text != CaptchaText.Text)
            {
                MessageBox.Show("Неверно введена капча!", "Ошибка");
                CaptchaChange();
            }
            else
            {
                MessageBox.Show("Капча пройдена, попробуйте войти снова.", "Успех");
                CaptchaSwitch();
                failedAttempts = 0;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e) { }

        private void ButtonReg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new RegPage());
        }

        private void ButtonChangePassword_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("это для красоты");
        }
    }
}