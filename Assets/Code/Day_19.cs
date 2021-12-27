using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_19 : MonoBehaviour
{
    
    private void Start()
    {
        Part_1();


        TestRotation();
    }

    

    private void Part_1()
    {
        //Get the data 
        string[] allRowsString = FileManagement.GetInputData("Day_19", "input_test.txt");

        Debug.Log(allRowsString.Length);
        Debug.Log(allRowsString[0]);
        Debug.Log(allRowsString[allRowsString.Length - 1]);

        //Data is from several scanners
        //Each scanner starts with --- and theres a space between each scanner
        //Below --- theres the 3d coordinate in integer form

        List<List<Vector3Int>> scannerData = new List<List<Vector3Int>>();

        List<Vector3Int> thisScanner = new List<Vector3Int>();

        foreach (string row in allRowsString)
        {
            //Important to use row[1] to detect --- scanner 1 --- If we had used row[0] it will detect - signs infornt of numbers
            if (row.Length != 0 && row[1] == '-')
            {
                continue;
            }

            if (row.Length == 0)
            {
                scannerData.Add(thisScanner);
            
                thisScanner = new List<Vector3Int>();

                continue;
            }

            //Now we have something like 567,511,478
            string[] coordinateString = row.Split(',');

            int coordX = int.Parse(coordinateString[0].Trim());
            int coordY = int.Parse(coordinateString[1].Trim());
            int coordZ = int.Parse(coordinateString[2].Trim());

            Vector3Int coord = new Vector3Int(coordX, coordY, coordZ);

            thisScanner.Add(coord);
        }

        //Add the last scanner which never will be added in the loop
        scannerData.Add(thisScanner);

        
        Debug.Log($"Number of scanners: {scannerData.Count}"); //Should be 39

        Debug.Log(scannerData[0][0]);
        Debug.Log(scannerData[0][scannerData[0].Count - 1]);

        Debug.Log(scannerData[1][0]);
        Debug.Log(scannerData[1][scannerData[1].Count - 1]);

        Debug.Log(scannerData[scannerData.Count - 1][0]);
        Debug.Log(scannerData[scannerData.Count - 1][scannerData[scannerData.Count - 1].Count - 1]);
        


        //A beacon can be at most 1000 units away from a scanner in 3d space
        //Input is relative positions of beacons detected by each scanner
        //But the positon of a scanner in 3d space is not known!
        //Each scanner might also have different orientation (clamped to 90 around each axis)
        //To rotate a scanner you just change y to -y, or replace x with y resulting in 24 different combinations
        //Assume scanner 0 is at (0,0,0)

        //8*6 = 48
        Vector3Int[] orientationMultiplier = new Vector3Int[]
        {
            new Vector3Int( 1,  1,  1),
            
            new Vector3Int( 1, -1,  1),
            new Vector3Int( 1,  1, -1),
            new Vector3Int( 1, -1, -1),
            
            new Vector3Int(-1,  1,  1),
            //new Vector3Int( 1,  1, -1),
            new Vector3Int(-1,  1, -1),
            
            //new Vector3Int(-1,  1,  1),
            //new Vector3Int( 1, -1,  1),
            new Vector3Int(-1, -1,  1),
            
            new Vector3Int(-1, -1, -1)
        };

        //Switch of coordinate system:
        //[x, y, z]
        //[x, z, y]
        //[x, -y, z]
        //[x, -z, y]

        //[-x, y, z]
        //[-x, z, y]
        //[-x, -y, z]
        //[-x, -z, y]


        //[z, x, y]
        //[z, y, x]
        //[y, x, z]
        //[y, z, x]


        

    }


    private void TestRotation()
    {
        Vector3Int p0 = new Vector3Int(0, 0, 0);
        Vector3Int p1 = new Vector3Int(2, -3, -1);
        Vector3Int p2 = new Vector3Int(2, 3, -1);
        Vector3Int p3 = new Vector3Int(2, 0, 4);

        //Vector3Int[] coordinates = new Vector3Int[]

        DrawTestTetrahedron(p0, p1, p2, p3);


        //Rotate 90 degrees (ccw)

        //Multiply the y part of the vector by -1, and then swap y and z values
        Vector3Int p0_90 = new Vector3Int(0, 0, 10);
        Vector3Int p1_90 = new Vector3Int(2, -1, 3) + p0_90;
        Vector3Int p2_90 = new Vector3Int(2, -1, -3) + p0_90;
        Vector3Int p3_90 = new Vector3Int(2, 4, -0) + p0_90;

        DrawTestTetrahedron(p0_90, p1_90, p2_90, p3_90);


        //Rotate 180 degrees
        Vector3Int p0_180 = new Vector3Int(0, 0, 20);
        Vector3Int p1_180 = new Vector3Int(2, 3, 1) + p0_180;
        Vector3Int p2_180 = new Vector3Int(2, -3, 1) + p0_180;
        Vector3Int p3_180 = new Vector3Int(2, 0, -4) + p0_180;

        DrawTestTetrahedron(p0_180, p1_180, p2_180, p3_180);


        //Rotate 270 degrees
        Vector3Int p0_270 = new Vector3Int(0, 0, 30);
        Vector3Int p1_270 = new Vector3Int(2, 1, -3) + p0_270;
        Vector3Int p2_270 = new Vector3Int(2, 1, 3) + p0_270;
        Vector3Int p3_270 = new Vector3Int(2, -4, 0) + p0_270;

        DrawTestTetrahedron(p0_270, p1_270, p2_270, p3_270);
    }



    private void DrawTestTetrahedron(Vector3Int p0, Vector3Int p1, Vector3Int p2, Vector3Int p3)
    {
        float time = 10000f;

        Debug.DrawLine(p0, p1, Color.blue, time);
        Debug.DrawLine(p0, p2, Color.red, time);
        Debug.DrawLine(p0, p3, Color.green, time);

        Debug.DrawLine(p1, p2, Color.white, time);
        Debug.DrawLine(p2, p3, Color.white, time);
        Debug.DrawLine(p3, p1, Color.white, time);
    }
}
