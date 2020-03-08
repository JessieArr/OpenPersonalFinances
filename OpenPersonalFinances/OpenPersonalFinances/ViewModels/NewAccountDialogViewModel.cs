using Avalonia.Controls;
using Newtonsoft.Json;
using OpenPersonalFinances.Models;
using OpenPersonalFinances.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenPersonalFinances.ViewModels
{
    public class NewAccountDialogViewModel : ViewModelBase
    {
        private Window _ParentWindow;
        public string NewAccountName { get; set; } = "My Account";

        public NewAccountDialogViewModel(Window parentWindow)
        {
            _ParentWindow = parentWindow;
        }

        public void NewAccountCommand()
        {
            var newAccount = new OPFAccount();
            newAccount.Name = NewAccountName;
            CurrentProjectService.ActiveProject.Accounts.Add(newAccount);
            _ParentWindow.Close();
        }
    }
}
