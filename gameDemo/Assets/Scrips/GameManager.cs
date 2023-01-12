using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    
    public GameObject[] cells;
    
    public GameObject selected;
    public GameObject startCell;


    public List<GameObject> moveList;
    private List<GameObject> now;
    private List<GameObject> open;
    private List<GameObject> closed;

    private PathManager pathManager;
    // Start is called before the first frame update
    void Start()
    {
        cells = GameObject.FindGameObjectsWithTag("Cell");
        moveList = new List<GameObject>();
        now = new List<GameObject>();
        open = new List<GameObject>();
        closed = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMoveRange(int MoveRange)
    {
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
        now.Clear();
        open.Clear();
        closed.Clear();
        selected.GetComponent<PlayerControl>().status = 2;
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
    */
    public void dischargeMagic(int Skilltype,int Skillrange,int Skillvalue)
    {
        switch(Skilltype)
        {
            case 1:
            
            break;
        }
    }

   public void yiDong()
   {
        ShowMoveRange(selected.GetComponent<PlayerControl>().stamina);
   }

    public void gongJi()
    {
        ShowAttackRange(selected.GetComponent<PlayerControl>().attackRange);
    }

    public void jiNeng1()
    {
        ShowAttackRange(selected.GetComponent<PlayerControl>().skill_1_Range);
    }
}
