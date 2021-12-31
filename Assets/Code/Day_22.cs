using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_22 : MonoBehaviour
{
    
    private void Start()
    {
        Part_1();    
    }

    

    private void Part_1()
    {
        //Get the data 
        string[] allRowsString = FileManagement.GetInputData("Day_22", "input_test.txt");

        //Rows such as on x=10..12,y=10..12,z=10..12

        List<Cuboid> rebootSteps = new List<Cuboid>();
        
        foreach (string s in allRowsString)
        {
            string[] onOff_coordinate = s.Split(' ');

            bool isOn = onOff_coordinate[0] == "on" ? true : false;

            //[x=10..12, y=10..12, z=10..12]
            string[] ranges = onOff_coordinate[1].Split(',');

            string[] xRanges = ranges[0].Substring(2).Split(new string[] { ".." }, StringSplitOptions.None);
            string[] yRanges = ranges[1].Substring(2).Split(new string[] { ".." }, StringSplitOptions.None);
            string[] zRanges = ranges[2].Substring(2).Split(new string[] { ".." }, StringSplitOptions.None);

            Vector3Int minRange = new Vector3Int(int.Parse(xRanges[0]), int.Parse(yRanges[0]), int.Parse(zRanges[0]));
            Vector3Int maxRange = new Vector3Int(int.Parse(xRanges[1]), int.Parse(yRanges[1]), int.Parse(zRanges[1]));

            Cuboid newCuboid = new Cuboid(isOn, minRange, maxRange);

            rebootSteps.Add(newCuboid);

            //break;
        }

        //Cuboid c1 = rebootSteps[0];

        //c1.Display();

        //Cuboid c2 = rebootSteps[rebootSteps.Count - 1];

        //c2.Display();


        //To generate the reactor we need to know the min and max values of all cubes
        int minX = int.MaxValue;
        int minY = int.MaxValue;
        int minZ = int.MaxValue;

        int maxX = int.MinValue;
        int maxY = int.MinValue;
        int maxZ = int.MinValue;

        foreach (Cuboid c in rebootSteps)
        {
            if (c.min.x < minX)
            {
                minX = c.min.x;
            }
            if (c.min.y < minY)
            {
                minY = c.min.y;
            }
            if (c.min.z < minZ)
            {
                minZ = c.min.z;
            }

            if (c.max.x > maxX)
            {
                maxX = c.max.x;
            }
            if (c.max.y > maxY)
            {
                maxY = c.max.y;
            }
            if (c.max.z > maxZ)
            {
                maxZ = c.max.z;
            }
        }

        //All cubes start as off
        //The min values can be negative, so we have to find the range, and then translate from reactor boot step to 3d array pos
        //+1 to include 0, which we should?????
        int xRange = Math.Abs(maxX - minX)+ 1;
        int yRange = Math.Abs(maxY - minY)+ 1;
        int zRange = Math.Abs(maxZ - minZ)+ 1;

        //Size x: 77545, Size y: 166235, Size z: 81132 which is too big for a single [,,,] size 10^15 so we have to use voxels
        //Debug.Log($"Size x: {xRange}, Size y: {yRange}, Size z: {zRange}");


        //But in part 1, then we are only interested in cuboids within the range [-50, 50]
        int arrayRange = 101;
        bool[,,] reactor = new bool[arrayRange, arrayRange, arrayRange];

        //max = 30, min = -20 -> range = 51
        //So if we have -20 we want to get 0 -> -20 + -20 = 0;
        //If we have 30 we want 50 -> 30 + -20 = 50

        

        foreach (Cuboid c in rebootSteps)
        {        
            //We can use AABB to quickly determine if a cell is within range
            for (int x = 0; x < reactor.GetLength(0); x++)
            {
                for (int y = 0; y < reactor.GetLength(1); y++)
                {
                    for (int z = 0; z < reactor.GetLength(2); z++)
                    {
                        //Translate reactor coordinates to array data structure
                        Vector3Int reactorMin = new Vector3Int(c.min.x + minX, c.min.y + minY, c.min.z + minZ);
                        Vector3Int reactorMax = new Vector3Int(c.max.x + minX, c.max.y + minY, c.max.z + minZ);

                        if (
                            x >= reactorMin.x && x <= reactorMax.x &&
                            y >= reactorMin.y && y <= reactorMax.y &&
                            z >= reactorMin.z && z <= reactorMax.z
                        )
                        {
                            reactor[x, y, x] = c.turnOn;
                        }
                    }
                }
            }
        }


        //Count number of on cubes after the initalization step
        //The initalization step considers only cubes in the region -50 -> 50, so count those
        int numberOfOnCubes = 0;

        for (int x = 0; x < reactor.GetLength(0); x++)
        {
            for (int y = 0; y < reactor.GetLength(1); y++)
            {
                for (int z = 0; z < reactor.GetLength(2); z++)
                {
                    //Translate from array to reactor coordinates
                    Vector3Int coordinate = new Vector3Int(x - minX, y - minY, z - minZ);

                    if (coordinate.x > 50 || coordinate.x < -50 ||
                        coordinate.y > 50 || coordinate.y < -50 ||
                        coordinate.z > 50 || coordinate.z < -50
                    )
                    {
                        continue;
                    }

                    if (reactor[x, y, z])
                    {
                        numberOfOnCubes += 1;
                    }
                }
            }
        }

        Debug.Log($"Number of on cubes: {numberOfOnCubes}");
    }



    public struct Cuboid
    {
        public bool turnOn;

        //If we have x=-20..26,y=-36..17,z=-47..7
        //Then min = (-20, -36, -47)
        //max = (26, 17, 7)
        public Vector3Int min;
        public Vector3Int max;

        public Cuboid(bool turnOn, Vector3Int min, Vector3Int max)
        {
            this.turnOn = turnOn;
            this.min = min;
            this.max = max;
        }



        public void Display()
        {
            Debug.Log(turnOn);
            Debug.Log(min);
            Debug.Log(max);

            Debug.Log(" ");
        }
    }
}

