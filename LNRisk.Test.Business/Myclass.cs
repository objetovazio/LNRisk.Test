using LNRisk.Test.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LNRisk.Test.Business
{
    public class Myclass
    {
        private List<Item> myData;

        public Myclass()
        {
            myData = new List<Item>();
        }

        public void StoreData(string id, string payload)
        {
            var newItem = new Item
            {
                Id = id,
                Payload = payload
            };

            this.myData.Add(newItem);
        }

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

        public string GetPayload(string id)
        {
            var item = this.myData.FirstOrDefault(x => x.Id.Equals(id));

            if (item != null) return item.Payload;

            return string.Empty;
        }

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

        public int CountDates(string id)
        {
            var item = this.myData.FirstOrDefault(x => x.Id.Equals(id));

            Regex dateRegex = new Regex("\\d{4}/\\d{2}/\\d{2}");

            return dateRegex.Matches(item.Payload).Count;
        }

        public Dictionary<char, int> CountLetters(string id)
        {
            var item = this.myData.FirstOrDefault(x => x.Id.Equals(id));
            var dictCountedLetters = new Dictionary<char, int>();

            foreach (char charc in item.Payload)
            {
                if (dictCountedLetters.ContainsKey(charc))
                {
                    dictCountedLetters[charc]++;
                }
                else if (char.IsLetter(charc))
                {
                    dictCountedLetters.Add(charc, 1);
                }
            }

            return dictCountedLetters;
        }

        public List<string> SearchPayload(string payload)
        {
            return this.myData
                .Where(x => x.Payload.Equals(payload)) // Filter all items that has the same payload
                .Select(x => x.Id).ToList(); // Put all id's filteres into a list
        }

        public string SearchBiggestPalindrome(string id)
        {
            var item = this.myData.FirstOrDefault(x => x.Id.Equals(id));

            string[] allWords = item.Payload.Split(' ');
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
    }
}
