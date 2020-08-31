using Caliburn.Micro;
using MvvmDialogs.DialogTypeLocators;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdCaliburnApp.Helpers
{
    public class DialogLocator : IDialogTypeLocator
    {
        public Type Locate(INotifyPropertyChanged viewModel)
        {
            Type viewModelType = viewModel.GetType();

            string dialogFullName = viewModelType.FullName;
            dialogFullName = dialogFullName.Substring(0,dialogFullName.Length - "Model".Length);

            return viewModelType.Assembly.GetType(dialogFullName);

        }
        
       
       
    }

   
}
