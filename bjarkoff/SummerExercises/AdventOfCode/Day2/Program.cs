using System;
using System.IO;

class AoC_puzzle2 {
    
    
    // Reads a .txt-file into a list, where each entry in list is 
    // each line in the input file.
    public static List<string> FileReader(string str) {
        List<string> listOfRounds = new List<string>();
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
                    listOfRounds.Add(line);
                }
            }
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        return listOfRounds;
    }
    
    // Method for calculating the points in a given round. Assumes the input string is
    // of order str[0] = ('A' or 'B' or 'C'), str[1] == '', str[2] = ('X' or 'Y' or 'Z'). 
    public static int PointOfRound(string str) {
        switch (str[2]) {
            case 'X':
                if (str[0] == 'A') {
                    return 3+1;
                }
                else if (str[0] == 'B') {
                    return 0+1;
                }
                else if (str[0] == 'C') {
                    return 6+1;
                }
                break;
            case 'Y':
                if (str[0] == 'A') {
                    return 6+2;
                }
                else if (str[0] == 'B') {
                    return 3+2;
                }
                else if (str[0] == 'C') {
                    return 0+2;
                }
                break;
            case 'Z':
                if (str[0] == 'A') {
                    return 0+3;
                }
                else if (str[0] == 'B') {
                    return 6+3;
                }
                else if (str[0] == 'C') {
                    return 3+3;
                }
                break;
            deafult:
                Console.WriteLine("the third char of input string in round was not X, Y or Z");
                return Int32.MinValue;
                break;
        }
        return Int32.MinValue;
    }

    public static int PointsOfRoundBasedOnResult(string str) {
        switch (str[2]) {
            case 'X':
                if (str[0] == 'A') {
                    return 0+3;
                }
                else if (str[0] == 'B') {
                    return 0+1;
                }
                else if (str[0] == 'C') {
                    return 0+2;
                }
                break;
            case 'Y':
                if (str[0] == 'A') {
                    return 3+1;
                }
                else if (str[0] == 'B') {
                    return 3+2;
                }
                else if (str[0] == 'C') {
                    return 3+3;
                }
                break;
            case 'Z':
                if (str[0] == 'A') {
                    return 6+2;
                }
                else if (str[0] == 'B') {
                    return 6+3;
                }
                else if (str[0] == 'C') {
                    return 6+1;
                }
                break;
            deafult:
                Console.WriteLine("the third char of input string in round was not X, Y or Z");
                return Int32.MinValue;
                break;
        }
        return Int32.MinValue;        
    }

    public static void Main() {
        // We need to calculate the sum of points for each round. 
        // Should be easily done as such:
        int sumOfRounds = 0;
        List<string> listOfRounds = FileReader("AoC_puzzle2.txt");

        foreach (string str in listOfRounds) {
            sumOfRounds += PointOfRound(str);
        }
        Console.WriteLine(sumOfRounds);
        
        // // Vi tester lige på en lille ting for at teste om det virker som vi tror:
        // int testSumOfRounds = 0;
        // List<string> listOfRounds2 = new List<string> {"A Y", "B X", "C Z"};
        // foreach (string str in listOfRounds2) {
        //     testSumOfRounds += PointOfRound(str);
        // }
        // Console.WriteLine(testSumOfRounds);

        // Now we need to calculate based on the new way of reacting to 
        // oppponents choice:
        int sumOfRounds2 = 0;
        foreach (string str in listOfRounds) {
            sumOfRounds2 += PointsOfRoundBasedOnResult(str);
        }
        Console.WriteLine(sumOfRounds2);

        int testSum = 0;
        List<string> testListOfRounds = new List<string> {"A Y", "B X", "C Z"};
        foreach (string str in testListOfRounds) {
            testSum += PointsOfRoundBasedOnResult(str);
        }
        Console.WriteLine(testSum);



        


    }

}