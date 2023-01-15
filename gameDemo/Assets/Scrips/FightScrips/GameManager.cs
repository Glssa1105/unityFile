using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool TEST;//true 不从 GameMessage中读取信息   false 读取信息
    public int SceneNUMBER; 
    
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    [SerializeField]private GameMessage gameMessage;


    public GameObject[] cells;
    public GameObject[] enemyList;

    private GameObject[] insistPlayerList; 
    private GameObject[] insistEnemyList;
    [SerializeField] List<GameObject> iPlist;
    [SerializeField] List<GameObject> iElist;  
    
    public GameObject selected;
    public GameObject startCell;


    public int Skilltype;
    public int SkillStatus;//技能攻击属性 伤害1，2，3
    public int Skillrange;
    public int Skillvalue;
    public int SkillMPcost;
    public string SkillName;
    public int SkillNumber;
    public GameObject target;

    public List<GameObject> moveList;
    private List<GameObject> now;
    private List<GameObject> open;
    private List<GameObject> closed;
    [SerializeField]private UIManager uIManager;
    // Start is called before the first frame update
    void Start()
    {
        gameMessage = GameObject.Find("GameMessage").GetComponent<GameMessage>();
        cells = GameObject.FindGameObjectsWithTag("Cell");
        moveList = new List<GameObject>();
        now = new List<GameObject>();
        open = new List<GameObject>();
        closed = new List<GameObject>();
        iElist = new List<GameObject>();
        iPlist = new List<GameObject>();
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        insistEnemyList = GameObject.FindGameObjectsWithTag("Enemy");
        insistPlayerList = GameObject.FindGameObjectsWithTag("Player");
        if(TEST)
        {
            if(gameMessage.Player1Insist)
            {
                player1.SetActive(true);
            }
            if(gameMessage.Player2Insist)
            {
                player2.SetActive(true);
            }
            if(gameMessage.Player3Insist)
            {
                player3.SetActive(true);
            }
            if(gameMessage.Player4Insist)
            {
                player4.SetActive(true);
            }

        }
        foreach(var item in insistEnemyList)
        {
            iElist.Add(item);
        }
        foreach(var item in insistPlayerList)
        {
            iPlist.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowMoveRange(int MoveRange)
    {
        CleanMoveList();
        selected.GetComponent<PlayerControl>().status = 1;
        CloseMoveRange();
        CloseAttackRange();
        now.Clear();
        open.Clear();
        closed.Clear();
        int range=MoveRange;
        if(selected.GetComponent<PlayerControl>().ableToMove==false) return ;
        now.Add(startCell);//将开始方块加入NOW
        closed.Add(startCell);
        for(int i=0;i<range;i++)
        {
            foreach(var current in now)
            {
                closed.Add(current);
                List<GameObject>neighbours =  current.GetComponent<CellControl>().GetNeighbour();
                foreach(var neighbour in neighbours)
                {
                    if(closed.Contains(neighbour))
                    {
                        continue;
                    }
                    if(!open.Contains(neighbour))
                    {
                        open.Add(neighbour);
                        neighbour.GetComponent<CellControl>().Moveable = true;
                        neighbour.GetComponent<CellControl>().moveCell.SetActive(true);
                        moveList.Add(neighbour);
                    }
                }
            }
            now.Clear();
            foreach(var item in open)
            {
                now.Add(item);
            }
            open.Clear();
        }
        now.Clear();
        open.Clear();
        closed.Clear();
    }

    public void ShowAttackRange(int AttackRange)
    {  
        CleanMoveList();
        now.Clear();
        open.Clear();
        closed.Clear();
        CloseMoveRange();
        CloseAttackRange();
        int range=AttackRange;
        Debug.Log(range);
        if(selected.GetComponent<PlayerControl>().ableToMove==false) return ;
        now.Add(startCell);//将开始方块加入NOW
        closed.Add(startCell);
        for(int i=0;i<range;i++)
        {
            foreach(var current in now)
            {
                closed.Add(current);
                List<GameObject>neighbours =  current.GetComponent<CellControl>().GetNeighbourAttack();
                foreach(var neighbour in neighbours)
                {
                    if(closed.Contains(neighbour))
                    {
                        continue;
                    }
                    if(!open.Contains(neighbour))
                    {
                        open.Add(neighbour);
                        neighbour.GetComponent<CellControl>().Moveable = true;
                        neighbour.GetComponent<CellControl>().attackCell.SetActive(true);
                        moveList.Add(neighbour);
                    }
                }
            }
            now.Clear();
            foreach(var item in open)
            {
                now.Add(item);
            }
            open.Clear();
        }
        now.Clear();
        open.Clear();
        closed.Clear();
    }

    public void CloseMoveRange()
    {
        foreach(var cell in moveList)
        {
            cell.GetComponent<CellControl>().Moveable = false;
            cell.GetComponent<CellControl>().moveCell.SetActive(false);
        }
        // moveList.Clear();
    }

    public void CloseAttackRange()
    {
        foreach(var cell in moveList)
        {
            cell.GetComponent<CellControl>().Moveable = false;
            cell.GetComponent<CellControl>().attackCell.SetActive(false);
        }
        // moveList.Clear();
    }

    /*
     Skilltype : 1. 单点打击   2. AOE //后面待定
    
    int Skilltype;//技能种类
    int Skillrange;//效果范围
    int Skillvalue;//效果数值
    
    当 skilltype 为 1 时为单点打击
    skillrange为可打击范围
    skillvalue为伤害

    当 skilltype 为 2 时为AOE
    skillrange为群体打击范围
    skillvalue为伤害

    当 skilltype 为 3，4 时为对目标及其周边的AOE
    3为敌人及其周边距离小于等于1
    4为敌人及其周边距离小于等于2
    */
    public void dischargeMagic()
    {
        selected.GetComponent<PlayerControl>().Mp-=SkillMPcost;
        if(SkillNumber == 1)
        {
            selected.GetComponent<PlayerControl>().skill_1_CDwait = selected.GetComponent<PlayerControl>().skill_1_CD;
        }
        else if(SkillNumber == 2)
        {
            selected.GetComponent<PlayerControl>().skill_2_CDwait = selected.GetComponent<PlayerControl>().skill_2_CD;
        }
        switch(Skilltype)
        {
            case 1:
                target.GetComponent<CellControl>().persona.GetComponent<EnemyAI>().blood-=countHurt(SkillStatus,target.GetComponent<CellControl>().persona.GetComponent<EnemyAI>().defend,Skillvalue);
                uIManager.MessagePrinter.text = selected.name+"用技能"+SkillName + "对 " +target.GetComponent<CellControl>().persona.name +  "造成了" + Skillvalue + "点伤害！";
                if(target.GetComponent<CellControl>().persona.GetComponent<EnemyAI>().blood<=0)
                {
                    uIManager.MessagePrinter.text = target.GetComponent<CellControl>().persona.name+"死亡！";
                    Dead(target.GetComponent<CellControl>().persona.gameObject);
                }      
                break;
            case 2:
                uIManager.MessagePrinter.text = null;
                foreach(var item in moveList)
                {
                    if(item.GetComponent<CellControl>().persona!=null)
                    {
                        if(item.GetComponent<CellControl>().persona.tag=="Enemy")
                        {
                            item.GetComponent<CellControl>().persona.GetComponent<EnemyAI>().blood-=countHurt(SkillStatus,item.GetComponent<CellControl>().persona.GetComponent<EnemyAI>().defend,Skillvalue);
                            uIManager.MessagePrinter.text = selected.name+"用技能" + SkillName +"对 " +target.GetComponent<CellControl>().persona.name +  "造成了" + countHurt(SkillStatus,item.GetComponent<CellControl>().persona.GetComponent<EnemyAI>().defend,Skillvalue) + "点伤害！";
                        }
                    }
                }
                enemyList = GameObject.FindGameObjectsWithTag("Enemy");
                foreach(var item in enemyList)
                {
                    if(item.GetComponent<EnemyAI>().blood<=0)
                    {
                        uIManager.MessagePrinter.text = item.name + "死亡！\n";
                        Dead(item);
                    }
                }
                break;
            case 3:
                uIManager.MessagePrinter.text = null;
                List<GameObject> attackable = target.GetComponent<CellControl>().GetNeighbourAttack();
                attackable.Add(target);
                foreach(var item in attackable)
                {
                    if(item.GetComponent<CellControl>().persona!=null)
                    {
                        if(item.GetComponent<CellControl>().persona.tag=="Enemy")
                        {
                            item.GetComponent<CellControl>().persona.GetComponent<EnemyAI>().blood-=countHurt(SkillStatus,item.GetComponent<CellControl>().persona.GetComponent<EnemyAI>().defend,Skillvalue);
                            uIManager.MessagePrinter.text = selected.name+"用技能" + SkillName +"对 " +target.GetComponent<CellControl>().persona.name +  "造成了" + countHurt(SkillStatus,item.GetComponent<CellControl>().persona.GetComponent<EnemyAI>().defend,Skillvalue) + "点伤害！";
                        }
                    }
                }
                attackable.Clear();
                enemyList = GameObject.FindGameObjectsWithTag("Enemy");
                foreach(var item in enemyList)
                {
                    if(item.GetComponent<EnemyAI>().blood<=0)
                    {
                        uIManager.MessagePrinter.text = item.name + "死亡！\n";
                        Dead(item);
                    }
                }
                break;
            case 4:
                uIManager.MessagePrinter.text = null;
                List<GameObject> now = new List<GameObject>();
                List<GameObject> closed = new List<GameObject>();
                List<GameObject> open = new List<GameObject>();
                List<GameObject> targetList = new List<GameObject>();
                now.Add(target);
                closed.Add(target);
                for(int i=0;i<2;i++)
                {
                    foreach(var current in now)
                    {
                        closed.Add(current);
                        List<GameObject>neighbours = current.GetComponent<CellControl>().GetNeighbourAttack();
                        foreach(var neighbour in neighbours)
                        {
                            if(closed.Contains(neighbour))
                            {
                                continue;
                            }
                            if(!open.Contains(neighbour))
                            {
                                open.Add(neighbour);
                                targetList.Add(neighbour);
                            }
                        }
                    }
                    now.Clear();
                    foreach(var ittt in open)
                    {
                        now.Add(ittt);
                    }
                    open.Clear();
                }
                now.Clear();
                open.Clear();
                closed.Clear();
                targetList.Add(target);
                foreach(var item in targetList)
                {
                    if(item.GetComponent<CellControl>().persona!=null)
                    {
                        if(item.GetComponent<CellControl>().persona.tag=="Enemy")
                        {
                            item.GetComponent<CellControl>().persona.GetComponent<EnemyAI>().blood-=countHurt(SkillStatus,item.GetComponent<CellControl>().persona.GetComponent<EnemyAI>().defend,Skillvalue);
                            uIManager.MessagePrinter.text = selected.name+"用技能" + SkillName +"对 " +item.GetComponent<CellControl>().persona.name +  "造成了" + countHurt(SkillStatus,item.GetComponent<CellControl>().persona.GetComponent<EnemyAI>().defend,Skillvalue) + "点伤害！";
                        }
                    }
                }
                enemyList = GameObject.FindGameObjectsWithTag("Enemy");
                foreach(var item in enemyList)
                {
                    if(item.GetComponent<EnemyAI>().blood<=0)
                    {
                        uIManager.MessagePrinter.text = item.name + "死亡！\n";
                        Dead(item);
                    }
                }
                targetList.Clear();
                break;    
        }
    }

   public void yiDong()
   {
        CloseMoveRange();
        CloseAttackRange();
        selected.GetComponent<PlayerControl>().status = 1;
        ShowMoveRange(selected.GetComponent<PlayerControl>().stamina);
   }

    public void gongJi()
    {
        CloseMoveRange();
        CloseAttackRange();
        if(selected.GetComponent<PlayerControl>().HaveAttacked==true)
        {
            uIManager.MessagePrinter.text = selected.name + "在本回合已经攻击过了";
            return ;
        }
        selected.GetComponent<PlayerControl>().status = 2;
        ShowAttackRange(selected.GetComponent<PlayerControl>().attackRange);
    }

    public void jiNeng1()
    {
        CloseMoveRange();
        CloseAttackRange();
        if(selected.GetComponent<PlayerControl>().Mp<selected.GetComponent<PlayerControl>().skill_1_MpCost)
        {
            uIManager.MessagePrinter.text = "该角色MP不足，无法释放该技能";
            return ;
        }
        if(selected.GetComponent<PlayerControl>().skill_1_CDwait!=0)
        {
            uIManager.MessagePrinter.text = "该技能尚未冷却完毕！";
            return ;
        }
        selected.GetComponent<PlayerControl>().status = 3;
        Debug.Log(selected.gameObject.name + "使用技能 1 此时的状态statu值为" + selected.GetComponent<PlayerControl>().status);
        Skillrange = selected.GetComponent<PlayerControl>().skill_1_Range;
        Skilltype = selected.GetComponent<PlayerControl>().skill_1_type;
        Skillvalue = selected.GetComponent<PlayerControl>().skill_1_value;
        SkillMPcost = selected.GetComponent<PlayerControl>().skill_1_MpCost;
        SkillName = selected.GetComponent<PlayerControl>().skill_1_name;
        SkillStatus = selected.GetComponent<PlayerControl>().skill_1_SkillStatu;
        SkillNumber = 1;
        ShowAttackRange(selected.GetComponent<PlayerControl>().skill_1_Range);
    }

    public void jiNeng2()
    {
        CloseMoveRange();
        CloseAttackRange();
        if(selected.GetComponent<PlayerControl>().Mp<selected.GetComponent<PlayerControl>().skill_2_MpCost)
        {
            uIManager.MessagePrinter.text = "该角色MP不足，无法释放该技能";
            return ;
        }
        if(selected.GetComponent<PlayerControl>().skill_2_CDwait!=0)
        {
            uIManager.MessagePrinter.text = "该技能尚未冷却完毕！";
            return ;
        }
        selected.GetComponent<PlayerControl>().status =3;
        Debug.Log("使用技能 2 此时的状态statu值为" + selected.GetComponent<PlayerControl>().status);
        Skillrange = selected.GetComponent<PlayerControl>().skill_2_Range;
        Skilltype = selected.GetComponent<PlayerControl>().skill_2_type;
        Skillvalue = selected.GetComponent<PlayerControl>().skill_2_value;
        SkillMPcost = selected.GetComponent<PlayerControl>().skill_2_MpCost;
        SkillName = selected.GetComponent<PlayerControl>().skill_2_name;
        SkillStatus = selected.GetComponent<PlayerControl>().skill_2_SkillStatu;
        SkillNumber = 2;
        ShowAttackRange(selected.GetComponent<PlayerControl>().skill_2_Range);
    }
    
    public void Dead(GameObject target)
    {
        if(target.tag == "Player")
        {
            for(int i=0;i<iPlist.Count;i++)
            {
                if(target == iPlist[i])
                {
                    iPlist.Remove(target);
                    target.SetActive(false);
                }
            }
            if(iPlist.Count == 0)
            {
                End(1);
                return ;
            }
        }
        if(target.tag == "Enemy")
        {
            for(int i=0;i<iElist.Count;i++)
            {
                if(target == iElist[i])
                {
                    iElist.Remove(target);
                    target.SetActive(false);
                }
            }
            if(iElist.Count== 0)
            {
                End(0);
                return ;
            }
        }
    }

    private void End(int EndNum)
    {
        if(EndNum ==1)
        {
            uIManager.MessagePrinter.text = "Enemy Win";
            uIManager.ShowLoseUI();
        }
        else
        {
            uIManager.MessagePrinter.text = "Player Win";
            uIManager.ShowWinUI();
        }
    }

    public int countHurt(int assaultNum,int defendNum,int power)
    {
        switch(assaultNum)
        {
            case 1:
            switch(defendNum)
            {
                case 1:
                return Mathf.FloorToInt((float)power * (float)0.8);
                case 2:
                return Mathf.FloorToInt((float)power * (float)1);
                default:
                return Mathf.FloorToInt((float)power * (float)1.2);
            }
            case 2:
            switch(defendNum)
            {
                case 1:
                return Mathf.FloorToInt((float)power * (float)1.2);
                case 2:
                return Mathf.FloorToInt((float)power * (float)0.8);
                default:
                return Mathf.FloorToInt((float)power * (float)1.0);
            }
            default:
            switch(defendNum)
            {
                case 1:
                return Mathf.FloorToInt((float)power * (float)1.0);
                case 2:
                return Mathf.FloorToInt((float)power * (float)1.2);
                default:
                return Mathf.FloorToInt((float)power * (float)0.8);
            }
        }
    }

    public void CleanMoveList()
    {
        moveList.Clear();
    }
}
