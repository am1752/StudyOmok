using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SecondCliburnApp.ViewModels
{
    class FirstChildViewModel:Screen
    {
        public void FirstClicked()
        {
            MessageBox.Show("Hello World!");
        }
    }
}
