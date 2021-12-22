using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day_17 : MonoBehaviour
{
    
    private void Start()
    {
        //Part_1();

        Part_2();
    }

    
    private void Part_1()
    {
        //string input = "target area: x=269..292, y=-68..-44";

        //string input_example = "target area: x=20..30, y=-10..-5";

        //Test values
        //Vector2Int maxTargetRange = new Vector2Int(30, -5);
        //Vector2Int minTargetRange = new Vector2Int(20, -10);

        //Actual values
        Vector2Int maxTargetRange = new Vector2Int(292, -44);
        Vector2Int minTargetRange = new Vector2Int(269, -68);

        //Fire a probe and hit a target area!

        //Integer velocity: 
        //x - forward
        //y - upward or downwards (negative) 
        //(0, 10) means straight up

        //This velocity is affected by drag and gravity
        //If you fire with a too high velocity, the probe can move so fast that it goes through the target area without hitting it

        //This should hit the target in the example
        //Vector2Int currentVel = new Vector2Int(7, 2); // 7 steps
        //Vector2Int currentVel = new Vector2Int(6, 3); // 9 steps
        //Vector2Int currentVel = new Vector2Int(9, 0); // 4 steps
        //Vector2Int currentVel = new Vector2Int(17, -4); // Will go through the target because too fast
        //Vector2Int currentVel = new Vector2Int(6, 9); //The start vel that will generate the trajectory with maximum y value

        //Number of values we will test as start velocities
        int NUMBER_OF_VALUES = 10000;

        //How many time steps will we simulate a single bullet
        int MAX_SIMULATION_STEPS = 10000;

        //x can only be positive
        int[] xTestVelocities = Enumerable.Range(0, NUMBER_OF_VALUES).ToArray();
        //int[] yTestVelocities = Enumerable.Range(-20, 40).ToArray();
        //Launching in negative y seems like a waste of computation power
        int[] yTestVelocities = Enumerable.Range(0, NUMBER_OF_VALUES).ToArray();

        //foreach (int x in xTestVelocities)
        //{
        //    Debug.Log(x);
        //}

        int maxY = -int.MaxValue;

        Vector2Int maxYVel = new Vector2Int(0, 0);

        bool hitAnyTarget = false;

        foreach (int xVel in xTestVelocities)
        {
            foreach (int yVel in yTestVelocities)
            {
                Vector2Int vel = new Vector2Int(xVel, yVel);

                FireProbe(vel, maxTargetRange, minTargetRange, MAX_SIMULATION_STEPS, out bool hitTarget, out int maxYThisLaunch);

                if (hitTarget && maxYThisLaunch > maxY)
                {
                    maxY = maxYThisLaunch;

                    maxYVel = vel;

                    hitAnyTarget = true;
                }
            }
        }



        //FireProbe(currentVel, maxTargetRange, minTargetRange, out bool hitTarget, out int maxY);


        if (!hitAnyTarget)
        {
            Debug.Log("We didn't hit the target! Sadge");
        }
        else
        {
            //Should be 2278
            Debug.Log($"We hit the target with the velocity ({maxYVel.x},{maxYVel.y})! Maximum y was: {maxY}");
        }
    }



    private void Part_2()
    {
        //string input = "target area: x=269..292, y=-68..-44";

        //string input_example = "target area: x=20..30, y=-10..-5";

        //Test values
        //Vector2Int maxTargetRange = new Vector2Int(30, -5);
        //Vector2Int minTargetRange = new Vector2Int(20, -10);

        //Actual values
        Vector2Int maxTargetRange = new Vector2Int(292, -44);
        Vector2Int minTargetRange = new Vector2Int(269, -68);

        //Fire a probe and hit a target area!

        //Integer velocity: 
        //x - forward
        //y - upward or downwards (negative) 
        //(0, 10) means straight up

        //This velocity is affected by drag and gravity
        //If you fire with a too high velocity, the probe can move so fast that it goes through the target area without hitting it

        //Number of values we will test as start velocities
        int NUMBER_OF_VALUES = 10000;

        //How many time steps will we simulate a single bullet
        int MAX_SIMULATION_STEPS = 10000;

        //x can only be positive
        int[] xTestVelocities = Enumerable.Range(0, NUMBER_OF_VALUES).ToArray();

        //In part 2 we have to launch in negative y as well
        int[] yTestVelocities = Enumerable.Range(-NUMBER_OF_VALUES, NUMBER_OF_VALUES * 2).ToArray();

      
        //Count how many distinctive velocities that does hit the target
        HashSet<Vector2Int> successfulStartVelocities = new HashSet<Vector2Int>();
        

        foreach (int xVel in xTestVelocities)
        {
            foreach (int yVel in yTestVelocities)
            {
                Vector2Int vel = new Vector2Int(xVel, yVel);

                FireProbe(vel, maxTargetRange, minTargetRange, MAX_SIMULATION_STEPS, out bool hitTarget, out int maxYThisLaunch);

                if (hitTarget)
                {
                    successfulStartVelocities.Add(vel);
                }
            }
        }


        //Should be 112 for test example
        //Should be 996 for actual values
        Debug.Log($"Number of distinctive start velocities that hit the target {successfulStartVelocities.Count}");
    }



    private void FireProbe(Vector2Int currentVel, Vector2Int maxTargetRange, Vector2Int minTargetRange, int MAX_STEPS, out bool hitTarget, out int maxY)
    {
        //We always start at (0,0)
        Vector2Int currentPos = new Vector2Int(0, 0);

        hitTarget = false;

        maxY = 0;

        for (int step = 1; step < MAX_STEPS; step++)
        {
            //The probe's x,y position increases by its x,y velocity
            int xPosNew = currentPos.x + currentVel.x;
            int yPosNew = currentPos.y + currentVel.y;

            //Velocity x is affected by drag
            int xVelNew = currentVel.x;

            if (xVelNew > 0)
            {
                xVelNew -= 1;
            }
            if (xVelNew < 0)
            {
                xVelNew += 1;
            }

            //Velocity y is affected by drag 
            int yVelNew = currentVel.y -= 1;

            //Update pos and vel with the new values
            currentPos = new Vector2Int(xPosNew, yPosNew);

            currentVel = new Vector2Int(xVelNew, yVelNew);

            if (currentPos.y > maxY)
            {
                maxY = currentPos.y;
            }

            //Check if we have hit the target
            if (IsWithinTarget(maxTargetRange, minTargetRange, currentPos))
            {
                //Debug.Log($"Hit the target after {step} steps");

                hitTarget = true;

                break;
            }

            //We should also stop if y is below the target range, because it cant suddenly lift again!
            if (currentPos.y < minTargetRange.y)
            {
                //Debug.Log($"The probe is below the target so aborted early");

                break;
            }

            //We can also abort early if the bullet is to far too right of target range
            if (currentPos.x > maxTargetRange.x)
            {
                //Debug.Log($"The probe is to the right of the target so aborted early");

                break;
            }
        }
    }



    private bool IsWithinTarget(Vector2Int maxRange, Vector2Int minRange, Vector2Int pos)
    {
        if (pos.x <= maxRange.x && pos.x >= minRange.x && pos.y <= maxRange.y && pos.y >= minRange.y)
        {
            return true;
        }

        return false;
    }
}
