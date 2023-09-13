using System;


namespace CSVreader;
public class CSVReaderClass{

    public static int CharToInt(char cha) {
        switch (cha) {
            case '0':
                return 0;
                break;
            case '1':
                return 1;
                break;
            case '2':
                return 2;
                break;
            case '3':
                return 3;
                break;
            case '4':
                return 4;
                break;
            case '5':
                return 5;
                break;
            case '6':
                return 6;
                break;
            case '7':
                return 7;
                break;
            case '8':
                return 8;
                break;
            case '9':
                return 9;
                break;
            default:
                return Int32.MinValue;
                break;
        }
    }
    // recursive method that parses a string and transforms it to an int.
    public static int StringToInt(string str) {
        if (str.Length == 0) {
            return 0;
        }
        else if (str[0] == '-') {
            return (-1)*StringToInt(str.Substring(1));
        }
        else if ((int)str[str.Length - 1] >= 48 && (int)str[str.Length - 1] <= 57) {
            return CharToInt(str[str.Length - 1])+ 10*StringToInt(str.Remove(str.Length - 1));
        }
        else {
            return StringToInt(str.Remove(str.Length - 1));
        }
    }

    public static int[] StringToIntArray(string str) {
        string[] stringArray = str.Split(',');
        int[] intArray = new int[stringArray.Length];
        for (int i = 0; i < intArray.Length; i++) {
            intArray[i] = StringToInt(stringArray[i]);
        }
        return intArray;
    }


    public static void Main() {
        Console.WriteLine("Hello, world!");
    }
}

