using System;
using System.IO;
using System.Collections.Generic;

class AoC_puzzle11 {
    
    /// <summary>
    /// Reads a input file with name given by input string, returns Monkeys in a list.
    /// Note that we use a lot of magic strings, so if input changes we need to change a lot
    /// in this method.
    /// </summary>
    /// <param name="str">name of input string</param>
    /// <returns>a list of monkeys</returns>
    public static List<Monkey> ReadFileOutputMonkeys(string str) {
        List<Monkey> monkeyList = new List<Monkey>();
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(str))
            {
                string line;
                List<ulong> startingItems = new List<ulong>();
                string operation = "";
                ulong test = 0;
                string[] testArray = new string[2];
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null) {
                    // Here we assume all types of lines are covered except newline
                    if (line.Contains(':')) {
                        
                        string[] splitLine = line.Split(": ");
                        switch (splitLine[0]) {
                            case "  Starting items":
                                string[] itemNumbers = splitLine[1].Split(", ");
                                
                                foreach (string item in itemNumbers) {
                                    // Console.WriteLine(item);
                                    startingItems.Add(UInt32.Parse(item));
                                }
                                break;
                            case "  Operation":
                                operation = splitLine[1];
                                break;
                            case "  Test":
                                string[] divisible = splitLine[1].Split("by ");
                                test = UInt64.Parse(divisible[1]);
                                break;
                            case "    If true":
                                testArray[0] = splitLine[1];
                                break;
                            case "    If false":
                                testArray[1] = splitLine[1];
                                // Now we are ready to add the monkey to the list:
                                monkeyList.Add(new Monkey(new List<ulong>(startingItems), operation, test, testArray));
                                startingItems.Clear();
                                break;
                            default:
                                // Do nothing
                                break;
                        } 
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
        return monkeyList;
    }

    /// <summary>
    /// Class for monkeys. A monkey holds a list of items, that it can inspect one at a time
    /// and throw to other monkeys. Which monkey it throws to is decided by the divisbility
    /// of the items value with the test-parameter. Finally we can keep track of the number of
    /// items a monkey has inspected, by looking at its NumberOfInspections-property.
    /// </summary>
    public class Monkey {
        private List<ulong> items;
        private string operation;
        private ulong test;
        private int throwToTrue;
        private int throwToFalse;
        private ulong numberOfInspections;

        public List<ulong> Items {
            get {return items;}
        }

        public ulong NumberOfInspections {
            get {return numberOfInspections;}
        }
        public Monkey(string operation) {
            // Do nothing, initialize later
            this.items = new List<ulong>();
            this.operation = operation;
            this.numberOfInspections = 0;
        }
        public Monkey(List<ulong> startingItems, string operation, ulong test, string[] testArray) {
            this.items = startingItems;
            this.operation = operation;
            this.test = test;
            string[] throwToTrueHelper = testArray[0].Split(" monkey ");
            this.throwToTrue = Int32.Parse(throwToTrueHelper[1]);
            string[] throwToFalseHelper = testArray[1].Split(" monkey ");
            this.throwToFalse = Int32.Parse(throwToFalseHelper[1]);
            this.numberOfInspections = 0;
        }
        public bool Test(ulong item) {
            return (item % test == 0); 
        }
        public ulong Operation(ulong item) {
            this.numberOfInspections++;
            string[] split = operation.Split(" old ");
            if (split[1] == "* old") {
                return item * item;
            }
            else if (split[1][0] == '*') {

                return item * UInt64.Parse(split[1].Substring(2));
            }
            else if (split[1][0] == '+') {
                return item + UInt64.Parse(split[1].Substring(2));
            }
            else {
                Console.WriteLine("Error, operation could not perform due to unkown operator");
            }
            return item;
        }

        public void AddToItems(ulong item) {
            items.Add(item);
        }
        public ulong GetsBored(ulong item) {
            // For part 2, the following will work:
            return item % (2*3*5*7*11*13*17*19);
            // since the divisibility of any number on the form 
            // (2*3*5*7*11*13*17*19+x) with any of the divisors 2, 3, 5, ..., 19
            // is equal to x's divisibility with that number. So the remainder from 
            // integer division by the product of these prime divisors keeps the 
            // result of the test by integer-division intact.

            // For part 1, uncomment the line below:
            // return item / 3;
            // For testMonkeys in part 2, the following will work:
            // return item % (23*19*17*13);
        }
        public void ThrowTo(ulong item, List<Monkey> monkeyMob) {
            if (Test(item)) {
                monkeyMob[throwToTrue].AddToItems(item);
            }
            else {
                monkeyMob[throwToFalse].AddToItems(item);
            }   
        }

        public void InspectItems(List<Monkey> monkeyMob) {
            foreach (uint item in items) {
                ThrowTo(GetsBored(Operation(item)),monkeyMob);
            }
            this.items.Clear();
        }
    }

   

    // -------------------------- Part 1 ---------------------------------
 

    

    public static void Main() {
        
        List<Monkey> inputMonkeys = ReadFileOutputMonkeys("AoC_puzzle11.txt");
        List<Monkey> testMonkeys = ReadFileOutputMonkeys("AoC_puzzle11test.txt");
        // Now we conduct 20 rounds of inspections:

        // foreach (Monkey monkey in testMonkeys) {
        //     monkey.InspectItems(testMonkeys);
        // }
        // foreach (Monkey monkey in testMonkeys) {
        //     foreach (uint item in monkey.Items) {
        //         Console.Write("{0}, ", item.ToString());
        //     }
        //     Console.WriteLine("");
        // }
        for (int i = 0; i < 10000; i++) {
            // foreach (Monkey monkey in testMonkeys) {
            //     monkey.InspectItems(testMonkeys);
            // }
            foreach (Monkey monkey in inputMonkeys) {
                monkey.InspectItems(inputMonkeys);
            }
        }
        // Console.WriteLine("");
        // int j = 0;
        // foreach (Monkey monkey in testMonkeys) {
        //     Console.WriteLine("Monkey {0} in testMonkeys had {1} number of inspections over 20 rounds",
        //         j,monkey.NumberOfInspections);
        //     j++;
        // }

        Console.WriteLine("");
        int j = 0;
        foreach (Monkey monkey in inputMonkeys) {
            Console.WriteLine("Monkey {0} in inputMonkeys had {1} number of inspections over 10000 rounds",
                j,monkey.NumberOfInspections);
            j++;
        }


        
    }   
}