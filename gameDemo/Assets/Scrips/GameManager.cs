using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] cells;
    public GameObject selected;

    private List<GameObject> moveList;
    // Start is called before the first frame update
    void Start()
    {
        cells = GameObject.FindGameObjectsWithTag("Cell");
        moveList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMoveRange()
    {
        CloseMoveRange();
        foreach(var cell in cells)
        {
            int range = selected.GetComponent<PlayerControl>().moveRange;
            if(((int)(Mathf.Abs(cell.transform.position.x-selected.transform.position.x)+(Mathf.Abs(cell.transform.position.y-selected.transform.position.y))))<=range&&cell.GetComponent<CellControl>().personaInsist!=true)
            {
                cell.GetComponent<CellControl>().Moveable = true;
                cell.GetComponent<CellControl>().moveCell.SetActive(true);
                moveList.Add(cell);
            }
        }

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
