using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class AoC_puzzle11 {
    
    public static (int,int) startPos = (-1,-1);
    public static (int,int) endPos = (-1,-1);

    
    /// <summary>
    /// Reads a input file with name given by input string, returns a tuple of the 
    /// input file as a 2D-array, each character converted to its ASCII-value, and the
    /// source- and endpoints of the algorithm.
    /// </summary>
    /// <param name="str">name of input string</param>
    /// <returns>a tuple of 2Darray of points, and a source- and end-position.</returns>
    public static (Point[,],(int,int),(int,int)) ReadFileOutputArray(string str) {
        // first we read through the file once just to establish length and width of
        // array.
        int lineLength = 0;
        int numberOfLines = 0;
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(str)) {
                string line;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null) {
                    lineLength = line.Length;
                    numberOfLines++;

                } 
            }
        }
        
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        // Now we can create the array, and begin to assign ASCII values:
        Point[,] outputArray = new Point[numberOfLines,lineLength];
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr2 = new StreamReader(str)) {
                string line2;
                int i = 0;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line2 = sr2.ReadLine()) != null) {
                    for (int j = 0; j < lineLength; j++) {
                        if (line2[j] == 'S') {
                            startPos = (i,j);
                            outputArray[i,j] = new Point((i,j),(int)'a');
                        }
                        else if (line2[j] == 'E') {
                            endPos = (i,j);
                            outputArray[i,j] = new Point((i,j),(int)'z');
                        }
                        else {
                            outputArray[i,j] = new Point((i,j),(int)line2[j]);
                        }
                    }
                    i++;
                } 
            }
        }
        
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read the second time:");
            Console.WriteLine(e.Message);
        }
        return (outputArray,startPos,endPos);
    }

    // ------------------------------- Part 1 -------------------------------------------
 
    // OK, so first we need to be able to find viable candidate moves from anywhere in the
    // array. The following method returns that:
    public static List<(int,int)> Candidates((int,int) currentPoint, Point[,] arr) {
        List<(int,int)> outputList = new List<(int, int)>();
        (int,int)[] candidateArray = new (int,int)[] {
            (currentPoint.Item1 - 1,currentPoint.Item2),
            (currentPoint.Item1 + 1,currentPoint.Item2),
            (currentPoint.Item1, currentPoint.Item2 - 1),
            (currentPoint.Item1, currentPoint.Item2 + 1)};
        foreach ((int,int) coord in candidateArray) {
            if (coord.Item1 >= 0 && coord.Item1 < arr.GetLength(0) && coord.Item2 >= 0 && coord.Item2 < arr.GetLength(1)) {
                if (arr[coord.Item1,coord.Item2].Value - 1 <= arr[currentPoint.Item1,currentPoint.Item2].Value) {
                    outputList.Add((coord.Item1,coord.Item2));
                }
            }
        }
        return outputList;
    }

    // OK, we'll try a Breadth-First Search (BFS) algorithm for finding the shortest path,
    // or at least finding the shortest distance. See p. 556, chapter 20 in Cormen et. al.
    // To do this we have to make sure that each point in the array has the appropriate properties.
    // Each point needs to have 1) a value to test if it is possible to go there, 2) a color,
    // white if not visited before, grey if under inspection and black if done, 3) a distance from source
    // vertex.
    public enum Color {
        White,
        Black,
        Gray
    }
    public class Point {
        private Color color;
        private uint sourceDistance;
        private int value;
        private (int,int) arrayPosition;

        public Color Color {
            get {return color;}
            set {color = value;}
        }
        public uint SourceDistance {
            get {return sourceDistance;}
            set {sourceDistance = value;}
        }
        public (int,int) Coordinates {
            get {return arrayPosition;}
        }
        public int Value {
            get {return value;}
        }

        public Point((int,int) arrayPosition, int value) {
            this.color = Color.White;
            this.sourceDistance = uint.MaxValue;
            this.arrayPosition = arrayPosition;
            this.value = value;
        }
    }

    // Now we make the BFS-algorithm, it will be very interesting to see how this goes!
    public static void BFS(Point[,] arr, (int,int) source, (int,int) end) {
        arr[source.Item1,source.Item2].Color = Color.Gray;
        arr[source.Item1,source.Item2].SourceDistance = 0;
        Queue<Point> queue = new Queue<Point>();
        queue.Enqueue(arr[source.Item1,source.Item2]);
        while (queue.Count() != 0) {
            Point u = queue.Dequeue();
            foreach ((int,int) candidate in Candidates(u.Coordinates, arr)) {
                if (arr[candidate.Item1,candidate.Item2].Color == Color.White) {
                    arr[candidate.Item1,candidate.Item2].Color = Color.Gray;
                    arr[candidate.Item1,candidate.Item2].SourceDistance = u.SourceDistance + 1;
                    queue.Enqueue(arr[candidate.Item1,candidate.Item2]);
                }
            }
            u.Color = Color.Black;
        }
    }
    
    public static void SetAllPointsWhite(Point[,] arr) {
        foreach (Point point in arr) {
            point.Color = Color.White;
        }
    }
    // In order to solve part 2, we need to search through all the possible starting positions.
    // This is done via the below method:
    public static uint ShortestRoute(Point[,] arr, (int,int) end) {
        SetAllPointsWhite(arr);
        uint shortestRoute = Int32.MaxValue;
        foreach (Point point in arr) {
            if (point.Value == (int)'a') {
                BFS(arr, (point.Coordinates.Item1,point.Coordinates.Item2), end);
                shortestRoute = System.Math.Min(shortestRoute,arr[end.Item1,end.Item2].SourceDistance);
                SetAllPointsWhite(arr);
            }
        }
        return shortestRoute;
    }
    

    public static void Main() {
        (Point[,],(int,int),(int,int)) input = ReadFileOutputArray("AoC_puzzle12.txt");

        (Point[,],(int,int),(int,int)) test = ReadFileOutputArray("AoC_puzzle12test.txt");
        Console.WriteLine("The third entry in first line testArray is: {0}",test.Item1[0,2].Coordinates);

        Console.WriteLine("The candidate moves from S in testArray are: {0}", 
            Candidates((0,0),test.Item1).Count());
        Console.WriteLine("Endpoint in test-array is: {0}",test.Item3);

        // OK, now we try on our test-array the BFS-algorithm and see:
        Point[,] testArray = test.Item1;
        BFS(testArray,test.Item2,test.Item3);
        (int,int) testEnd = test.Item3;
        Console.WriteLine("The shortest path from source to endpoint in testArray takes {0} number of steps",
            testArray[testEnd.Item1,testEnd.Item2].SourceDistance);

        // OK, time to try on our input-array:

        Point[,] inputArray = input.Item1;
        BFS(inputArray,input.Item2,input.Item3);
        (int,int) inputEnd = input.Item3;
        Console.WriteLine("The shortest path from source to endpoint in inputArray takes {0} number of steps",
            inputArray[inputEnd.Item1,inputEnd.Item2].SourceDistance);

        // Part 2: OK we try to find the shortest route possible:

        Console.WriteLine("The shortest possible round starting from a place of 'a' in testArray is: {0}",
            ShortestRoute(testArray,testEnd));

        Console.WriteLine("The shortest possible round starting from a place of 'a' in inputArray is: {0}",
            ShortestRoute(inputArray,inputEnd));    







    }   
}