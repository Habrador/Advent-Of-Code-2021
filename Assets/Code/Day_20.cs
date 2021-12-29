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

        //char[,] paddedImage = AddPaddingToImage(image, padding: 1);

        //DisplayImage(paddedImage);
        //return;
        

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

        int STEPS = 2;

        for (int step = 0; step < STEPS; step++)
        {
            //Add a 1 cell border to the image
            if (step % 2 != 0)
            {
                image = AddPaddingToImage(image, padding: 1, paddingCharacter: '#');
            }
            else
            {
                image = AddPaddingToImage(image, padding: 1, paddingCharacter: '.');
            }

            

            int imageSize = image.GetLength(0);

            char[,] imageBuffer = GetEmptyImage(imageSize, defaultCharacter: '.');

            for (int row = 0; row < imageSize; row++)
            {
                for (int col = 0; col < imageSize; col++)
                {
                    string binaryString = "";

                    //Get the 9 cells 
                    foreach (Vector2Int dir in directions)
                    {
                        Vector2Int cellPos = new Vector2Int(row + dir.x, col + dir.y);

                        char c = '.'; //Just default
                        
                        //If we are outside of the image then we have dark (.) cells
                        if (cellPos.x < 0 || cellPos.x >= imageSize || cellPos.y < 0 || cellPos.y >= imageSize)
                        {
                            c = '.';

                            //Unven numbers means we have to swap
                            if (step % 2 != 0)
                            {
                                c = '#';
                            }
                        }
                        else
                        {
                            c = image[cellPos.x, cellPos.y];
                        }

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

                    imageBuffer[row, col] = correspondingChar;

                    //if (row == 7 && col == 7)
                    //{
                    //    Debug.Log(binaryString);

                    //    Debug.Log(correspondingChar);
                    //}
                }
            }

            //Swap with the buffer
            image = imageBuffer;

            //DisplayImage(image);
        }



        DisplayImage(image);

        //Count number of lit pixels in the final image = count the number 0f #

        int counter = 0;

        int finalImageSize = image.GetLength(0);

        for (int row = 0; row < finalImageSize; row++)
        {
            for (int col = 0; col < finalImageSize; col++)
            {
                char c = image[row, col];
                
                if (c == '#')
                {
                    counter += 1;
                }
            }
        }


        //Should be 5301
        Debug.Log($"Number of lit pixels: {counter}");
    }



    private char[,] AddPaddingToImage(char[,] image, int padding, char paddingCharacter)
    {
        int size = image.GetLength(0);

        int paddedImageSize = (padding * 2) + size;

        char[,] paddedImage = GetEmptyImage(paddedImageSize, paddingCharacter);

        for (int i = padding; i < padding + size; i++)
        {
            for (int j = padding; j < padding + size; j++)
            {
                paddedImage[i, j] = image[i - padding, j - padding];
            }
        }

        return paddedImage;
    }



    private char[,] GetEmptyImage(int size, char defaultCharacter)
    {
        char[,] paddedImage = new char[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                paddedImage[i, j] = defaultCharacter;
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
