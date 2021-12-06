using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_06 : MonoBehaviour
{
    
    private void Start()
    {
        //Part_1();

        Part_2();
    }

    
    
    private void Part_1()
    {
        //Get the input data
        List<int> fishAges = GetData();


        //Simulate the lives of Lanternfish

        //Rules:
        // - Each lanternfish creates a new lanternfish once every 7 days
        // - Each fish is modeled as a single number that represents the number of days until it creates a new lanternfish
        // - A new lanternfish needs longer before it's capable of producing more lanternfish: two more days for its first cycle
        // - After reaching day 0, its timer is reset to 6, and it creates and new fish with internal timer 8

        //Simulate 80 days and count the number of fishes


        for (int day = 0; day < 80; day++)
        {
            List<int> newFishes = new List<int>();
        
            for (int i = 0; i < fishAges.Count; i++)
            {
                int fishAge = fishAges[i];

                fishAge -= 1;

                //Fish gets a baby
                if (fishAge < 0)
                {
                    fishAge = 6;

                    newFishes.Add(8);
                }

                fishAges[i] = fishAge;
            }

            //Add new fishes
            fishAges.AddRange(newFishes);
        }

        //Should be 386536
        Debug.Log($"Number of fishes: {fishAges.Count}");
    }



    //Basically the same as Part 1 but we have to simulate 256 days, meaning that we will end up with many fishes and will run out of memory if we simulate all of them at the same time
    private void Part_2()
    {
        //Get the input data
        List<int> fishAges = GetData();


        //Simulate fish-by-fish

        int totalFishes = 0;

        for (int i = 0; i < fishAges.Count; i++)
        {
            List<int> fishAges_SubList = new List<int>();

            fishAges_SubList.Add(fishAges[i]);

            for (int day = 0; day < 100; day++)
            {
                List<int> newFishes = new List<int>();

                for (int j = 0; j < fishAges_SubList.Count; j++)
                {
                    int fishAge = fishAges_SubList[j];

                    fishAge -= 1;

                    //Fish gets a baby
                    if (fishAge < 0)
                    {
                        fishAge = 6;

                        newFishes.Add(8);
                    }

                    fishAges_SubList[j] = fishAge;
                }

                //Add new fishes
                fishAges_SubList.AddRange(newFishes);

                GC.Collect();
            }


            totalFishes += fishAges_SubList.Count;

            GC.Collect();

            Debug.Log($"Finished processing fish {i + 1} of {fishAges.Count}");
        }


        Debug.Log($"Number of fishes: {totalFishes}");
    }



    private List<int> GetData()
    {
        //Get the data 
        string[] allRowsString = FileManagement.GetInputData("Day_06", "input.txt");

        //Standardize
        //The input data is one row with ints that are comma-separated
        string[] dataStrings = allRowsString[0].Split(',');

        //Convert strings to int
        int[] fishAgesArray = Array.ConvertAll(dataStrings, int.Parse);

        //Test
        //Debug.Log(fishAges.Length);
        //Debug.Log(fishAges[0]);
        //Debug.Log(fishAges[fishAges.Length - 1]);

        List<int> fishAges = new List<int>(fishAgesArray);

        return fishAges;
    }
}
