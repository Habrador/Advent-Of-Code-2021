using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_15 : MonoBehaviour
{
    
    private void Start()
    {
        //Part 1 and part 2
        Part_1();
    }

    

    private void Part_1()
    {
        //int[,] riskMap = GetRiskMap_Part_1();
        int[,] riskMap = GetRiskMap_Part_2();
        
        //riskMap.Display();

        int rows = riskMap.GetLength(0);
        int cols = riskMap.GetLength(1);


        //Find the path with the lowest risk (= same as finding the shortest path where risk is the g)
        //We cant find all paths like in Day 12 because there are too many of them!
        Node lowestRiskNode = null;

        Node startNode = new Node(new Vector2Int(0, 0), 0, null);

        Vector2Int endCoordinates = new Vector2Int(rows - 1, cols - 1);

        bool[,] visitedCells = new bool[rows, cols];


        HashSet<Node> nodesToInvestigate = new HashSet<Node>();

        //Add the start node
        nodesToInvestigate.Add(startNode);

        //Node bestNodeSoFar = null;

        //List<Node> allNewNodes = new List<Node>();

        int safety = 0;

        while (true)
        {
            safety += 1;

            if (safety > 1000000)
            {
                Debug.Log("Stuck in infinite loop");

                break;
            }

            if (nodesToInvestigate.Count == 0)
            {
                Debug.Log("We have found all paths");

                break;
            }

            Node bestNodeSoFar = GetLowestRiskNode(nodesToInvestigate);

            //Did we reach the end?
            if (bestNodeSoFar.coordinates == endCoordinates)
            {
                lowestRiskNode = bestNodeSoFar;

                break;
            }

            //Remove it
            //This list is not sorted so dont remove 0!
            nodesToInvestigate.Remove(bestNodeSoFar);

            //Find all nodes this path can connect to
            List<Node> nextNodes = FindNextNodes(bestNodeSoFar, riskMap, visitedCells, nodesToInvestigate);

            //Create new paths and add them to the list of all active paths
            //May be 0 if we didnt find any next nodes
            foreach (Node nextNode in nextNodes)
            {
                nodesToInvestigate.Add(nextNode);

                //We have now visited this node
                visitedCells[nextNode.coordinates.x, nextNode.coordinates.y] = true;

                //Dont check for goal node here!

                //allNewNodes.Add(nextNode);
            }
        }

        //riskMap.Display();

        //DisplayPath(riskMap, bestPathSoFar);
        //DisplayPath(riskMap, allNewNodes);

        //Debug.Log($"Best path so far risk: {}");

        /*
        foreach (List<Vector2Int> path in pathsToInvestigate)
        {
            int risk = CalculateRisk(path, riskMap);

            Debug.Log(risk);

            DisplayCoordinates(path);
            
            DisplayPath(riskMap, path);
        }
        */


        //Debug.Log($"Number of paths found: {allValidPaths.Count}");

        //int totalRisk = CalculateRisk(lowestRiskPath, riskMap);

        if (lowestRiskNode != null)
        {
            //List<Node> lowestRiskPath = GeneratePathFromNode(lowestRiskNode);
        
            //DisplayPath(riskMap, lowestRiskPath);

            //Should be 589 for Part 1
            //Should be 2885 for Part 2
            Debug.Log($"Total risk: {lowestRiskNode.totalRisk}");
        }
        
    }



    private class Node
    {
        public Vector2Int coordinates;

        public int totalRisk;

        public Node previousNode;

        public Node(Vector2Int coordinates, int totalRisk, Node previousNode)
        {
            this.coordinates = coordinates;
            this.totalRisk = totalRisk;
            this.previousNode = previousNode;
        }
    }



    private List<Node> GeneratePathFromNode(Node lowestRiskNode)
    {
        List<Node> lowestRiskPath = new List<Node>();

        int safety = 0;

        Node currentNode = lowestRiskNode;

        while (true)
        {
            safety += 1;

            if (safety > 1000)
            {
                Debug.Log("Stuck in infinite loop");

                break;
            }

            lowestRiskPath.Add(currentNode);

            if (currentNode.previousNode == null)
            {
                break;
            }

            currentNode = currentNode.previousNode;
        }

        return lowestRiskPath;
    }



    private Node GetLowestRiskNode(HashSet<Node> allNodes)
    {
        int lowestRisk = int.MaxValue;

        Node lowestRiskNode = null;

        foreach (Node node in allNodes)
        {
            if (node.totalRisk < lowestRisk)
            {
                lowestRisk = node.totalRisk;

                lowestRiskNode = node;
            }
        }

        return lowestRiskNode;
    }



    private List<Node> FindNextNodes(Node node, int[,] riskMap, bool[,] visitedCells, HashSet<Node> nodesToInvestigate)
    {
        List<Node> nextNodes = new List<Node>();

        //The directions we can move
        Vector2Int[] directions = { new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(0, -1) };

        int mapSize = visitedCells.GetLength(0);

        foreach (Vector2Int dir in directions)
        {
            Vector2Int nextNodesCoordinates = new Vector2Int(node.coordinates.x + dir.x, node.coordinates.y + dir.y);

            //Is this node outside the map?
            if (nextNodesCoordinates.x < 0 || nextNodesCoordinates.x >= mapSize || nextNodesCoordinates.y < 0 || nextNodesCoordinates.y >= mapSize)
            {
                continue;
            }

            //Is this node already in the path?
            if (visitedCells[nextNodesCoordinates.x, nextNodesCoordinates.y])
            {
                //Debug.Log("This path already contains this node");

                //If we have already visited this node, we have to check if the path to that node from this node is shorter
                //Example if we have A (7) and B (2) is connected to the Start node
                //And B is connected to A (3)
                //Then the shorter path to A from Start is through B (2 + 3 < 7)
                //So A's previous node should be B and not Start

                //This is not needed in this case because it will never happen!
                //Node alreadyVisited = TryFindNodeInQueue(nextNodesCoordinates, nodesToInvestigate);

                //if (alreadyVisited != null)
                //{
                //    if (alreadyVisited.totalRisk > node 
                //}

                continue;
            }

            int totalRisk = node.totalRisk + riskMap[nextNodesCoordinates.x, nextNodesCoordinates.y];

            Node nextNode = new Node(nextNodesCoordinates, totalRisk, node);

            nextNodes.Add(nextNode);
        }

        return nextNodes;
    }



    private Node TryFindNodeInQueue(Vector2Int nextNodesCoordinates, HashSet<Node> nodesToInvestigate)
    {
        foreach (Node node in nodesToInvestigate)
        {
            if (node.coordinates == nextNodesCoordinates)
            {
                return node;
            }
        }

        return null;
    }



    private void DisplayPath(int[,] riskMap, List<Node> path)
    {
        int mapSize = riskMap.GetLength(0);

        //Transfer data from path to map to make it easier to display the path
        bool[,] isPathMap = new bool[mapSize, mapSize];

        foreach (Node node in path)
        {
            isPathMap[node.coordinates.x, node.coordinates.y] = true;
        }


        for (int row = 0; row < mapSize; row++)
        {
            string rowString = "";

            for (int col = 0; col < mapSize; col++)
            {
                Vector2Int cellPos = new Vector2Int(row, col);

                if (isPathMap[row, col])
                {
                    //rowString += $"<b>{riskMap[row, col]}</b>";
                    rowString += "-";
                }
                else
                {
                    rowString += riskMap[row, col];
                }
                

                if (col < mapSize - 1)
                {
                    rowString += ", ";
                }
            }

            Debug.Log(rowString);
        }

        Debug.Log("-");
    }



    private void DisplayCoordinates(List<Vector2Int> paths)
    {
        string displayString = "";

        foreach (Vector2Int path in paths)
        {
            displayString += $"({path.x}, {path.y}) ";
        }

        Debug.Log(displayString);
    }



    private int[,] GetRiskMap_Part_1()
    {
        //Get the data 
        string[] allRowsString = FileManagement.GetInputData("Day_15", "input.txt");

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

        return riskMap;
    }



    private int[,] GetRiskMap_Part_2()
    {
        int[,] riskMap_part_1 = GetRiskMap_Part_1();

        int rows = riskMap_part_1.GetLength(0);
        int cols = riskMap_part_1.GetLength(1);

        //The risk map in part 2 is 5*5 the original risk map
        int[,] riskMap = new int[rows * 5, cols * 5];

        //For each row and col inbthe original map
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                //For each section in the new map
                for (int rowSection = 0; rowSection < 5; rowSection++)
                {
                    for (int colSection = 0; colSection < 5; colSection++)
                    {
                        int oldRisk = riskMap_part_1[row, col];

                        int newRisk = oldRisk + rowSection + colSection;

                        if (newRisk > 9)
                        {
                            newRisk = newRisk - 9;
                        }

                        int rowNew = row + (rowSection * rows);
                        int colNew = col + (colSection * cols);

                        riskMap[rowNew, colNew] = newRisk;
                    }
                }
            }
        }

        //Debug.Log(riskMap[0, 0]); // 0
        //Debug.Log(riskMap[riskMap.GetLength(0) - 1, riskMap.GetLength(1) - 2]); // 7

        return riskMap;
    }
}
