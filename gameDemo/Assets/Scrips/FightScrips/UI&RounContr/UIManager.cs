using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public int AbleToMoveNumber;
    public Text MessagePrinter;
    private int ControlNumber;
    private bool ifOptionOpen;

    [SerializeField]private Text player_HP;
    [SerializeField]private Text player_MP;
    [SerializeField]private Text player_stamina;
    [SerializeField]private Text player_Name;


    [SerializeField]private Text Skill_1_name;
    [SerializeField]private Text Skill_1_Detail;
    [SerializeField]private Text Skill_1_Range;
    [SerializeField]private Text Skill_1_CostMp;
    [SerializeField]private Text Skill_1_CD;
    [SerializeField]private Text Skill_2_name;
    [SerializeField]private Text Skill_2_Detail;
    [SerializeField]private Text Skill_2_Range;
    [SerializeField]private Text Skill_2_CostMp;
    [SerializeField]private Text Skill_2_CD;
    
    [SerializeField]private GameObject ControlOption;
    [SerializeField]private Text Roundside;
    [SerializeField]private GameManager gameManager;
    [SerializeField]private GameObject[] enemyList;
    [SerializeField]private GameObject[] playerList;

    [SerializeField]private GameObject WinUI;
    [SerializeField]private GameObject LoseUI;
    [SerializeField]private GameObject UnmoveableTips;
    [SerializeField]private GameMessage gameMessage;
    void Start()
    {
        AbleToMoveNumber = 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameMessage = GameObject.Find("GameMessage").GetComponent<GameMessage>();
        playerList = GameObject.FindGameObjectsWithTag("Player");
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        CheckRound();
        if(ifOptionOpen)
        CheckSkillStatu();
    }

    public void CheckRound()
    {
        switch(AbleToMoveNumber)
        {
            case 1:
                Roundside.text = "敌方！";
                break;
            default:
                Roundside.text = "友方！";
                break;
        }
    }

    //后续技能CD，消耗MP显示在此处
    public void CheckSkillStatu()
    {
        if(gameManager.selected.tag == "Player")
        {
            player_Name.text = gameManager.selected.GetComponent<PlayerControl>().Player_name;
            player_stamina.text = "耐力：" + gameManager.selected.GetComponent<PlayerControl>().stamina.ToString() + " / " + gameManager.selected.GetComponent<PlayerControl>().Maxstamina.ToString(); 
            player_HP.text = "HP:" + gameManager.selected.GetComponent<PlayerControl>().blood.ToString()+ " / " + gameManager.selected.GetComponent<PlayerControl>().maxBlood.ToString(); 
            player_MP.text = "MP: " + gameManager.selected.GetComponent<PlayerControl>().Mp.ToString()+ " / " + gameManager.selected.GetComponent<PlayerControl>().MaxMP.ToString(); 
            Skill_1_name.text = gameManager.selected.GetComponent<PlayerControl>().skill_1_name;
            Skill_2_name.text = gameManager.selected.GetComponent<PlayerControl>().skill_2_name;
            Skill_1_Detail.text =gameManager.selected.GetComponent<PlayerControl>().skill_1_Detail;
            Skill_2_Detail.text =gameManager.selected.GetComponent<PlayerControl>().skill_2_Detail;
            Skill_1_Range.text = "攻击范围："+gameManager.selected.GetComponent<PlayerControl>().skill_1_Range.ToString();
            Skill_2_Range.text = "攻击范围："+gameManager.selected.GetComponent<PlayerControl>().skill_2_Range.ToString();
            Skill_1_CostMp.text = "Mp消耗："+gameManager.selected.GetComponent<PlayerControl>().skill_1_MpCost.ToString();
            Skill_2_CostMp.text = "Mp消耗："+gameManager.selected.GetComponent<PlayerControl>().skill_2_MpCost.ToString();
            Skill_1_CD.text = "冷却时间："+gameManager.selected.GetComponent<PlayerControl>().skill_1_CDwait.ToString()+"/"+gameManager.selected.GetComponent<PlayerControl>().skill_1_CD.ToString();
            Skill_2_CD.text = "冷却时间："+gameManager.selected.GetComponent<PlayerControl>().skill_2_CDwait.ToString()+"/"+gameManager.selected.GetComponent<PlayerControl>().skill_2_CD.ToString();
        }
    }

    public void UpdateEnemyList()
    {
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
    }
    
    public void CreateOption()
    {
        ControlOption.SetActive(true);
        ifOptionOpen =true;
    }

    public void DestroyOption()
    {
        ControlOption.SetActive(false);
        ifOptionOpen =false;
    }

    public void ChangeRound()
    {
        gameManager.CloseMoveRange();
        gameManager.CloseAttackRange();
        //////////////
        if(gameManager.startCell!=null)
        gameManager.startCell.GetComponent<CellControl>().moveCell.SetActive(false);
        DestroyOption();
        switch(AbleToMoveNumber)
        {
            case 1:
                AbleToMoveNumber=0;
                foreach(var item in playerList)
                {
                    if(item.GetComponent<PlayerControl>().skill_1_CDwait>0)
                    {
                        item.GetComponent<PlayerControl>().skill_1_CDwait--;
                    }
                    if(item.GetComponent<PlayerControl>().skill_2_CDwait>0)
                    {
                        item.GetComponent<PlayerControl>().skill_2_CDwait--;
                    }
                    item.GetComponent<PlayerControl>().Mp+=item.GetComponent<PlayerControl>().MpRecover;
                    if(item.GetComponent<PlayerControl>().Mp>item.GetComponent<PlayerControl>().MaxMP)
                    {
                        item.GetComponent<PlayerControl>().Mp = item.GetComponent<PlayerControl>().MaxMP;
                    }              
                    item.GetComponent<PlayerControl>().SetStartCell();
                    item.GetComponent<PlayerControl>().HaveAttacked = false;
                    item.GetComponent<PlayerControl>().stamina  = item.GetComponent<PlayerControl>().Maxstamina;
                }
                break;
            default:
                AbleToMoveNumber=1;
                ControlNumber = 0;
                UpdateEnemyList();
                enemyList[0].GetComponent<EnemyAI>().Go();
                break;
        }
        
    }

    public void nextRun()
    {
        ControlNumber ++ ;
        if(ControlNumber==enemyList.Length)
        {
            ChangeRound();
            return;
        }
        Debug.Log(1);
        if(enemyList[ControlNumber]!=null)
        {
            enemyList[ControlNumber].GetComponent<EnemyAI>().Go();
        }
    }
    
    public void ShowWinUI()
    {
        WinUI.SetActive(true);
    }

    public void CloseWinUI()
    {
        WinUI.SetActive(false);
    }

    public void ShowLoseUI()
    {
        LoseUI.SetActive(true);
    }

    public void CloseLoseUI()
    {
        LoseUI.SetActive(false);
    }

    public void ShowUnmoveableTips()
    {
        UnmoveableTips.SetActive(true);
    }

    public void CloseUnmoveableTips()
    {
        UnmoveableTips.SetActive(false);
    }

    public void LoadExploreScene()
    {
        foreach(var item in playerList)
        {
            if(item.GetComponent<PlayerControl>().playerNumber == 1)
            {
                gameMessage.Player_1_Hp = item.GetComponent<PlayerControl>().blood;
                gameMessage.Player_1_Mp = item.GetComponent<PlayerControl>().Mp;
            }
            if(item.GetComponent<PlayerControl>().playerNumber == 2)
            {
                gameMessage.Player_2_Hp = item.GetComponent<PlayerControl>().blood;
                gameMessage.Player_2_Mp = item.GetComponent<PlayerControl>().Mp;
            }
            if(item.GetComponent<PlayerControl>().playerNumber == 3)
            {
                gameMessage.Player_3_Hp = item.GetComponent<PlayerControl>().blood;
                gameMessage.Player_3_Mp = item.GetComponent<PlayerControl>().Mp;
            }
            if(item.GetComponent<PlayerControl>().playerNumber == 4)
            {
                gameMessage.Player_4_Hp = item.GetComponent<PlayerControl>().blood;
                gameMessage.Player_4_Mp = item.GetComponent<PlayerControl>().Mp;
            }
        }
        SceneManager.LoadScene(7);
    }
    
    public void ResetScene()
    {
        SceneManager.LoadScene(gameManager.SceneNUMBER);
    }
    
}
