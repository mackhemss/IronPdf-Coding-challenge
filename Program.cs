using System;
using System.Text;
using System.Collections.Generic;

class Program
{
     static void Main(string[] args)
    {
        string[] inputs = { "33#", "227*#", "4433555 555666#", "8 88777444666*664#", };
        
        foreach (var input in inputs)
        {
            if (string.IsNullOrWhiteSpace(input)) continue;
            string result = OldPhonePad(input);
            Console.WriteLine($"Input: {input}");
            Console.WriteLine($"Output: {result}");
            Console.WriteLine();
        }

        Console.WriteLine("Do you want to try more test cases? Yes/no ");
        string choice = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;

        if (choice == "YES")
        {
            Console.WriteLine("Enter input strings (separate multiple inputs with commas accepted):");
            string userInput = Console.ReadLine() ?? string.Empty;
            userInput = new string(userInput.Where(c => char.IsDigit(c) || c == '*' || c == '#' || c == ' ').ToArray());
            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("No valid input provided. Please enter digits, *, or #.");
                return;
            }
            else
            {
                string[] customStringsInput = userInput.Split(',');
                foreach (var input in customStringsInput)
                {
                    string inputWithoutSpace = input;
                    if (string.IsNullOrWhiteSpace(inputWithoutSpace)) continue;
                    string result = OldPhonePad(inputWithoutSpace);
                    Console.WriteLine($"Input: {inputWithoutSpace}");
                    Console.WriteLine($"Output: {result}");
                    Console.WriteLine();
                }
            }   
        }
    }
    public static string OldPhonePad(string input)
    {
        var keyDigitAndAlpha = new Dictionary<char, string>
        { { '2', "ABC" }, { '3', "DEF" }, { '4', "GHI" }, { '5', "JKL" }, { '6', "MNO" }, { '7', "PQRS" }, { '8', "TUV" }, { '9', "WXYZ" }};

        var result = new StringBuilder();
        char lastDigit = ' ';  
        int count = 0; 
        foreach (var c in input)
        {
            if (c == '*') // Backspace
            {
                if  (count > 0)
                {
                 count = 0; // Reset press count without appending
                }
                else if (result.Length > 0)
                {
                    result.Length--; // Remove last character in result
                }
                continue;
            }

            if (c == ' ') // Pause
            {
                AppendCharacter(result, lastDigit, count, keyDigitAndAlpha);
                lastDigit = ' '; // Reset the current digit
                count = 0;
                continue;
            }

            if (!keyDigitAndAlpha.ContainsKey(c)) // Ignoring invalid characters
            {
                continue;
            }

            if (c == lastDigit)
            {
             count++;
            }
            else
            {
                AppendCharacter(result, lastDigit, count, keyDigitAndAlpha); 
                lastDigit = c; 
                count = 1;
            }
        }
       
        AppendCharacter(result, lastDigit, count, keyDigitAndAlpha);

        return result.ToString();
    }
    private static void AppendCharacter(StringBuilder result, char digit, int count, Dictionary<char, string> keyDigitAndAlpha)
    {
        if  (count > 0 && keyDigitAndAlpha.ContainsKey(digit))
        {
            string letters = keyDigitAndAlpha[digit];
            result.Append(letters[ (count - 1) % letters.Length]);
        }
    }
}
