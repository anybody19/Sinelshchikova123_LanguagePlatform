using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sinelshchikova123_LanguagePlatform.Pages
{
    public partial class AddProgressPage : Page
    {
        private Progress _currentProgress;
        public AddProgressPage(Progress selectedProgress)
        {
            InitializeComponent();
            _currentProgress = selectedProgress ?? new Progress();
            DataContext = _currentProgress;

            using (var db = new BD_Sinelshchikova123_LanguagePlatformEntities())
            {
                CBUser.ItemsSource = db.User.ToList();
                CBLanguage.ItemsSource = db.Language.ToList();
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_currentProgress.Name)) { MessageBox.Show("Введите тему!"); return; }
            using (var db = new BD_Sinelshchikova123_LanguagePlatformEntities())
            {
                if (_currentProgress.ID == 0) db.Progress.Add(_currentProgress);
                db.SaveChanges();
            }
            MessageBox.Show("Сохранено!"); NavigationService?.GoBack();
        }
    }
}