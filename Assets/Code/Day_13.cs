using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_13 : MonoBehaviour
{
    
    private void Start()
    {
        Part_1();
    }

    

    private void Part_1()
    {
        //Get the data 
        string[] allRowsString = FileManagement.GetInputData("Day_13", "input.txt");

        //Debug.Log($"Rows: {allRowsString.Length}");
        //Debug.Log($"Columns: {allRowsString[0]}");

        //Data consists of two parts
        //First set of rows are two integers separated by ,
        //The second part are rows consisting of "fold along x=655"
        //These sections are separated by a space

        List<string> dotsStrings = new List<string>();
        List<string> instructionsStrings = new List<string>();

        bool hasFoundSpace = false;

        foreach (string row in allRowsString)
        {
            if (row.Length == 0)
            {
                hasFoundSpace = true;
            }
            else if (!hasFoundSpace)
            {
                dotsStrings.Add(row);
            }
            else
            {
                instructionsStrings.Add(row);
            }
        }

        //Debug.Log(instructionsStrings.Count);

        //Find max rows and max columns (they are not the same!)
        int maxRow = -int.MaxValue;
        int maxCol = -int.MaxValue;

        foreach (string row in dotsStrings)
        {
            string[] cooordinates = row.Split(',');

            int coord_row = int.Parse(cooordinates[0]);
            int coord_col = int.Parse(cooordinates[1]);

            if (coord_row > maxRow)
            {
                maxRow = coord_row;
            }
            if (coord_col > maxCol)
            {
                maxCol = coord_col;
            }
        }

        //Debug.Log(maxRow);
        //Debug.Log(maxCol);

        //Generate the transparent paper
        //For some reason, the data is cols, rows
        bool[,] transparentPaper = new bool[maxCol + 1, maxRow + 1];

        //Fill the paper with dots
        foreach (string row in dotsStrings)
        {
            string[] cooordinates = row.Split(',');

            int coord_row = int.Parse(cooordinates[0]);
            int coord_col = int.Parse(cooordinates[1]);

            transparentPaper[coord_col, coord_row] = true;
        }

        //Display the map
        //DisplayPaper(transparentPaper);


        //Generate the instructions
        List<Instruction> allInstructions = new List<Instruction>();

        foreach (string s in instructionsStrings)
        {
            string[] instruction = s.Split('=');

            char direction = instruction[0][instruction[0].Length - 1];

            int line = int.Parse(instruction[1]);

            allInstructions.Add(new Instruction(direction, line));
        }

        //foreach (Instruction instruction in allInstructions)
        //{
        //    Debug.Log(instruction.direction + ": " + instruction.line);
        //}


        //
        // Fold the paper
        //

        //(0, 0) coordinate is top-left
        //y means fold the paper up at the line determined
        //x means fold left
        //Overlapping dots merge

        //int numberOfFolds = allInstructions.Count;
        int numberOfFolds = 1;

        for (int fold = 0; fold < numberOfFolds; fold++)
        {
            Instruction instruction = allInstructions[fold];

            bool[,] transparentPaperAfterFold;

            int rows = transparentPaper.GetLength(0);
            int cols = transparentPaper.GetLength(1);

            if (instruction.direction == 'x')
            {
                //Debug.Log("Hello");
            
                int colsAfterFold = Mathf.FloorToInt(cols / 2);

                transparentPaperAfterFold = new bool[rows, colsAfterFold];

                for (int row = 0; row < transparentPaper.GetLength(0); row++)
                {
                    for (int col = 0; col < colsAfterFold; col++)
                    {
                        //The corresponding col on the other side of the paper
                        int colOther = (colsAfterFold * 2) - col;

                        bool dot_1 = transparentPaper[row, col];
                        bool dot_2 = transparentPaper[row, colOther];

                        if (dot_1 || dot_2)
                        {
                            transparentPaperAfterFold[row, col] = true;
                        }
                    }
                }

                transparentPaper = transparentPaperAfterFold;
            }
            else if (instruction.direction == 'y')
            {
                int rowsAfterFold = Mathf.FloorToInt(rows / 2);

                transparentPaperAfterFold = new bool[rowsAfterFold, cols];

                for (int row = 0; row < rowsAfterFold; row++)
                {
                    //The corresponding row on the other side of the paper
                    int rowOther = (rowsAfterFold * 2) - row;
          
                    for (int col = 0; col < transparentPaper.GetLength(1); col++)
                    {
                        bool dot_1 = transparentPaper[row, col];
                        bool dot_2 = transparentPaper[rowOther, col];

                        if (dot_1 || dot_2)
                        {
                            transparentPaperAfterFold[row, col] = true;
                        }
                    }
                }

                transparentPaper = transparentPaperAfterFold;
            }

            
        }


        //DisplayPaper(transparentPaper);


        //Count number of visible dots
        int visibleDots = 0;

        for (int row = 0; row < transparentPaper.GetLength(0); row++)
        {
            for (int col = 0; col < transparentPaper.GetLength(1); col++)
            {
                if (transparentPaper[row, col])
                {
                    visibleDots += 1;
                }
            }
        }


        //Should be 669
        Debug.Log($"Number of visible dots: {visibleDots}");
    }



    private void DisplayPaper(bool[,] transparentPaper)
    {
        for (int row = 0; row < transparentPaper.GetLength(0); row++)
        {
            string rowString = "";

            for (int col = 0; col < transparentPaper.GetLength(1); col++)
            {
                if (transparentPaper[row, col])
                {
                    rowString += 1;
                }
                else
                {
                    rowString += 0;
                }
            }

            Debug.Log(rowString);
        }
    }
}



public struct Instruction
{
    public char direction;
    public int line;

    public Instruction(char direction, int line)
    {
        this.direction = direction;
        this.line = line;
    }
}
