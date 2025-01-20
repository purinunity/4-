using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : User
{
    public Computer computer;
    public bool player_pai_get = false;
    public int target_number; //�}�E�X�ŃN���b�N�����v
    public bool click_flag = false;
    public bool game_end_flag = false;
    public bool trushed = false;
    public int totalscore = 0;

    public bool dev_mode = false; //�J���҃��[�h

    public void SetTehai(Taku taku)
    {
        //��v�i11�j�̗p��
        for (int i = 0; i < 11; i++)
        {
            tehai.Add(taku.yama[i]);
            taku.yama[i] = "ep";
        }
    }

    public int CalculateMentsu(int[] array) //���g�p
    {
        int mentsu_count = 0;
        for (int i = 0; i < 27; i++)
        {
            if (i % 9 < 7) //���q����������͈́i1-7�j
            {
                while (array[i] > 0 && array[i + 1] > 0 && array[i + 2] > 0)
                {
                    array[i]--;
                    array[i + 1]--;
                    array[i + 2]--;
                    mentsu_count++; // �ʎq�J�E���g
                }
            }
        }
        //1-2,���q�̌��o
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

    public bool Judge(List<string> tehai) //���g�p
    {
        List<string> copy = new List<string>();
        copy = tehai.ToList(); //�R�s�[���쐬
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

    public void Check(List<string> kawa) //��������֐�
    {
        List<string> copy = new List<string>();
        copy = tehai.ToList(); //�R�s�[���쐬
        copy.Add(kawa.Last());
        ToArray(copy);
        if (CalculateMentsu(tehai_counter) == 4) //������
        {
            Debug.Log("�v���C���[�̏����ł��i�����j");
            game_end_flag = true;
            tehai.Add(kawa.Last());
            CalculateScore();
            Debug.Log("�v���C���[�̃X�R�A" + totalscore);
            Application.Quit();
        }
    }

    public void Action(Taku taku)
    {
        if(Judge(tehai) == true)
        {
            Debug.Log("�v���C���[�̏����ł��i�c���j");
            game_end_flag = true;
            CalculateScore();
            Debug.Log("�v���C���[�̃X�R�A" + totalscore);
            Application.Quit();

        }
        else //��������
        {
            if(computer.game_end_flag == false)
            {
                if (dev_mode == true)
                {
                    string temp = tehai[11];
                    tehai.RemoveAt(11);
                    //���������v���͂ɒǉ�
                    taku.kawa.Add(temp);
                    trushed = true;
                }
                else if (click_flag == true) //�N���b�N���鎞
                {
                    for (int i = 0; i < 12; i++)
                    {
                        if (target_number == i)
                        {
                            //���������J�n
                            string temp = tehai[target_number];
                            tehai[target_number] = tehai[11];
                            tehai.RemoveAt(11);
                            //���������v���͂ɒǉ�
                            taku.kawa.Add(temp);
                            trushed = true;
                            //�\�[�g
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
            //�b
            for (int j = 1; j < 10; j++)
            {
                if (tehai[i] == j + "m")
                {
                    totalscore += j;
                }
            }
            //��
            for (int j = 1; j < 10; j++)
            {
                if (tehai[i] == j + "p")
                {
                    totalscore += j;
                }
            }
            //��
            for (int j = 1; j < 10; j++)
            {
                if (tehai[i] == j + "s")
                {
                    totalscore += j;
                }
            }
            //���v
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