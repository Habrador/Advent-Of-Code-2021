using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_06 : MonoBehaviour
{
    private static long totalFishes;



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

        //Use an recursive algorithm so we don't have to bother with list sizes running out of memory

        //One fishes generates a total of 1,911,854,036 fishes, so 300 fishes should generate 573,556,210,800
        //Max size of int32 is 2,147,483,647
        //Max Size of int64 (long) is 9,223,372,036,854,775,807



        //This one is static
        //totalFishes = fishAges.Count;

        //We dont have to simulate ALL fishes - fishes that start with for example a 1 is only needed to simulate once - and then we just reuse the result. FailFish

        //Find frequency of days and which days are present (which is 1,2,3,4,5)
        List<int> days = new List<int>();

        for (int i = 0; i < fishAges.Count; i++)
        {
            if (!days.Contains(fishAges[i]))
            {
                days.Add(fishAges[i]);

                //Debug.Log(fishAges[i]);
            }
        }


        int[] dayFrequency = new int[days.Count];

        for (int i = 0; i < fishAges.Count; i++)
        {
            int thisDay = fishAges[i];

            //-1 because days are 1,2,3,4,5 and array starts at 0
            dayFrequency[thisDay - 1] += 1;
        }

        //for (int i = 0; i < dayFrequency.Length; i++)
        //{
        //    Debug.Log(dayFrequency[i]);
        //}

        //Find out how many fishes each day is going to generate
        int totalSimDays = 256;

        //1
        SimulateFish(1, 0, totalSimDays);

        long totalFishes_1 = totalFishes + 1;

        totalFishes = 0;

        //2
        SimulateFish(2, 0, totalSimDays);

        long totalFishes_2 = totalFishes + 1;

        totalFishes = 0;

        //3
        SimulateFish(3, 0, totalSimDays);

        long totalFishes_3 = totalFishes + 1;

        totalFishes = 0;

        //4
        SimulateFish(4, 0, totalSimDays);

        long totalFishes_4 = totalFishes + 1;

        totalFishes = 0;

        //5
        SimulateFish(5, 0, totalSimDays);

        long totalFishes_5 = totalFishes + 1;

        totalFishes = 0;


        //Now we can calculate the total amount of fishes
        totalFishes += totalFishes_1 * dayFrequency[0];
        totalFishes += totalFishes_2 * dayFrequency[1];
        totalFishes += totalFishes_3 * dayFrequency[2];
        totalFishes += totalFishes_4 * dayFrequency[3];
        totalFishes += totalFishes_5 * dayFrequency[4];

        Debug.Log($"Number of fishes: {totalFishes}");
    }



    private void SimulateFish(int fishAge, int days, int maxDays)
    {
        if (days >= maxDays)
        {
            return;
        }
    
        for (int day = days; day < maxDays; day++)
        {
            fishAge -= 1;

            //Fish gets a baby
            if (fishAge < 0)
            {
                fishAge = 6;

                SimulateFish(8, day + 1, maxDays);

                totalFishes += 1;
            }
        }
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
