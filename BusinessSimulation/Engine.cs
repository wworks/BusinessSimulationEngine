using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSimulation
{
    public static class Engine
    {
       static List<Business> Businesses = new List<Business>();

        public class Business {
            public class History
            {



            }

            public class Finances
            {
                public int Budget = 0;
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

                    return 0.0M;



                }


            }

            public class Quantity
            {
                public Location currentLocation { get; set; }





                public class Location
                {


                    public String Naam { get; set; }
                    public String Description { get; set; }
                    public int Scope { get; set; }
                    public Decimal TotalMonthlyCost { get; set; }
                    public int MaximalAmountOfItemsInStorage { get; set; }
                    public Decimal Worth { get; set; }


                    class customerCollection
                    {
                        private Location ParentLocation;
                        List<Customer> Customers = new List<Customer>();

                        customerCollection(Location Location)
                        {
                            ParentLocation = Location;

                        }

                        void MakeNew()
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

                        public class CustomerStatistics
                        {
                            customerCollection Customers;

                            CustomerStatistics(customerCollection CustomerCollection)
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

        static public int Turns {
            get { return Cycles.TotalAmountOfCycles; }
            set { Cycles.TotalAmountOfCycles = value; }
        }


        public static void Initialize()
        {
            



        }

         static class Cycles
        {

            public static int TotalAmountOfCycles = 0;
            public static int CurrentCycle = 0;
            static void AdvanceCycle()
            {
                foreach (Business Business in Businesses)
                {
                    
                }
                CurrentCycle++;

            }




        }


    }
}
