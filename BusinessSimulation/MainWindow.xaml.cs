﻿using System;
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
            Location Location = new Engine.Location(5000)
            {
                Naam = "Barneveld",
                Description = "mooie toko",
                MaximalAmountOfItemsInStorage = 2000,
                TotalMonthlyCost = 500,
                Worth = 200000
            };


            Business Business = new Engine.Business(Location, 30000);

            Businesses.Add(Business);

            RegisterPlugins(Business);




        }

        private void AdvanceCycle(object sender, RoutedEventArgs e)
        {
            Engine.Cycles.AdvanceCycle();

        }
        private void ViewReports(object sender, RoutedEventArgs e)
        {
            foreach (Business Business in Businesses)
            {

                for (int Report = 0; Report < Business.History.Reports.Count - 1; Report++)
                {
                   Report currReport = Business.History.Reports.ElementAt(Report);

                    foreach (KeyValuePair<string, decimal> Data in currReport.Data)
                    {
                        MessageBox.Show(Report+1 + " : " + Data.Key + " = " + Data.Value);
                    }

                }

                foreach (Report Report in Business.History.Reports)
                {
                }

            }
        }


        private void RegisterPlugins(Business Business)
        {

            Engine.Business.BusinessFinances.PerformCalculation  DoTax = TaxCost;
            int Phase = (int)Engine.Business.BusinessFinances.Phases.CalculateMonthlyCost;
            

            Business.Finances.RegisterPlugin(Phase,DoTax);




        }

        private void TaxCost(ref decimal MonthlyCost)
        {
            MonthlyCost =MonthlyCost* 0.9M;


        }

    }


}


