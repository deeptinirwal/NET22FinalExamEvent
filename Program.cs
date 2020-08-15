using System;
using System.Collections;
using System.Collections.Generic;



namespace NET22FinalExamEvent
{
    class Program
    {
        public enum Meals
        {
            
            starter=0,
            main,
            desert,
            done
        }


        static void Main(string[] args)
        {
            Queue<Customer> customer = GetCustomerInfo();
            var table = new Table();
            var cusMeal = new ChangeMealOfPerson();
            foreach (Customer cus in customer)
            {
                table.TableOpenEvent += cus.Handletable;
               
                cusMeal.ChangeMealEvent += cus.HandleChangeMeal;
               
                table.TableOpen(cus.FirstName, cus.LastName);
                
                int mealCount = Enum.GetNames(typeof(Meals)).Length;
                for (int i = 1; i < mealCount; i++)
                {
                    cusMeal.ChangeMeal(cus.FirstName, cus.LastName, ((Meals)i));
                }
                table.TableOpenEvent -= cus.Handletable;
                cusMeal.ChangeMealEvent -= cus.HandleChangeMeal;
            }
            Console.WriteLine("Nobody wants to eat any more!");
            Console.ReadLine();

        }

        private static Queue<Customer> GetCustomerInfo()
        {
            Queue<Customer> customer = new Queue<Customer>();
            Customer cust = new Customer();
            
            cust.FirstName = "Maria";
            cust.LastName = "Lopez";
            customer.Enqueue(cust);

            cust = new Customer();
            cust.FirstName = "Nick";
            cust.LastName = "Jonas";
            customer.Enqueue(cust);

            cust = new Customer();
            cust.FirstName = "Sara";
            cust.LastName = "Mill";
            customer.Enqueue(cust);

            cust = new Customer();
            cust.FirstName = "Merry";
            cust.LastName = "Will";
            customer.Enqueue(cust);
                                  

            return customer;
        }

        class Customer
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Meals Meal { get; set; }

            public void HandleChangeMeal(object sender, ChangeMealEventArgs e)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName}is enjoying {e.Meal}");
            }

            public void Handletable(object sender, TableOpenEventArgs e)
            {
                Console.WriteLine("Table is avaibale to be joined!");
                Console.WriteLine($"{e.FirstName} {e.LastName} now has table.");
            }


        }
        public class ChangeMealOfPerson
        {
            public event EventHandler<ChangeMealEventArgs> ChangeMealEvent;
            public void ChangeMeal(string fname, string lname, Meals m)
            {
                               if (ChangeMealEvent != null)
                {
                    ChangeMealEvent(this, new ChangeMealEventArgs(fname, lname, m));
                }
            }

        }
        public class Table
        {
            public event EventHandler<TableOpenEventArgs> TableOpenEvent;
            public void TableOpen(string fname, string lname)
            {
               
                if (TableOpenEvent != null)
                {
                    TableOpenEvent(this, new TableOpenEventArgs(fname, lname));
                }
            }
        }

       
        public class TableOpenEventArgs : EventArgs
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public TableOpenEventArgs(string fname, string lname)
            {
                this.FirstName = fname;
                this.LastName = lname;
            }
        }

                public class ChangeMealEventArgs : EventArgs
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Meals Meal { get; set; }
            public ChangeMealEventArgs(string fname, string lname, Meals meal)
            {
                this.Meal = meal;
                this.FirstName = fname;
                this.LastName = lname;

            }
        }
    }
}