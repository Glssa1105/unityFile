using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int AbleToMoveNumber;
    public Text MessagePrinter;
    private int ControlNumber;
    [SerializeField]private GameObject ControlOption;
    [SerializeField]private GameObject SkillBoard;
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
        switch(AbleToMoveNumber)
        {
            case 1:
                AbleToMoveNumber=0;
                foreach(var item in playerList)
                {
                    item.GetComponent<PlayerControl>().SetStartCell();
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
        if(enemyList[ControlNumber++]!=null)
        {
            enemyList[ControlNumber].GetComponent<EnemyAI>().Go();
        }
    }

    public void OpenSkillBoard()
    {
        SkillBoard.SetActive(true);
        ControlOption.SetActive(false);
    }

    public void ClossSkillBoard()
    {
        SkillBoard.SetActive(false);
        ControlOption.SetActive(true);
    }

    
}
