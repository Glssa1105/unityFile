using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMessage : MonoBehaviour
{
    public bool Player1Insist;
    public bool Player2Insist;
    public bool Player3Insist;
    public bool Player4Insist;
    public int stageNum;
    public int Player_1_Hp;//目前血量
    public int Player_1_MaxHp;
    public int Player_1_Mp;
    public int Player_1_MaxMp;
    public int Player_2_Hp;//目前血量
    public int Player_2_MaxHp;
    public int Player_2_Mp;
    public int Player_2_MaxMp;
    public int Player_3_Hp;//目前血量
    public int Player_3_MaxHp;
    public int Player_3_Mp;
    public int Player_3_MaxMp;
    public int Player_4_Hp;//目前血量
    public int Player_4_MaxHp;
    public int Player_4_Mp;
    public int Player_4_MaxMp;
    [SerializeField]private bool test;
    static GameMessage _instance;
    public static GameMessage instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameMessage>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {
        if(GameObject.Find("Stage0"))
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if(this != _instance)
        {
            Destroy(gameObject);
        }
    }
    void check()
    {
            if(test)
            {
                Debug.Log("去7");
                test=!test;
                SceneManager.LoadScene("FightingScene");
            }
            else
            {
                Debug.Log("去1");
                test=!test;
                SceneManager.LoadScene("ExploreMap");
            }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            check();
        }
    }
}
