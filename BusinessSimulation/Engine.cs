using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSimulation
{
    public static class Engine
    {
        static public List<Business> Businesses = new List<Business>();
        static public List<Product> Products = new List<Product>();
        static public List<Supplier> Suppliers = new List<Supplier>();
        static public List<Machine> Machines = new List<Machine>();
        static public List<Employee> Employees = new List<Employee>();


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
            public BusinessPersonnel Personnel;

            public Location currentLocation { get; set; }


            public Business(Location Location, int StartingBudget)
            {
                this.currentLocation = Location;
                this.Finances = new BusinessFinances(this);
                this.Finances.Budget = StartingBudget;
                this.Personnel = new BusinessPersonnel(this);

            }



            public class BusinessHistory
            {
                public List<Report> Reports = new List<Report>();
            }


            public class BusinessFinances
            {
                Business ParentBusiness;
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
                    else
                    {
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



                public delegate void PerformCalculation(ref decimal Argument);
                private List<Tuple<int, PerformCalculation>> Plugins = new List<Tuple<int, PerformCalculation>>();
                public enum Phases
                {
                    CalculateMonthlyCost = 0
                };
                public bool RegisterPlugin(int Phase, PerformCalculation Action)
                {
                    Plugins.Add(new Tuple<int, PerformCalculation>(Phase, Action));
                    return true;

                }


                public Decimal CalculateProfit()
                {
                    decimal MonthlyCost = 0;
                    MonthlyCost = ParentBusiness.currentLocation.TotalMonthlyCost;


                    //Phase CalculateMonthlyCost
                    foreach (Tuple<int, PerformCalculation> Action in Plugins.Where<Tuple<int, PerformCalculation>>(x => x.Item1 == (int)Phases.CalculateMonthlyCost))
                    {
                        Action.Item2(ref MonthlyCost);


                    }

                    Report Report = new Report();
                    Report.Data.Add("Profit", MonthlyCost);

                    ParentBusiness.History.Reports.Add(Report);
                    return 90M;



                }



            }

            public class ProductInventory
            {
                public int MaximalAmountOfItemsInStorage { get; set; }
                public List<Product> BoughtProducts = new List<Product>();


                public int getTotalAmountOfProductsInInventory()
                {
                    int TotalAmountOfProductsInInventory = 0;
                    foreach (Product Product in BoughtProducts)
                    {
                        throw new NotImplementedException();
                    }
                    return TotalAmountOfProductsInInventory;

                }

                public decimal getInventoryWorth()
                {
                    decimal InventoryWorth = 0;

                    foreach (Product Product in BoughtProducts)
                    {
                        throw new NotImplementedException();

                    }
                    return InventoryWorth;

                }
                public void Buy(Product Product)
                {
                    BoughtProducts.Add(Product);

                    throw new NotImplementedException();

                }

            }
            public class MachineInventory
            {
                public List<Machine> BoughtMachines = new List<Machine>();


            }
            public class Promotion { }

            public class BusinessPersonnel


            {
                public event EventHandler EmployeeAdded;




                Business ParentBusiness;

                public BusinessPersonnel(Business Business)

                {
                    ParentBusiness = Business;

                }

                public List<Employee> getEmployees()
                {

                    List<Employee> EmployedEmployees = new List<Employee>();
                    foreach (Employee Employee in Employees)
                    {
                        if (Employee.Employer == ParentBusiness)
                        {
                            EmployedEmployees.Add(Employee);
                        }
                    }
                    return EmployedEmployees;

                }


                public void Employ(Employee Employee)
                {
                    Employee.Employer = ParentBusiness;
                    EmployeeAdded(ParentBusiness, new EventArgs());




                }
                public decimal getTotalPersonnelCosts()
                {
                    decimal TotalPersonnelCosts = 0;
                    foreach (Employee Employee in getEmployees())
                    {
                        TotalPersonnelCosts += Employee.SalaryPerCycle;
                    }
                    return TotalPersonnelCosts;
                }
                public int getTotalProductsPerCycle()
                {
                    int TotalProductsPerCycle = 0;
                    foreach (Employee Employee in getEmployees())
                    {
                        TotalProductsPerCycle += Employee.ProductsPerCycle;

                    }
                    return TotalProductsPerCycle;


                }
            }
        }

        public class Employee
        {
            public string Name = "Nameless";
            public int ProductsPerCycle = 0;
            public decimal SalaryPerCycle;
            public Business Employer;

            public override string ToString()
            {
                return Name;
            }
        }
        public class Supplier
        {
            public string Name;
            public List<Product> getProducts()
            {
                return (List<Product>)Products.Where<Product>(product => product.Supplier == this);


            }


        }
        public class Machine
        {
            public string Name;
            public decimal currentWorth;
            public decimal NewWorth;
            public int OutputCapacity;




        }
        public class Product
        {
            public string Name;
            public Supplier Supplier;
            public decimal SellingPrice = 0.0M;
            public decimal PurchasingPrice = 0.0M;
            public int packageSize;

            /// <summary>
            /// These properties will be matched with the whishes of the customers.
            /// </summary>
            public List<String> Properties = new List<string>();

            public int QuantityInStore = -1;



        }


        public class Report
        {
            public int Cycle;
            public Report()
            {
                Cycle = Cycles.CurrentCycle;

            }
            public Dictionary<String, Decimal> Data = new Dictionary<string, decimal>();

        }

        public class Location
        {
            public String Naam { get; set; }
            public String Description { get; set; }

            public Decimal TotalMonthlyCost { get; set; }
            public Decimal Worth { get; set; }

            public Tuple<int, int> mapLocation = new Tuple<int, int>(0, 0);

            public int getScope()
            {
                //do positional stuff
                return 0;

            }


        }


        class Market
        {
            public int Size;
            List<Customer> Customers = new List<Customer>();

            public Market(int Size)
            {
                this.Size = Size;
            }

            public void MakeNew()
            {
                for (int Customer = 0; Customer < Size; Customer++)
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

            public class MarketStatistics
            {
                Market Customers;

                MarketStatistics(Market CustomerCollection)
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
                    return TotalAmountOfCustomersWithThisWish * 100 / Customers.Size;
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
                    return TotalAmountOfCustomersWithTheseWishes * 100 / Customers.Size;

                }
            }
        }

    }

}

