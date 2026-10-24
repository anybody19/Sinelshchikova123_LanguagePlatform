using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sinelshchikova123_LanguagePlatform.Pages
{
    public partial class LanguageTabPage : Page
    {
        public LanguageTabPage()
        {
            InitializeComponent();
            DataGridLanguage.ItemsSource = new BD_Sinelshchikova123_LanguagePlatformEntities().Language.ToList();
            this.IsVisibleChanged += (s, e) => {
                if (Visibility == Visibility.Visible) DataGridLanguage.ItemsSource = new BD_Sinelshchikova123_LanguagePlatformEntities().Language.ToList();
            };
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e) => NavigationService?.Navigate(new AddLanguagePage(null));
        private void ButtonEdit_Click(object sender, RoutedEventArgs e) => NavigationService?.Navigate(new AddLanguagePage((sender as Button).DataContext as Language));
        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var items = DataGridLanguage.SelectedItems.Cast<Language>().ToList();
            if (items.Count == 0) return;
            if (MessageBox.Show($"Удалить {items.Count}?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (var db = new BD_Sinelshchikova123_LanguagePlatformEntities()) { db.Language.RemoveRange(items); db.SaveChanges(); }
                DataGridLanguage.ItemsSource = new BD_Sinelshchikova123_LanguagePlatformEntities().Language.ToList();
            }
        }
    }
}