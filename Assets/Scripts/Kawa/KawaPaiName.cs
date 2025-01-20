using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KawaPaiName : MonoBehaviour
{
    new public string name;
    public int number;
    public Taku taku;

    void Update()
    {
        if (taku != null && taku.kawa != null && number >= 0 && number < taku.kawa.Count)
        {
            name = taku.kawa[number];
        }
    }
}