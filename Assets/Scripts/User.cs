using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class User : MonoBehaviour
{
    public List<string> tehai = new List<string>();
    public int[] tehai_counter = new int[30];
    public int next_get_index;

    public void Get(Taku taku)
    {
        tehai.Add(taku.yama[next_get_index]);
        taku.yama[next_get_index] = "ep";
        next_get_index += 2;
    }

    public void Sort()
    {
        tehai = tehai.OrderBy(item => item.Substring(1)) //•¶š‚Åƒ\[ƒg
                     .ThenBy(item => int.Parse(item.Substring(0, 1)))
                     .ToList(); // ”š‚Åƒ\[ƒg
    }

    public void ToArray(List<string> tehai)
    {
        //1s`9s = 0`8 , 1m`9m = 9`17 , 1p`9p = 18`26
        int[] count = new int[30];
        //1s`9s
        for (int i = 0; i < tehai.Count; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (tehai[i] == (j + 1) + "m")
                {
                    count[j]++;
                }
            }
        }
        //1m`9m
        for (int i = 0; i < tehai.Count; i++)
        {
            for (int j = 9; j < 18; j++)
            {
                if (tehai[i] == (j - 8) + "p")
                {
                    count[j]++;
                }
            }
        }
        //1p`9p
        for (int i = 0; i < tehai.Count; i++)
        {
            for (int j = 18; j < 27; j++)
            {
                if (tehai[i] == (j - 17) + "s")
                {
                    count[j]++;
                }
            }
        }
        //ex
        for(int i = 0; i < tehai.Count; i++)
        {
            if (tehai[i] == 9 + "x")
            {
                count[27]++;
            }
            if (tehai[i] == 9 + "y")
            {
                count[28]++;
            }
            if (tehai[i] == 9 + "z")
            {
                count[29]++;
            }
        }
        //ƒRƒs[
        for (int i = 0; i < 27; i++)
        {
            tehai_counter[i] = count[i];
        }
    }
}
