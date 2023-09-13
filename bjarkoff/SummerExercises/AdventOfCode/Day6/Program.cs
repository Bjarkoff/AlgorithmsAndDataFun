using System;
using System.IO;
using System.Collections.Generic;

class AoC_puzzle6 {
    
    
    // Reads a .txt-file into an output string. 
    public static string ReadFileOutputString(string str) {
        string output = "";
        
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(str))
            {
                string line;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    output += line;
                }
            }
            
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        return output;
    }

    // Method for determining whether a string contains any
    // duplicate characters.
    private static bool DoesContainRepeat(string str) {
        for (int i = 0; i < str.Length - 1; i++) {
            if (str.Substring(i+1).Contains(str[i])) {
                return true;
            }
        }    
        return false;
    }

    // Method for finding the index after the first n-character sequence 
    // without repeating characters occured in an input string.
    private static int IndexOfNoRepetition(string str, int n) {
        if (str.Length < n) {
            Console.WriteLine($"input string {str} was of less than n={n} characters");
            throw new ArgumentException(
                $"Tried to use string of length {str.Length} and find repetitions of length n={n} in method IndexOfNoRepetition().");
        }
        for (int i = 0; i < str.Length - n; i++) {
            if (!DoesContainRepeat(str.Substring(i,n))) {
                return i+n;
            }
        }
        Console.WriteLine($"No string of n={n} characters without repetition was found");
        return 0;
    }

    public static void Main() {
        
        string inputString = ReadFileOutputString("AoC_puzzle6.txt");
        
        Console.WriteLine(inputString);

        Console.WriteLine("The answer to today's puzzle 1 is: {0}",IndexOfNoRepetition(inputString,4));
        Console.WriteLine("The answer to today's puzzle 1 is: {0}",IndexOfNoRepetition(inputString,14));
        Console.WriteLine("Length of inputString is: {0}",inputString.Length);
    }
}