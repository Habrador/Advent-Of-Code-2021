using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_07 : MonoBehaviour
{
    
    private void Start()
    {
        //Part_1();
        Part_2();
    }



    private void Part_1()
    {
        int[] crabPositions = GetCrabPositions();


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

        //Debug.Log($"Max position: {maxPos}, Min position: {minPos}"); //1911, 0

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

        //Should be 343605
        Debug.Log($"Minimum fuel needed: {minFuelConsumption} to move to position: {optimalPos}");
    }



    private void Part_2()
    {
        int[] crabPositions = GetCrabPositions();

        //Basically the same as Part 1 but each step a crab moves costs +1 extra fuel: 
        //Step 1: 1
        //Step 2: 2
        //Step 3: 3

        //Debug.Log(GetFuelConsumption(11)); //66
        //Debug.Log(GetFuelConsumption(0)); //0

        
        int minFuelConsumption = Int32.MaxValue;
        int optimalPos = 0;

        for (int wantedPos = 0; wantedPos <= 1911; wantedPos++)
        {
            //Move all crabs to this position while counting how much fuel they need
            int fuelConsumptionAllCrabs = 0;

            for (int i = 0; i < crabPositions.Length; i++)
            {
                int pos = crabPositions[i];

                int distanceToMove = Mathf.Abs(wantedPos - pos);

                fuelConsumptionAllCrabs += GetFuelConsumption(distanceToMove);
            }

            if (fuelConsumptionAllCrabs < minFuelConsumption)
            {
                minFuelConsumption = fuelConsumptionAllCrabs;

                optimalPos = wantedPos;
            }
        }

        //Should be 96744904
        Debug.Log($"Minimum fuel needed: {minFuelConsumption} to move to position: {optimalPos}");
        
    }



    private int GetFuelConsumption(int distance)
    {
        //16 to 5 has distance 16-5 = 11
        //Fuel consumption is then 1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10 + 11 = 66

        int fuelConsumption = 0;

        for (int i = 1; i <= distance; i++)
        {
            fuelConsumption += i;
        }

        return fuelConsumption;
    }



    private int[] GetCrabPositions()
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

        return crabPositions;
    }
}
