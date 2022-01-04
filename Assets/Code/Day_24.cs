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

                if (operation == "add")
                {
                    Add(a, b, ref x, ref y, ref z, ref w);
                }
                else if (operation == "mul")
                {
                    Mul(a, b, ref x, ref y, ref z, ref w);
                }
                else if (operation == "mul")
                {
                    Div(a, b, ref x, ref y, ref z, ref w);
                }
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


    private void Add(char a, string bString, ref int x, ref int y, ref int z, ref int w)
    {
        if (int.TryParse(bString, out int result))
        {
            if (a == 'x')
            {
                x += result;
            }
            else if (a == 'y')
            {
                y += result;
            }
            else if (a == 'z')
            {
                z += result;
            }
            else if (a == 'w')
            {
                w += result;
            }
        }
        else
        {
            char b = char.Parse(bString);

            //x
            if (a == 'x' && b == 'x')
            {
                x += x;
            }
            else if (a == 'x' && b == 'y')
            {
                x += y;
            }
            else if (a == 'x' && b == 'z')
            {
                x += z;
            }
            else if (a == 'x' && b == 'w')
            {
                x += w;
            }
            //y
            else if (a == 'y' && b == 'x')
            {
                y += x;
            }
            else if (a == 'y' && b == 'y')
            {
                y += y;
            }
            else if (a == 'y' && b == 'z')
            {
                y += z;
            }
            else if (a == 'y' && b == 'w')
            {
                y += w;
            }
            //z
            else if (a == 'z' && b == 'x')
            {
                z += x;
            }
            else if (a == 'z' && b == 'y')
            {
                z += y;
            }
            else if (a == 'z' && b == 'z')
            {
                z += z;
            }
            else if (a == 'z' && b == 'w')
            {
                z += w;
            }
            //w
            else if (a == 'w' && b == 'x')
            {
                w += x;
            }
            else if (a == 'w' && b == 'y')
            {
                w += y;
            }
            else if (a == 'w' && b == 'z')
            {
                w += z;
            }
            else if (a == 'w' && b == 'w')
            {
                w += w;
            }
        }
    }



    private void Mul(char a, string bString, ref int x, ref int y, ref int z, ref int w)
    {
        if (int.TryParse(bString, out int result))
        {
            if (a == 'x')
            {
                x *= result;
            }
            else if (a == 'y')
            {
                y *= result;
            }
            else if (a == 'z')
            {
                z *= result;
            }
            else if (a == 'w')
            {
                w *= result;
            }
        }
        else
        {
            char b = char.Parse(bString);

            //x
            if (a == 'x' && b == 'x')
            {
                x *= x;
            }
            else if (a == 'x' && b == 'y')
            {
                x *= y;
            }
            else if (a == 'x' && b == 'z')
            {
                x *= z;
            }
            else if (a == 'x' && b == 'w')
            {
                x *= w;
            }
            //y
            else if (a == 'y' && b == 'x')
            {
                y *= x;
            }
            else if (a == 'y' && b == 'y')
            {
                y *= y;
            }
            else if (a == 'y' && b == 'z')
            {
                y *= z;
            }
            else if (a == 'y' && b == 'w')
            {
                y *= w;
            }
            //z
            else if (a == 'z' && b == 'x')
            {
                z *= x;
            }
            else if (a == 'z' && b == 'y')
            {
                z *= y;
            }
            else if (a == 'z' && b == 'z')
            {
                z *= z;
            }
            else if (a == 'z' && b == 'w')
            {
                z *= w;
            }
            //w
            else if (a == 'w' && b == 'x')
            {
                w *= x;
            }
            else if (a == 'w' && b == 'y')
            {
                w *= y;
            }
            else if (a == 'w' && b == 'z')
            {
                w *= z;
            }
            else if (a == 'w' && b == 'w')
            {
                w *= w;
            }
        }
    }



    private void Div(char a, string bString, ref int x, ref int y, ref int z, ref int w)
    {
        if (int.TryParse(bString, out int result))
        {
            if (a == 'x')
            {
                x = (int)Math.Truncate((double)x / (double)result);
            }
            else if (a == 'y')
            {
                y = (int)Math.Truncate((double)y / (double)result);
            }
            else if (a == 'z')
            {
                z = (int)Math.Truncate((double)z / (double)result);
            }
            else if (a == 'w')
            {
                w = (int)Math.Truncate((double)w / (double)result);
            }
        }
        else
        {
            char b = char.Parse(bString);

            //x
            if (a == 'x' && b == 'x')
            {
                x = (int)Math.Truncate((double)x / (double)x);
            }
            else if (a == 'x' && b == 'y')
            {
                x = (int)Math.Truncate((double)x / (double)y);
            }
            else if (a == 'x' && b == 'z')
            {
                x = (int)Math.Truncate((double)x / (double)z);
            }
            else if (a == 'x' && b == 'w')
            {
                x = (int)Math.Truncate((double)x / (double)w);
            }
            //y
            else if (a == 'y' && b == 'x')
            {
                y = (int)Math.Truncate((double)y / (double)x);
            }
            else if (a == 'y' && b == 'y')
            {
                y = (int)Math.Truncate((double)y / (double)y);
            }
            else if (a == 'y' && b == 'z')
            {
                y = (int)Math.Truncate((double)y / (double)z);
            }
            else if (a == 'y' && b == 'w')
            {
                y = (int)Math.Truncate((double)y / (double)w);
            }
            //z
            else if (a == 'z' && b == 'x')
            {
                z = (int)Math.Truncate((double)z / (double)x);
            }
            else if (a == 'z' && b == 'y')
            {
                z = (int)Math.Truncate((double)z / (double)y);
            }
            else if (a == 'z' && b == 'z')
            {
                z = (int)Math.Truncate((double)z / (double)z);
            }
            else if (a == 'z' && b == 'w')
            {
                z = (int)Math.Truncate((double)z / (double)w);
            }
            //w
            else if (a == 'w' && b == 'x')
            {
                w = (int)Math.Truncate((double)w / (double)x);
            }
            else if (a == 'w' && b == 'y')
            {
                w = (int)Math.Truncate((double)w / (double)y);
            }
            else if (a == 'w' && b == 'z')
            {
                w = (int)Math.Truncate((double)w / (double)z);
            }
            else if (a == 'w' && b == 'w')
            {
                w = (int)Math.Truncate((double)w / (double)w);
            }
        }
    }
}
