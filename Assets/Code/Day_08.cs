using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_08 : MonoBehaviour
{
    
    private void Start()
    {
        Part_1();
    }

    

    private void Part_1()
    {
        //Get the data 
        string[] allRowsString = FileManagement.GetInputData("Day_08", "input.txt");

        //Data consists of two columns separated by a | 
        //200 rows
        List<string[]> column_L = new List<string[]>();
        List<string[]> column_R = new List<string[]>();

        foreach (string row in allRowsString)
        {
            string[] columns = row.Split('|');

            //Test
            //Debug.Log(columns[0]);
            //Debug.Log(columns[1]);

            //break;

            //Each column consists of letter combinations (a-g) separated by " " (column 1 has 10 part-columns and column 2 has 4 part-columns)
            
            //Needs a separator array because using ' ' requires .Net 6.0
            string[] separator = { " " };

            string[] L = columns[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);
            string[] R = columns[1].Split(separator, StringSplitOptions.RemoveEmptyEntries);

            column_L.Add(L);
            column_R.Add(R);

            //Test
            //Debug.Log(L.Length);
            //Debug.Log(L[0]);
            //Debug.Log(L[L.Length - 1]);

            //Debug.Log(R.Length);
            //Debug.Log(R[0]);
            //Debug.Log(R[R.Length - 1]);
        }

        //Debug.Log(column_L.Count);
        //Debug.Log(column_R.Count);



        //This is the display:
        //  aaaa
        // b    c
        // b    c
        //  dddd 
        // e    f
        // e    f
        //  gggg
        //...if all letters are turned on forming the number 8
        //You can turn off letters to render a number: to render a 1, only segments c and f would be turned on; the rest would be off
        //This is called "Seven-segment display" and is what digital clocks use to display numbers 0-9

        //The submarine has a 4 digit display, so it can display 4 numbers using the above system
        //BUT this system is broken, so the wires to each segment in each display is randomly connected
        //All wires to a single segment in a single display are the same!

        //In the input data we have 10 unique signals in the first column and right column represents the 4 digit display
        //Each row is an entry. Within an entry, the same wire/segment connections are used
        //Your job is to use the left column to figure out what the 4 numbers in the right column are 
        //Clues:
        //2-letter combination: 1
        //3-letter combination: 7
        //4-letter combination: 4
        //7-letter combination: 8
        //If you see bg you know you want to display a 1 because only number 1 has 2 segments (= 2 wires) so that means that b and g should be connected to c and f instead of b and g (we still dont know if c is connected to b or g)


        //In the output values, how many times do digits 1, 4, 7, or 8 appear?

        int numberCounter = 0;

        foreach (string[] output in column_R)
        {
            foreach (string s in output)
            {
                if (s.Length == 2 || s.Length == 3 || s.Length == 4 || s.Length == 7)
                {
                    numberCounter += 1;
                }
            }
        }

        Debug.Log($"Number of digits that are 1, 4, 7, 8: {numberCounter}");
    }
}
