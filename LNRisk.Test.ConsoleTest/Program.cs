using LNRisk.Test.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNRisk.Test.ConsoleTest
{
    class Program
    {
        public static void Main(string[] args)
        {
            MyClass myClass = new MyClass();

            // Creating First Data
            // Testing the StoreData()
            myClass.StoreData("1", "arara");
            myClass.StoreData("2", "Anna");
            myClass.StoreData("3", "Civic");
            myClass.StoreData("5", "Kayak");
            myClass.StoreData("6", "Level");
            myClass.StoreData("7", "Madam");
            myClass.StoreData("8", "On 2014/03/22 I was testing the hello word. And now on 2019/02/15, I'm testing this.");
            myClass.StoreData("9", "Level");
            myClass.StoreData("10", "Civic Noon Madam Kayak tattarrattat");

            // Testing RemoveData()
            if (myClass.RemoveData("1"))
            {
                Console.WriteLine("Data removed.");
            }

            // Testing GetPayload()
            Console.WriteLine("Payload 2: " + myClass.GetPayload("2"));

            // Testing EditPaylod()
            if (myClass.EditData("2", "NewPayload is Loaded"))
            {
                Console.WriteLine("Edited Payload 2: " + myClass.GetPayload("2"));
            }

            // Testing CountDates()
            Console.WriteLine("Count Dates from Id 8: " + myClass.CountDates("8")); //TODO: Check this again

            // Testing CountLetters()
            Console.WriteLine("Count Letters from Id 8:"); //TODO: Check this again
            var dict = myClass.CountLetters("8");

            foreach (var dictKey in dict.Keys)
            {
                var value = dict[dictKey];
                Console.Write(string.Format("|{0} = {1}| ", dictKey.ToString(), value));    
            }
            Console.WriteLine("");


            //Testing SearchPayload()
            Console.Write("Search Payload 'Level': ");
            List<string> ids = myClass.SearchPayload("Level");
            foreach (var id in ids)
            {
                Console.Write("| " + id + " | ");
            }
            Console.WriteLine("");

            // Testing SearchBiggestPalindrome()
            Console.WriteLine("Biggest Palindrome found 10: " + myClass.SearchBiggestPalindrome("10"));

            Console.ReadKey();
        }
    }
}
