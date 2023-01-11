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

    public void ShowMoveRange()
    {
        CloseMoveRange();
        int range= selected.GetComponent<PlayerControl>().moveRange;
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

    public void ShowAttackRange()
    {

    }

    public void CloseMoveRange()
    {
        foreach(var cell in moveList)
        {
            cell.GetComponent<CellControl>().Moveable = false;
            cell.GetComponent<CellControl>().moveCell.SetActive(false);
        }
    }

    public void CloseAttackRange()
    {
        
    }

   


}
