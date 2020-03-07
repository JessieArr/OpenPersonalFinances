using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenPersonalFinances.Views
{
    public class ProjectMainView : UserControl
    {
        public ProjectMainView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
