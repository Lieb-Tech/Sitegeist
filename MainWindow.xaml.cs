using Sitegeist.Scripting.Engine;
using Sitegeist.Utils;
using System.Linq;
using System.Windows;

namespace Sitegeist
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {            
            InitializeComponent();
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            var engine = new Scripting.Engine.ScriptEngine();
            var results = engine.RunScripts();
            var data = results.StepResults.Select(z => new
            {                
                WasOk = (!z.ActionResults.Any(a => a.ActionException != null) &&
                            !z.ExpectResults.Any(a => a.EpectException != null) &&
                            !z.ExpectResults.Any(a => !a.MetExpectation) ),
                Summary = getSummary(z),
                Actions = z.ActionResults.Count,
                Expects = z.ExpectResults.Count,
                ActionErrors = z.ActionResults.Count(a => a.ActionException != null),
                ExpectErrors = z.ExpectResults.Count(a => a.EpectException != null),
                MetExpects = !z.ExpectResults.Any(a => !a.MetExpectation),
                z.ActionResults,
                z.ExpectResults,
                HasAction = z.ActionResults.Any() ? Visibility.Visible : Visibility.Collapsed,
                HasExcept = z.ExpectResults.Any() ? Visibility.Visible : Visibility.Collapsed,
                StartingURL = z.StartingUrl,
                EndingURL = z.EndingUrl,
            }).Where(z => z.Actions + z.Expects > 0).ToList();

            txtURL.Text = results.StartingUrl;

            dgResults.ItemsSource = data;
        }
        string getSummary(StepResult result)
        {
            if (result.ActionResults.Count == 1 && result.ExpectResults.Count == 0)
                return result.ActionResults[0].ActionType;
            else if (result.ExpectResults.Count == 1 && result.ActionResults.Count == 0)
                return result.ExpectResults[0].ExpectType;
            else if (result.ActionResults.Count == 0)
                return string.Join(",", result.ExpectResults.GroupBy(z => z.ExpectType).Select(z => z.Key));
            else if (result.ExpectResults.Count == 0)
                return string.Join(",", result.ExpectResults.GroupBy(z => z.ExpectType).Select(z => z.Key));
            else
                return
                    string.Join(",", result.ActionResults.GroupBy(z => z.ActionType).Select(z => z.Key))
                    + ","
                    + string.Join(",", result.ExpectResults.GroupBy(z => z.ExpectType).Select(z => z.Key));
        }
    }
}
