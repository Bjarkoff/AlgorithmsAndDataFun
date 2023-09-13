using System;
using System.IO;

class AoC_puzzle3 {
    
    
    // Reads a .txt-file into a list, where each entry in list is 
    // each line in the input file.
    public static List<string> ReadFile(string str) {
        List<string> listOfStrings = new List<string>();
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            // We save each elf's entire inventory in a single list entry.
            using (StreamReader sr = new StreamReader(str))
            {
                string line;
                
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    listOfStrings.Add(line);
                }
            }
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        return listOfStrings;
    }
    
    // Method for deciding, which character figurates in both halfs of the input string.
    public static char Error(string str) {
        string compartmentOne = str.Remove(str.Length / 2);
        string compartmentTwo = str.Remove(0,str.Length / 2);

        foreach (char chari in compartmentOne) {
            foreach (char charj in compartmentTwo) {
                if (chari == charj) {
                    return chari;
                }
            }
        }
        Console.WriteLine("No error was found in rucksack:" + str);
        return '!';
    }

    // Method for finding the one character that figurates in the 
    // consecutive lines of a string:
    public static char RecurrentCharacter(string str1, string str2, string str3) {
        List<char> ListOfCrossOffs = new List<char>();
        foreach (char character1 in str1) {
            if (!ListOfCrossOffs.Contains(character1)) {
                foreach (char character2 in str2) {
                    if (character1 == character2) {
                        foreach (char character3 in str3) {
                            if (character2 == character3) {
                                return character3;
                            }
                        }
                    }
                    
                }
                // At this point character1 was not on the ListOfCrossOffs, but
                // it was not a match, so we add it to the list:
                ListOfCrossOffs.Add(character1);
            }
        }
        // Here an error with our design by contract occured, so we writeline:
        Console.WriteLine("The three strings had no matching character");
        return '!';
    }

    // Method for calculating the priority of a given error char.
    // We simply use the ASCII-values of the given chars.
    public static uint ErrorPriority(char character) {
        if (char.IsLower(character)) {
            return Convert.ToUInt32(character) - 96;
        }
        else if (char.IsUpper(character)) {
            return Convert.ToUInt32(character) - 64 + 26;
        }
        else {
            return 0;
        }
    }
    

    public static void Main() {
        // We need to calculate the sum of points for each round. 
        // Should be easily done as such:
        uint sumOfErrors = 0;
        List<string> RucksackContents = ReadFile("AoC_puzzle3.txt");
        Console.WriteLine(RucksackContents[0]);
        Console.WriteLine(Error(RucksackContents[0]));
        Console.WriteLine(ErrorPriority(Error(RucksackContents[0])));
        Console.WriteLine(ErrorPriority(Error("aBcCDB")));

        // It seems to be working; now we calculate through a foreach loop:

        foreach (string str in RucksackContents) {
            sumOfErrors += ErrorPriority(Error(str));
        }
        Console.WriteLine(sumOfErrors);

        // Now for solving the second assignment:

        uint sumOfBadgeItems = 0;
        // Now we go three-lines at a time, like this:
        for (int i = 0; i < RucksackContents.Count; i += 3) {
            sumOfBadgeItems += ErrorPriority(RecurrentCharacter(RucksackContents[i],
                RucksackContents[i+1], RucksackContents[i+2]));
            
        }
       
        Console.WriteLine(sumOfBadgeItems);

        // OK, vi tester lige på det input, der var i opgaven:

        Console.WriteLine(ErrorPriority(RecurrentCharacter(
            "vJrwpWtwJgWrhcsFMMfFFhFp", "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
            "PmmdzqPrVvPwwTWBwg")));
        
        Console.WriteLine(ErrorPriority(RecurrentCharacter(
            "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn","ttgJtRGJQctTZtZT",
            "CrZsJsPPZsGzwwsLwLmpwMDw")));

        Console.WriteLine(RucksackContents[0]);
        


    }

}