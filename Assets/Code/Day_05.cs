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
        Vector2Int p1;
        Vector2Int p2;

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
