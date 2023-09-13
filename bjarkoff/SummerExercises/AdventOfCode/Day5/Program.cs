using System;
using System.IO;
using System.Collections.Generic;

class AoC_puzzle5 {
    
    
    // Reads a .txt-file into two lists. First it reads until the first
    // line character is not '['. All of the stuff before is put into list1.
    // Secondly it reads everything starting with an 'm' into a second list.
    public static void ReadFile(string str,List<string> list1, List<string> list2) {
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
                    if (line.Length != 0 && line[0] == '[') {
                        list1.Add(line);
                    }
                    else if (line.Length != 0 && line[0] == 'm') {
                        list2.Add(line);
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
    }
    
    // Method for building the stacks that we need for this exercise.
    // Input is listOfCrates
    public static Stack<char>[] StacksOfCrates(List<string> listOfStrings) {
        int numberOfStacks = 9;
        int initialHeightOfStacks = listOfStrings.Count;
        Stack<char>[] stackArray = new Stack<char>[numberOfStacks];
        for (int k = 0; k < numberOfStacks; k++) {
            stackArray[k] = new Stack<char>();
        }
        // We now iterate through lines in str (i) and
        // the stack-number (j) in stackArray:
        for (int i = initialHeightOfStacks - 1; i >=0; i--) {
            for (int j = 0; j < numberOfStacks; j++) {
                if (listOfStrings[i][4*j+1] != ' ') {
                    stackArray[j].Push(listOfStrings[i][4*j+1]);
                    // Console.Write(stackArray[j].Peek());
                }
            }
            // Console.WriteLine(i);
        }
        return stackArray;
    }

    // Alternative implementation of StackOfCrates:
    public static Stack<char>[] AlternativeStacksOfCrates(List<string> listOfStrings) {
        int numberOfStacks = 9;
        int initialHeightOfStacks = listOfStrings.Count;
        Stack<char>[] stackArray = new Stack<char>[numberOfStacks];
        for (int k = 0; k < numberOfStacks; k++) {
            stackArray[k] = new Stack<char>();
        }
        // We now iterate through lines in str (i) and
        // the stack-number (j) in stackArray:
        for (int i = initialHeightOfStacks - 1; i >=0; i--) {
            for (int j = 0; j < listOfStrings[0].Length; j++) {
                if (listOfStrings[i][j] == '[') {
                    stackArray[j/4].Push(listOfStrings[i][j+1]);
                    // Console.Write(stackArray[j].Peek());
                }
            }
            // Console.WriteLine(i);
        }
        return stackArray;
    }




    // Method/Command from converting a string on the form 
    // "move int32 to int32 from int32"
    // Into a command upon an array of stacks of char.
    public static int[] CommandInts(string stringOfMoves) {
        // First we divide the string into parts:
        string commandString = ((stringOfMoves.Replace(" from ",",")).Replace("move ","")).Replace(" to ",",");
        string[] commandArray = commandString.Split(',');
        int[] commandInts = new int[3];
        for (int i = 0; i < commandArray.Length; i++) {
            commandInts[i] = Int32.Parse(commandArray[i]);
        }
        return commandInts;
    }


    // Method for carrying out a list of commands on the form 
    // "move int32 to int32 from int32"
    // Into a command upon an array of stacks of char.
    public static void MoveCrates(List<string> listString, Stack<char>[] stackArray) {
        // First we divide the string into parts:
        foreach (string line in listString) {
            int[] commandInts = CommandInts(line);
            // Now we have the command-array in ints, commandInts.
            while (commandInts[0] > 0) {
                
                char popped = stackArray[commandInts[1]-1].Pop();
                // Console.WriteLine(popped);
                stackArray[commandInts[2]-1].Push(popped);
                commandInts[0]--;
            }
        }
    }

    // Alternartive method for carrying out a list of commands on the form 
    // "move int32 to int32 from int32"
    // Into a command upon an array of stacks of char. Here the 
    // order of crates is to be maintained when moving multiple crates,
    // like when you lift up more at once.
    public static void AlternativeMoveCrates(List<string> listString, Stack<char>[] stackArray) {
        // First we divide the string into parts:
        foreach (string line in listString) {
            int[] commandInts = CommandInts(line);
            // Now we have the command-array in ints, commandInts.
            // We make a stack so we can fill in the crates and pop
            // from there to fill up other stacks
            Stack<char> stackToPushFrom = new Stack<char>();
            while (commandInts[0] > 0) {
                
                stackToPushFrom.Push(stackArray[commandInts[1]-1].Pop());
                commandInts[0]--;
            }
            while (stackToPushFrom.Count != 0) {
                stackArray[commandInts[2]-1].Push(stackToPushFrom.Pop());
            }
            
        }
    }

    
    public static void Main() {
        List<string> listOfStacks = new List<string>();
        List<string> listOfMoves = new List<string>();
        ReadFile("AoC_puzzle5.txt",listOfStacks,listOfMoves);
        // foreach (string line in listOfStacks) {
        //     Console.WriteLine(line);
        // }
        Stack<char>[] stackArray = AlternativeStacksOfCrates(listOfStacks);

        foreach (Stack<char> stack in stackArray) {
            foreach (char character in stack) {
                Console.Write(character);
            }
            Console.WriteLine("");
        }

        // Now we continue:
        // Console.WriteLine("\nPopping '{0}'", stackArray[8].Pop());
        // Console.WriteLine("Peek at next item to destack: {0}",
        //     stackArray[1].Peek());
        // Console.WriteLine("Popping '{0}'", stackArray[8].Pop());
        
        // foreach (int line in CommandInts("move 11 from 7 to 2")) {
        //     Console.WriteLine(line);
        // }

        MoveCrates(listOfMoves, stackArray);
        Console.WriteLine("Answer for puzzle 1:");
        foreach (Stack<char> stack in stackArray) {
            Console.Write(stack.Peek());
        }
        Console.WriteLine("");

        Stack<char>[] stackArray2 = AlternativeStacksOfCrates(listOfStacks);
        
        AlternativeMoveCrates(listOfMoves, stackArray2);
        Console.WriteLine("Answer for puzzle 2:");
        foreach (Stack<char> stack in stackArray2) {
            Console.Write(stack.Peek());
        }
    }
}