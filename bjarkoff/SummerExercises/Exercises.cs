// See https://aka.ms/new-console-template for more information
using System;
using System.Reflection;
public class Program
{

    // En rekursiv funktion, der udregner antal 1'taller i den binære representation
    // af en int i. Der er åbenbart et køretidsproblem for en af de tests, som 
    public static int CountOnes(int i)
    {
		if (i == 0) {
		    return 0;
			}	
		int j = 1;
		while (2*j <= i) {
		    j = 2*j;
		}
		return 1 + CountOnes(i-j);		
    }		

    // En alternativ implementation af CountOnes.
    public static int AlternativeCountOnes(int i) {
        string binaryString = System.Convert.ToString(i,2);
        int counter = 0;
        for (int index = 0; index < binaryString.Length; index++) {
            if (binaryString[index].ToString() == "1") {
                counter++;
            }
        }
        return counter;
    }

    


    // Nu skal vi noget med char upper og lower shit. Vi skal bytte rundt på upper og
    // lower, hvis der altså er sådan en af delene. Det gøres nemt sådan:

    public static int CounterpartCharCode(char symbol) {
		if (char.IsUpper(symbol)) {
			return (int)char.ToLower(symbol); 	
		}
		return (int)char.ToUpper(symbol);
    }

    // Nu skal vi lave en funktion, som tjekker om to forskellige objekter er ens.
    // Her bruger vi åbenbart system.reflections.

    public static bool CheckEquality(object a, object b)
	{
		return a.Equals(b);
	}

    // Nu skal vi lave et array af relevante indices i en streng, altså de indices,
    // hvor char er upper:

    public static int[] IndexOfCapitals(string str) 
    {
		List<int> indexList = new List<int>();
		for (int i = 0; i<str.Length; i++) {
			if (char.IsUpper(str[i])) {
				indexList.Add(i);
			}	
		}
		return indexList.ToArray();
    }

    // Nu skal vi fjerne alle vokaler fra en input string, meget sjovt.

    public static string Disemvowel(string str)
    {
      string outputString = "";
      for (int index = 0; index < str.Length; index++) {
        if ("aeiouAEIOU".IndexOf(str[index]) < 0) {
          outputString += str[index];
        }
      }  
      return outputString;
    }

    // NU skal vi teste om en streng er et isogram, i.e. en ting, der indeholder gentagne 
    // characters.

    public static bool IsIsogram(string str) {
        // Code on you crazy diamond!
        string upperCaseString = str.ToUpperInvariant();
        bool outputBool = true;
        foreach (char character in upperCaseString) {
            int strLengthBefore = upperCaseString.Length;
            int strLengthAfter = (upperCaseString.Replace(character.ToString(),"")).Length;   
            if (strLengthBefore - strLengthAfter > 1) {
                outputBool = false;
            }
        }
        return outputBool;
    }
    
    public static void Main(string[] args) {
        Console.WriteLine($"The two methods yield the same result for input 3234783: {CountOnes(3234783) == AlternativeCountOnes(3234783)}");
        Console.WriteLine($"Countones-method works for number with many i's: {CountOnes(1073741823)}");
        Console.WriteLine(Disemvowel("AAbcdaa"));
        Console.WriteLine(IsIsogram("JajA"));
    }

}
