using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_07 : MonoBehaviour
{
    
    private void Start()
    {
        Part_1();
    }



    private void Part_1()
    {
        //Get the data 
        string[] allRowsString = FileManagement.GetInputData("Day_07", "input.txt");

        //Standardize
        //The input data is one row with ints that are comma-separated
        string[] dataStrings = allRowsString[0].Split(',');

        //Convert strings to int
        int[] crabPositions = Array.ConvertAll(dataStrings, int.Parse);

        //Test
        //Debug.Log(crabPositions.Length); //1000
        //Debug.Log(crabPositions[0]); //1101
        //Debug.Log(crabPositions[crabPositions.Length - 1]); //305


        //Move the crabs so the have the same horizontal position, while minimizing total fuel
        //Each change of 1 step in horizontal position of a single crab costs 1 fuel: "Move from 16 to 2: 14 fuel"

        //Find the max and min horizontal position and test all values to find the lowest total fuel
        int maxPos = -Int32.MaxValue;
        int minPos = Int32.MaxValue;

        for (int i = 0; i < crabPositions.Length; i++)
        {
            int pos = crabPositions[i];

            if (pos > maxPos)
            {
                maxPos = pos;
            }
            if (pos < minPos)
            {
                minPos = pos;
            }
        }

        Debug.Log($"Max position: {maxPos}, Min position: {minPos}"); //1911, 0

        int minFuelConsumption = Int32.MaxValue;
        int optimalPos = 0;

        for (int wantedPos = 0; wantedPos <= 1911; wantedPos++)
        {
            //Move all crabs to this position while counting how much fuel they need
            int fuelConsumption = 0;

            for (int i = 0; i < crabPositions.Length; i++)
            {
                int pos = crabPositions[i];

                fuelConsumption += Mathf.Abs(wantedPos - pos);
            }

            if (fuelConsumption < minFuelConsumption)
            {
                minFuelConsumption = fuelConsumption;

                optimalPos = wantedPos;
            }
        }


        Debug.Log($"Minimum fuel needed: {minFuelConsumption} to move to position: {optimalPos}");
    }
}
