using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using BusinessLogic;


namespace BikeShopApp
{
    /// <summary>
    /// TestPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TestPage : Page
    {
        public TestPage()
        {
            InitializeComponent();

            InitListBox();

        }

        private void InitListBox()
        {
            List<Car> lists = new List<Car>();
            for (int i = 0; i < 10; i++)
            {
                lists.Add(new Car
                {
                    Speed = i * 30
                });
            }
           this.DataContext = lists;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Human h = new Human();
            h.Name = "Nick";
            h.HasDrivingLicense = true;

            Car car2 = new Car();
            car2.Speed = 100.5;
            car2.Color = Colors.Blue;
            car2.Driver = h;

        }
    }

 }

