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

    //ゲーム処理
    void Stanby()
    {
        //対戦準備（卓）
        taku.Default();
        taku.Random();
        //対戦準備（プレイヤー）
        player.SetTehai(taku);
        player.Sort();
        //対戦準備（コンピュータ）
        computer.SetTehai(taku);
        computer.Sort();
    }

    void Play()
    {
        if(taku.kawa.Count == 86)
        {
            Debug.Log("引き分けです");
            Application.Quit();
        }
        else
        {
            //プレイヤーは牌を獲得
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
            //プレイヤーはツモか確認後捨て牌を決定
            if (next_move == true && player.trushed == false)
            {
                player.Action(taku);
                //コンピュータのアクション
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
 