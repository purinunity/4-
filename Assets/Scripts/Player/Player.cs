using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : User
{
    public Computer computer;
    public bool player_pai_get = false;
    public int target_number; //マウスでクリックした牌
    public bool click_flag = false;
    public bool game_end_flag = false;
    public bool trushed = false;
    public int totalscore = 0;

    public bool dev_mode = false; //開発者モード

    public void SetTehai(Taku taku)
    {
        //手牌（11個）の用意
        for (int i = 0; i < 11; i++)
        {
            tehai.Add(taku.yama[i]);
            taku.yama[i] = "ep";
        }
    }

    public int CalculateMentsu(int[] array) //未使用
    {
        int mentsu_count = 0;
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

        return mentsu_count;
    }

    public bool Judge(List<string> tehai) //未使用
    {
        List<string> copy = new List<string>();
        copy = tehai.ToList(); //コピーを作成
        ToArray(copy);
        if (CalculateMentsu(tehai_counter) == 4)
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
        if (CalculateMentsu(tehai_counter) == 4) //役完成
        {
            Debug.Log("プレイヤーの勝ちです（ロン）");
            game_end_flag = true;
            tehai.Add(kawa.Last());
            CalculateScore();
            Debug.Log("プレイヤーのスコア" + totalscore);
            Application.Quit();
        }
    }

    public void Action(Taku taku)
    {
        if(Judge(tehai) == true)
        {
            Debug.Log("プレイヤーの勝ちです（ツモ）");
            game_end_flag = true;
            CalculateScore();
            Debug.Log("プレイヤーのスコア" + totalscore);
            Application.Quit();

        }
        else //交換処理
        {
            if(computer.game_end_flag == false)
            {
                if (dev_mode == true)
                {
                    string temp = tehai[11];
                    tehai.RemoveAt(11);
                    //交換した牌を河に追加
                    taku.kawa.Add(temp);
                    trushed = true;
                }
                else if (click_flag == true) //クリックする時
                {
                    for (int i = 0; i < 12; i++)
                    {
                        if (target_number == i)
                        {
                            //交換処理開始
                            string temp = tehai[target_number];
                            tehai[target_number] = tehai[11];
                            tehai.RemoveAt(11);
                            //交換した牌を河に追加
                            taku.kawa.Add(temp);
                            trushed = true;
                            //ソート
                            Sort();
                            click_flag = false;
                        }
                    }
                }
            }
        }
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