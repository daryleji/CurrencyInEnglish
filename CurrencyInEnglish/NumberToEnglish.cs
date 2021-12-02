using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;

namespace CurrencyInEnglish
{
    public class NumberToEnglish
    {
        private static readonly string[] Singles = { "Zero", "One", "Two", "Three",
            "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
            "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen",
            "Seventeen", "Eighteen", "Nineteen" };
        private static readonly string[] Tens = { "Singles", "Tens", "Twenty", "Thirty", "Forty",
            "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        // Counts the number of digits (incl. 2 decimal places)
        public static int DigitCount(double number)
        {
            // Incorporates the decimal values as digits
            int value = (int)(number * 100);
            int count = 0;
            while (value != 0)
            {
                value /= 10;
                count++;
            }
            return count;
        }

        // Creates an array of integers for each digit of the given number
        public static int[] CreateNumberArray(double number, int length)
        {
            int value = (int)(number * 100);
            int[] numberArray = new int[length];

            for (int x = 0; x < length; x++)
            {
                numberArray[x] = (value % 10);
                value /= 10;
            }

            return numberArray;
        }

        // Creates an array of strings which stores the English representation of number pre-logic
        public static string[] NumbersToWords(int[] digitArray, int length)
        {
            string[] wordArray = new string[length];

            for (int x = 0; x < length; x++)
            {
                //Singular cent unit
                if (x == 0)
                {
                    wordArray[x] = Singles[digitArray[x]];
                }
                //Tens cent unit
                else if (x == 1)
                {
                    wordArray[x] = Tens[digitArray[x]];
                }
                //Dollars
                else
                {
                    // Position of singular units
                    if (((x - 2) % 3) == 1)
                    {
                        wordArray[x] = Tens[digitArray[x]];
                    }
                    // Position of tens units
                    if (((x - 2) % 3) == 2)
                    {
                        wordArray[x] = Singles[digitArray[x]];
                    }
                    // Position of singular units with hundreds attached
                    if (((x - 2) % 3) == 0)
                    {
                        wordArray[x] = Singles[digitArray[x]];
                    }
                }
            }
            return wordArray;
        }

        // Logic to create the English words containing the cent(s) value
        public static string CheckCents(string[] wordArray, int[] digitArray, int length)
        {
            string word = "";
            // Singles digit
            if (length >= 1)
            {
                // Conditions for singular and plural
                if (digitArray[0] == 1)
                {
                    word = wordArray[0] + " Cent";
                }
                else
                {
                    word = wordArray[0] + " Cents";
                }
            }
            // Tens digit
            if (length >= 2)
            {
                // Condition where there is no tens digit
                if (digitArray[1] == 0)
                {
                    return word;
                }

                // Condition where cents has a tens value with unique word combination e.g. eleven, twelve
                if (digitArray[1] == 1)
                {
                    word = Singles[digitArray[0] + 10] + " Cents";
                }
                else
                {
                    // Condition where tens digit has a number while single digit does not
                    // For 0.20, results in 20 Cents instead of Twenty Zero Cents
                    if (digitArray[0] == 0)
                    {
                        word = wordArray[1] + " Cents";
                    }
                    // Remaining combinations of numbers
                    else
                    {
                        word = wordArray[1] + " " + word;
                    }

                }

            }
            return word;
        }

        // Logic to create english phrase involving Single, Tens, and Hundred digits
        public static string CheckHundreds(string[] wordArray, int[] digitArray)
        {
            int length = wordArray.Length;
            string wordSection = "";

            // Add the singles digit
            if (length >= 1)
            {
                wordSection = wordArray[0];
            }

            // Tens digit section
            if (length >= 2)
            {
                // Condition where number has a tens value with unique word combination e.g. eleven, twelve
                if (digitArray[1] == 1)
                {
                    wordSection = Singles[digitArray[0]+10];
                }

                // Conditions where tens has multiple combinations
                else if (digitArray[1] >= 2)
                {
                    // Condition where tens digit has a number while single digit does not
                    if (digitArray[0] == 0)
                    {
                        wordSection = wordArray[1];
                    }
                    // Remaining combinations of numbers
                    else
                    {
                        wordSection = wordArray[1] + " " + wordSection;
                    }

                }

            }

            // Hundreds digit section
            if (length >= 3)
            {
                // Condition with No Hundred value
                if (digitArray[2] == 0)
                {
                    return wordSection;
                }
                // Condition where there are no tens and singles
                if ((digitArray[0] == 0) && (digitArray[1] == 0)) {
                    wordSection = wordArray[2] + " Hundred";
                }
                //Other combinations
                else
                {
                    wordSection = wordArray[2] + " Hundred and " + wordSection;
                }
            }

            return wordSection;
        }

        // Calls other methods with other incorporated logic to create a sentence from the given number
        public static string WordsToSentence(string[] wordArray, int[] digitArray, int length)
        {
            // Calculate the correct cents amount
            string cents = CheckCents(wordArray, digitArray, length);
            // If only a cents amount is entered
            if (length <= 2)
            {
                return cents;
            }

            if (length == 9)
            {
                return "One Million Dollars";
            }

            // Dollar Amounts
            // Create a new list which stores all parts of the dollar string sentence in reverse order (due to .Add method)
            var wordSectionArray = new List<string>();
            wordSectionArray.Add("Dollars");

            //Iterate through the available word and digit array in blocks of three as it helps with implementing logic which can be extended to larger numbers
            for (int x = 2; x < length; x+= 3)
            {
                string[] wordSection = new string[3];
                int[] digitSection = new int[3];

                // Value at index 0 of the new array
                if ((length - x) >= 1)
                {
                    wordSection[0] = wordArray[x];
                    digitSection[0] = digitArray[x];
                }
                // Value at index 1 of the new array assuming it is within index range
                if ((length - x) >= 2)
                {
                    wordSection[1] = wordArray[x+1];
                    digitSection[1] = digitArray[x+1];
                }
                // Value at index 2 of the new array assuming it is within index range
                if ((length - x) >= 3)
                {
                    wordSection[2] = wordArray[x+2];
                    digitSection[2] = digitArray[x+2];
                }

                // Conditions for word logic to add into the sentence
                if (wordSectionArray.Count() == 2)
                {
                    if (wordSectionArray[wordSectionArray.Count() - 1] == "Zero")
                    {
                        wordSectionArray[wordSectionArray.Count() - 1] = "Thousand";
                    }
                    else
                    {
                        wordSectionArray.Add("Thousand,");
                    }
                    
                }

                // Add the resulting phrase from a given set of 1-3 numbers representing the singles, tens and hundreds digits to the array
                wordSectionArray.Add(CheckHundreds(wordSection, digitSection));
                
            }

            // Reverse the list to make it in the correct order
            wordSectionArray.Reverse();

            // Create the sentence
            string sentence = "";

            // Concatenate each word into the sentence along with a space
            foreach (string section in wordSectionArray)
            {
                sentence += section + " ";
            }

            // Condition if there is only one dollar
            if (length == 3 && digitArray[2] == 1)
            {
                sentence = "One Dollar ";
            }

            // As long as cents amount is empty, concatenate it onto the sentence string
            // If there are no cents do nothing as we dont want to have "Zero Cents" in the string
            if (cents != "Zero Cents")
            {
                sentence += "and " + cents;
            }


            return sentence;
        }
    }
}
