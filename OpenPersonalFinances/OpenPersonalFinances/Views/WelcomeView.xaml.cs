using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenPersonalFinances.Views
{
    public class WelcomeView : UserControl
    {
        public WelcomeView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
