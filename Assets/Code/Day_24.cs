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

        foreach (string instructionString in allRowsString)
        {
            //Most are "add y w" but some are "inp w"
            //But the operation is always 3 characters
            string operation = instructionString.Substring(0, 3);

            if (operation == "inp")
            {
                instructions.Add(new Instruction(Operations.Inp, '0', "0"));
            }
            else
            {
                //"add y w" -> y 
                char a = char.Parse(instructionString.Substring(4, 1));

                //b can be either variable or number
                //"add y w" -> w 
                string b = instructionString.Substring(6, 1);

                if (operation == "add")
                {
                    instructions.Add(new Instruction(Operations.Add, a, b));
                }
                else if (operation == "mul")
                {
                    instructions.Add(new Instruction(Operations.Mul, a, b));
                }
                else if (operation == "div")
                {
                    instructions.Add(new Instruction(Operations.Div, a, b));
                }
                else if (operation == "mod")
                {
                    instructions.Add(new Instruction(Operations.Mod, a, b));
                }
                else if (operation == "eql")
                {
                    instructions.Add(new Instruction(Operations.Eql, a, b));
                }
            }
        }




        //Input is a 14 digit integer 1-9 where each integer is input to a calculation

        //We should find the largest valid model number, so start with max
        long modelNumber = 99999999999999;

        string modelNumberString = modelNumber.ToString();

        //Debug.Log(modelNumberString);

        int safety = 0;

        while (modelNumber > 0)
        {
            //safety += 1;

            //if (safety > 1000)
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
                continue;
            }
        
            //Pos in modelNumberString
            int calculationNumber = -1;

            //Integer variables start with 0
            int x = 0;
            int y = 0;
            int z = 0;
            int w = 0;

            foreach (Instruction instruction in instructions)
            {
                if (instruction.operation == Operations.Inp)
                {
                    calculationNumber += 1;

                    w = int.Parse(modelNumberString[calculationNumber].ToString());
                }
                else
                {
                    //Now we need to figure out if a, b are x, y, z, w
                    Find_a_b_then_do_math(instruction.a, instruction.b, instruction.operation, ref x, ref y, ref z, ref w);
                }
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

    private enum Operations 
    {
        Inp, Add, Mul, Div, Mod, Eql
    }

    private struct Instruction
    {
        public Operations operation;

        public char a;
        public string b;

        public Instruction(Operations operation, char a, string b)
        {
            this.operation = operation;
            this.a = a;
            this.b = b;
        }
    }


    //Combinations
    //x x
    //x y
    //x z
    //x w

    //y x
    //y y
    //y z
    //y w

    //z x
    //z y
    //z z
    //z w

    //w x
    //w y
    //w z
    //w w

    //x number
    //y number
    //z number
    //w number

    private void Find_a_b_then_do_math(char a, string bString, Operations operation, ref int x, ref int y, ref int z, ref int w)
    {
        if (int.TryParse(bString, out int result))
        {
            int b = result;

            if (a == 'x')
            {
                DoMath(ref x, b, operation);
            }
            else if (a == 'y')
            {
                DoMath(ref y, b, operation);
            }
            else if (a == 'z')
            {
                DoMath(ref z, b, operation);
            }
            else if (a == 'w')
            {
                DoMath(ref w, b, operation);
            }
        }
        else
        {
            char b = char.Parse(bString);

            //x
            if (a == 'x' && b == 'x')
            {
                DoMath(ref x, x, operation);
            }
            else if (a == 'x' && b == 'y')
            {
                DoMath(ref x, y, operation);
            }
            else if (a == 'x' && b == 'z')
            {
                DoMath(ref x, z, operation);
            }
            else if (a == 'x' && b == 'w')
            {
                DoMath(ref x, w, operation);
            }
            //y
            else if (a == 'y' && b == 'x')
            {
                DoMath(ref y, x, operation);
            }
            else if (a == 'y' && b == 'y')
            {
                DoMath(ref y, y, operation);
            }
            else if (a == 'y' && b == 'z')
            {
                DoMath(ref y, z, operation);
            }
            else if (a == 'y' && b == 'w')
            {
                DoMath(ref y, w, operation);
            }
            //z
            else if (a == 'z' && b == 'x')
            {
                DoMath(ref z, x, operation);
            }
            else if (a == 'z' && b == 'y')
            {
                DoMath(ref z, y, operation);
            }
            else if (a == 'z' && b == 'z')
            {
                DoMath(ref z, z, operation);
            }
            else if (a == 'z' && b == 'w')
            {
                DoMath(ref z, w, operation);
            }
            //w
            else if (a == 'w' && b == 'x')
            {
                DoMath(ref w, x, operation);
            }
            else if (a == 'w' && b == 'y')
            {
                DoMath(ref w, y, operation);
            }
            else if (a == 'w' && b == 'z')
            {
                DoMath(ref w, z, operation);
            }
            else if (a == 'w' && b == 'w')
            {
                DoMath(ref w, w, operation);
            }
        }
    }



    private void DoMath(ref int a, int b, Operations operation)
    {
        if (operation == Operations.Add)
        {
            Add(ref a, b);
        }
        else if (operation == Operations.Mul)
        {
            Mul(ref a, b);
        }
        else if (operation == Operations.Div)
        {
            Div(ref a, b);
        }
        else if (operation == Operations.Mod)
        {
            Mod(ref a, b);
        }
        else if (operation == Operations.Eql)
        {
            Eql(ref a, b);
        }
    }



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
        if (a == b)
        {
            a = 1;
        }
        else
        {
            a = 0;
        }
    }
}

