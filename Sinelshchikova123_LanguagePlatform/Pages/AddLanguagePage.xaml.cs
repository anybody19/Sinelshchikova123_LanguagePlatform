using System.Windows;
using System.Windows.Controls;

namespace Sinelshchikova123_LanguagePlatform.Pages
{
    public partial class AddLanguagePage : Page
    {
        private Language _currentLanguage;
        public AddLanguagePage(Language selectedLanguage)
        {
            InitializeComponent();
            _currentLanguage = selectedLanguage ?? new Language();
            DataContext = _currentLanguage;
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_currentLanguage.Name)) { MessageBox.Show("Введите название!"); return; }
            using (var db = new BD_Sinelshchikova123_LanguagePlatformEntities())
            {
                if (_currentLanguage.ID == 0) db.Language.Add(_currentLanguage);
                db.SaveChanges();
            }
            MessageBox.Show("Сохранено!"); NavigationService?.GoBack();
        }
    }
}