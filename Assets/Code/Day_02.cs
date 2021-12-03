using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_02 : MonoBehaviour
{
    
    private void Start()
    {
        //Part_1();
        Part_2();
    }

   

    private void Part_1()
    {
        List<Command> allCommands = GetData();

        //Move the submarine starting at 0
        int horizontalPos = 0;
        int verticalPos = 0;

        foreach (Command command in allCommands)
        {
            if (command.direction == Direction.forward)
            {
                horizontalPos += command.posChange;
            }
            else if (command.direction == Direction.up)
            {
                verticalPos -= command.posChange;
            }
            else
            {
                verticalPos += command.posChange;
            }
        }


        //Multiply them together to submit a single result
        Debug.Log($"Answer {horizontalPos * verticalPos}");
    }



    //Similar to part 1 but more complicated movements
    private void Part_2()
    {
        List<Command> allCommands = GetData();

        //Move the submarine starting at 0
        int horizontalPos = 0;
        int verticalPos = 0;
        int aim = 0;

        foreach (Command command in allCommands)
        {
            if (command.direction == Direction.forward)
            {
                horizontalPos += command.posChange;
                verticalPos += (aim * command.posChange);
            }
            else if (command.direction == Direction.up)
            {
                aim -= command.posChange;
            }
            //Down 
            else
            {
                aim += command.posChange;
            }
        }


        //Multiply them together to submit a single result
        Debug.Log($"Answer {horizontalPos * verticalPos}");
    }



    private List<Command> GetData()
    {
        string[] allRowsString = FileManagement.GetInputData("Day_02", "input.txt");

        //The data is organized as commands (forward, down, up) and change in position, such as forward 5
        //down 7 increases depth
        //Standardize data from "forward 7" to Command

        List<Command> allCommands = new List<Command>();

        foreach (string row in allRowsString)
        {
            string[] command = row.Split(' ');

            int posChange = int.Parse(command[1]);

            Direction direction = Direction.forward;

            if (command[0].Equals("up"))
            {
                direction = Direction.up;
            }
            else if (command[0].Equals("down"))
            {
                direction = Direction.down;
            }

            allCommands.Add(new Command(direction, posChange));
        }

        //Test
        //Debug.Log(allCommands[0].direction + " " + allCommands[0].posChange);
        //Debug.Log(allCommands[allCommands.Count-1].direction + " " + allCommands[allCommands.Count - 1].posChange);
        //Debug.Log(allCommands[3].direction + " " + allCommands[3].posChange); //down
        //Debug.Log(allCommands[16].direction + " " + allCommands[16].posChange); //up

        return allCommands;
    }



    public enum Direction { forward, up, down }



    public struct Command
    {
        public Direction direction;
        public int posChange;

        public Command(Direction direction, int posChange)
        {
            this.direction = direction;
            this.posChange = posChange;
        }
    }
}
