using OpenPersonalFinances.Models;
using OpenPersonalFinances.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenPersonalFinances.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        ViewModelBase content;
        public MainViewModel()
        {
            Content = new WelcomeViewModel();
            CurrentProjectService.ActiveProjectChanged += ActiveProjectChangedHandler;
        }

        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        private void ActiveProjectChangedHandler(object sender, OPFProject e)
        {
            Content = new ProjectMainViewModel();
        }
    }
}
