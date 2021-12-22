using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_18 : MonoBehaviour
{
    
    private void Start()
    {
        Part_1();
    }

    

    private void Part_1()
    {
        //Get the data 
        string[] allRowsString = FileManagement.GetInputData("Day_18", "input.txt");

        Debug.Log(allRowsString.Length);
        Debug.Log(allRowsString[0]);
        Debug.Log(allRowsString[allRowsString.Length - 1]);

        //100 rows of 
        //[ 
        //    [7, [1,5]], [[5,7], [[0,8],2] ] 
        //]
    }
}
