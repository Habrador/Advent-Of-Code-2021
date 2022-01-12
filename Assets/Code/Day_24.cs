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
        string[] allRowsString = FileManagement.GetInputData("Day_24", "input_test.txt");

        //Line after line of "add y w," etc

        //Integer variables
        int x = 0;
        int y = 0;
        int z = 0;
        int w = 0;

        foreach (string instruction in allRowsString)
        {
            //Most are "add y w" but some are "inp w"

            string operation = instruction.Substring(0, 3);

            char a = char.Parse(instruction.Substring(4, 1));

            if (instruction.Length > 5)
            {
                //b can be either variable or number
                string b = instruction.Substring(6, 1);

                //Now we need to figure out if a, b are x, y, z, w
                Find_a_b_then_do_math(a, b, operation, ref x, ref y, ref z, ref w);
            }
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

    private void Find_a_b_then_do_math(char a, string bString, string operation, ref int x, ref int y, ref int z, ref int w)
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



    private void DoMath(ref int a, int b, string operation)
    {
        if (operation == "add")
        {
            Add(ref a, b);
        }
        else if (operation == "mul")
        {
            Mul(ref a, b);
        }
        else if (operation == "mul")
        {
            Div(ref a, b);
        }
        else if (operation == "mod")
        {
            Mod(ref a, b);
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
}

