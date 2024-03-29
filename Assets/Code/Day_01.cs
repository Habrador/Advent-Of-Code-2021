using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class Day_01 : MonoBehaviour
{
    
    private void Start()
    {
        //Part_1();
        Part_2();
    }



    private void Part_1()
    {
        string[] allRowsString = FileManagement.GetInputData("Day_01", "input.txt");

        //Convert from string to int
        int[] allRows = Array.ConvertAll(allRowsString, int.Parse);


        //Actual problem: "count the number of times a depth measurement increases from the previous measurement"

        int numberOfIncreases = 0;

        for (int i = 1; i < allRows.Length; i++)
        {
            int thisDepth = allRows[i];
            int previousDepth = allRows[i - 1];

            if (thisDepth > previousDepth)
            {
                numberOfIncreases += 1;
            }
        }

        Debug.Log($"Number of increases: { numberOfIncreases }");
    }



    private void Part_2()
    {
        string[] allRowsString = FileManagement.GetInputData("Day_01", "input.txt");

        //Convert from string to int
        int[] allRows = Array.ConvertAll(allRowsString, int.Parse);


        //Similar to first problem, but use the sum of 3 measurements

        //Convert to measurement windows
        List<int> allWindows = new List<int>();

        for (int i = 2; i < allRows.Length; i++)
        {
            int data_1 = allRows[i - 2];
            int data_2 = allRows[i - 1];
            int data_3 = allRows[i - 0];

            allWindows.Add(data_1 + data_2 + data_3);
        }


        int numberOfIncreases = 0;

        for (int i = 1; i < allWindows.Count; i++)
        {
            int thisDepth = allWindows[i];
            int previousDepth = allWindows[i - 1];

            if (thisDepth > previousDepth)
            {
                numberOfIncreases += 1;
            }
        }

        Debug.Log($"Number of increases: { numberOfIncreases }");
    }
}
