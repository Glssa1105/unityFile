using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySQ : MonoBehaviour
{
    [SerializeField]private int action1;
    [SerializeField]private int action2;
    [SerializeField]private int action3;
    [SerializeField]private int actionNode;//当前执行行为编号

    [SerializeField]private int action_1_Power;//表示 行为数值
    [SerializeField]private int action_2_Power;//表示 行为数值
    [SerializeField]private int action_3_Power;//表示 行为数值

    private int NowActionPower;//表示 行为数值
    [SerializeField]private EnemyAI enemyAI;
    [SerializeField]private GameManager gameManager;
    [SerializeField]private UIManager uIManager;
    void Start()
    {
        actionNode =1;
        enemyAI = gameObject.GetComponent<EnemyAI>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
   public void Move()
   {
        switch(actionNode)
        {
            case 1:
            NowActionPower= action_1_Power;
            Run(action1);
            actionNode++;
            break;
            case 2:
            NowActionPower= action_2_Power;
            Run(action2);
            actionNode++;
            break;
            case 3:
            NowActionPower= action_3_Power;
            Run(action3);
            actionNode=1;
            break;
        }
   } 
/*

防御1：对伤害1造成的伤害*80%，伤害2的*120%，伤害3的*100%

防御2：对伤害2的*80%，伤害3的*120%，伤害1的*100%

防御3：对伤害3的*80%，伤害1的*120%，伤害2的*100%

public int Defend;//角色防御属性！！ 
public int assault;//角色攻击属性！！

ActionNum表 
1.普通攻击
2.盗贼  以目标角色为中心1圈范围造成20伤害
3.失控的警卫机器人 3从目标角色开始，对和怪物一条直线的后方3格，造成30伤害
*/

   private void Run(int ActionNum)
   {
    Debug.Log(enemyAI.name + "此时AcitonNum为" + ActionNum);
        switch(ActionNum)
        {
            case 1:
            Debug.Log("单点");
            foreach(var item in enemyAI.movelist)
            {
                if(item.GetComponent<CellControl>().personaInsist&&item.GetComponent<CellControl>().persona.tag=="Player")
                {
                    Attack(item.GetComponent<CellControl>().persona.GetComponent<PlayerControl>());
                    break;
                }
            }
            break;
            case 2:
            Debug.Log("十字");
            foreach(var item in enemyAI.movelist)
            {
                if(item.GetComponent<CellControl>().personaInsist&&item.GetComponent<CellControl>().persona.tag=="Player")
                {
                    Attack(item.GetComponent<CellControl>().persona.GetComponent<PlayerControl>());
                    List<GameObject> neighbours = item.GetComponent<CellControl>().GetNeighbourAttack();
                    foreach(var target in neighbours)
                    {
                        Debug.Log("十字目标方块" + target.name);
                        if(target.GetComponent<CellControl>().personaInsist&&target.GetComponent<CellControl>().persona.tag=="Player")
                        {
                            Attack(target.GetComponent<CellControl>().persona.GetComponent<PlayerControl>());
                        }
                    }
                    // uIManager.MessagePrinter.text = gameObject.name+"对"+item.GetComponent<CellControl>().persona.name+"使用十字伤害";
                    break;
                }
            }
            break;
            case 3:
            Debug.Log("2层AOE");
            foreach(var item in enemyAI.movelist)
            {
                if(item.GetComponent<CellControl>().personaInsist&&item.GetComponent<CellControl>().persona!=null)
                {
                    if(item.GetComponent<CellControl>().persona.tag=="Player")
                    {
                        List<GameObject> now = new List<GameObject>();
                        List<GameObject> closed = new List<GameObject>();
                        List<GameObject> open = new List<GameObject>();
                        List<GameObject> moveList = new List<GameObject>();
                        now.Add(item);//将开始方块加入NOW
                        closed.Add(item);
                        for(int i=0;i<2;i++)
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
                                        moveList.Add(neighbour);
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
                        moveList.Add(item);
                        foreach(var target in moveList)
                        {                            
                            if(target.GetComponent<CellControl>().personaInsist&&target.GetComponent<CellControl>().persona.tag=="Player")
                            {
                                Attack(target.GetComponent<CellControl>().persona.GetComponent<PlayerControl>());
                            }
                        }
                        break;
                    }
                }
            }
            
            break;
        }
   }

    private void Attack(PlayerControl target)
    {
        Debug.Log("目标为！："+target.name);
        target.blood -= gameManager.countHurt(enemyAI.assault,target.defend,NowActionPower);
        uIManager.MessagePrinter.text =gameObject.name+" 对 " + target.gameObject.name +  "造成了" + gameManager.countHurt(enemyAI.assault,target.defend,NowActionPower) + "点伤害！";
        if(target.blood<=0)
        {
            uIManager.MessagePrinter.text += target.name+"死亡！";
            gameManager.Dead(target.gameObject);
        }
    }


    
}
