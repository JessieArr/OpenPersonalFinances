using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using OpenPersonalFinances.Models;
using OpenPersonalFinances.ViewModels;

namespace OpenPersonalFinances.Windows
{
    public class ImportCSVDialog : Window
    {
        public ImportCSVDialog()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        public ImportCSVDialog(CSVFile file)
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.DataContext = new ImportCSVDialogViewModel(this, file);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
