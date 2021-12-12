using System.Collections.Generic;
using UnityEngine;

public class Day_12 : MonoBehaviour
{
    
    private void Start()
    {
        Part_1();
    }

    

    private void Part_1()
    {
        //Get the data 
        string[] allRowsString = FileManagement.GetInputData("Day_12", "input.txt");

        //Debug.Log($"Rows: {allRowsString.Length}");
        //Debug.Log($"Columns: {allRowsString[0].Length}");


        //The data consists of 2 characters, a dash, 2 characters, like: "Ws-bg" in 25 rows
        //Sometimes the characters have been replaced by start or end
        //These are connections in a network, so start-SQ means the start node and the SQ node are connected
        //Capital letters means big cave and lowercase means small cave

        string[] testNetworkStrings = {
            "start-A",
            "start-b",
            "A-c",
            "A-b",
            "b-d",
            "A-end",
            "b-end"
        };

        //    start
        //    /   \
        //c--A-----b--d
        //    \   /
        //     end

        //A valid path is start-A-c-A-b-end
        //A non valid path is start-A-c-A-b-d-b-end because b is a small cave and we can't visit it multiple times

        //In total, we get 10 valid paths:
        //start, A,b,A,c,A,end
        //start, A, b, A, end
        //start, A,b,end
        //start, A, c, A, b, A, end
        //start, A,c,A,b,end
        //start, A, c, A, end
        //start, A,end
        //start, b, A, c, A, end
        //start, b,A,end
        //start, b, end

        allRowsString = testNetworkStrings;


        //Find distinctive nodes and node connections
        List<string> nodeNames = new List<string>();

        foreach (string s in allRowsString)
        {
            string[] nodes = s.Split('-');

            foreach (string name in nodes)
            {
                if (!nodeNames.Contains(name))
                {
                    nodeNames.Add(name);
                }
            }
        }

        nodeNames.Display();


        //Init the nodes
        HashSet<Node> allNodes = new HashSet<Node>();

        for (int i = 0; i < nodeNames.Count; i++)
        {
            string name = nodeNames[i];

            bool isBigCave = false;

            if (char.IsUpper(name[0]))
            {
                isBigCave = true;
            }

            Node newNode = new Node(i, isBigCave);

            allNodes.Add(newNode);
        }


        //foreach (Node node in allNodes)
        //{
        //    Debug.Log(nodeNames[node.nameIndex] + " " + node.isBigGave);
        //}


        //Connect the nodes
        Node.ConnectNodes(allNodes, nodeNames, allRowsString);

        //Node.DisplayConnections(allNodes, nodeNames);



        //
        // Find all valid paths from start to end
        //

        //Find the start and end nodes
        Node startNode = null;
        Node endNode = null;

        foreach (Node node in allNodes)
        {
            if (nodeNames[node.nameIndex] == "start")
            {
                startNode = node;

                break;
            }
        }
        foreach (Node node in allNodes)
        {
            if (nodeNames[node.nameIndex] == "end")
            {
                endNode = node;

                break;
            }
        }


        //startNode.DisplayNode(nodeNames);
        //endNode.DisplayNode(nodeNames);
    }



    public class Node
    {
        //Position in list with all nodes
        public int nameIndex;

        public List<Node> connections = new List<Node>();

        public bool isBigGave = false;


        public Node(int nameIndex, bool isBigCave)
        {
            this.nameIndex = nameIndex;
            this.isBigGave = isBigCave;
        }


        public void DisplayNode(List<string> nodeNames)
        {
            string displayString = nodeNames[this.nameIndex] + ": ";

            foreach (Node connectedNode in this.connections)
            {
                displayString += nodeNames[connectedNode.nameIndex] + ", ";
            }

            Debug.Log(displayString);
        }



        //
        // Static methods
        //

        public static Node GetNodeFromString(string name, HashSet<Node> allNodes, List<string> nodeNames)
        {
            Node correspondingNode = null;

            foreach (Node node in allNodes)
            {
                if (nodeNames[node.nameIndex] == name)
                {
                    correspondingNode = node;
                }
            }

            return correspondingNode;
        }



        public static void DisplayConnections(HashSet<Node> allNodes, List<string> nodeNames)
        {
            foreach (Node node in allNodes)
            {
                node.DisplayNode(nodeNames);
            }
        }



        public static void ConnectNodes(HashSet<Node> allNodes, List<string> nodeNames, string[] allRowsString)
        {
            foreach (Node node in allNodes)
            {
                string thisNodeName = nodeNames[node.nameIndex];

                //Find how this node is connected
                foreach (string s in allRowsString)
                {
                    //Each row is a connection between two nodes, separated by -
                    //Conection A-b is only once in the list, so you wont find b-a anywhere!
                    string[] nodes = s.Split('-');

                    string nodeName_1 = nodes[0];
                    string nodeName_2 = nodes[1];

                    //This node is connected to name2
                    if (thisNodeName == nodeName_1)
                    {
                        //Find the node with name 2
                        Node name2Node = Node.GetNodeFromString(nodeName_2, allNodes, nodeNames);

                        node.connections.Add(name2Node);
                    }
                    else if (thisNodeName == nodeName_2)
                    {
                        Node name1Node = Node.GetNodeFromString(nodeName_1, allNodes, nodeNames);

                        node.connections.Add(name1Node);
                    }
                }
            }
        }
    }
}
