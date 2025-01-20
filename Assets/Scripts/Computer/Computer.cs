using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Computer : User
{
    public Player player;
    public bool computer_pai_get = false;
    public bool game_end_flag = false;
    public int totalscore = 0;

    public void SetTehai(Taku taku)
    {
        //手牌（11個）の用意
        for (int i = 11; i < 22; i++)
        {
            tehai.Add(taku.yama[i]);
            taku.yama[i] = "ep";
        }
    }

    public int CalculateShanten(int[] array)
    {
        int shanten = 0;
        int mentsu_count = 0; //面子の数
        int mentsu_kouho_count = 0; //塔子の数
        //1,面子の数を数える
        //1-1,順子の検出
        for (int i = 0; i < 27; i++)
        {
            if (i % 9 < 7) //順子が成立する範囲（1-7）
            {
                while (array[i] > 0 && array[i + 1] > 0 && array[i + 2] > 0)
                {
                    array[i]--;
                    array[i + 1]--;
                    array[i + 2]--;
                    mentsu_count++; // 面子カウント
                }
            }
        }
        //1-2,刻子の検出
        for (int i = 0; i < 30; i++)
        {
            if (array[i] >= 3)
            {
                array[i] -= 3;
                mentsu_count++;
            }
        }
        //2,面子候補の検出
        //2-1,順子候補の検出
        for (int i = 0; i < 27; i++)
        {
            if (i % 9 < 8) //順子候補が成立する範囲（1-7）
            {
                while (array[i] > 0 && array[i + 1] > 0)
                {
                    array[i]--;
                    array[i + 1]--;
                    mentsu_kouho_count++; // 面子カウント
                }
            }
        }
        //2-2,刻子候補の検出
        for (int i = 0; i < 30; i++)
        {
            if (array[i] >= 2)
            {
                array[i] -= 2;
                mentsu_kouho_count++;
            }
        }
        //向聴数の計算
        shanten = 8 - (mentsu_count * 2) - mentsu_kouho_count;

        return shanten;
    }
    public int Decision_Out(List<string> tehai)
    {
        int index = 0;
        int min_shanten = 8;
        List<string> copy = new List<string>();
        for (int i = 0; i < 12; i++)
        {
            copy = tehai.ToList(); //コピーを作成
            copy.RemoveAt(i); //i番目の要素を削除
            ToArray(copy);
            int shanten = CalculateShanten(tehai_counter);
            //Console.WriteLine(shanten);
            if (shanten < min_shanten)//向聴数を比較
            {
                min_shanten = shanten;
                //Console.WriteLine("最小の向聴数" + min_shanten);
                index = i;
            }
        }
        return index;
    }

    public bool Judge(List<string> tehai)
    {
        List<string> copy = new List<string>();
        copy = tehai.ToList(); //コピーを作成
        ToArray(copy);
        if (CalculateShanten(tehai_counter) == 0) //役完成
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Check(List<string> kawa) //ロンする関数
    {
        List<string> copy = new List<string>();
        copy = tehai.ToList(); //コピーを作成
        copy.Add(kawa.Last());
        ToArray(copy);
        if (CalculateShanten(tehai_counter) == 0) //役完成
        {
            Debug.Log("コンピュータの勝ちです（ロン）");
            game_end_flag = true;
            tehai.Add(kawa.Last());
            CalculateScore();
            Debug.Log("コンピュータのスコア" + totalscore);
            Application.Quit();
        }
    }

    public void Action(Taku taku)
    {
        if(Judge(tehai) == true)
        {
            Debug.Log("コンピュータの勝ちです（ツモ）");
            game_end_flag = true;
            CalculateScore();
            Debug.Log("コンピュータのスコア" + totalscore);
            Application.Quit();
        }
        else
        {
            StartCoroutine(DelayedComputerMethod(taku, 1.0f));
        }
    }

    private IEnumerator DelayedComputerMethod(Taku taku, float delay) //AI生成
    {
        yield return new WaitForSeconds(delay);
        ComputerMethod(taku);
    }

    public void ComputerMethod(Taku taku)
    {
        int index = Decision_Out(tehai);
        string delete_target = tehai[index];
        tehai.RemoveAt(index);
        taku.kawa.Add(delete_target);
        Sort();
        player.player_pai_get = false;
    }

    public void CalculateScore()
    {
        for (int i = 0; i < tehai.Count; i++)
        {
            //甲
            for (int j = 1; j < 10; j++)
            {
                if (tehai[i] == j + "m")
                {
                    totalscore += j;
                }
            }
            //乙
            for (int j = 1; j < 10; j++)
            {
                if (tehai[i] == j + "p")
                {
                    totalscore += j;
                }
            }
            //丙
            for (int j = 1; j < 10; j++)
            {
                if (tehai[i] == j + "s")
                {
                    totalscore += j;
                }
            }
            //字牌
            if (tehai[i] == 9 + "x")
            {
                totalscore += 10;
            }
            if (tehai[i] == 9 + "y")
            {
                totalscore += 10;
            }
            if (tehai[i] == 9 + "z")
            {
                totalscore += 10;
            }
        }
    }
}
