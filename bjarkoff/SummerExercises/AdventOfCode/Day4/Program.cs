using System;
using System.IO;

class AoC_puzzle4 {
    
    
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
    
    // Method for taking an input string on the form
    // uint1-uint2,uint3-uint4
    // and returning the array [uint1,uint2,uint3,uint4].
    public static uint[] ParsingUintsFromString(string str) {
        string[] sections = str.Split(',');
        string[] section1 = sections[0].Split('-');
        string[] section2 = sections[1].Split('-');
        return new uint[] {UInt32.Parse(section1[0]),UInt32.Parse(section1[1]),
            UInt32.Parse(section2[0]),UInt32.Parse(section2[1])};
    }

    // Method for determining if the range from [uint1 - uint2] overlaps completely
    // with [uint3 - uint4], or vice versa. 
    public static bool DoOverlap(uint[] arr) {
        return (arr[0] <= arr[2] && arr[1] >= arr[3]) || 
            (arr[2] <= arr[0] && arr[3] >= arr[1]);
    }

    // Method for determining if the range from [uint1 - uint2] overlaps at all
    // with [uint3 - uint4], or vice versa.
    public static bool AnyOverlap(uint[] arr) {
        return (arr[1] >= arr[2] && arr[0] <= arr[3]) || 
            (arr[3] >= arr[0] && arr[2] <= arr[1]);
    }

    public static void Main() {
        
        uint sumOfOverlappingPairs = 0;
        List<string> InputStringList = ReadFile("AoC_puzzle4.txt");
        foreach (string str in InputStringList) {
            sumOfOverlappingPairs += Convert.ToUInt32(DoOverlap(ParsingUintsFromString(str)));
        }
        Console.WriteLine(sumOfOverlappingPairs);

        uint sumOfAnyOverlappingPairs = 0;
        foreach (string str in InputStringList) {
            sumOfAnyOverlappingPairs += Convert.ToUInt32(AnyOverlap(ParsingUintsFromString(str)));
            Console.WriteLine(AnyOverlap(ParsingUintsFromString(str)));
        }   
        Console.WriteLine(sumOfAnyOverlappingPairs);

    }

}