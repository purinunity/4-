using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taku : MonoBehaviour
{
    public List<string> yama = new List<string>(); //string�^�̃��X�g���`
    public List<string> kawa = new List<string>();
    System.Random random = new System.Random();

    public void Default()
    {
        //1m�`9m���Z�b�g
        SetTaku("m");
        //1p�`9p���Z�b�g
        SetTaku("p");
        //1s�`9s���Z�b�g
        SetTaku("s");

        SetExtra("x");
        SetExtra("y");  
        SetExtra("z");
    }

    public void Random()
    {
        for (int i = yama.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            string temp = yama[i];
            yama[i] = yama[j];
            yama[j] = temp;
        }
    }

    public void SetTaku(string s)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                yama.Add((i + 1) + s);
            }
        }
    }

    public void SetExtra(string ex)
    {
        for(int i = 0; i < 4; i++)
        {
            yama.Add(9 + ex);
        }
    }
}
