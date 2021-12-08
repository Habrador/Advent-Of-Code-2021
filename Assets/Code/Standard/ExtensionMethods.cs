using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{

    public static void Display(this List<char> charList, string message = "")
    {
        string displayString = message + " ";

        foreach (char c in charList)
        {
            displayString += c + " ";
        }

        Debug.Log(displayString);
    }
}
