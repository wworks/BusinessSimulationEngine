﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSimulation
{
    public static class Engine
    {
        static public List<Business> Businesses = new List<Business>();
        static public int Turns
        {
            get { return Cycles.TotalAmountOfCycles; }
            set { Cycles.TotalAmountOfCycles = value; }
        }


        public static void Initialize(int AmountOfCycles)
        {
            Cycles.TotalAmountOfCycles = AmountOfCycles;


        }

        public static class Cycles
        {

            public static int TotalAmountOfCycles = 0;
            public static int CurrentCycle = 0;
            public static void AdvanceCycle()
            {
                foreach (Business Business in Businesses)
                {
                    Business.Finances.CalculateProfit();
                }
                CurrentCycle++;
            }
        }

        public class Business
        {

            public BusinessHistory History = new BusinessHistory();
            public BusinessFinances Finances;
            public BusinessMarket Market = new BusinessMarket();


            public Business(Location Location, int StartingBudget)
            {
                this.Market.currentLocation = Location;
                this.Finances = new BusinessFinances(this);
                this.Finances.Budget = StartingBudget;

            }



            public class BusinessHistory
            {
                public List<Report> Reports = new List<Report>();


            }

            public class BusinessFinances
            {
                private Business ParentBusiness;
                public int Budget = 0;

                public BusinessFinances(Business Business)
                {
                    ParentBusiness = Business;

                }
                public bool checkBudget()
                {
                    if (Budget >= 0)
                    {
                        return true;
                    }
                    else {
                        return false;

                    }
                }
                public bool IsBuyable(decimal Price)
                {
                    if (Budget - Price >= 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                public Decimal CalculateProfit()
                {

                    Report Report = new Report();
                    Report.Profit = 90;
                    Report.Data.Add("Profit", 90);

                    ParentBusiness.History.Reports.Add(Report);
                    return 90M;



                }


            }

            public class BusinessMarket
            {
                public Location currentLocation { get; set; }

            }

            public class Report
            {
                public Decimal Profit;
                public Dictionary<String, Decimal> Data = new Dictionary<string, decimal>();

            }

        }
        public class Location
        {
            public String Naam { get; set; }
            public String Description { get; set; }
            public int Scope { get; set; }
            public Decimal TotalMonthlyCost { get; set; }
            public int MaximalAmountOfItemsInStorage { get; set; }
            public Decimal Worth { get; set; }
            private Market LocationMarket;


            public Location(int Scope)
            {
                this.Scope = Scope;
                LocationMarket = new Market(this);
                LocationMarket.MakeNew();
            }



            class Market
            {
                private Location ParentLocation;
                List<Customer> Customers = new List<Customer>();

                public Market(Location Location)
                {
                    ParentLocation = Location;

                }

                public void MakeNew()
                {
                    for (int Customer = 0; Customer < ParentLocation.Scope; Customer++)
                    {
                        //generate random customer
                        Customer NewRandomCustomer = new Customer()
                        {
                            Age = 15,
                            Income = 15000,
                            PurchasingPower = 5
                        };
                        NewRandomCustomer.Wishes.Add("Cheap");

                        Customers.Add(NewRandomCustomer);
                    }


                }
                public int CalculatePurchasingPower()
                {
                    int TotalPurchasingPower = 0;
                    foreach (Customer Customer in Customers)
                    {
                        TotalPurchasingPower += Customer.PurchasingPower;
                    }
                    return TotalPurchasingPower;


                }

                class Customer
                {
                    public decimal Income { get; set; }
                    public int Age { get; set; }
                    public List<String> Wishes = new List<string>();
                    public int PurchasingPower { get; set; }




                }

                public class RenameStatistics
                {
                    Market Customers;

                    RenameStatistics(Market CustomerCollection)
                    {
                        this.Customers = CustomerCollection;
                    }

                    int PercentageWithWish(String Wish)
                    {
                        int TotalAmountOfCustomersWithThisWish = 0;

                        foreach (Customer Customer in Customers.Customers)
                        {
                            if (Customer.Wishes.Contains(Wish)) { }
                            {
                                TotalAmountOfCustomersWithThisWish++;
                            }
                        }
                        return TotalAmountOfCustomersWithThisWish * 100 / Customers.ParentLocation.Scope;
                    }


                    int PercentageWithWishes(List<String> WishList)
                    {
                        int TotalAmountOfCustomersWithTheseWishes = 0;
                        foreach (Customer Customer in Customers.Customers)
                        {
                            bool CustomerMatches = false;
                            foreach (String Wish in WishList)
                            {
                                if (Customer.Wishes.Contains(Wish))
                                {
                                    CustomerMatches = true;
                                    break;
                                }
                            }
                            if (CustomerMatches)
                            {
                                TotalAmountOfCustomersWithTheseWishes++;
                            }
                        }
                        return TotalAmountOfCustomersWithTheseWishes * 100 / Customers.ParentLocation.Scope;

                    }
                }
            }
        }


    }
}
