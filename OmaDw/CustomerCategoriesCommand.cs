using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

namespace OmaDw
{
    [Command]
    public class CustomerCategoriesCommand : ICommand
    {
        public ValueTask ExecuteAsync(IConsole console)
        {
            console.Output.WriteLine("Kategorioiden ylläpito");
            console.Output.WriteLine("");
            CustomerCategoriesModes[] values =
                (CustomerCategoriesModes[]) Enum.GetValues(typeof(CustomerCategoriesModes));
            bool checktap = false;

            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine($"{i}. {values[i]}");
            }

            string SelectedValue = "";
            while (!checktap)
            {
                int selectionIndex;
                bool success = int.TryParse(console.Input.ReadLine(), out selectionIndex);
                checktap = Enum.IsDefined(typeof(CustomerCategoriesModes), selectionIndex);
                if (!checktap)
                {
                    console.Output.WriteLine($"There is no selection with {selectionIndex}, try again");
                }
                else
                {
                    SelectedValue = values[selectionIndex].ToString();
                }
            }

            console.Output.WriteLine(SelectedValue + " selected");
            Enum.TryParse<CustomerCategoriesModes>(SelectedValue, out var enumValue);
            switch (enumValue)
            {
                case CustomerCategoriesModes.AddCustomersToCategories:
                    AddCustomersToCategories();
                    break;
                default:
                    console.Output.WriteLine($"Ei määritetty vielä");
                    break;
            }


            var test = console.Input.ReadLine();

            return default;
        }

        private void AddCustomersToCategories()
        {
            var customers = GetCustomersFromDb();
            var existingCustomerCategoryLinks = ReadExistingLinks();
            var existingCategories = existingCustomerCategoryLinks.Select(x => x.CustomerCategory).ToList();
            Console.WriteLine($"Löydettiin {customers.Count} asiakasta");
            Console.WriteLine($"Löydettiin {existingCustomerCategoryLinks.Count} riviä");

            foreach (var customer in customers.Where(x =>
                !existingCustomerCategoryLinks.Select(y => y.CustomerName).Contains(x)))
            {
                Console.WriteLine($" {customer}");
                //var input = Console.ReadLine();
                var input = ConsoleAutocomplete(existingCategories);
                existingCategories.Add(input);
                existingCategories = existingCategories.Distinct().ToList();
                var newLink = new CustomerCategoryLink
                {
                    CustomerName = customer,
                    CustomerCategory = input
                };
                SaveCustomerCategoryLinks(new List<CustomerCategoryLink>() {newLink});
            }


            SaveCustomerCategoryLinks(new List<CustomerCategoryLink>());
        }

        private static void ClearCurrentLine()
        {
            var currentLine = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLine);
        }

        private string ConsoleAutocomplete(IEnumerable<string> data)
        {
            var builder = new StringBuilder();
            var input = Console.ReadKey(intercept: true);
            var returnValue = "";

            while (input.Key != ConsoleKey.Enter)
            {
                var currentInput = builder.ToString();
                if (input.Key == ConsoleKey.Tab)
                {
                    var match = data.FirstOrDefault(item =>
                        item != currentInput && item.StartsWith(currentInput, true, CultureInfo.InvariantCulture));
                    if (string.IsNullOrEmpty(match))
                    {
                        input = Console.ReadKey(intercept: true);
                        continue;
                    }

                    ClearCurrentLine();
                    builder.Clear();
                    Console.Write(match);
                    builder.Append(match);
                }
                else
                {
                    if (input.Key == ConsoleKey.Backspace && currentInput.Length > 0)
                    {
                        builder.Remove(builder.Length - 1, 1);
                        ClearCurrentLine();

                        currentInput = currentInput.Remove(currentInput.Length - 1);
                        Console.Write(currentInput);
                    }
                    else
                    {
                        var key = input.KeyChar;
                        builder.Append(key);
                        Console.Write(key);
                    }
                }

                input = Console.ReadKey(intercept: true);
            }
            return builder.ToString();
        }

        private List<string> GetCustomersFromDb()
        {
            var tmpList = new List<string>();
            using (SqlConnection myConnection =
                new SqlConnection("server=localhost;Database=Prime;User Id=Sa;Password=salasana12!"))
            {
                string oString = "Select distinct saajamaksaja from dbo.transactionstmp";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        tmpList.Add(oReader[0].ToString());
                    }

                    myConnection.Close();
                }
            }

            return tmpList;
        }

        private void SaveCustomerCategoryLinks(List<CustomerCategoryLink> customerCategoryLinks)
        {
            var csv = new StringBuilder();

            var existingCustomerCategoryLinks = ReadExistingLinks();
            var newCustomerCategoryLinks = customerCategoryLinks.Concat(existingCustomerCategoryLinks.Where(x =>
                !customerCategoryLinks.Select(x => x.CustomerName).Contains(x.CustomerName)));
            foreach (var customerCategoryLink in newCustomerCategoryLinks.OrderBy(x => x.CustomerName))
            {
                var newLine = $"{customerCategoryLink.CustomerCategory};{customerCategoryLink.CustomerName}";
                csv.AppendLine(newLine);
            }

            File.WriteAllText("./customerCategoryLinks.csv", csv.ToString());
        }

        private List<CustomerCategoryLink> ReadExistingLinks()
        {
            List<CustomerCategoryLink> listA = new List<CustomerCategoryLink>();
            if (!File.Exists("./customerCategoryLinks.csv")) return listA;
            using var reader = new StreamReader(@"./customerCategoryLinks.csv");
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                listA.Add(new CustomerCategoryLink
                {
                    CustomerCategory = values[0],
                    CustomerName = values[1],
                });
            }

            return listA;
        }
    }


    internal class CustomerCategoryLink
    {
        public string CustomerName { get; set; }
        public string CustomerCategory { get; set; }
    }

    public enum CustomerCategoriesModes
    {
        AddCustomersToCategories
    }
}