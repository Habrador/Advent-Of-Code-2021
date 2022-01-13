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

                if (int.TryParse(bString, out int result))
                {
                    bValue = result;
                }
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
                if (modelNumberString[i] == '0')
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
                if (instruction.operation == Operations.Inp)
                {
                    calculationNumber += 1;

                    //Char to int
                    wWrapper.value = modelNumberString[calculationNumber] - '0';
                }
                else
                {
                    //Now we need to figure out if a, b are x, y, z, w
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
}

