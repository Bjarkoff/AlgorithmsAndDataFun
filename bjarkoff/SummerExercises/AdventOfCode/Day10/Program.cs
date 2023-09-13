using System;
using System.IO;
using System.Collections.Generic;

class AoC_puzzle10 {
    
    public const string NO_OPERATION = "noop";
    public const string ADD = "addx";
    /// <summary>
    /// Reads a input file with name given by input string, returns (string,int)-tuples in a list,
    /// where each entry in list corresponds to each line in input .txt-file.
    /// </summary>
    /// <param name="str">name of input string</param>
    /// <returns>a list of (char,int)-tuples</returns>
    public static List<(string,int)> ReadFileOutputCommandList(string str) {
        List<(string,int)> outputList = new List<(string,int)>();
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(str))
            {
                string line;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null) {
                    if (line.Length <= 5) {
                        outputList.Add( (line.Substring(0,4),0)) ;
                    }
                    else {
                        outputList.Add( (line.Substring(0,4),Int32.Parse(line.Substring(5))) );
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
        return outputList;
    }

    // -------------------------- Part 1 ---------------------------------
    // To set up the solution, we note that each operation has a different runtime,
    // and that the effect of the method only occurs AFTER the runtime has passed.
    // What we need is to access this property of the CPU at any given input-time.

    public class CPU {
        private List<int> registerList;
        private string ctuString;
        private int register;
        private int CTU;
        
        public string CTUString {
            get {return ctuString;}
        }

        public CPU() {
            this.registerList = new List<int>();
            this.ctuString = "#";
            this.CTU = 1;
            this.register = 1;
            registerList.Add(register);
            // Add one more time, so that the value at some integer "time",
            // will be found in registerList at index "time".
            registerList.Add(register);
        }
        // Method that returns the value of the register right AFTER time,
        // that is, right before (time-1).
        public int GetRegister(int time) {
            return registerList[time];
        }
        // Method to increase the CTU-value:
        public void IncreaseCTU() {
            this.CTU = (this.CTU + 1) % 40;
        }
        // Method to add to the CTUstring:
        public void UpdateCTUString() {
            if (this.CTU == 0) {
                ctuString += "\n";
            }
            if (this.CTU > this.register - 2 && this.CTU < this.register + 2) {
                ctuString += "#";
            }
            else {
                ctuString += ".";
            }
        }


        // No change to the value of register, one cycle is added.
        public void Noop() {
            UpdateCTUString();
            IncreaseCTU();
            registerList.Add(register); 
        }
        // Over a time of two cycles, register-value is increased by value.
        public void AddX(int value) {
            registerList.Add(register); 
            UpdateCTUString();
            IncreaseCTU();
            register += value;
            registerList.Add(register); 
            UpdateCTUString();
            IncreaseCTU();
        }

        // Method that executes a list of instructions.
        public void ExecuteInstructions(List<(string,int)> lst) {
            foreach ((string,int) tuple in lst) {
                switch (tuple.Item1) {
                    case NO_OPERATION:
                        this.Noop();
                        break;
                    case ADD:
                        this.AddX(tuple.Item2);
                        break;
                    default:
                        Console.WriteLine("Input string {0} in ExecuteInstructions() unknown",
                            tuple.Item1);
                        break;
                }
            }
        }
        // Method that returns the signal strength at time, as 
        // time multiplied by the register-value at that time.
        public int SignalStrength(int time) {
            return time*registerList[time];
        }

        public int SumOfSignalStrengths(int threshold) {
            int output = 0;
            for (int i = 20; i<=threshold; i += 40) {
                output += this.SignalStrength(i);
            }
            return output;
        }


    }

    public static void Main() {
        
        List<(string,int)> input = ReadFileOutputCommandList("AoC_puzzle10.txt");
        List<(string,int)> testInput = ReadFileOutputCommandList("AoC_puzzle10test.txt");

        Console.WriteLine("første input i testInput er: {0}", testInput[0]);
        CPU testCPU = new CPU();
        CPU theCPU = new CPU();
        Console.WriteLine("Length of testInput is: {0}",testInput.Count());

        testCPU.ExecuteInstructions(testInput);
        Console.WriteLine("SumOfSignalStrengths up to 220'th cycle for testInput er: {0}",
            testCPU.SumOfSignalStrengths(220));

        theCPU.ExecuteInstructions(input);
        Console.WriteLine("SumOfSignalStrengths up to 220'th cycle for testInput er: {0}",
            theCPU.SumOfSignalStrengths(220));

        Console.WriteLine("\n And the CTUstring output for testCPU is: \n{0}",
            testCPU.CTUString);
        Console.WriteLine("\n And the CTUstring output for theCPU is: \n{0}",
            theCPU.CTUString);


        
    }   
}