using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public Player player;
    public int number;

    void OnMouseDown()
    {
        player.target_number = number;
        player.click_flag = true;
    }
}
