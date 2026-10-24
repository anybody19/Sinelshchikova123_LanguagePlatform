using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace Sinelshchikova123_LanguagePlatform.Pages
{
    public partial class DiagrammPage : Page
    {
        private BD_Sinelshchikova123_LanguagePlatformEntities _context = new BD_Sinelshchikova123_LanguagePlatformEntities();

        public DiagrammPage()
        {
            InitializeComponent();

            ChartPayments.ChartAreas.Add(new ChartArea("Main"));
            var currentSeries = new Series("Сумма") { IsValueShownAsLabel = true };
            ChartPayments.Series.Add(currentSeries);

            CmbUser.ItemsSource = _context.User.ToList();
            CmbDiagram.ItemsSource = Enum.GetValues(typeof(SeriesChartType));
        }

        private void UpdateChart(object sender, SelectionChangedEventArgs e)
        {
            if (CmbUser.SelectedItem is User currentUser && CmbDiagram.SelectedItem is SeriesChartType currentType)
            {
                Series currentSeries = ChartPayments.Series.FirstOrDefault();
                currentSeries.ChartType = currentType;
                currentSeries.Points.Clear();

                var languagesList = _context.Language.ToList();
                foreach (var lang in languagesList)
                {
                    var sum = _context.Progress.ToList()
                        .Where(p => p.UserID == currentUser.ID && p.LanguageID == lang.ID)
                        .Sum(p => p.Price * p.Num);

                    currentSeries.Points.AddXY(lang.Name, sum);
                }
            }
        }

        
        private void ExportWord_Click(object sender, RoutedEventArgs e)
        {
            var allUsers = _context.User.ToList();
            var allLanguages = _context.Language.ToList();

            var application = new Word.Application();
            Word.Document document = application.Documents.Add();

            foreach (var user in allUsers)
            {
                Word.Paragraph userParagraph = document.Paragraphs.Add();
                Word.Range userRange = userParagraph.Range;
                userRange.Text = user.FIO;
                userParagraph.set_Style("Заголовок 1");
                userRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                userRange.InsertParagraphAfter();
                document.Paragraphs.Add();

                Word.Paragraph tableParagraph = document.Paragraphs.Add();
                Word.Range tableRange = tableParagraph.Range;
                Word.Table paymentsTable = document.Tables.Add(tableRange, allLanguages.Count() + 1, 2);
                paymentsTable.Borders.InsideLineStyle = paymentsTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                paymentsTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                Word.Range cellRange;
                cellRange = paymentsTable.Cell(1, 1).Range;
                cellRange.Text = "Язык";
                cellRange = paymentsTable.Cell(1, 2).Range;
                cellRange.Text = "Сумма баллов";
                paymentsTable.Rows[1].Range.Bold = 1;
                paymentsTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                for (int i = 0; i < allLanguages.Count(); i++)
                {
                    var currentLang = allLanguages[i];
                    cellRange = paymentsTable.Cell(i + 2, 1).Range;
                    cellRange.Text = currentLang.Name;
                    cellRange.Font.Size = 12;

                    cellRange = paymentsTable.Cell(i + 2, 2).Range;
                    var sum = _context.Progress.ToList()
                        .Where(p => p.UserID == user.ID && p.LanguageID == currentLang.ID)
                        .Sum(p => p.Price * p.Num);
                    cellRange.Text = sum.ToString("N2") + " баллов";
                    cellRange.Font.Size = 12;
                }
                document.Paragraphs.Add();
            }

            application.Visible = true;
            document.SaveAs2(@"D:\Export_Word.docx");
        }

        
        private void ExportExcel_Click(object sender, RoutedEventArgs e)
        {
            var application = new Excel.Application();
            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

            worksheet.Cells[1, 1] = "Пользователь";
            worksheet.Cells[1, 2] = "Язык";
            worksheet.Cells[1, 3] = "Сумма";

            var allProgress = _context.Progress.ToList();
            int row = 2;
            foreach (var p in allProgress)
            {
                var user = _context.User.FirstOrDefault(u => u.ID == p.UserID);
                var lang = _context.Language.FirstOrDefault(l => l.ID == p.LanguageID);

                worksheet.Cells[row, 1] = user?.FIO;
                worksheet.Cells[row, 2] = lang?.Name;
                worksheet.Cells[row, 3] = p.Price * p.Num;
                row++;
            }

            application.Visible = true;
            workbook.SaveAs(@"D:\Export_Excel.xlsx");
        }
    }
}