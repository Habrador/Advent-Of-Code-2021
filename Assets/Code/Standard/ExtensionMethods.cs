using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{

    public static void Display(this List<char> charList, string message = "")
    {
        string displayString = message + " ";

        if (message.Length == 0)
        {
            displayString = "";
        }

        foreach (char c in charList)
        {
            displayString += c + " ";
        }

        Debug.Log(displayString);
    }


    public static void Display(this List<string> stringsList, string message = "")
    {
        string displayString = message + " ";

        if (message.Length == 0)
        {
            displayString = "";
        }

        foreach (string s in stringsList)
        {
            displayString += s + " ";
        }

        Debug.Log(displayString);
    }


    public static void Display(this int[,] int2dArray)
    {
        for (int row = 0; row < int2dArray.GetLength(0); row++)
        {
            string rowString = "";
        
            for (int col = 0; col < int2dArray.GetLength(0); col++)
            {
                rowString += int2dArray[row, col];

                if (col < int2dArray.GetLength(0) - 1)
                {
                    rowString += ", ";
                }
            }

            Debug.Log(rowString);
        }

        Debug.Log("-");
    }
}
