using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_11 : MonoBehaviour
{

    private void Start()
    {
        //Part_1();

        Part_2();
    }




    //Same as Part 1 but you have to figure out number of steps untill all octopuses flashes at the same time!
    private void Part_2()
    {
        int[,] octopuses = GetData();

        //Each cell is an octopus's energy level [0, 9]
        //Each octopus gains energy over time and flashes when its energy is 9 (then energy level is reset to 0)
        //BUT this also affects the 8 surrounding octopuses by increasing their energy level by 1, making them potentially flas this step as well
        //An octopus can only flash once every step

        //octopuses.Display();

        int rows = octopuses.GetLength(0);
        int cols = octopuses.GetLength(1);

        int totalSteps = 0;

        while (true)
        {

            bool[,] hasFlashed = new bool[rows, cols];


            //Step 1. Increase the energy level of all octopuses
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int energyLevel = octopuses[row, col];

                    energyLevel += 1;

                    octopuses[row, col] = energyLevel;
                }
            }

            //octopuses.Display();


            //Step 2. Flash octopuses
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    TryFlashOctopus(new Vector2Int(row, col), octopuses, hasFlashed);
                }
            }


            //Step 3. Reset all energy levels of octopuses that flashed
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int energyLevel = octopuses[row, col];

                    if (energyLevel > 9)
                    {
                        energyLevel = 0;

                        octopuses[row, col] = energyLevel;
                    }
                }
            }


            int numberOfOctopusesThatFlashed = 0;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    bool hasOctopusFlashed = hasFlashed[row, col];

                    if (hasOctopusFlashed)
                    {
                        numberOfOctopusesThatFlashed += 1;
                    }
                }
            }

            if (numberOfOctopusesThatFlashed == (rows * cols))
            {
                break;
            }


            totalSteps += 1;

            if (totalSteps > 1000000000)
            {
                Debug.Log("Stuck in infinite loop");

                break;
            }
        }


        //Should be 400
        Debug.Log($"Total steps: {totalSteps + 1}");


        //octopuses.Display();
    }



    private void TryFlashOctopus(Vector2Int cell, int[,] energyLevels, bool[,] hasFlashed)
    {
        if (hasFlashed[cell.x, cell.y])
        {
            return;
        }
    
        int energyLevel = energyLevels[cell.x, cell.y];

        if (energyLevel > 9)
        {
            //energyLevel = 0;

            hasFlashed[cell.x, cell.y] = true;

            //energyLevels[cell.x, cell.y] = energyLevel;

            //Increase energy level of surrounding cells
            UpdateSurroundingCells(cell, energyLevels, hasFlashed);
        }
    }



    private void UpdateSurroundingCells(Vector2Int cell, int[,] energyLevels, bool[,] hasFlashed)
    {
        //Debug.Log("Hello");
    
        for (int row = cell.x - 1; row <= cell.x + 1; row++)
        {
            for (int col = cell.y - 1; col <= cell.y + 1; col++)
            {
                //Ignore the cell we are in
                if (row == cell.x && col == cell.y)
                {
                    continue;
                }

                //Ignore cells outside of the map
                if (col < 0 || col >= energyLevels.GetLength(0) || row < 0 || row >= energyLevels.GetLength(1))
                {
                    continue;
                }

                //Debug.Log($"({row}, {col})");

                //Update energy levels
                int energyLevel = energyLevels[row, col];

                energyLevel += 1;

                energyLevels[row, col] = energyLevel;

                TryFlashOctopus(new Vector2Int(row, col), energyLevels, hasFlashed);
            }
        }
    }



    
    private void Part_1()
    {
        int[,] octopuses = GetData();

        //Each cell is an octopus's energy level [0, 9]
        //Each octopus gains energy over time and flashes when its energy is 9 (then energy level is reset to 0)
        //BUT this also affects the 8 surrounding octopuses by increasing their energy level by 1, making them potentially flas this step as well
        //An octopus can only flash once every step

        //octopuses.Display();

        int rows = octopuses.GetLength(0);
        int cols = octopuses.GetLength(1);

        long totalFlashes = 0;

        int totalSteps = 100;

        for (int step = 0; step < totalSteps; step++)
        {

            bool[,] hasFlashed = new bool[rows, cols];


            //Step 1. Increase the energy level of all octopuses
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int energyLevel = octopuses[row, col];

                    energyLevel += 1;

                    octopuses[row, col] = energyLevel;
                }
            }

            //octopuses.Display();


            //Step 2. Flash octopuses
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    TryFlashOctopus(new Vector2Int(row, col), octopuses, hasFlashed);
                }
            }


            //Step 3. Reset all energy levels of octopuses that flashed
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int energyLevel = octopuses[row, col];

                    if (energyLevel > 9)
                    {
                        energyLevel = 0;

                        octopuses[row, col] = energyLevel;

                        totalFlashes += 1;
                    }
                }
            }
        }


        //Should be 1735
        Debug.Log($"Total flashes: {totalFlashes}");


        octopuses.Display();
    }



    private int[,] GetData()
    {
        //Get the data 
        string[] allRowsString = FileManagement.GetInputData("Day_11", "input.txt");

        //The data consists of numbers 0-9 in rows (not comma separated and no spaces between) 10x10
        //Debug.Log($"Rows: {allRowsString.Length}");
        //Debug.Log($"Columns: {allRowsString[0].Length}");

        int rows = allRowsString.Length;
        int cols = allRowsString[0].Length;

        int[,] octopuses = new int[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            string thisRow = allRowsString[row];

            for (int col = 0; col < cols; col++)
            {
                //From char to int
                //heightMap[row, col] = thisRow[col] - '0';
                //heightMap[row, col] = (int)thisRow[col]; //Doesnt work!
                octopuses[row, col] = int.Parse(thisRow[col].ToString());
            }
        }

        //Debug.Log(octopuses[0, 0]); //2
        //Debug.Log(octopuses[0, cols - 1]); //5
        //Debug.Log(octopuses[rows - 1, 0]); //8
        //Debug.Log(octopuses[rows - 1, cols - 1]); //1

        return octopuses;
    }
}
