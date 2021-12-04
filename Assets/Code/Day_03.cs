using System;
using System.Collections.Generic;
using UnityEngine;

public class Day_03 : MonoBehaviour
{
    
    private void Start()
    {
        //Part_1();
        Part_2();
    }



    private void Part_1()
    {
        List<int[]> allBinaryNumbers = GetData();

        //Find the gamma and epsilon rate
        //The gamma rate is the most common bit if you look at each column
        //The epsilon rate is the opposite of the gamma rate (the least common bit)

        int bitArrayLength = allBinaryNumbers[0].Length;

        int[] gammaBits = new int[bitArrayLength];
        int[] epsilonBits = new int[bitArrayLength];

        int numberOfOnes = 0;
        int numberOfZeros = 0;

        //For each column
        for (int column = 0; column < bitArrayLength; column++)
        {
            //Count the number of 1s and 0s in this column
            foreach (int[] binaryNumber in allBinaryNumbers)
            {
                if (binaryNumber[column] == 1)
                {
                    numberOfOnes += 1;
                }
                else
                {
                    numberOfZeros += 1;
                }
            }

            if (numberOfOnes > numberOfZeros)
            {
                gammaBits[column] = 1;
                epsilonBits[column] = 0;
            }
            else
            {
                gammaBits[column] = 0;
                epsilonBits[column] = 1;
            }

            //Reset the counters before we move on to the next column
            numberOfOnes = 0;
            numberOfZeros = 0;
        }

        //foreach (int integer in epsilonBits)
        //{
        //    Debug.Log(integer);
        //}


        //Convert the gamma and epsilon bit arrays to integers
        string gammaString = string.Join("", gammaBits);
        string epsilonString = string.Join("", epsilonBits);

        int gamma = Convert.ToInt32(gammaString, 2);
        int epsilon = Convert.ToInt32(epsilonString, 2);

        Debug.Log(gamma);
        Debug.Log(epsilon);

        int powerConsumption = gamma * epsilon;

        Debug.Log($"Power consumption: {powerConsumption}");
    }



    private void Part_2()
    {
        List<int[]> allBinaryNumbers = GetData();

        //Find the "oxygen generator rating" and "the CO2 scrubber rating" by using certain rules

        int bitArrayLength = allBinaryNumbers[0].Length;

        int[] oxygen_Bits = new int[bitArrayLength];
        int[] CO2_Bits = new int[bitArrayLength];

        //Add items to these arrays until we have just one left by following the rules
        List<int[]> rowsOfInterest = new List<int[]>(allBinaryNumbers);
        List<int[]> rowsToSave = new List<int[]>();

        //Oxygen

        //For each column
        for (int columnIndex = 0; columnIndex < bitArrayLength; columnIndex++)
        {
            CountZerosAndOnesInColumn(rowsOfInterest, columnIndex, out int numberOfOnes, out int numberOfZeros);

            if (numberOfOnes >= numberOfZeros)
            {
                //Save all rows with a 1 in the column
                SaveRows(rowsOfInterest, rowsToSave, columnIndex, 1);
            }
            else
            {
                //Save all rows with a 1 in the column
                SaveRows(rowsOfInterest, rowsToSave, columnIndex, 0);
            }

            //Switch arrays
            rowsOfInterest.Clear();
            rowsOfInterest.AddRange(rowsToSave);
            rowsToSave.Clear();

            //Check if we have finished?
            if (rowsOfInterest.Count == 1)
            {
                oxygen_Bits = rowsOfInterest[0];

                break;
            }
        }

        //Debug.Log(rowsOfInterest.Count);

        //CO2

        rowsOfInterest = new List<int[]>(allBinaryNumbers);
        rowsToSave = new List<int[]>();

        //For each column
        for (int columnIndex = 0; columnIndex < bitArrayLength; columnIndex++)
        {
            CountZerosAndOnesInColumn(rowsOfInterest, columnIndex, out int numberOfOnes, out int numberOfZeros);

            if (numberOfOnes < numberOfZeros)
            {
                //Save all rows with a 1 in the column
                SaveRows(rowsOfInterest, rowsToSave, columnIndex, 1);
            }
            else
            {
                //Save all rows with a 1 in the column
                SaveRows(rowsOfInterest, rowsToSave, columnIndex, 0);
            }

            //Switch arrays
            rowsOfInterest.Clear();
            rowsOfInterest.AddRange(rowsToSave);
            rowsToSave.Clear();

            //Check if we have finished?
            if (rowsOfInterest.Count == 1)
            {
                CO2_Bits = rowsOfInterest[0];

                break;
            }
        }


        //Convert the oxygen and CO2 bit arrays to integers
        string oxygen_String = string.Join("", oxygen_Bits);
        string CO2_String = string.Join("", CO2_Bits);

        int oxygen = Convert.ToInt32(oxygen_String, 2);
        int CO2 = Convert.ToInt32(CO2_String, 2);

        Debug.Log(oxygen);
        Debug.Log(CO2);

        int lifeSupportRating = oxygen * CO2;

        Debug.Log($"Life support rating: {lifeSupportRating}");
    }



    private void SaveRows(List<int[]> rowsOfInterest, List<int[]> rowsToSave, int columnIndex, int oneOrZero)
    {
        foreach (int[] row in rowsOfInterest)
        {
            if (row[columnIndex] == oneOrZero)
            {
                rowsToSave.Add(row);
            }
        }
    }



    //Check if the numberOfInterest (0 or 1) is most common in a column
    //Returns true if equally common
    private void CountZerosAndOnesInColumn(List<int[]> allBinaryNumbers, int columnIndex, out int numberOfOnes, out int numberOfZeros)
    {
        numberOfOnes = 0;
        numberOfZeros = 0;

        //For each row
        foreach (int[] binaryNumber in allBinaryNumbers)
        {
            if (binaryNumber[columnIndex] == 1)
            {
                numberOfOnes += 1;
            }
            else
            {
                numberOfZeros += 1;
            }
        }
    }



    private List<int[]> GetData()
    {
        string[] allBinaryNumbersStrings = FileManagement.GetInputData("Day_03", "input.txt");

        //Convert to array of array
        List<int[]> allBinaryNumbers = new List<int[]>();

        foreach (string s in allBinaryNumbersStrings)
        {
            int[] binaryNumbers = new int[s.Length];

            for (int i = 0; i < binaryNumbers.Length; i++)
            {
                binaryNumbers[i] = int.Parse(s[i].ToString());
            }

            allBinaryNumbers.Add(binaryNumbers);
        }

        //Test
        //int[] binaryNumbersTest = allBinaryNumbers[0];

        //foreach (int integer in binaryNumbersTest)
        //{
        //    Debug.Log(integer);
        //}

        return allBinaryNumbers;
    }
}
