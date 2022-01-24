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
    private void SimplifiedInstructions(ref int z, ref int w, int dx, int dy, int dz)
    {
        int x = z & 26 + dx;

        if (dz != 0)
        {
            z = (int)Math.Truncate((double)z / (double)dz); //Or should it be floor?
        }

        //x = (x == w) ? 1 : 0;
        //x = (x == 0) ? 1 : 0;
        //Above can be simplified to
        x = (x != w) ? 1 : 0;

        int y = (25 * x) + 1;

        z = z * y;

        y = (w + dy) * x;

        z = z + y;
    }



    private void Part_1()
    {
        //Get the data 
        string[] allRowsString = FileManagement.GetInputData("Day_24", "input.txt");

        //Line after line of "add y w," etc
        //Consists of 14 calculations each starting with "inp w"

        //Standardize instructions to speed up calculations
        List<Instruction> instructions = new List<Instruction>();

        Wrapper xWrapper = new Wrapper();
        Wrapper yWrapper = new Wrapper();
        Wrapper zWrapper = new Wrapper();
        Wrapper wWrapper = new Wrapper();

        foreach (string instructionString in allRowsString)
        {
            //Most are "add y w" but some are "inp w"
            //But the operation is always 3 characters
            string operation = instructionString.Substring(0, 3);

            if (operation == "inp")
            {
                instructions.Add(new Instruction(Operations.Inp, wWrapper, null, 0));
            }
            else
            {
                //"add y w" -> y 
                char a = char.Parse(instructionString.Substring(4, 1));

                Wrapper aWrapper = null;

                if (a.Equals('x'))
                {
                    aWrapper = xWrapper;
                }
                else if (a.Equals('y'))
                {
                    aWrapper = yWrapper;
                }
                else if (a.Equals('z'))
                {
                    aWrapper = zWrapper;
                }
                else if (a.Equals('w'))
                {
                    aWrapper = wWrapper;
                }


                //b can be either variable or number
                //"add y w" -> w 
                string bString = instructionString.Substring(6, 1);

                Wrapper bWrapper = null;
                int bValue = -1;

                //b is a number
                if (int.TryParse(bString, out int result))
                {
                    bValue = result;
                }
                //b is a variable
                else
                {
                    char b = char.Parse(bString);

                    if (b.Equals('x'))
                    {
                        bWrapper = xWrapper;
                    }
                    else if (b.Equals('y'))
                    {
                        bWrapper = yWrapper;
                    }
                    else if (b.Equals('z'))
                    {
                        bWrapper = zWrapper;
                    }
                    else if (b.Equals('w'))
                    {
                        bWrapper = wWrapper;
                    }
                }

                if (operation == "add")
                {
                    instructions.Add(new Instruction(Operations.Add, aWrapper, bWrapper, bValue));
                }
                else if (operation == "mul")
                {
                    instructions.Add(new Instruction(Operations.Mul, aWrapper, bWrapper, bValue));
                }
                else if (operation == "div")
                {
                    instructions.Add(new Instruction(Operations.Div, aWrapper, bWrapper, bValue));
                }
                else if (operation == "mod")
                {
                    instructions.Add(new Instruction(Operations.Mod, aWrapper, bWrapper, bValue));
                }
                else if (operation == "eql")
                {
                    instructions.Add(new Instruction(Operations.Eql, aWrapper, bWrapper, bValue));
                }
            }
        }

        //char test = 'x';

        //Debug.Log(test.Equals('x'));

        StartCoroutine(DoCalculations(instructions, xWrapper, yWrapper, zWrapper, wWrapper));
        
    }



    private IEnumerator DoCalculations(List<Instruction> instructions, Wrapper xWrapper, Wrapper yWrapper, Wrapper zWrapper, Wrapper wWrapper)
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


            //Pos in modelNumberString
            int calculationNumber = -1;

            //Integer variables start with 0
            xWrapper.value = 0;
            yWrapper.value = 0;
            zWrapper.value = 0;
            wWrapper.value = 0;

            foreach (Instruction instruction in instructions)
            {
                //Inp means that we set w variable to a number
                if (instruction.operation == Operations.Inp)
                {
                    calculationNumber += 1;

                    //Char to int 
                    wWrapper.value = modelNumberString[calculationNumber] - '0';
                }
                //Otherwise we do some math instruction
                else
                {
                    DoMath(instruction);
                }
            }

            //When the 14 calculations are finished, we check variable z. If z is 0, then the model number is valid, otherwise we try a new model
            if (zWrapper.value == 0)
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


    private class Wrapper
    {
        public int value = 0;
    }


    private enum Operations 
    {
        Inp, Add, Mul, Div, Mod, Eql
    }

    private struct Instruction
    {
        public Operations operation;

        public Wrapper aWrapper;
        public Wrapper bWrapper;
        public int b;

        public Instruction(Operations operation, Wrapper aWrapper, Wrapper bWrapper, int b)
        {
            this.operation = operation;
            this.aWrapper = aWrapper;
            this.bWrapper = bWrapper;
            this.b = b;
        }
    }
    


    private void DoMath(Instruction instruction)
    {
        Wrapper aWrapper = instruction.aWrapper;
    
        int b = instruction.bWrapper == null ? instruction.b : instruction.bWrapper.value;

        if (instruction.operation == Operations.Add)
        {
            Add(aWrapper, b);
        }
        else if (instruction.operation == Operations.Mul)
        {
            Mul(aWrapper, b);
        }
        else if (instruction.operation == Operations.Div)
        {
            Div(aWrapper, b);
        }
        else if (instruction.operation == Operations.Mod)
        {
            Mod(aWrapper, b);
        }
        else if (instruction.operation == Operations.Eql)
        {
            Eql(aWrapper, b);
        }
    }



    private void Add(Wrapper aWrapper, int b)
    {
        aWrapper.value += b;
    }



    private void Mul(Wrapper aWrapper, int b)
    {
        aWrapper.value *= b;
    }



    private void Div(Wrapper aWrapper, int b)
    {
        //Make sure no division by 0
        if (b == 0)
        {
            return;
        }

        aWrapper.value = (int)Math.Truncate((double)aWrapper.value / (double)b);
    }



    private void Mod(Wrapper aWrapper, int b)
    {
        //5 % 3 = 2 because 3 fits in 5 once and whats left is the 2

        //Make sure a < 0 and b <= 0 will not happen

        if (aWrapper.value < 0 || b <= 0)
        {
            return;
        }

        aWrapper.value = aWrapper.value % b;
    }



    private void Eql(Wrapper aWrapper, int b)
    {
        if (aWrapper.value == b)
        {
            aWrapper.value = 1;
        }
        else
        {
            aWrapper.value = 0;
        }
    }


    //
    // Integers
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

