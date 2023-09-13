using System;
using System.IO;
using System.Collections.Generic;

class AoC_puzzle9 {
    
    
    /// <summary>
    /// Reads a input file with name given by input string, returns (char,int)-tuples in a list,
    /// where each entry in list corresponds to each line in input .txt-file.
    /// </summary>
    /// <param name="str">name of input string</param>
    /// <returns>a list of (char,int)-tuples</returns>
    public static List<(char,uint)> ReadFileOutputCommandList(string str) {
        List<(char,uint)> outputList = new List<(char,uint)>();
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
                    outputList.Add( (line[0],UInt32.Parse(line.Substring(2))) );
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

    // Now we will make a class for the rope. The rope contains a head and a tail, 
    // and we want to be able to move the head. When the head moves, the tail has to
    // follow as prescribed in the assignment text. Furthermore, we want to be able to
    // not only move in increments of 1, but of any int in any direction. Finally, we want
    // to be able to backtrack all the places/sites that the tail has visited.
    public class ShortRope {
        (int,int) head;
        (int,int) tail;
        List<(int,int)> tailSites;
        public (int,int) Head {
            get {return head;}
        }
        public (int,int) Tail {
            get {return tail;}
        }
        public List<(int,int)> TailSites {
            get {return tailSites;}
        }

        // Default rope starts in (0,0):
        public ShortRope() {
            head = (0,0);
            tail = (0,0);
            tailSites = new List<(int,int)>();
            tailSites.Add(tail);
        }

        public void MoveHead(char cha) {
            switch (cha) {
                case 'R':
                    head.Item1++;
                    break;
                case 'L':
                    head.Item1--;
                    break;
                case 'U':
                    head.Item2++;
                    break;
                case 'D':
                    head.Item2--;
                    break;
                default:
                    Console.WriteLine("Unknown input character in Rope.Move(char)-method");
                    break;
            }
            UpdateTail();
        }
        public void MoveHead(char cha, uint amount) {
            uint loopInt = amount;
            while (loopInt > 0) {
                MoveHead(cha);
                loopInt--;
            }
        }
        public void MoveHead(List<(char,uint)> list) {
            foreach ((char,uint) tuple in list) {
                MoveHead(tuple.Item1,tuple.Item2);
            }
        }
        private void UpdateTailSites((int,int) pos) {
            if (!tailSites.Contains(pos)) {
                tailSites.Add(pos);
            }
        }


        private bool TailMoveRightUp() {
            return (head.Item1 > tail.Item1+1 && head.Item2 > tail.Item2) || 
                (head.Item1 > tail.Item1 && head.Item2 > tail.Item2+1);
        }
        private bool TailMoveRightDown() {
            return (head.Item1 > tail.Item1+1 && head.Item2 < tail.Item2) || 
                (head.Item1 > tail.Item1 && head.Item2+1 < tail.Item2);
        }
        private bool TailMoveLeftUp() {
            return (head.Item1+1 < tail.Item1 && head.Item2 > tail.Item2) || 
                (head.Item1 < tail.Item1 && head.Item2 > tail.Item2+1);
        }
        private bool TailMoveLeftDown() {
            return (head.Item1+1 < tail.Item1 && head.Item2 < tail.Item2) || 
                (head.Item1 < tail.Item1 && head.Item2+1 < tail.Item2);
        }
        private bool TailMoveLeft() {
            return (head.Item1+1 < tail.Item1 && head.Item2 == tail.Item2);
        }
        private bool TailMoveRight() {
            return (head.Item1 > tail.Item1+1 && head.Item2 == tail.Item2);
        }
        private bool TailMoveUp() {
            return (head.Item1 == tail.Item1 && head.Item2 > tail.Item2+1);
        }
        private bool TailMoveDown() {
            return (head.Item1 == tail.Item1 && head.Item2+1 < tail.Item2);
        }
        private void UpdateTail() {
            if (TailMoveRightUp()) {
                tail.Item1++;
                tail.Item2++;
                UpdateTailSites((tail.Item1,tail.Item2));
            }
            else if (TailMoveRightDown()) {
                tail.Item1++;
                tail.Item2--;
                UpdateTailSites((tail.Item1,tail.Item2));
            }
            else if (TailMoveLeftUp()) {
                tail.Item1--;
                tail.Item2++;
                UpdateTailSites((tail.Item1,tail.Item2));
            }
            else if (TailMoveLeftDown()) {
                tail.Item1--;
                tail.Item2--;
                UpdateTailSites((tail.Item1,tail.Item2));
            }
            else if (TailMoveLeft()) {
                tail.Item1--;
                UpdateTailSites((tail.Item1,tail.Item2));
            }
            else if (TailMoveRight()) {
                tail.Item1++;
                UpdateTailSites((tail.Item1,tail.Item2));
            }
            else if (TailMoveUp()) {
                tail.Item2++;
                UpdateTailSites((tail.Item1,tail.Item2));
            }   
            else if (TailMoveDown()) {
                tail.Item2--;
                UpdateTailSites((tail.Item1,tail.Item2));
            }
        }
    }

    
    
    
    
    
    
    
    
    
    
    
    // Part two:
    // It turns out that for a longer rope, it is probably
    // more sensible to make a knot-class, and then a rope-class consisting
    // of a number of knots.

    // A knot should have a head that it follows except if it is itself the head
    // of the rope.
    public class Knot {
        private Knot head;
        private (int,int) position;
        private List<(int,int)> whereabouts;
        public (int,int) Position {
            get {return this.position;}
        }
        public List<(int,int)> Whereabouts {
            get {return this.whereabouts;}
        }
        // the head-Knot's position can be set.
        // head-Knot's field "head" remains null.
        public Knot((int,int) pos) {
            this.position = pos;
            whereabouts = new List<(int, int)>();
            whereabouts.Add(this.Position);
        }
        // Default knot has position equal to its head's.
        // But head has to be set in constructor.
        public Knot(Knot head) {
            this.head = head;
            this.position = head.Position;
            whereabouts = new List<(int, int)>();
            whereabouts.Add(this.position);
        }

        private void UpdateWhereabouts((int,int) pos) {
            if (!whereabouts.Contains(pos)) {
                whereabouts.Add(pos);
            }
        }
        public void Move(char cha) {
            switch (cha) {
                case 'R':
                    this.position.Item1++;
                    break;
                case 'L':
                    this.position.Item1--;
                    break;
                case 'U':
                    this.position.Item2++;
                    break;
                case 'D':
                    this.position.Item2--;
                    break;
                default:
                    Console.WriteLine("Unknown input character in Knot.Move(char)-method");
                    break;
            }
        }
        public void Move(char cha, uint amount) {
            uint loopInt = amount;
            while (loopInt > 0) {
                Move(cha);
                loopInt--;
            }
        }
        public void Move(List<(char,uint)> list) {
            foreach ((char,uint) tuple in list) {
                Move(tuple.Item1,tuple.Item2);
            }
        }

        public void UpdatePosition() {
            if (this.head != null) {
                if (MoveRightUp()) {
                    this.position.Item1++;
                    this.position.Item2++;
                    UpdateWhereabouts((position.Item1,position.Item2));
                }
                else if (MoveRightDown()) {
                    position.Item1++;
                    position.Item2--;
                    UpdateWhereabouts((position.Item1,position.Item2));
                }
                else if (MoveLeftUp()) {
                    position.Item1--;
                    position.Item2++;
                    UpdateWhereabouts((position.Item1,position.Item2));
                }
                else if (MoveLeftDown()) {
                    position.Item1--;
                    position.Item2--;
                    UpdateWhereabouts((position.Item1,position.Item2));
                }
                else if (MoveLeft()) {
                    position.Item1--;
                    UpdateWhereabouts((position.Item1,position.Item2));
                }
                else if (MoveRight()) {
                    position.Item1++;
                    UpdateWhereabouts((position.Item1,position.Item2));
                }
                else if (MoveUp()) {
                    position.Item2++;
                    UpdateWhereabouts((position.Item1,position.Item2));
                }   
                else if (MoveDown()) {
                    position.Item2--;
                    UpdateWhereabouts((position.Item1,position.Item2));
                }
            }
        }

        private bool MoveRightUp() {
            return (head.Position.Item1 > position.Item1+1 && head.Position.Item2 > position.Item2) || 
                (head.Position.Item1 > position.Item1 && head.Position.Item2 > position.Item2+1);
        }
        private bool MoveRightDown() {
            return (head.Position.Item1 > position.Item1+1 && head.Position.Item2 < position.Item2) || 
                (head.Position.Item1 > position.Item1 && head.Position.Item2+1 < position.Item2);
        }
        private bool MoveLeftUp() {
            return (head.Position.Item1+1 < position.Item1 && head.Position.Item2 > position.Item2) || 
                (head.Position.Item1 < position.Item1 && head.Position.Item2 > position.Item2+1);
        }
        private bool MoveLeftDown() {
            return (head.Position.Item1+1 < position.Item1 && head.Position.Item2 < position.Item2) || 
                (head.Position.Item1 < position.Item1 && head.Position.Item2+1 < position.Item2);
        }
        private bool MoveLeft() {
            return (head.Position.Item1+1 < position.Item1 && head.Position.Item2 == position.Item2);
        }
        private bool MoveRight() {
            return (head.Position.Item1 > position.Item1+1 && head.Position.Item2 == position.Item2);
        }
        private bool MoveUp() {
            return (head.Position.Item1 == position.Item1 && head.Position.Item2 > position.Item2+1);
        }
        private bool MoveDown() {
            return (head.Position.Item1 == position.Item1 && head.Position.Item2+1 < position.Item2);
        }
    }

    // Now for the Rope-class, which is just a container for knots.
    // One head and a bunch of other knots.
    public class Rope {
        private Knot[] knots;
        public Knot[] Knots {
            get {return knots;}
        }
        public Knot Head {
            get {return knots[0];}
        }

        // default Rope starts with all knots in (0,0).
        public Rope(uint numberOfKnots) {
            this.knots = new Knot[numberOfKnots];
            knots[0] = new Knot((0,0));   
            for (int i = 1; i<numberOfKnots; i++) {
                knots[i] = new Knot(knots[i-1]);
            } 
        }

        public void Move(char cha) {
            this.Head.Move(cha);
            for (int i = 1; i<knots.Length; i++) {
                knots[i].UpdatePosition();
            }
        }

        public void Move(char cha, uint amount) {
            uint loopInt = amount;
            while (loopInt > 0) {
                Move(cha);
                loopInt--;
            }
        }
        public void Move(List<(char,uint)> list) {
            foreach ((char,uint) tuple in list) {
                Move(tuple.Item1,tuple.Item2);
            }
        }      
    }

    public static void Main() {
        
        List<(char,uint)> input = ReadFileOutputCommandList("AoC_puzzle9.txt");
        List<(char,uint)> testInput = ReadFileOutputCommandList("AoC_puzzle9test.txt");
        
        ShortRope testRope = new ShortRope();
        ShortRope rope = new ShortRope();
        // Now we move the testRope:
        testRope.MoveHead(testInput);
        Console.WriteLine("After moving the testRope, the tail visited {0} number of sites.",
            testRope.TailSites.Count());
        Console.WriteLine("");
        // Now we move the real rope:
        rope.MoveHead(input);
        Console.WriteLine("After moving the real rope, the tail visited {0} number of sites.",
            rope.TailSites.Count());
        
        Console.WriteLine("");
        List<(char,uint)> testInput2 = ReadFileOutputCommandList("AoC_puzzle9test2.txt");
        Rope testRope2 = new Rope(10);
        // Now we move the test rope 2:
        testRope2.Move(testInput2);
        Console.WriteLine("After moving the testRope2, the tail visited {0} number of sites.",
            testRope2.Knots[testRope2.Knots.Length-1].Whereabouts.Count());
        
        Console.WriteLine("");
        Rope rope2 = new Rope(10);
        // Now we move the test rope 2:
        rope2.Move(input);
        Console.WriteLine("After moving the rope2, the tail visited {0} number of sites.",
            rope2.Knots[rope2.Knots.Length-1].Whereabouts.Count());


    }   
}