using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]private GameManager GameManager;
    [SerializeField]private PathManager pathManager;
    [SerializeField]private UIManager uIManager;
    
    public float waitTime;
    public float speed;
    public Vector2 PaTarget;
    public bool ableToMove;
    private bool Moving;
    public bool HaveAttacked;


    //以下为玩家属性
        public int defend;//角色防御属性！！ 
        public int assault;//角色攻击属性！！
        public int Maxstamina;//最大体力
        public int stamina;//目前体力
        public int attackRange;//可攻击范围
        public int number;//角色立场
        public int maxBlood;//最大血量
        public int blood;//目前血量
        public int power;//攻击力
        public int MaxMP;//最大MP
        public int Mp;//目前MP
        public int status;//表示目标状态！0：静止 1：运动 2：攻击 3:技能


        public string skill_1_name;//表示技能 1 名称
        public int skill_1_Range;//表示 1 技能范围
        public int skill_1_type;//表示 1 技能种类
        public int skill_1_value;//表示 1 数值 根据技能种类含不同意义
        public int skill_1_MpCost;
        public int skill_1_CD;
        public int skill_1_CDwait;//目前冷却进度

        public string skill_2_name;//表示技能 2 名称
        public int skill_2_Range;//表示 2 技能范围
        public int skill_2_type;//表示 2 技能种类
        public int skill_2_value;//表示 2 数值 根据技能种类含不同意义
        public int skill_2_MpCost;
        public int skill_2_CD;
        public int skill_2_CDwait;//目前冷却进度
    //

    void Start()
    {
        HaveAttacked = false;
        stamina=Maxstamina;
        pathManager = GameObject.Find("PathManager").GetComponent<PathManager>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
    
   
    void Update()
    {
        CheckAbleToMove();
        if(Moving)
        {
           Running(PaTarget);
        }
    }
    private void CheckAbleToMove()
    {
        if(uIManager.AbleToMoveNumber!=number)
        {
            ableToMove = false;
        }
        else
        ableToMove = true;
    }

    private void OnMouseDown()
    {
        Debug.Log("这是一个玩家,属性为" + this.tag+"位置为"+transform.position.x+","+transform.position.y);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag=="Cell")
        {
            other.GetComponent<CellControl>().personaInsist = true;
        } 
    }
    
    public void Move(List<GameObject> Path)
    {
        int times = Path.Count;
        Debug.Log(times);
        ableToMove = false;
        StartCoroutine(counter(waitTime,times,Path));  
        GameManager.selected.GetComponent<PlayerControl>().stamina -= (int)(Mathf.Abs(pathManager.StartNode.transform.position.x-pathManager.EndNode.transform.position.x)+Mathf.Abs(pathManager.StartNode.transform.position.y-pathManager.EndNode.transform.position.y));
        GameManager.startCell.GetComponent<CellControl>().moveCell.SetActive(false);
        uIManager.MessagePrinter.text = "角色" + gameObject.name + "从" + GameManager.startCell.name + "移动至" + pathManager.EndNode.name;  
        GameManager.startCell = pathManager.EndNode;
    }

    private void Running(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position,target,speed*Time.deltaTime);
    }

    public void Attack(EnemyAI target)
    {
        target.blood -=GameManager.countHurt(assault,target.defend,power);
        Debug.Log(gameObject.name+" 对 " + target.gameObject.name +  "造成了" + GameManager.countHurt(assault,target.defend,power) + "点伤害！");
        uIManager.MessagePrinter.text = gameObject.name+" 对 " + target.gameObject.name +  "造成了" + GameManager.countHurt(assault,target.defend,power) + "点伤害！";
        if(target.blood<=0)
        {
            uIManager.MessagePrinter.text += target.name+"死亡！";
            GameManager.Dead(target.gameObject);
        }
        HaveAttacked= true;
    }


    IEnumerator counter(float waitTime,int times,List<GameObject> target)
    {
        Moving = true;
        for(int i=times-1;i>=0;i--)////////times-1
        {
            yield return new WaitForSeconds(waitTime);
            PaTarget= new Vector2(target[i].transform.position.x,target[i].transform.position.y);
            PathDisplayer pathDisplayer = target[i].transform.Find("PathDisplayer").GetComponent<PathDisplayer>();
            pathDisplayer.CancelNode();
        }
        yield return new WaitForSeconds(waitTime);
        Moving = false;
        Debug.Log("Moving");
        GameManager.CloseMoveRange();
        ableToMove =true;
    }


    public void SetStartCell()//确定脚下位置
    {
        RaycastHit2D hits = Physics2D.Raycast(transform.position,Vector2.zero);
        hits.collider.gameObject.GetComponent<CellControl>().personaInsist=true;
        hits.collider.gameObject.GetComponent<CellControl>().persona = gameObject;
    }

}
