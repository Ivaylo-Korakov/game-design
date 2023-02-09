using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Assets.IntLibs
{
    public class InputController
    {
        // A function to parse the player keyboard inputs and return a list of them
        public static List<string> GetPlayerInputs()
        {
            List<string> inputs = new List<string>();

            // If the player is pressing W, move forward
            if (Input.GetKey(KeyCode.W))
            {
                inputs.Add("W");
            }

            // If the player is pressing S, move backward
            if (Input.GetKey(KeyCode.S))
            {
                inputs.Add("S");
            }

            // If the player is pressing A, move left
            if (Input.GetKey(KeyCode.A))
            {
                inputs.Add("A");
            }

            // If the player is pressing D, move right
            if (Input.GetKey(KeyCode.D))
            {
                inputs.Add("D");
            }

            return inputs;
        }
    }
}