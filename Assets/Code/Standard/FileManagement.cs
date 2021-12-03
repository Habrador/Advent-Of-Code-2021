using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FileManagement
{

    //Get all rows from a file located in Assets/StreamingAssets folder
    public static int[] GetInputData(string folderName, string fileName)
    {
        //Read the input
        //File should be in Assets/StreamingAssets
        //https://stackoverflow.com/questions/67744910/importing-each-line-from-text-file-from-resources-in-unity-to-list-in-c-sharp
        string[] allRowsString = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, $"{folderName}/{fileName}"));

        //Convert from string to int
        int[] allRows = Array.ConvertAll(allRowsString, int.Parse);

        //Debug.Log(allRows.Length);

        return allRows;
    }
}
