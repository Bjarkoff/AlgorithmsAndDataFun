using System;
using System.IO;

class AoC_puzzle1
{
    
    // Reads a .txt-file into a list, where each entry in list is 
    // found using blank lines in .txt-file.
    public static List<string> FileReader(string str) {
        List<string> caloryList = new List<string>();
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            // We save each elf's entire inventory in a single list entry.
            using (StreamReader sr = new StreamReader(str))
            {
                string line;
                string caloriesPerElve = "";
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    if (line != "" && caloriesPerElve == "") {
                        caloriesPerElve += line;
                    }
                    else if (line != "") {
                        caloriesPerElve += "\n" + line;
                    }
                    else {
                        caloryList.Add(caloriesPerElve);
                        caloriesPerElve = "";
                    }
                }
            }
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        return caloryList;
    }



    // Method that takes a string and calculates the sum of lines as an int.
    public static int StringOfCaloriesToInt(string str) {
        int outputInt = 0;
        string intermediateString = "";
        foreach (char character in str) {
            if (character != '\n') {
                intermediateString += character;
            }
            else {
                outputInt += Int32.Parse(intermediateString);
                intermediateString = "";
            }
        }
        outputInt += Int32.Parse(intermediateString);
        return outputInt;
    }




    // Takes a list of strings of elf calories, converts to array where each elf's 
    // calories are summed.
    public static int[] CaloryArrayCreator(List<string> caloryList) {
        int[] arrayOfElfCalories = new int[caloryList.Count];

        for (int i = 0; i < arrayOfElfCalories.Length; i++) {
            arrayOfElfCalories[i] = StringOfCaloriesToInt(caloryList[i]);
        }
        return arrayOfElfCalories;
    }




    // Method for finding the tuple (index,maxValue) of an int[], where
    // maxValue is the (first) largest value in the array, and its index.
    public static (int,int) MaximalValueInArray(int[] intArray) {
        int index = 0;
        int currentMaxCaloryValue = 0;
        for (int i = 0; i < intArray.Length; i++) {
            if (intArray[i] > currentMaxCaloryValue) {
                currentMaxCaloryValue = intArray[i];
                index = i;
            }
        }
        return (index,currentMaxCaloryValue);
    }


    // Method for finding the top-3 largest values in an int[] of POSITIVE values.
    // Result is (third largest int, second largest, largest).
    public static (int,int,int) TopThreeInts(int[] intArray) {
        int int1 = 0;
        int int2 = 0;
        int int3 = 0; 
        for (int i = 0; i < intArray.Length; i++) {
            if (intArray[i] > int3) {
                int1 = int2;
                int2 = int3;
                int3 = intArray[i];
            }
            else if (intArray[i] > int2) {
                int1 = int2;
                int2 = intArray[i];
            }
            else if (intArray[i] > int1) {
                int1 = intArray[i];
            }
        }
        return (int1,int2,int3);
    }


    public static void Main()
    {
        // First we get the data from the .txt-file:
        List<string> caloryList = FileReader("AoC_puzzle1.txt");

        // Then we get an array of each elf's calories:
        int[] arrayOfElfCalories = CaloryArrayCreator(caloryList);

        // Now we just need to find the index of maximal calory value.
        int maxValue = MaximalValueInArray(arrayOfElfCalories).Item2;

        // Now we print to check:
        Console.WriteLine(TopThreeInts(arrayOfElfCalories));
        Console.WriteLine(TopThreeInts(arrayOfElfCalories).Item1 + 
        TopThreeInts(arrayOfElfCalories).Item2 +
        TopThreeInts(arrayOfElfCalories).Item3);
        
    }
    
    
}