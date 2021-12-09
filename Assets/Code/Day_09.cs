using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_09 : MonoBehaviour
{
    
    private void Start()
    {
        //Part_1();

        Part_2();
    }

    
   
    private void Part_1()
    {
        int[,] heightMap = GetHeightMap();

        //Each number in the height map represents a height (9 is highest, 0 is lowest)

        //Find the low points (the height in the cell is lower than the 4 surrounding cells)
        //The risk level of a low point is height + 1
        //Find all risk levels and sum them up

        List<Vector2Int> lowPoints = GetLowPoints(heightMap);

        int totalRiskLevel = 0;

        foreach (Vector2Int lowPoint in lowPoints)
        {
            totalRiskLevel += heightMap[lowPoint.x, lowPoint.y] + 1;
        }


        //Should be 535
        Debug.Log($"Total risk level: {totalRiskLevel}");
    }



    private void Part_2()
    {
        int[,] heightMap = GetHeightMap();

        //Each number in the height map represents a height (9 is highest, 0 is lowest)

        //Find the low points (the height in the cell is lower than the 4 surrounding cells)
        List<Vector2Int> lowPoints = GetLowPoints(heightMap);

        //Find the basins originating at these low points
        //A basin is like water flowing from the low point until the water hits a cell with height 9
        //So we need a flowfield algorithm to identify the size of each basin

        List<Basin> basins = new List<Basin>();

        foreach (Vector2Int lowPoint in lowPoints)
        {
            Basin basin = new Basin(lowPoint, heightMap);

            basins.Add(basin);
        }


        //Sort to find the 3 largest basins
    }



    public class Basin
    {
        //All cells that belongs to this basin
        public List<Vector2Int> cells = new List<Vector2Int>();

        public int Size => cells.Count;

        //The lowest point of the basin
        public Vector2Int lowPoint;



        public Basin(Vector2Int lowPoint, int[,] heightMap)
        {
            this.lowPoint = lowPoint;

            GenerateBasin(lowPoint, heightMap);
        }

        

        //The flowfield algorithm
        private void GenerateBasin(Vector2Int lowPoint, int[,] heightMap)
        {
            Queue<Vector2Int> cellsToFlowFrom = new Queue<Vector2Int>();

            //Add the start low point
            cellsToFlowFrom.Enqueue(lowPoint);

            int safety = 0;

            while (cellsToFlowFrom.Count > 0)
            {
                safety += 1;

                if (safety > 1000000000)
                {
                    Debug.Log("Flowfield stuck in infinite loop");
                
                    break;
                }


                Vector2Int currentCell = cellsToFlowFrom.Dequeue();

                //Add it to the list of cells that belongs to this basin
                cells.Add(currentCell);

                //Get valid surrounding cells 
                List<Vector2Int> surroundingCells = GetSurroundingCells(currentCell, heightMap, cellsToFlowFrom);

                //Add surroinding cells to the queue
                foreach (Vector2Int cell in surroundingCells)
                {
                    cellsToFlowFrom.Enqueue(cell);
                }
            }
        }



        //Get valid surrounding cells: 
        //- Cells we haven't visited so far
        //- Cells that don't have height 9
        //- Cells that are inside of the map
        private List<Vector2Int> GetSurroundingCells(Vector2Int currentCell, int[,] heightMap, Queue<Vector2Int> cellsToFlowFrom)
        {
            List<Vector2Int> surroundingCells = new List<Vector2Int>();

            return surroundingCells;
        }
    }



    private List<Vector2Int> GetLowPoints(int[,] heightMap)
    {
        int mapSize = heightMap.GetLength(0);

        List<Vector2Int> lowPoints = new List<Vector2Int>();

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
                    lowPoints.Add(new Vector2Int(x, y));
                }
            }
        }

        return lowPoints;
    }



    //Is a cell within the map
    private bool IsWithinMap(int x, int y, int mapSize)
    {
        //Make sure this cell is within the map
        if (x < 0 || x >= mapSize || y < 0 || y >= mapSize)
        {
            return false;
        }

        return true;
    }



    private int[,] GetHeightMap()
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

        return heightMap;
    }
}
