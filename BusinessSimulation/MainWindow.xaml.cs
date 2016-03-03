using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static BusinessSimulation.Engine;


namespace BusinessSimulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            Engine.Initialize(20);
            Engine.Location Location = new Engine.Location(5000)
            {
                Naam = "Barneveld",
                Description = "mooie toko",
                MaximalAmountOfItemsInStorage = 2000,
                TotalMonthlyCost = 500,
                Worth = 200000
            };


            Engine.Business Business = new Engine.Business(Location, 30000);
            Engine.Businesses.Add(Business);


        }

        private void AdvanceCycle(object sender, RoutedEventArgs e)
        {
            Engine.Cycles.AdvanceCycle();

        }

        private void ViewReports(object sender, RoutedEventArgs e)
        {
            foreach (Business Business in Businesses)
            {
                foreach (Business.Report Report in Business.History.Reports)
                {
                    foreach (KeyValuePair<string, decimal> Data in Report.Data)
                    {
                        MessageBox.Show(Data.Key + " = " + Data.Value);
                    }
                }

            }
        }
    }
}
