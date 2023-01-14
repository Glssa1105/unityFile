using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private List<GameObject> now;
    private List<GameObject> open;
    private List<GameObject> closed;
    
    public List<GameObject> movelist;
    public GameObject StartCell;
    [SerializeField]private GameObject CloestPlayer;
    [SerializeField]private Vector2 PaTarget;
    [SerializeField]private PathManager pathManager;
    [SerializeField]private GameManager gameManager;
    [SerializeField]private UIManager uIManager;
    [SerializeField]private EnemySQ enemySQ;
    
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
    */

        //以下为AI属性
        public int defend;//角色防御属性！！！
        public int assault;//角色攻击属性！！！
        public int moveRange;//移动范围
        public int attackRange;//可攻击范围大小
        public int number;//角色立场
        public int maxBlood;//最大血量
        public int blood;//目前血量
        [SerializeField]private float waitTime;//每次运动等待时间
        [SerializeField]private int BestWantDistance;//AI倾向与最近玩家的位置距离
    
  
  
    [SerializeField]private GameObject[] playerList;
    [SerializeField]private float speed;
    [SerializeField]private bool Moving;
    [SerializeField]private bool end;
    //    public int status;//表示目标状态！0：静止 1：运动 2：攻击
    //


   
    // Start is called before the first frame update
    void Start()
    {
        now = new List<GameObject>();
        open = new List<GameObject>();
        closed = new List<GameObject>();
        movelist = new List<GameObject>();
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerList = GameObject.FindGameObjectsWithTag("Player");
        pathManager = GameObject.Find("PathManager").GetComponent<PathManager>();
        enemySQ = gameObject.GetComponent<EnemySQ>();
    }

    // Update is called once per frame
    void Update()
    {   
        
        if(Input.GetKeyDown(KeyCode.K))
        {
            Go();
        }
        if(Moving)
        {
           Running(PaTarget);
        }
    }
    
    public void Go()//主要流程！
    {
        updatePlayerList();
        SetStartCell();
        GetMoveList(moveRange);
        FindClosestPlayer();
        Debug.Log(CloestPlayer.name);
        gameManager.startCell = StartCell;
        gameManager.selected = gameObject;
        pathManager.EndNode = FindBestCell();
        Debug.Log(pathManager.EndNode.name);
        pathManager.StartNode=StartCell;
        Debug.Log(pathManager.StartNode.name);
        pathManager.rFindPath();
        pathManager.GetrPath();
        gameObject.GetComponent<EnemyAI>().Move(pathManager.rPath);
    }

    public void SetStartCell()//确定脚下位置
    {
        RaycastHit2D hits = Physics2D.Raycast(transform.position,Vector2.zero);
        StartCell = hits.collider.gameObject;
        //Debug.Log(StartCell.name);
    }

    public void GetMoveList(int range)
    {
        CloseMoveList();
        now.Clear();
        open.Clear();
        closed.Clear();
        now.Add(StartCell);//将开始方块加入NOW
        closed.Add(StartCell);
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
                        movelist.Add(neighbour);
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

    public void GetAttackList(int attackRange)
    {  
        now.Clear();
        open.Clear();
        closed.Clear();
        CloseMoveList();
        now.Add(StartCell);//将开始方块加入NOW
        closed.Add(StartCell);
        for(int i=0;i<attackRange;i++)
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
                        movelist.Add(neighbour);
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

    public void FindClosestPlayer()
    {
        CloestPlayer = playerList[0];
        foreach(var item in playerList)
        {
            if(Mathf.Abs(item.transform.position.x-gameObject.transform.position.x)+Mathf.Abs(item.transform.position.y-gameObject.transform.position.y)<
            Mathf.Abs(CloestPlayer.transform.position.x-gameObject.transform.position.x)+Mathf.Abs(CloestPlayer.transform.position.y-gameObject.transform.position.y))
            {
                CloestPlayer = item;
            }
        }
        Debug.Log("离敌人" + gameObject.name+ "最近的角色为"+CloestPlayer.name);

    }

    public GameObject FindBestCell()
    {
        
        GameObject BestCell=movelist[0];
        float BestScore = 0;
        foreach(var item in movelist)
        {
            float score = (10-Mathf.Abs(BestWantDistance-Mathf.Abs(item.transform.position.x-CloestPlayer.transform.position.x)-Mathf.Abs(item.transform.position.y-CloestPlayer.transform.position.y)));
            if(score>=BestScore)
            {
                BestScore = score;
                BestCell = item;
            }
        }
        return BestCell;
    }

    public void CloseMoveList()
    {
        foreach(var cell in movelist)
        {
            cell.GetComponent<CellControl>().Moveable = false;
        }
        movelist.Clear();
    }

    IEnumerator counter(float waitTime,int times,List<GameObject> target)
    {
        Moving = true;
        for(int i=times-1;i>=0;i--)
        {
            if(i!=times-1)
            yield return new WaitForSeconds(waitTime);
            PaTarget= new Vector2(target[i].transform.position.x,target[i].transform.position.y);
        }
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Moving");
        CloseMoveList();
        SetStartCell();
        GetAttackList(attackRange);
        enemySQ.Move();
        CloseMoveList();
        SetStartCell();
        uIManager.nextRun();
    }

    private void Running(Vector2 target)
    {
        transform.position = Vector3.MoveTowards(transform.position,target,speed*Time.deltaTime);
    }

    public void Move(List<GameObject> Path)
    {
        int times = Path.Count;
        Debug.Log(times);
        StartCoroutine(counter(waitTime,times,Path));  
        uIManager.MessagePrinter.text = "角色" + gameObject.name + "从" +StartCell.name + "移动至" + pathManager.EndNode.name;  
    }

    // public void Attack(PlayerControl target)
    // {
    //     Debug.Log("目标为！："+target.name);
    //     target.blood -=power;
    //     uIManager.MessagePrinter.text =gameObject.name+" 对 " + target.gameObject.name +  "造成了" + power + "点伤害！";
    //     if(target.blood<=0)
    //     {
    //         uIManager.MessagePrinter.text += target.name+"死亡！";
    //         gameManager.Dead(target.gameObject);
    //     }
    // }

    private void updatePlayerList()
    {
        playerList = GameObject.FindGameObjectsWithTag("Player");
    }
}
