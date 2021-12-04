using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_04 : MonoBehaviour
{
    
    private void Start()
    {
        Part_1();
    }



    private void Part_1()
    {
        //
        // Prepare the data
        //
    
        string[] allData = FileManagement.GetInputData("Day_04", "input.txt");

        //Debug.Log(allData[0]);
        //Debug.Log(allData[1]);
        //Debug.Log(allData[2]);

        //The first row is the random numbers being drawn and they are comma separated
        string[] bingoBallsStrings = allData[0].Split(',');

        int[] bingoBalls = Array.ConvertAll(bingoBallsStrings, int.Parse);

        //Debug.Log($"Number of balls: {bingoBalls.Length}");
        //Debug.Log(bingoBalls[0]);
        //Debug.Log(bingoBalls[bingoBalls.Length - 1]);

        List<int[,]> allBoards = GetBingoBoards(allData);

        //We also need an array that keep tracks of how the bingo game is going for each board
        List<bool[,]> boardProgress = new List<bool[,]>();

        for (int i = 0; i < allBoards.Count; i++)
        {
            bool[,] progress = new bool[5, 5];

            boardProgress.Add(progress);
        }



        //
        // Play the game
        //

        int winningBoardIndex = -1;
        int winningBingoBall = -1;

        for (int i = 0; i < bingoBalls.Length; i++)
        {
            int bingoBall = bingoBalls[i];

            //Mark all numbers
            MarkBoards(allBoards, boardProgress, bingoBall);

            //Check if a board has been winning
            winningBoardIndex = CheckWinningBoard(boardProgress);

            if (winningBoardIndex != -1)
            {
                Debug.Log($"The winning board is: {winningBoardIndex}");

                winningBingoBall = bingoBall;

                break;
            }
        }


        //To calculate the final number you add all the unmarked numbers of the winning board and multiply them with the last number drawn
        int[,] winningBoard = allBoards[winningBoardIndex];

        int sum = 0;

        for (int row = 0; row < 5; row++)
        {
            for (int column = 0; column < 5; column++)
            {
                if (!boardProgress[winningBoardIndex][row, column])
                {
                    sum += winningBoard[row, column];
                }
            }
        }

        int finalScore = sum * winningBingoBall;

        Debug.Log($"Final score: {finalScore}");
    }


    //Only rows and columns can win - no diagonals!
    //Returns -1 if no board has been winning, otherwise the index (only one board can win???)
    private int CheckWinningBoard(List<bool[,]> boardProgress)
    {
        int winningBoardIndex = -1;

        for (int boardIndex = 0; boardIndex < boardProgress.Count; boardIndex++)
        {
            bool[,] thisBoard = boardProgress[boardIndex];

            //Check rows
            for (int row = 0; row < 5; row++)
            {
                int winCounter = 0;

                for (int column = 0; column < 5; column++)
                {
                    if (boardProgress[boardIndex][row, column])
                    {
                        winCounter += 1;
                    }
                }

                if (winCounter == 5)
                {
                    return boardIndex;
                }
            }


            //Check columns
            for (int column = 0; column < 5; column++)
            {
                int winCounter = 0;

                for (int row = 0; row < 5; row++)
                {
                    if (boardProgress[boardIndex][row, column])
                    {
                        winCounter += 1;
                    }
                }

                if (winCounter == 5)
                {
                    return boardIndex;
                }
            }

        }

        return winningBoardIndex;
    }



    private void MarkBoards(List<int[,]> allBoards, List<bool[,]> boardProgress, int bingoBallNumber)
    {
        for (int boardIndex = 0; boardIndex < allBoards.Count; boardIndex++)
        {
            int[,] thisBoard = allBoards[boardIndex];

            for (int row = 0; row < 5; row++)
            {
                for (int column = 0; column < 5; column++)
                {
                    if (thisBoard[row, column] == bingoBallNumber)
                    {
                        boardProgress[boardIndex][row, column] = true;
                    }
                }
            }
        }
    }



    private List<int[,]> GetBingoBoards(string[] allData)
    {
        List<int[,]> allBoards = new List<int[,]>();

        //The boards are 5x5 and theres a space between each board and a space between the first row and the first board
        //Single numbers also have a space infront of them

        //string[] testData = allData[2].Split(' ');

        //Debug.Log(testData[0]);
        //Debug.Log(testData[testData.Length - 1]);

        //int[] testData = ConvertBingoRowDataToString(allData[2]);

        //Debug.Log(testData[0]);
        //Debug.Log(testData[testData.Length - 1]);


        for (int row = 2; row < allData.Length; row += 6)
        {
            int[,] singleBoard = new int[5, 5];

            int[] boardRow_1 = ConvertBingoRowStringToIntArray(allData[row + 0]);
            int[] boardRow_2 = ConvertBingoRowStringToIntArray(allData[row + 1]);
            int[] boardRow_3 = ConvertBingoRowStringToIntArray(allData[row + 2]);
            int[] boardRow_4 = ConvertBingoRowStringToIntArray(allData[row + 3]);
            int[] boardRow_5 = ConvertBingoRowStringToIntArray(allData[row + 4]);

            AddColumnsToArray(singleBoard, boardRow_1, 0);
            AddColumnsToArray(singleBoard, boardRow_2, 1);
            AddColumnsToArray(singleBoard, boardRow_3, 2);
            AddColumnsToArray(singleBoard, boardRow_4, 3);
            AddColumnsToArray(singleBoard, boardRow_5, 4);

            allBoards.Add(singleBoard);
        }


        //Debug.Log(allBoards.Count);
        //Debug.Log(allBoards[0][0, 0]); //7
        //Debug.Log(allBoards[0][0, 1]); // 42 so they are organized as [row, column]
        //Debug.Log(allBoards[0][4, 4]);
        //Debug.Log(allBoards[allBoards.Count - 1][0, 0]);
        //Debug.Log(allBoards[allBoards.Count - 1][4, 4]);

        return allBoards;
    }



    private int[] ConvertBingoRowStringToIntArray(string rowString)
    {
        string[] noSpacesBetweenNumbers = rowString.Split(' ');

        //Single numbers also have a space infront of them so the above array may include some empty strings

        List<int> rowNumbers = new List<int>();

        foreach (string s in noSpacesBetweenNumbers)
        {
            if (!String.IsNullOrWhiteSpace(s))
            {
                int number = int.Parse(s);

                rowNumbers.Add(number);
            }
        }

        return rowNumbers.ToArray();
    }



    private void AddColumnsToArray(int[,] singleBoard, int[] boardRows, int rowIndex)
    {
        for (int columnIndex = 0; columnIndex < boardRows.Length; columnIndex++)
        {
            singleBoard[rowIndex, columnIndex] = boardRows[columnIndex];
        }
    }
    
}
