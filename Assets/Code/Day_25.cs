using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_25 : MonoBehaviour
{
    
    private void Start()
    {
        Part_1();    
    }

    

    private void Part_1()
    {
        //Get the data 
        string[] allRowsString = FileManagement.GetInputData("Day_25", "input.txt");

        //Map with >.v characters - not size m x m

        int rows = allRowsString.Length;
        int cols = allRowsString[0].Length;

        char[,] map = new char[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                map[row, col] = allRowsString[row][col];
            }
        }


        //DisplayMap(map);


        //Move creatures
        //> always moves east if there's empty space (.)
        //When all > have tried to move, then v tries to move down if there's empty space
        //If any of them reach outside the map they teleport to the other side

        int steps = 0;

        while (true)
        {
            steps += 1;

            if (steps > 100000000)
            {
                Debug.Log("Stuck in infinite loop");

                break;
            }
        
            char[,] nextMap = GenerateEmptyMap(map);

            bool hasMoved = false;

            //Try to move every >
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (map[row, col] == '>')
                    {
                        int newColPos = col + 1;

                        if (newColPos >= cols)
                        {
                            newColPos = 0;
                        }

                        //Check if it can move to this pos
                        if (map[row, newColPos] == '.')
                        {
                            nextMap[row, newColPos] = '>';

                            hasMoved = true;
                        }
                        else
                        {
                            nextMap[row, col] = '>';
                        } 
                    }
                }
            }


            //Try to move every v
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (map[row, col] == 'v')
                    {
                        int newRow = row + 1;

                        if (newRow >= rows)
                        {
                            newRow = 0;
                        }

                        //Check if it can move to this pos
                        if (map[newRow, col] != 'v' && nextMap[newRow, col] == '.')
                        {
                            nextMap[newRow, col] = 'v';

                            hasMoved = true;
                        }
                        else
                        {
                            nextMap[row, col] = 'v';
                        }
                    }
                }
            }


            //Swap the maps
            map = nextMap;


            if (!hasMoved)
            {
                break;
            }
        }


        //DisplayMap(map);

        //Should be 579
        Debug.Log($"Number of steps: {steps}");
    }



    private char[,] GenerateEmptyMap(char[,] map)
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);

        char[,] emptyMap = new char[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                emptyMap[row, col] = '.';
            }
        }

        return emptyMap;
    }



    private void DisplayMap(char[,] map)
    {
        for (int row = 0; row < map.GetLength(0); row++)
        {
            string rowString = "";

            for (int col = 0; col < map.GetLength(1); col++)
            {
                rowString += map[row, col];

                if (col < map.GetLength(1) - 1)
                {
                    rowString += " ";
                }
            }

            Debug.Log(rowString);
        }

        Debug.Log("-");
    }
}
