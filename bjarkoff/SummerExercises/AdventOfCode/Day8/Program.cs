using System;
using System.IO;
using System.Collections.Generic;

class AoC_puzzle8 {
    
    
    /// <summary>
    /// Reads a input file with name given by input string, returns file data as string.
    /// </summary>
    /// <param name="str">name of input string</param>
    /// <returns>a string of data, plus the length of line and number of lines</returns>
    public static (string,int,int) ReadFileOutputString(string str) {
        string output = "";
        int lineLength = 0;
        int numberOfLines = 0;
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
                    numberOfLines += 1;
                    lineLength = line.Length;
                }
            }
            
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        return (output,lineLength,numberOfLines);
    }

    public static int[,] ReadStringOutput2DArray(string str,int lineLength, int numberOfLines) {
        int[,] output2DArray = new int[numberOfLines,lineLength];
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(str))
            {
                string line;
                int lineNumber = 0;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length; i++) {
                        output2DArray[lineNumber,i] = (int)(line[i]-'0');
                    }
                    lineNumber++;
                }
            }
            
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        return output2DArray;
    } 

    // Method for finding the number of trees visible from outside the map:
    private static int TreesVisible(int[,] arr) {
        List<(int,int)> listOfVisibleTrees = new List<(int, int)>();
        int visibleTrees = 0;
        // First we traverse each row of the array and look for visible trees:
        for (int i = 0; i<arr.GetLength(0); i++) {
            int highestTreeInRowFromLeft = -1;
            int highestTreeInRowFromRight = -1;
            //traversing from the left:
            for (int j = 0; j<arr.GetLength(1); j++) {
                if (arr[i,j] > highestTreeInRowFromLeft) {
                    highestTreeInRowFromLeft = arr[i,j];
                    if (!listOfVisibleTrees.Contains((i,j))) {
                        visibleTrees++;
                        listOfVisibleTrees.Add((i,j));
                    }
                }
            }
            //traversing from the right:
            for (int j = arr.GetLength(1)-1; j>=0; j--) {
                if (arr[i,j] > highestTreeInRowFromRight) {
                    highestTreeInRowFromRight = arr[i,j];
                    if (!listOfVisibleTrees.Contains((i,j))) {
                        visibleTrees++;
                        listOfVisibleTrees.Add((i,j));
                    }
                }
            }
        }
        // Now we traverse each column of the array and look for visible trees:
        for (int j = 0; j< arr.GetLength(1); j++) {
            int highestTreeInColumnFromTop = -1;
            int highestTreeInColumnFromButtom = -1;
            // Traversing from the top:
            for (int i = 0; i<arr.GetLength(0);i++) {
                if (arr[i,j] > highestTreeInColumnFromTop) {
                    highestTreeInColumnFromTop = arr[i,j];
                    if (!listOfVisibleTrees.Contains((i,j))) {
                        visibleTrees++;
                        listOfVisibleTrees.Add((i,j));
                    }
                }
            }
            //Traversing from the buttom:
            for (int i = arr.GetLength(0)-1; i>=0; i--) {
                if (arr[i,j] > highestTreeInColumnFromButtom) {
                    highestTreeInColumnFromButtom = arr[i,j];
                    if (!listOfVisibleTrees.Contains((i,j))) {
                        visibleTrees++;
                        listOfVisibleTrees.Add((i,j));
                    }
                }
            }
        }
        return visibleTrees;
    }    

    // Method for finding the scenic score of a given tree in an array.
    private static int ScenicScore((int,int) coord, int[,] arr) {
        int scoreFromLeft = 0;
        int scoreFromRight = 0;
        int scoreUp = 0;
        int scoreDown = 0;
        // Now we iterate left-to-right:
        int index = coord.Item2 + 1;
        while (index < arr.GetLength(1)) {
            scoreFromLeft++;
            if (arr[coord.Item1,index] < arr[coord.Item1,coord.Item2]) {
                index++;
            }
            else {
                break;
            }
        }
        // Console.WriteLine("ScenicScoreFactor looking from {0} to the right is: {1}",
        //     coord,scoreFromLeft);
        // Now we iterate right-to-left:
        index = coord.Item2 - 1;
        while (index >= 0) {
            scoreFromRight++;
            if (arr[coord.Item1,index] < arr[coord.Item1,coord.Item2]) {
                index--;
            }
            else {
                break;
            }
        }
        // Console.WriteLine("ScenicScoreFactor looking from {0} to the left is: {1}",
        //     coord,scoreFromRight);
        // Now we iterate from top:
        index = coord.Item1 + 1;
        while (index < arr.GetLength(0)) {
            scoreDown++;
            if (arr[index,coord.Item2] < arr[coord.Item1,coord.Item2]) {
                index++;
            }
            else {
                break;
            }
        }
        // Console.WriteLine("ScenicScoreFactor looking down from {0} is: {1}",
        //     coord,scoreDown);
        // Now we iterate from buttom:
        index = coord.Item1 - 1;
        while (index >= 0) {
            scoreUp++;
            if (arr[index,coord.Item2] < arr[coord.Item1,coord.Item2]) {
                index--;
            }
            else {
                break;
            }
        }
        // Console.WriteLine("ScenicScoreFactor looking up from {0} is: {1}",
        //     coord,scoreUp);
        // Console.WriteLine("tree at {0} has scenic score: {1}",coord,
        //     scoreFromLeft*scoreFromRight*scoreUp*scoreDown);
        return scoreFromLeft*scoreFromRight*scoreUp*scoreDown;
    }

    // Method for finding the highest scenic score in a 2DArray:
    private static int HighestScenicScore(int[,] arr) {
        int highestScenicScore = 0;
        for (int i = 0; i<arr.GetLength(0); i++) {
            for (int j = 0; j<arr.GetLength(1); j++) {
                highestScenicScore = System.Math.Max(highestScenicScore,ScenicScore((i,j),arr));
            }
        }
        return highestScenicScore;
    }

    public static void Main() {
        
        (string,int,int) input = ReadFileOutputString("AoC_puzzle8.txt");
        int[,] input2DArray = ReadStringOutput2DArray("AoC_puzzle8.txt",input.Item2,input.Item3);
        
        (string,int,int) testInput = ReadFileOutputString("AoC_puzzle8testString.txt");
        int[,] test2DArray = ReadStringOutput2DArray("AoC_puzzle8testString.txt",testInput.Item2,testInput.Item3);
        // Vi tester at inputtet er forløbet korrekt:

        Console.WriteLine("input2DArray's entry (0,0) is: {0}",input2DArray[0,0]);
        Console.WriteLine("input2DArray's entry (0,1) is: {0}",input2DArray[0,1]);
        Console.WriteLine("input2DArray's number of lines is: {0}",input2DArray.GetLength(0));
        Console.WriteLine("input2DArray's length of each line is: {0}",input2DArray.GetLength(1));
        
        Console.WriteLine("test2DArray's entry (0,0) is: {0}",test2DArray[0,0]);
        Console.WriteLine("test2DArray's entry (0,1) is: {0}",test2DArray[0,1]);
        Console.WriteLine("test2DArray's number of lines is: {0}",test2DArray.GetLength(0));
        Console.WriteLine("test2DArray's length of each line is: {0}",test2DArray.GetLength(1));

        Console.WriteLine("Number of visible trees in array \"test2DArray\" is: {0}",TreesVisible(test2DArray));
        Console.WriteLine("Number of visible trees in array \"input2DArray\" is: {0}",TreesVisible(input2DArray));

        Console.WriteLine("");
        Console.WriteLine("Highest scenic score of test2DArray is: {0}", HighestScenicScore(test2DArray));
        Console.WriteLine("Highest scenic score of input2DArray is: {0}", HighestScenicScore(input2DArray));
        // Console.WriteLine("ScenicScore of tree at (1,1) in test2DArray is: {0}",ScenicScore((1,1),test2DArray));
    }   
}