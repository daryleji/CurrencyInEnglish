/*
 * Author: Daryle Ji
 * Start date: 1/12/2021
 * Completion date:
 * Description: A console program which takes a user input (integer value) and converts it to English currency representation in words.
 */

using System;
using static CurrencyInEnglish.NumberToEnglish;

namespace CurrencyInEnglish
{
    class CurrencyInEnglish
    {
        //Local function for converting numeric values into english representation in words

        static void Main(string[] args)
        {
            // Loop to allow user to continue to input numbers
            double number = -1;
            while (number != 0) {
                Console.WriteLine("Please enter the numeric value you wish to convert to English currency representation in words.\nOtherwise, enter 0 to exit.");
                string userInput = Console.ReadLine();

                //Try-catch block for exceptions
                try
                {
                    number = Convert.ToDouble(userInput);
                    // Check for exit condition
                    if (number == 0)
                    {
                        break;
                    }

                    // If number inputted is outside the scope of the program
                    if (number > 1000000 || number < 0)
                    {
                        Console.WriteLine("Numbers less than 0 or greater than 1,000,000 are out of the scope of this program.");
                        continue;
                    }


                    // Code block to convert user input to english currency representation
                    // Get the number of digits
                    int length = DigitCount(number);
                    // Separate each digit into an array, each index represents a base10 unit
                    var digitArray = CreateNumberArray(number, length);
                    // Create an equal array with the english word representations in each index
                    var wordArray = NumbersToWords(digitArray, length);
                    // Convert the words into a full sentence
                    var sentence = WordsToSentence(wordArray, digitArray, length);
                    Console.WriteLine("{0}\n", sentence);

                } catch (Exception e) 
                {
                    Console.WriteLine("An ERROR has occurred!\n{0}", e);
                }
                    
            }

        }
    }
}
