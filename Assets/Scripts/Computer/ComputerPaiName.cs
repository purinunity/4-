using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPaiName : MonoBehaviour
{
    new public string name;
    public int number;
    public Computer computer;

    void Update()
    {
        if(computer.game_end_flag == true)
        {
            if (computer.tehai != null && number >= 0 && number < computer.tehai.Count)
            {
                name = computer.tehai[number];
            }
        }
    }
}
