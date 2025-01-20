using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Taku taku;
    public Player player;
    public Computer computer;
    public GameObject player_pai_get;
    public GameObject computer_pai_get;
    public int draw_count = 86;

    private bool next_move = false;

    // Start is called before the first frame update
    void Start()
    {
        Stanby();
    }

    // Update is called once per frame
    void Update()
    {
        Play();
    }

    //�Q�[������
    void Stanby()
    {
        //�ΐ폀���i��j
        taku.Default();
        taku.Random();
        //�ΐ폀���i�v���C���[�j
        player.SetTehai(taku);
        player.Sort();
        //�ΐ폀���i�R���s���[�^�j
        computer.SetTehai(taku);
        computer.Sort();
    }

    void Play()
    {
        if(taku.kawa.Count == 86)
        {
            Debug.Log("���������ł�");
            Application.Quit();
        }
        else
        {
            //�v���C���[�͔v���l��
            if (player.player_pai_get == false)
            {
                computer_pai_get.SetActive(false);
                player_pai_get.SetActive(true);
                if (taku.kawa.Count != 0)
                {
                    player.Check(taku.kawa);
                }
                if (player.game_end_flag == false)
                {
                    player.Get(taku);
                    player.player_pai_get = true;
                    next_move = true;
                }
            }
            //�v���C���[�̓c�����m�F��̂Ĕv������
            if (next_move == true && player.trushed == false)
            {
                player.Action(taku);
                //�R���s���[�^�̃A�N�V����
                if (player.trushed == true)
                {
                    player_pai_get.SetActive(false);
                    player.trushed = false;
                    computer.Check(taku.kawa);
                    if (computer.game_end_flag == false)
                    {
                        computer_pai_get.SetActive(true);
                        computer.Get(taku);
                        computer.computer_pai_get = true;
                    }
                }
                if (computer.computer_pai_get == true)
                {
                    computer.computer_pai_get = false;
                    computer.Action(taku);
                    next_move = false;
                }
            }
        }
    }
}
 