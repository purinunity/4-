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
        //��v�i11�j�̗p��
        for (int i = 11; i < 22; i++)
        {
            tehai.Add(taku.yama[i]);
            taku.yama[i] = "ep";
        }
    }

    public int CalculateShanten(int[] array)
    {
        int shanten = 0;
        int mentsu_count = 0; //�ʎq�̐�
        int mentsu_kouho_count = 0; //���q�̐�
        //1,�ʎq�̐��𐔂���
        //1-1,���q�̌��o
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
        //2,�ʎq���̌��o
        //2-1,���q���̌��o
        for (int i = 0; i < 27; i++)
        {
            if (i % 9 < 8) //���q��₪��������͈́i1-7�j
            {
                while (array[i] > 0 && array[i + 1] > 0)
                {
                    array[i]--;
                    array[i + 1]--;
                    mentsu_kouho_count++; // �ʎq�J�E���g
                }
            }
        }
        //2-2,���q���̌��o
        for (int i = 0; i < 30; i++)
        {
            if (array[i] >= 2)
            {
                array[i] -= 2;
                mentsu_kouho_count++;
            }
        }
        //�������̌v�Z
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
            copy = tehai.ToList(); //�R�s�[���쐬
            copy.RemoveAt(i); //i�Ԗڂ̗v�f���폜
            ToArray(copy);
            int shanten = CalculateShanten(tehai_counter);
            //Console.WriteLine(shanten);
            if (shanten < min_shanten)//���������r
            {
                min_shanten = shanten;
                //Console.WriteLine("�ŏ��̌�����" + min_shanten);
                index = i;
            }
        }
        return index;
    }

    public bool Judge(List<string> tehai)
    {
        List<string> copy = new List<string>();
        copy = tehai.ToList(); //�R�s�[���쐬
        ToArray(copy);
        if (CalculateShanten(tehai_counter) == 0) //������
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
        if (CalculateShanten(tehai_counter) == 0) //������
        {
            Debug.Log("�R���s���[�^�̏����ł��i�����j");
            game_end_flag = true;
            tehai.Add(kawa.Last());
            CalculateScore();
            Debug.Log("�R���s���[�^�̃X�R�A" + totalscore);
            Application.Quit();
        }
    }

    public void Action(Taku taku)
    {
        if(Judge(tehai) == true)
        {
            Debug.Log("�R���s���[�^�̏����ł��i�c���j");
            game_end_flag = true;
            CalculateScore();
            Debug.Log("�R���s���[�^�̃X�R�A" + totalscore);
            Application.Quit();
        }
        else
        {
            StartCoroutine(DelayedComputerMethod(taku, 1.0f));
        }
    }

    private IEnumerator DelayedComputerMethod(Taku taku, float delay) //AI����
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
