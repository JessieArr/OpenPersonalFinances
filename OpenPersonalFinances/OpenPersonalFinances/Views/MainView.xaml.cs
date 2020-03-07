using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using OpenPersonalFinances.ViewModels;

namespace OpenPersonalFinances.Views
{
    public class MainView : UserControl
    {
        public MainView()
        {
            this.InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
