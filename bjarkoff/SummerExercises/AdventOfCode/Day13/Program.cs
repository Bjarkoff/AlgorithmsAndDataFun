using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class AoC_puzzle13 {
    
    // OK, we need to have possibly very nested lists. Therefore I think a recursive 
    // class is needed. So first I think we need to define a interface for comparing stuff:


    public class StandardInt : IComparable {
        private int value;

        public int Value {
            get {return value;}
        }
        public StandardInt(int value) {
            this.value = value;
        }
        public int CompareTo (object compare) {
            if (compare is StandardInt standardInt) {
                return value.CompareTo(standardInt.Value);
                // this means -1 will indicate correct order (int a is lower than int b => a.CompareTo(b) = -1)
            }
            else if (compare is NestedList nested) {
                NestedList nestedList = new NestedList();
                nestedList.AddToList(new StandardInt(value));
                return nestedList.CompareTo(nested);
            }
            else {
                Console.WriteLine(compare);
                throw new ArgumentException("Input in CompareTo was not an expected type!");
            }
            return Int32.MinValue;
        }
    }
    public class NestedList : IComparable {
        private List<IComparable> list;
        private NestedList parent;
        private int length;

        public int Length {
            get {return length;}
            set {length = value;}
        }

        public NestedList Parent {
            get {return parent;}
            set {parent = value;}
        }

        public List<IComparable> List {
            get {return list;}
            set {list = value;}
        }
        public NestedList() {
            this.list = new List<IComparable>();
        }
        public NestedList(NestedList parent) {
            this.list = new List<IComparable>();
            this.parent = parent;
        }
        public int CompareTo (object compare) {
            if (compare is StandardInt standardInt) {
                NestedList nestedList = new NestedList();
                nestedList.AddToList(standardInt);
                return this.CompareTo(nestedList);
            }
            else if (compare is NestedList nested) {
                for (int i = 0; i < System.Math.Min(nested.List.Count(),this.List.Count()); i++) {
                    if (this.list[i].CompareTo(nested.List[i]) != 0) {
                        return this.list[i].CompareTo(nested.List[i]);
                    }
                }
                if (this.list.Count() < nested.List.Count()) {
                    // -1 indicates things are in correct order
                    return -1;
                }
                else if (this.list.Count() > nested.List.Count()) {
                    return 1;
                }
                return 0;
            }
            else {
                throw new ArgumentException("Input in CompareTo was not an expected type!");
            }
            return Int32.MinValue;
        }
        public void AddToList(IComparable comparable) {
            this.list.Add(comparable);
            if (comparable is NestedList nestedList) {
                nestedList.Parent = this;
            }
        }

        public override string ToString() {
            string outputString = "[";
            for (int i = 0; i < this.list.Count(); i++) {
                if (this.list[i] is StandardInt standardInt) {
                    if (i < this.list.Count() - 1) {
                        outputString += standardInt.Value.ToString() + ',';
                    }
                    else {
                        outputString += standardInt.Value.ToString();
                    }
                }
                else if (this.list[i] is NestedList nestedList) {
                    if (i < this.list.Count() - 1) {
                    outputString += nestedList.ToString() + ',';
                    }
                    else {
                        outputString += nestedList.ToString();
                    }
                }
            }
            return outputString + ']';
        }
    }
        
    

    // OK right now my method for reading strings and creating NestedLists
    // is getting out of hand, better to start from scratch!
    // So, what do I want it to do:
    // 0) I want to iterate through the input string in appropriate fashion.
    // That is, it might need to jump a number of steps forward, depending on the input.
    // 1) if it meets a range of normal integers, it should just add those to
    // the NestedLists list.
    // 2) If it meets '[', then it should add a NestedList to the current NestedList.
    // this call must be recursive. It should then jump over all these steps in the input string.
    // therefore the method needs to be able to output the length of characters also.
    // 3) If it meets just single ']', then this method must break and output. Because any 
    // '[' that is met will start a new list.

    // Note that the input string below, str, must be line.Substring(1); since the first character
    // is always '[' in the input .txt file, it should not be included in input when using the below method.
    public static (NestedList, int) ReadLineOutputNestedList(string str) {
        NestedList outputList = new NestedList();
        bool terminate = false;
        int index = 0; 
        int length = 2; // a nested list always at least length 2
        while (index < str.Length && !terminate) {
            switch (str[index]) {
                case '[':
                    (NestedList,int) listToAdd = ReadLineOutputNestedList(str.Substring(index+1));
                    outputList.AddToList(listToAdd.Item1);
                    index += listToAdd.Item2;
                    length += listToAdd.Item2;
                    // do something, make sure index+length are increased the right amount
                    break;
                case ',':
                    // maybe do something, maybe not, maybe takes takes takes Qxh1 and mate
                    index++;
                    length++;
                    break;
                case ']':
                    // here the whole thing method must terminate
                    terminate = true;
                    index++;
                    // length not increased since it started at 2
                    break;
                default:
                    // Here we must have an integer, so we need to add this to the list.
                    string intString = "";
                    while (str[index] != ',' && str[index] != ']') {
                        intString += str[index];
                        index++;
                        length++;
                    }
                    outputList.AddToList(new StandardInt(Int32.Parse(intString)));
                    break;
            }
        }
        outputList.Length = length;
        return (outputList,length);
    }


    /// <summary>
    /// Reads a input file with name given by input string, returns a tuple of the 
    /// input file as a 2D-array, each character converted to its ASCII-value, and the
    /// source- and endpoints of the algorithm.
    /// </summary>
    /// <param name="str">name of input string</param>
    /// <returns>a tuple of 2Darray of points, and a source- and end-position.</returns>
    public static List<(NestedList,NestedList)> ReadFileOutputNestedListPairs(string str) {
        // first we read through the file once just to establish length and width of
        // array.
        List<(NestedList,NestedList)> outputList = new List<(NestedList, NestedList)>();
        (NestedList,NestedList) nestedListPair = (new NestedList(),new NestedList());
        bool firstItemOccupied = false;
        
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(str)) {
                string line;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null) {
                    if (line != "" && !firstItemOccupied) {
                        nestedListPair.Item1 = ReadLineOutputNestedList(line.Substring(1)).Item1;
                        // substring(1), because that is how we count the lengths of our
                        // stupid NestedLists
                        firstItemOccupied = true;
                    }
                    else if (line != "") {
                        nestedListPair.Item2 = ReadLineOutputNestedList(line.Substring(1)).Item1;
                        outputList.Add(nestedListPair);
                        firstItemOccupied = false;
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

    // ------------------------------- Part 2 -------------------------------------------
 
   
   // Method for returning the sum of indices (first index is 1) of all ordered pairs 
    public static int SumOfIndicesOfOrderedPairs(List<(NestedList,NestedList)> lst) {
        int outputInt = 0;
        for (int i = 0; i < lst.Count(); i++) {
            if (lst[i].Item1.CompareTo(lst[i].Item2) == -1) {
                outputInt += i+1;
            }
        }
        return outputInt;
    }
    // Method for creating an ordered list of NestedLists, ordered in the sense defined by
    // CompareTo-methods
    public static List<NestedList> OrderedNestedLists(List<NestedList> lst) {
        List<NestedList> sortedList = new List<NestedList>();
        sortedList.Add(lst[0]);
        for (int i = 1; i < lst.Count(); i++) {
            bool hasBeenInsertedToSortedList = false;
            for (int j = 0; j < sortedList.Count(); j++) {
                if (lst[i].CompareTo(sortedList[j]) == -1) {
                    sortedList.Insert(j,lst[i]);
                    hasBeenInsertedToSortedList = true;
                    break;
                }
            }
            if (!hasBeenInsertedToSortedList) {
                sortedList.Add(lst[i]);
            }
        }
        return sortedList;
    }
    // Method for finding the indices of the two items, item1 and item2, in input list of NestedLists.
    public static (int,int) FindPairOfIndices(List<NestedList> lst, NestedList item1, NestedList item2) {
        (int,int) outputTuple = (-1,-1);
        for (int i = 0; i < lst.Count(); i++) {
            if (lst[i].CompareTo(item1) == 0) {
                outputTuple.Item1 = i+1;
            }
            else if (lst[i].CompareTo(item2) == 0) {
                outputTuple.Item2 = i+1;
            }
        }
        return outputTuple;
    }

     


    

    public static void Main() {
        List<(NestedList,NestedList)> testLists = ReadFileOutputNestedListPairs("AoC_puzzle13test.txt");
        List<(NestedList,NestedList)> inputLists = ReadFileOutputNestedListPairs("AoC_puzzle13.txt");
        Console.WriteLine("The second item of second pair in testLists has {0} characters",
            testLists[1].Item2.Length);
        Console.WriteLine("The first list in the first pair looks like this on string-form: {0}",
            testLists[7].Item1.ToString());
        
        // int i = 1;
        // foreach ((NestedList,NestedList) tuple in testLists) {
        //     Console.WriteLine("Pair {0} returns {1} when comparing them", 
        //         i,tuple.Item1.CompareTo(tuple.Item2));
        //     i++;
        // }
        Console.WriteLine("Sum of ordered indices in testLists is: {0}", SumOfIndicesOfOrderedPairs(testLists)); 
        Console.WriteLine("Sum of ordered indices in inputLists is: {0}", SumOfIndicesOfOrderedPairs(inputLists)); 

        // ------------------- Part 2 ------------------------
        NestedList divider1 = new NestedList();
        NestedList helper1 = new NestedList();
        helper1.AddToList(new StandardInt(2));
        divider1.AddToList(helper1);

        NestedList divider2 = new NestedList();
        NestedList helper2 = new NestedList();
        helper2.AddToList(new StandardInt(6));
        divider2.AddToList(helper2);

        List<NestedList> testInputList = new List<NestedList>();
        testInputList.Add(divider1);
        testInputList.Add(divider2);
        foreach ((NestedList,NestedList) tuple in testLists) {
            testInputList.Add(tuple.Item1);
            testInputList.Add(tuple.Item2);
        }
        List<NestedList> testList = OrderedNestedLists(testInputList);
        // foreach(NestedList lst in testList) {
        //     Console.WriteLine(lst.ToString());
        // }
        // Console.WriteLine("The indices of dividers are {0} in testList",
        //     FindPairOfIndices(testList,divider1,divider2));

        List<NestedList> inputInputList = new List<NestedList>();
        inputInputList.Add(divider1);
        inputInputList.Add(divider2);
        foreach ((NestedList,NestedList) tuple in inputLists) {
            inputInputList.Add(tuple.Item1);
            inputInputList.Add(tuple.Item2);
        }
        List<NestedList> inputList = OrderedNestedLists(inputInputList);

        Console.WriteLine("The product of dividers are {0} in testList",
            FindPairOfIndices(testList,divider1,divider2).Item1*
            FindPairOfIndices(testList,divider1,divider2).Item2);
        
        Console.WriteLine("The product of dividers are {0} in inputList",
            FindPairOfIndices(inputList,divider1,divider2).Item1*
            FindPairOfIndices(inputList,divider1,divider2).Item2);
        
    }   
}