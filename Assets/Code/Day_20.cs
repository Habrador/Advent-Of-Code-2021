using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_20 : MonoBehaviour
{
    
    private void Start()
    {
        Part_1();
    }



    private void Part_1()
    {
        GetData(out string imageEnhancementAlgorithm, out char[,] image);

        //Enhance the image
        //The image is actually of infinite size, so we need to add padding with dark pixels (.)
        //DisplayImage(image);

        //Debug.Log(image[4, 0]);
        //Debug.Log(image[4, 1]);
        //Debug.Log(image[4, 2]);

        int padding = 10;

        int size = image.GetLength(0);

        int paddedImageSize = (padding * 2) + size;

        char[,] paddedImage = GetEmptyImage(paddedImageSize);

        for (int i = padding; i < padding + size; i++)
        {
            for (int j = padding; j < padding + size; j++)
            {
                paddedImage[i, j] = image[i - padding, j - padding];
            }
        }

        //DisplayImage(paddedImage);

        //Test conversion and lookup
        //Debug.Log(Convert.ToInt32("000100010", fromBase: 2));
        //Debug.Log(imageEnhancementAlgorithm[Convert.ToInt32("000100010", fromBase: 2)]);
        //Debug.Log(imageEnhancementAlgorithm.Length);

        //row, col
        //Debug.Log(paddedImage[7, 3]);
        //Debug.Log(paddedImage[7, 4]);
        //Debug.Log(paddedImage[7, 5]);
        //Debug.Log(paddedImage[7, 6]);

        //Enhance the image
        char[,] paddedImageBuffer = GetEmptyImage(paddedImageSize);

        //The directions we can move
        //These have to be sorted wo we get the pixels row-by-row to be able to convert to a binary number
        //Row, col
        // (-1, -1) (-1,  0) (-1,  1)
        // ( 0, -1) ( 0,  0) ( 0,  1)
        // ( 1, -1) ( 1,  0) ( 1,  1)

        Vector2Int[] directions = {
            //Upper row first!
            new Vector2Int(-1, -1),
            new Vector2Int(-1,  0),
            new Vector2Int(-1,  1),

            new Vector2Int( 0, -1),
            new Vector2Int( 0,  0),
            new Vector2Int( 0,  1),

            new Vector2Int( 1, -1),
            new Vector2Int( 1,  0),
            new Vector2Int( 1,  1)
        };

        int STEPS = 1;

        for (int step = 0; step < STEPS; step++)
        {

            //Ignore the outer row/column because they shouldnt change
            for (int row = 1; row < paddedImageSize - 1; row++)
            {
                for (int col = 1; col < paddedImageSize - 1; col++)
                {
                    string binaryString = "";

                    //Get the 9 cells 
                    foreach (Vector2Int dir in directions)
                    {
                        Vector2Int cellPos = new Vector2Int(row + dir.x, col + dir.y);

                        char c = paddedImage[cellPos.x, cellPos.y];

                        binaryString += (c == '.') ? '0' : '1';

                        //if (row == 7 && col == 7)
                        //{
                        //    Debug.Log(c);
                        //}
                    }

                    //When this cell is done, we have to find which character to put in its place
                    int decimalNumber = Convert.ToInt32(binaryString, fromBase: 2);

                    //Find the corresponding char in imageEnhancementAlgorithm string
                    char correspondingChar = imageEnhancementAlgorithm[decimalNumber];

                    paddedImageBuffer[row, col] = correspondingChar;

                    //if (row == 7 && col == 7)
                    //{
                    //    Debug.Log(binaryString);

                    //    Debug.Log(correspondingChar);
                    //}
                }
            }

            //Swap buffers
            paddedImage = paddedImageBuffer;

            //Reset buffer for next loop
            paddedImageBuffer = GetEmptyImage(paddedImageSize);
        }



        DisplayImage(paddedImage);

        //Count number of lit pixels in the final image = count the number 0f #

        int counter = 0;

        for (int row = 1; row < paddedImageSize - 1; row++)
        {
            for (int col = 1; col < paddedImageSize - 1; col++)
            {
                char c = paddedImage[row, col];
                
                if (c == '#')
                {
                    counter += 1;
                }
            }
        }


        //Smaller than 5302
        Debug.Log($"Number of lit pixels: {counter}");
    }



    private char[,] GetEmptyImage(int size)
    {
        char[,] paddedImage = new char[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                paddedImage[i, j] = '.';
            }
        }

        return paddedImage;
    }



    private void GetData(out string imageEnhancementAlgorithm, out char[,] image)
    {
        //Get the data 
        string[] allRowsString = FileManagement.GetInputData("Day_20", "input.txt");

        //First row is the image enhancement algorithm 
        //Then there's a space
        //And then comes the image, which is 100x100, where
        // - # light pixel
        // - . dark pixel
        imageEnhancementAlgorithm = allRowsString[0];

        //Debug.Log(allRowsString.Length);
        //Debug.Log(allRowsString[2].Length);

        int imageSize = allRowsString[2].Length;

        image = new char[imageSize, imageSize];

        for (int i = 2; i < allRowsString.Length; i++)
        {
            string s = allRowsString[i];

            for (int j = 0; j < s.Length; j++)
            {
                //-2 because i starts at 2 and we want 0-99 range
                image[i - 2, j] = s[j];
            }
        }

        //Debug.Log(image[0, 0]);
        //Debug.Log(image[0, imageSize - 1]);
        //Debug.Log(image[imageSize - 1, 0]);
        //Debug.Log(image[imageSize - 1, imageSize - 1]);
    }



    private void DisplayImage(char[,] image)
    {
        int size = image.GetLength(0);

        for (int i = 0; i < size; i++)
        {
            string display = "";
        
            for (int j = 0; j < size; j++)
            {
                char c = image[i, j];

                //Easier to see the map if we display - instead of .
                display += (c == '.') ? ", " : "-";
            }

            Debug.Log(display);
        }

        Debug.Log(" ");
    }
}
