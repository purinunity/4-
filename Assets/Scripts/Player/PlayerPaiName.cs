using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaiName : MonoBehaviour
{
    new public string name;
    public int number;
    public Player player;

    void Update()
    {
        if (player.tehai != null && number >= 0 && number < player.tehai.Count)
        {
            name = player.tehai[number];
        }
    }
}
