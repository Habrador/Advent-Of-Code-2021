using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_24 : MonoBehaviour
{
    
    private void Start()
    {
        Part_1();    
    }



    private void Part_1()
    {
        //Get the data 
        //string[] allRowsString = FileManagement.GetInputData("Day_24", "input.txt");

        //Line after line of "add y w," etc
        //Consists of 14 calculations each starting with "inp w"

        //Optimization is the key so we have to hard code the instructions - not read them line by line
        int[] all_dz = new int[] { 1, 1, 1, 1, 1, 26, 1, 26, 26, 1, 26, 26, 26, 26};
        int[] all_dx = new int[] { 15, 10, 12, 10, 14, -11, 10, -16, -9, 11, -8, -8, -10, -9 };
        int[] all_dy = new int[] { 13, 16, 2, 8, 11, 6, 12, 2, 2, 15, 1, 10, 14, 10}; 
        

        //char test = 'x';

        //Debug.Log(test.Equals('x'));

        StartCoroutine(DoCalculations(all_dx, all_dy, all_dz));
        
    }



    private IEnumerator DoCalculations(int[] all_dx, int[] all_dy, int[] all_dz)
    {
        //Input is a 14 digit integer 1-9 where each integer is input to a calculation

        //We should find the largest valid model number, so start with max
        long modelNumber = 99999999999999;

        string modelNumberString = modelNumber.ToString();

        //Debug.Log(modelNumberString);

        int safety = 0;

        while (modelNumber > 0)
        {
            //safety += 1;

            //if (safety > 10000)
            //{
            //    break;
            //}


            //Check if this is a valid model number that doesnt include any zeros
            bool isModelNumberValid = true;

            for (int i = 0; i < modelNumberString.Length; i++)
            {
                if (modelNumberString[i].Equals('0'))
                {
                    modelNumber -= 1;

                    modelNumberString = modelNumber.ToString();

                    isModelNumberValid = false;

                    break;
                }
            }

            if (!isModelNumberValid)
            {
                //Debug.Log("Not valid");
            
                continue;
            }
            
            //Cant be % 10 because 0 is not alloed wo will never happen
            //This also makes it easier to stop Unity without ctrl-alt-del
            if (modelNumber % 1111111 == 0)
            {
                Debug.Log(modelNumber);

                yield return null;
            }


            //Integer variables start with 0
            //We only need to set z to 0 because x and y are set to 0 every time we run the instructions
            int z = 0;

            for (int index = 0; index < all_dx.Length; index++)
            {
                //Char to int 
                int w = modelNumberString[index] - '0';

                //Do the calculations
                int dx = all_dx[index];
                int dy = all_dy[index];
                int dz = all_dz[index];

                DoSimplifiedInstructions(ref z, w, dx, dy, dz);
            }


            //When the 14 calculations are finished, we check variable z. If z is 0, then the model number is valid, otherwise we try a new model
            if (z == 0)
            {
                Debug.Log($"The highest model number is: {modelNumber}");

                break;
            }
            else
            {
                modelNumber -= 1;

                modelNumberString = modelNumber.ToString();
            }
        }
    }



    //All instructions look basically the same, so we should be able to simplify to speed up the bazilion of calculations
    //mul x 0       //Always sets x to 0
    //add x z       //x = z
    //mod x 26      //I always 26 
    //div z 1       //1 = dz
    //add x 10      //10 = dx 
    //eql x w
    //eql x 0
    //mul y 0       //Always sets y to 0
    //add y 25      //Alays adds 25, so we might as well always set y to 25
    //mul y x
    //add y 1       //alays adds 1
    //mul z y
    //mul y 0       //Always sets y to 0
    //add y w       //Always adds w to y, so we might as well set y to w
    //add y 16      //16 = dy
    //mul y x
    //add z y

    //mul x 0
    //add x z
    //mod x 26
    //div z 26 //26 = dz
    //add x -8 //-8 = dx
    //eql x w
    //eql x 0
    //mul y 0
    //add y 25 
    //mul y x
    //add y 1
    //mul z y
    //mul y 0
    //add y w

    //add y 10
    //mul y x
    //add z y

    //Do x, y, z, w need to be long?
    //x and y are always set to 0 in the instructions, so we dont need those as input
    private void DoSimplifiedInstructions(ref int z, int w, int dx, int dy, int dz)
    {
        int x = (z % 26) + dx;

        //if (dz != 0)
        //{
        //    z = (int)Math.Truncate((double)z / (double)dz); //Or should it be floor?
        //}

        //After inspection we can see that dz is never 0, so we can optimize that calculation
        z = (int)Math.Truncate((double)z / (double)dz); //Or should it be floor?

        //x = (x == w) ? 1 : 0;
        //x = (x == 0) ? 1 : 0;
        //Above can be simplified to
        x = (x != w) ? 1 : 0;

        int y = (25 * x) + 1;

        z = z * y;

        y = (w + dy) * x;

        z = z + y;
    }



    //
    // Math operations
    //

    private void Add(ref int a, int b)
    {
        a += b;
    }



    private void Mul(ref int a, int b)
    {
        a *= b;
    }



    private void Div(ref int a, int b)
    {
        //Make sure no division by 0
        if (b == 0)
        {
            return;
        }

        a = (int)Math.Truncate((double)a / (double)b);
    }



    private void Mod(ref int a, int b)
    {
        //5 % 3 = 2 because 3 fits in 5 once and whats left is the 2

        //Make sure a < 0 and b <= 0 will not happen

        if (a < 0 || b <= 0)
        {
            return;
        }

        a = a % b;
    }



    private void Eql(ref int a, int b)
    {
        a = (a == b) ? 1 : 0;
    }
}

