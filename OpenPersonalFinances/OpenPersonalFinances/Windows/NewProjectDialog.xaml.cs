using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using OpenPersonalFinances.ViewModels;
using System;

namespace OpenPersonalFinances.Windows
{
    public class NewProjectDialog : Window
    {
        public NewProjectDialog()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.DataContext = new NewProjectDialogViewModel("", this);
            throw new Exception("Must provide a path to New Project Dialog.");
        }

        public NewProjectDialog(string folderPath)
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.DataContext = new NewProjectDialogViewModel(folderPath, this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
