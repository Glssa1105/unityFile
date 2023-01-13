using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int AbleToMoveNumber;
    public Text MessagePrinter;
    private int ControlNumber;
    [SerializeField]private Text Skill_1_name;
    [SerializeField]private Text Skill_1_Hurt;
    [SerializeField]private Text Skill_1_Range;
    [SerializeField]private Text Skill_1_CostMp;
    [SerializeField]private Text Skill_1_CD;
    [SerializeField]private Text Skill_2_name;
    [SerializeField]private Text Skill_2_Hurt;
    [SerializeField]private Text Skill_2_Range;
    [SerializeField]private Text Skill_2_CostMp;
    [SerializeField]private Text Skill_2_CD;
    
    [SerializeField]private GameObject ControlOption;
    [SerializeField]private GameObject SkillBoard;
    [SerializeField]private bool skillBoardOpen;
    [SerializeField]private Text Roundside;
    [SerializeField]private GameManager gameManager;
    [SerializeField]private GameObject[] enemyList;
    [SerializeField]private GameObject[] playerList;
    void Start()
    {
        AbleToMoveNumber = 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerList = GameObject.FindGameObjectsWithTag("Player");
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        CheckRound();
        if(skillBoardOpen)
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
            Skill_1_name.text = gameManager.selected.GetComponent<PlayerControl>().skill_1_name;
            Skill_2_name.text = gameManager.selected.GetComponent<PlayerControl>().skill_2_name;
            Skill_1_Hurt.text = "伤害："+gameManager.selected.GetComponent<PlayerControl>().skill_1_value.ToString();
            Skill_2_Hurt.text = "伤害："+gameManager.selected.GetComponent<PlayerControl>().skill_2_value.ToString();
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
    }

    public void DestroyOption()
    {
        ControlOption.SetActive(false);
    }

    public void ChangeRound()
    {
        gameManager.CloseMoveRange();
        gameManager.CloseAttackRange();
        SkillBoard.SetActive(false);
        ControlOption.SetActive(false);
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

    public void OpenSkillBoard()
    {
        skillBoardOpen = true;
        SkillBoard.SetActive(true);
        ControlOption.SetActive(false);
        gameManager.CloseMoveRange();
        gameManager.CloseAttackRange();
    }

    public void ClossSkillBoard()
    {
        skillBoardOpen = false;
        SkillBoard.SetActive(false);
        ControlOption.SetActive(true);
        gameManager.CloseMoveRange();
        gameManager.CloseAttackRange();
    }

    
}
