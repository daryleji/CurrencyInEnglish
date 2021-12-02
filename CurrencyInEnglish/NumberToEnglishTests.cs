using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using static CurrencyInEnglish.NumberToEnglish;

namespace CurrencyInEnglish
{
    [TestFixture]
    public class NumberToEnglishTests
    {
        // Tests for DigitCount()
        [Test]
        public void DigitCount_WhenCalled_ReturnNumberOfDigits()
        {
            // 6 Digits in total including decimal points
            double number = 1000.00;
            var count = DigitCount(number);
            Assert.That(count, Is.EqualTo(6));
        }

        //Tests for CreateEnglishArray()
        // Singular number
        [Test]
        public void CreateEnglishArray_SingleDigit_ReturnArrayWithOneItem()
        {
            var number = 0.01;
            var length = 1;
            var result = CreateNumberArray(number, length);
            int[] testArray = {1};
            Assert.That(result, Is.EqualTo(testArray));
        }

        [Test]
        public void CreateEnglishArray_MultipleDigit_ReturnArrayWithOneItem()
        {
            var number = 1234.89;
            var length = 6;
            var result = CreateNumberArray(number, length);
            int[] testArray = {9, 8, 4, 3, 2, 1};
            Assert.That(result, Is.EqualTo(testArray));
        }


        // Test for NumbersToWords()
        [Test]
        public void NumbersToWords_FourDigits_ReturnAWordArray()
        {
            int[] digitArray = {9, 8, 4, 3, 2, 1};
            var length = 6;
            string[] testArray = {"Nine", "Eighty", "Four", "Thirty", "Two", "One"};
            var result = NumbersToWords(digitArray, length);
            Assert.That(result, Is.EqualTo(testArray));
        }


        // Test for CheckCents()
        [Test]
        public void CheckCents_ReturnOneCent()
        {
            int[] digitArray = { 1 };
            string[] wordArray = {"One"};
            var length = 1;

            string testString = "One Cent";

            var result = CheckCents(wordArray, digitArray, length);

            Assert.That(result, Is.EqualTo(testString));
        }


        [Test]
        public void CheckCents_ReturnTwoCents()
        {
            int[] digitArray = { 2 };
            string[] wordArray = { "Two" };
            var length = 1;

            string testString = "Two Cents";

            var result = CheckCents(wordArray, digitArray, length);

            Assert.That(result, Is.EqualTo(testString));
        }

        [Test]
        public void CheckCents_ReturnThirteenCents()
        {
            int[] digitArray = { 3, 1 };
            string[] wordArray = { "Three", "Tens" };
            var length = 2;

            string testString = "Thirteen Cents";

            var result = CheckCents(wordArray, digitArray, length);

            Assert.That(result, Is.EqualTo(testString));
        }

        [Test]
        public void CheckCents_ReturnTwentyFiveCents()
        {
            int[] digitArray = { 5, 2 };
            string[] wordArray = { "Five", "Twenty" };
            var length = 2;

            string testString = "Twenty Five Cents";

            var result = CheckCents(wordArray, digitArray, length);

            Assert.That(result, Is.EqualTo(testString));
        }


        // Test for CheckHundreds()
        [Test]
        public void CheckHundreds_ReturnOne()
        {
            int[] digitArray = { 1, 0, 0 };
            string[] wordArray = { "One" , "Zero", "Zero"};

            string testString = "One";

            var result = CheckHundreds(wordArray, digitArray);

            Assert.That(result, Is.EqualTo(testString));
        }


        [Test]
        public void CheckHundreds_ReturnTwenty()
        {
            int[] digitArray = { 0, 2, 0 };
            string[] wordArray = { "Zero", "Twenty", "Zero" };

            string testString = "Twenty";

            var result = CheckHundreds(wordArray, digitArray);

            Assert.That(result, Is.EqualTo(testString));
        }


        [Test]
        public void CheckHundreds_ReturnOneHundred()
        {
            int[] digitArray = { 0, 0, 1 };
            string[] wordArray = { "Zero", "Zero", "One" };

            string testString = "One Hundred";

            var result = CheckHundreds(wordArray, digitArray);

            Assert.That(result, Is.EqualTo(testString));
        }

        [Test]
        public void CheckHundreds_ReturnEightEightEight()
        {
            int[] digitArray = { 8, 8, 8 };
            string[] wordArray = { "Eight", "Eighty", "Eight" };

            string testString = "Eight Hundred and Eighty Eight";

            var result = CheckHundreds(wordArray, digitArray);

            Assert.That(result, Is.EqualTo(testString));
        }


        // Test for WordsToSentence()
        [Test]
        public void WordsToSentence_ReturnFourFiveSixOneTwoThree()
        {
            int[] digitArray = { 0, 0, 3, 2, 1, 6, 5, 4 };
            string[] wordArray = { "Zero", "Zero", "Three", "Twenty", "One", "Six", "Fifty", "Four"};
            var length = 8;
            string testString = "Four Hundred and Fifty Six Thousand, One Hundred and Twenty Three Dollars ";

            var result = WordsToSentence(wordArray, digitArray, length);

            Assert.That(result, Is.EqualTo(testString));
        }

        [Test]
        public void WordsToSentence_ReturnFiftyFourDollarsTwelveCents()
        {
            int[] digitArray = { 2, 1, 4, 5};
            string[] wordArray = { "Two", "Tens", "Four", "Fifty"};
            var length = 4;
            string testString = "Fifty Four Dollars and Twelve Cents";

            var result = WordsToSentence(wordArray, digitArray, length);

            Assert.That(result, Is.EqualTo(testString));
        }
    }
}
