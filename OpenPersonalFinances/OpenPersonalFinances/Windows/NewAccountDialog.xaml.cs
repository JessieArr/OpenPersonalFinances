using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using OpenPersonalFinances.ViewModels;

namespace OpenPersonalFinances.Windows
{
    public class NewAccountDialog : Window
    {
        public NewAccountDialog()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.DataContext = new NewAccountDialogViewModel(this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
