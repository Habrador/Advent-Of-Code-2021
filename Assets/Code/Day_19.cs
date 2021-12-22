using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_19 : MonoBehaviour
{
    
    private void Start()
    {
        Part_1();
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

        Vector3Int[] orientationMultiplier = new Vector3Int[]
        {
            new Vector3Int( 1,  1,  1),
            
            new Vector3Int( 1, -1,  1),
            new Vector3Int( 1,  1, -1),
            new Vector3Int( 1, -1, -1),
            
            new Vector3Int(-1,  1,  1),
            new Vector3Int( 1,  1, -1),
            new Vector3Int(-1,  1, -1),
            
            new Vector3Int(-1,  1,  1),
            new Vector3Int( 1, -1,  1),
            new Vector3Int(-1, -1,  1),
            
            new Vector3Int(-1, -1, -1)
        };



    }
}
