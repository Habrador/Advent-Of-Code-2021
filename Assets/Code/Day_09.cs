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

        int[,] heightMap = new int[allRowsString.Length, allRowsString[0].Length];

        for (int row = 0; row < allRowsString.Length; row++)
        {
            string thisRow = allRowsString[row];

            for (int col = 0; col < thisRow.Length; col++)
            {
                heightMap[row, col] = thisRow[col] - '0';
            }
        }

        //Debug.Log(heightMap[0, 0]); //7
        //Debug.Log(heightMap[0, 99]); //5
        //Debug.Log(heightMap[99, 0]); //6
        //Debug.Log(heightMap[99, 99]); //9

        //Each number in the height map represents a height (9 is highest, 0 is lowest)

        //Find the low points (the height in the cell is lower than the 4 surrounding cells)
        //The risk level of a low point is height + 1
        //Find all risk levels and sum them up

        int totalRiskLevel = 0;

        int mapSize = heightMap.GetLength(0);

        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                int height = heightMap[x, y];

                bool areSurroundingCellsHigher = true;

                //Go thorugh the 4 surrounding cells and see if they are higher
                if (IsWithinMap(x - 1, y + 0, mapSize))
                {
                    if (heightMap[x - 1, y + 0] <= height)
                    {
                        areSurroundingCellsHigher = false;
                    }
                }
                if (IsWithinMap(x + 0, y + 1, mapSize))
                {
                    if (heightMap[x + 0, y + 1] <= height)
                    {
                        areSurroundingCellsHigher = false;
                    }
                }
                if (IsWithinMap(x + 1, y + 0, mapSize))
                {
                    if (heightMap[x + 1, y + 0] <= height)
                    {
                        areSurroundingCellsHigher = false;
                    }
                }
                if (IsWithinMap(x + 0, y - 1, mapSize))
                {
                    if (heightMap[x + 0, y - 1] <= height)
                    {
                        areSurroundingCellsHigher = false;
                    }
                }

                if (areSurroundingCellsHigher)
                {
                    totalRiskLevel += height + 1;
                }
            }
        }


        //Should be 535
        Debug.Log($"Total risk level: {totalRiskLevel}");
    }


    private bool IsWithinMap(int x, int y, int mapSize)
    {
        //Make sure this cell is within the map
        if (x < 0 || x >= mapSize || y < 0 || y >= mapSize)
        {
            return false;
        }

        return true;
    }
}
