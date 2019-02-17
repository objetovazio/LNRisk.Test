using LNRisk.Test.Domain;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LNRisk.Test.Business
{
    public class MyClass
    {
        private List<Item> myData;

        public MyClass()
        {
            myData = new List<Item>();
        }

        /// <summary>
        /// Add a new Payload to this context
        /// </summary>
        /// <param name="id"></param>
        /// <param name="payload"></param>
        /// <returns>Returns true in case of success</returns>
        public bool StoreData(string id, string payload)
        {
            if (myData.Exists(x => x.Id.Equals(id))) 
            {
                return false;
            }

            var newItem = new Item
            {
                Id = id,
                Payload = payload
            };

            this.myData.Add(newItem);

            return true;
        }

        /// <summary>
        /// Remove a existing Payload to this context
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true in case of success.</returns>
        public bool RemoveData(string id)
        {
            var item = this.myData.FirstOrDefault(x => x.Id.Equals(id));

            if (item != null)
            {
                myData.Remove(item);
                return true;
            }

            return false;
        }

        public List<Item> List()
        {
            return myData;
        }

        /// <summary>
        /// Return a existing Payload
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the desired payload in case of success. Returns empty string in case of the Id doesn't exists.</returns>
        public string GetPayload(string id)
        {
            var item = this.myData.FirstOrDefault(x => x.Id.Equals(id));

            if (item != null) return item.Payload;

            return string.Empty;
        }

        /// <summary>
        /// Edit a existing Payload
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newPayload"></param>
        /// <returns>Returns true in case of success. Returns false in case of the Id doesn't exists.</returns>
        public bool EditData(string id, string newPayload)
        {
            var item = this.myData.FirstOrDefault(x => x.Id.Equals(id));

            if (item != null)
            {
                item.Payload = newPayload;
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Count the amount of dates in the payload
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the number of dates found.</returns>
        public int CountDates(string id)
        {
            var item = this.myData.FirstOrDefault(x => x.Id.Equals(id));

            int counter = 0;
            bool isValid;
            char[] splitChar = {' ', '\n'}; 
            
            string[] allContent = item.Payload.Split(splitChar);

            foreach (string content in allContent)
            {
                isValid = true;

                var dateContent = content.Split('/');
                if (dateContent.Length < 3)
                {
                    isValid = false;
                }
                else
                {
                    if (!isValidNumber(dateContent[0], 4, 0, 9999)) isValid = false; // Validate Year
                    if (!isValidNumber(dateContent[1], 2, 1, 12)) isValid = false;   // Validate  Month
                    if (!isValidNumber(dateContent[2], 2, 1, 30)) isValid = false;   // Validate Day
                }

                if (isValid) counter++;
            }


            return counter;
        }

        private bool isValidNumber(string number, int size, int start, int end)
        {
            if (number.Length < size) return false;

            int helper = 0;

            if (!int.TryParse(number, out helper)) return false;

            if (helper < start || helper > end) return false;

            return true;
        }

        /// <summary>
        /// Count the amount of each letter in the payload
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Dictionary filled with the letter as Key and the quantity as Value</returns>
        public Dictionary<char, int> CountLetters(string id) 
        {
            var item = this.myData.FirstOrDefault(x => x.Id.Equals(id));
            var dictCountedLetters = new Dictionary<char, int>();


            foreach (char charc in item.Payload)
            {
                var loweredChar = Char.ToLower(charc);

                if (dictCountedLetters.ContainsKey(loweredChar))
                {
                    dictCountedLetters[loweredChar]++;
                }
                else if (char.IsLetter(loweredChar))
                {
                    dictCountedLetters.Add(loweredChar, 1);
                }
            }

            return dictCountedLetters;
        }

        /// <summary>
        /// Find all equals payloads
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>A list filled with the Ids with the same Payload</returns>
        public List<string> SearchPayload(string payload)
        {
            return this.myData
                .Where(x => x.Payload.Equals(payload)) // Filter all items that has the same payload
                .Select(x => x.Id).ToList(); // Put all filtered id's into a list
        }

        /// <summary>
        /// Check if a word is a palindrome
        /// </summary>
        /// <param name="word"></param>
        /// <returns>True if the word is a palindrome.</returns>
        private bool IsPalindrome(string word)
        {
            int wordLenght = word.Length-1;
            var palindrome = true;
            word = word.ToLower();

            for (int i = 0; i < word.Length; i++)
            {
                if (!word[i].Equals(word[wordLenght - i]))
                {
                    palindrome = false;
                    break;
                }
            }

            return palindrome;
        }

        /// <summary>
        /// Check all words to find the biggest palindrome inside a payload
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The biggest palindrome found in the payload or a empty string in case of don't find any result.</returns>
        public string SearchBiggestPalindrome(string id)
        {
            var item = this.myData.FirstOrDefault(x => x.Id.Equals(id));

            char[] splitChar = {' ', '\n'}; 
            
            string[] allWords = item.Payload.Split(splitChar);
            string biggestPalindrome = string.Empty;


            foreach (string word in allWords)
            {
                if (IsPalindrome(word))
                {
                    if (biggestPalindrome.Length < word.Length)
                        biggestPalindrome = word;
                }
            }

            return biggestPalindrome;
        }
    }
}
