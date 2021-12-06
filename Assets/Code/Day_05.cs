using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_05 : MonoBehaviour
{
    
    private void Start()
    {
        Part_1();
    }



    private void Part_1()
    {
        List<LineSegment> lineSegments = GetData();

        //Find map max size = max x and y values (starts at 0,0)
        Vector2Int mapSize = GetMapSize(lineSegments);

        //Add 1 to map size or all numbers wont fit in the array because 0 is included in the list of coordinates
        int[,] map = new int[mapSize.x + 1, mapSize.y + 1];

        //Debug.Log(map[0, 0]);

        int diagonalLines = 0;

        //Plot the horizontal and vertical lines, then count how many overlaps
        foreach (LineSegment line in lineSegments)
        {
            Vector2Int p1 = line.p1;
            Vector2Int p2 = line.p2;

            //x is fixed
            if (p1.x == p2.x)
            {
                //Swap if line is reversed to make an easier for loop
                if (p2.y < p1.y)
                {
                    int tmp = p1.y;
                    p1.y = p2.y;
                    p2.y = tmp;
                }

                //Dont forget <= or we will not count the end point
                for (int y = p1.y; y <= p2.y; y++)
                {                
                    map[p1.x, y] += 1;
                }
            }
            //y is fixed
            else if (p1.y == p2.y)
            {
                //Swap if line is reversed to make an easier for loop
                if (p2.x < p1.x)
                {
                    int tmp = p1.x;
                    p1.x = p2.x;
                    p2.x = tmp;
                }

                for (int x = p1.x; x <= p2.x; x++)
                {
                    map[x, p1.y] += 1;
                }
            }
            //
            // Part 2: diagonal lines (45 degrees only)
            //
            else
            {
                diagonalLines += 1;

                int y = p1.y;

                if (p2.x < p1.x)
                {
                    for (int x = p1.x; x >= p2.x; x--)
                    {
                        map[x, y] += 1;

                        if (p1.y > p2.y)
                        {
                            y -= 1;
                        }
                        else
                        {
                            y += 1;
                        }
                    }
                }
                else
                {
                    for (int x = p1.x; x <= p2.x; x++)
                    {
                        map[x, y] += 1;

                        if (p1.y > p2.y)
                        {
                            y -= 1;
                        }
                        else
                        {
                            y += 1;
                        }
                    }
                }
            }
        }

        Debug.Log($"Diagional lines: {diagonalLines}");


        //Count number of overlapping lines (map number is > 1)
        int overlappingLines = 0;

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] > 1)
                {
                    overlappingLines += 1;
                }
            }
        }

        //Should be 7085 for Part 1
        //Should be 20271 for Part 2
        Debug.Log($"Number of overlapping lines: {overlappingLines}");
    }



    private Vector2Int GetMapSize(List<LineSegment> lineSegments)
    {
        int maxX = -Int32.MaxValue;
        int maxY = -Int32.MaxValue;

        foreach (LineSegment line in lineSegments)
        {
            Vector2Int p1 = line.p1;
            Vector2Int p2 = line.p2;

            if (p1.x > maxX)
            {
                maxX = p1.x;
            }
            if (p2.x > maxX)
            {
                maxX = p2.x;
            }
            if (p1.y > maxY)
            {
                maxY = p1.y;
            }
            if (p2.y > maxY)
            {
                maxY = p2.y;
            }
        }

        //Debug.Log($"Max x: {maxX}, Max y: {maxY}");

        return new Vector2Int(maxX, maxY);
    }



    private List<LineSegment> GetData()
    {
        string[] allRowsString = FileManagement.GetInputData("Day_05", "input.txt");

        //Standardize the data from 194,556 -> 739,556 to LineSegment((194,556), (739,556))

        List<LineSegment> lineSegments = new List<LineSegment>();

        foreach (string row in allRowsString)
        {
            //First split by -> and , and we end up with the four coordinates
            string[] separator = { "-> ", "," };

            int count = 4;

            string[] coordinatesStrings = row.Split(separator, count, StringSplitOptions.RemoveEmptyEntries);

            //foreach (string coordinate in coordinatesStrings)
            //{
            //    Debug.Log(coordinate);
            //}

            //String to int
            int[] coordinates = Array.ConvertAll(coordinatesStrings, Int32.Parse);

            //foreach (int coordinate in coordinates)
            //{
            //    Debug.Log(coordinate);
            //}

            Vector2Int lineStart = new Vector2Int(coordinates[0], coordinates[1]);
            Vector2Int lineEnd = new Vector2Int(coordinates[2], coordinates[3]);

            lineSegments.Add(new LineSegment(lineStart, lineEnd));
        }

        Debug.Log($"Number of line segments: {lineSegments.Count}");

        //lineSegments[0].Display();
        //lineSegments[lineSegments.Count - 8].Display();
        //lineSegments[lineSegments.Count - 2].Display();
        //lineSegments[lineSegments.Count - 1].Display();

        return lineSegments;
    }



    public struct LineSegment
    {
        public Vector2Int p1;
        public Vector2Int p2;

        public LineSegment(Vector2Int start, Vector2Int end)
        {
            this.p1 = start;
            this.p2 = end;
        }

        public void Display()
        {
            Debug.Log($"({p1.x},{p1.y}), ({p2.x},{p2.y})");
        }
    }
}
