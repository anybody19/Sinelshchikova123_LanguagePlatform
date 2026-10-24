using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sinelshchikova123_LanguagePlatform.Pages
{
    public partial class ProgressTabPage : Page
    {
        public ProgressTabPage()
        {
            InitializeComponent();
            DataGridProgress.ItemsSource = new BD_Sinelshchikova123_LanguagePlatformEntities().Progress.ToList();
            this.IsVisibleChanged += (s, e) => {
                if (Visibility == Visibility.Visible) DataGridProgress.ItemsSource = new BD_Sinelshchikova123_LanguagePlatformEntities().Progress.ToList();
            };
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e) => NavigationService?.Navigate(new AddProgressPage(null));
        private void ButtonEdit_Click(object sender, RoutedEventArgs e) => NavigationService?.Navigate(new AddProgressPage((sender as Button).DataContext as Progress));
        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var items = DataGridProgress.SelectedItems.Cast<Progress>().ToList();
            if (items.Count == 0) return;
            if (MessageBox.Show($"Удалить {items.Count}?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (var db = new BD_Sinelshchikova123_LanguagePlatformEntities()) { db.Progress.RemoveRange(items); db.SaveChanges(); }
                DataGridProgress.ItemsSource = new BD_Sinelshchikova123_LanguagePlatformEntities().Progress.ToList();
            }
        }
    }
}