using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_09 : MonoBehaviour
{
    
    private void Start()
    {
        Part_1();
    }

    
   
    private void Part_1()
    {
        //Get the data 
        string[] allRowsString = FileManagement.GetInputData("Day_09", "input.txt");

        //The data consists of numbers in rows (not comma separated and no spaces between) 100x100
        //Debug.Log(allRowsString.Length);
        //Debug.Log(allRowsString[0].Length);

        int[,] map = new int[allRowsString.Length, allRowsString[0].Length];

        for (int row = 0; row < allRowsString.Length; row++)
        {
            string thisRow = allRowsString[row];

            for (int col = 0; col < thisRow.Length; col++)
            {
                map[row, col] = thisRow[col] - '0';
            }
        }

        Debug.Log(map[0, 0]); //7
        Debug.Log(map[0, 99]); //5
        Debug.Log(map[99, 0]); //6
        Debug.Log(map[99, 99]); //9
    }
}
