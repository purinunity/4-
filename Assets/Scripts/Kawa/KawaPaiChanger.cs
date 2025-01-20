using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KawaPaiChanger : MonoBehaviour
{
    public KawaPaiName kawapainame;
    SpriteRenderer MainSpriteRenderer;
    public Sprite[] kou = new Sprite[9];
    public Sprite[] otu = new Sprite[9];
    public Sprite[] hei = new Sprite[9];
    public Sprite ino;
    public Sprite sika;
    public Sprite chou;

    void Start()
    {
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        for (int i = 1; i < 10; i++)
        {
            if (kawapainame.name == (i + "m"))
            {
                MainSpriteRenderer.sprite = kou[i - 1];
            }
        }
        for (int i = 1; i < 10; i++)
        {
            if (kawapainame.name == (i + "p"))
            {
                MainSpriteRenderer.sprite = otu[i - 1];
            }
        }
        for (int i = 1; i < 10; i++)
        {
            if (kawapainame.name == (i + "s"))
            {
                MainSpriteRenderer.sprite = hei[i - 1];
            }
        }

        if (kawapainame.name == (9 + "x"))
        {
            MainSpriteRenderer.sprite = ino;
        }
        if (kawapainame.name == (9 + "y"))
        {
            MainSpriteRenderer.sprite = sika;
        }
        if (kawapainame.name == (9 + "z"))
        {
            MainSpriteRenderer.sprite = chou;
        }
    }
}
