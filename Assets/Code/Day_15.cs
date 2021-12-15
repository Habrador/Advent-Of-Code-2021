using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_15 : MonoBehaviour
{
    
    private void Start()
    {
        Part_1();
    }

    

    private void Part_1()
    {
        //Get the data 
        string[] allRowsString = FileManagement.GetInputData("Day_15", "input_test.txt");

        //100x100 with numbers 1-9
        //Debug.Log(allRowsString.Length);
        //Debug.Log(allRowsString[0].Length);


        //Each number is a risk level (1-9)
        //You start at TL (0,0) and want to got to BR
        //You can't move diagonally

        int rows = allRowsString.Length;
        int cols = allRowsString[0].Length;

        int[,] riskMap = new int[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                string s = char.ToString(allRowsString[row][col]);

                riskMap[row, col] = int.Parse(s);
            }
        }

        //riskMap.Display();

        //return;


        //Find the path with the lowest risk (= same as finding the shortest path where risk is the g)
        //We cant find all paths like in Day 12 because there are too many of them!
        List<Vector2Int> lowestRiskPath = new List<Vector2Int>();

        Vector2Int startNode = new Vector2Int(0, 0);
        Vector2Int endNode = new Vector2Int(rows - 1, cols - 1);

        List<List<Vector2Int>> pathsToInvestigate = new List<List<Vector2Int>>();

        //Add the start node
        List<Vector2Int> firstPath = new List<Vector2Int>();

        firstPath.Add(startNode);

        //Debug.Log(firstPath.Contains(new Vector2Int(0, 0)));

        pathsToInvestigate.Add(firstPath);

        int safety = 0;

        while (true)
        {
            safety += 1;

            if (safety > 100)
            {
                Debug.Log("Stuck in infinite loop");

                break;
            }

            if (pathsToInvestigate.Count == 0)
            {
                Debug.Log("We have found all paths");

                break;
            }

            List<Vector2Int> thisPath = GetLowestRiskPath(pathsToInvestigate, riskMap);

            Debug.Log(thisPath[thisPath.Count - 1]);

            //Remove it
            pathsToInvestigate.RemoveAt(0);

            //Find all nodes this path can connect to
            List<Vector2Int> nextNodes = FindNextNodes(thisPath, rows);

            //If this path can continue
            if (nextNodes.Count > 0)
            {
                foreach (Vector2Int nextNode in nextNodes)
                {
                    //Clone the original path
                    List<Vector2Int> newPath = new List<Vector2Int>(thisPath);
                    //...and add the next node to the end of the path
                    newPath.Add(nextNode);

                    //This path has reached the goal
                    if (nextNode == endNode)
                    {
                        lowestRiskPath = newPath;

                        break;
                    }
                    //This path needs to be expanded further
                    else
                    {
                        pathsToInvestigate.Add(newPath);
                    }

                    //Debug.Log(nextNode);
                }

                //break;
            }
        }

        //Debug.Log($"Number of paths found: {allValidPaths.Count}");

        int totalRisk = CalculateRisk(lowestRiskPath, riskMap);


        Debug.Log($"Total risk: {totalRisk}");
    }



    private List<Vector2Int> GetLowestRiskPath(List<List<Vector2Int>> allPaths, int[,] riskMap)
    {
        int lowestRisk = int.MaxValue;
        List<Vector2Int> lowestRiskPath = null;

        foreach (List<Vector2Int> path in allPaths)
        {
            int risk = CalculateRisk(path, riskMap);

            if (risk < lowestRisk)
            {
                lowestRisk = risk;

                lowestRiskPath = path;
            }
        }

        return lowestRiskPath;
    }



    private int CalculateRisk(List<Vector2Int> path, int[,] riskMap)
    {
        int totalRisk = 0;

        //Don't count the risk level of your starting position unless you enter it
        for (int i = 1; i < path.Count; i++)
        {
            Vector2Int node = path[i];

            totalRisk += riskMap[node.x, node.y];
        }
        
        //Debug.Log(totalRisk);
        
        return totalRisk;
    }



    private List<Vector2Int> FindNextNodes(List<Vector2Int> path, int mapSize)
    {
        List<Vector2Int> nextNodes = new List<Vector2Int>();
    
        Vector2Int lastNode = path[path.Count - 1];

        //The directions we can move
        Vector2Int[] directions = { new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(0, -1) };

        foreach (Vector2Int dir in directions)
        {
            Vector2Int nextNode = new Vector2Int(lastNode.x + dir.x, lastNode.y + dir.y);

            //Is this node outside the map?
            if (nextNode.x < 0 || nextNode.x >= mapSize || nextNode.y < 0 || nextNode.y >= mapSize)
            {
                continue;
            }

            //Is this node already in the path?
            if (path.Contains(nextNode))
            {
                continue;
            }

            nextNodes.Add(nextNode);
        }

        return nextNodes;
    }
}
