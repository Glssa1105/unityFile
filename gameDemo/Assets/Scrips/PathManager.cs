using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField]private GameManager gameManager;
    public List<GameObject> OpenList;
    public List<GameObject> ClosedList;

    public List<GameObject> Path;
    public List<GameObject> rPath;
    public GameObject StartNode;
    public GameObject EndNode;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        OpenList = new List<GameObject>();
        ClosedList = new List<GameObject>();
        Path = new List<GameObject>();
        rPath = new List<GameObject>();
    }
    
    public void ClearNode()
    {
        foreach(var cell in ClosedList)
        {
            cell.GetComponent<CellControl>().pathDisplayer.CancelNode();
            cell.GetComponent<CellControl>().parentNode = null;
        }
        foreach(var cell in OpenList)
        {
            cell.GetComponent<CellControl>().pathDisplayer.CancelNode();
            cell.GetComponent<CellControl>().parentNode = null;
        }
        ClosedList.Clear();
        OpenList.Clear();
    }

    public GameObject LowestCost(List<GameObject> Openlist)
    {
        int minCost = 1000;
        GameObject LowestCostCell=OpenList[0];
        foreach(var cell in OpenList)
        {
            if(LowestCostCell == EndNode)
            return LowestCostCell;
            if(cell.GetComponent<CellControl>().fCost<=minCost)
            {
                LowestCostCell = cell;
                minCost = cell.GetComponent<CellControl>().fCost;
            }
        }
        return LowestCostCell;
    }

    public void ShowPathWay()
    {
        Path.Clear();
        GameObject begin = EndNode;
        GameObject last = EndNode;
        GameObject next;
        for(int i=0;i<100;i++)
        {
            // Debug.Log("此时last物体名称为"+last.name);
            // Debug.Log("此时begin物体名称为"+begin.name);
            // Debug.Log("此时next物体名称为"+begin.GetComponent<CellControl>().parentNode.name);
            next = begin.GetComponent<CellControl>().parentNode;
            Path.Add(begin);
            float beginx = begin.transform.position.x;
            float beginy = begin.transform.position.y;
            float nextx = next.transform.position.x;
            float nexty = next.transform.position.y;
            float lastx = last.transform.position.x;
            float lasty = last.transform.position.y;
            Debug.Log(begin.name+"  beginx="+beginx + "beginy=" + beginy+"nextx="+nextx+"nexty="+nexty);
            //begin.GetComponent<CellControl>().pathDisplayer.line.SetActive(true);
            if(i==0)//箭头位
            {
                begin.GetComponent<CellControl>().pathDisplayer.arrow.SetActive(true);
                if(nexty>beginy)
                {
                    begin.GetComponent<CellControl>().pathDisplayer.arrow.transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
                }
                else if(nextx>beginx)
                {
                    begin.GetComponent<CellControl>().pathDisplayer.arrow.transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
                }
                else if(nextx<beginx)
                {
                    begin.GetComponent<CellControl>().pathDisplayer.arrow.transform.rotation = Quaternion.Euler(new Vector3(0,0,-90));
                }
            }
            else
            {
                if(lasty<beginy)//当前在上一位上方
                {
                   if(nextx==beginx)
                   {
                        begin.GetComponent<CellControl>().pathDisplayer.line.SetActive(true);
                        begin.GetComponent<CellControl>().pathDisplayer.line.transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
                   }
                   else if(nextx<beginx)
                   {
                        begin.GetComponent<CellControl>().pathDisplayer.corner.SetActive(true);
                        begin.GetComponent<CellControl>().pathDisplayer.corner.transform.rotation = Quaternion.Euler(new Vector3(0,0,270));
                   }
                   else if(nextx>beginx)
                   {
                        begin.GetComponent<CellControl>().pathDisplayer.corner.SetActive(true);
                        begin.GetComponent<CellControl>().pathDisplayer.corner.transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
                   }
                }
                else if(lasty>beginy)//当前在上一位下方
                {
                    if(nextx==beginx)
                    {
                        begin.GetComponent<CellControl>().pathDisplayer.line.SetActive(true);
                        begin.GetComponent<CellControl>().pathDisplayer.line.transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
                    }
                    else if(nextx>beginx)
                    {
                        begin.GetComponent<CellControl>().pathDisplayer.corner.SetActive(true);
                    }
                    else if(nextx<beginx)
                    {
                        begin.GetComponent<CellControl>().pathDisplayer.corner.SetActive(true);
                        begin.GetComponent<CellControl>().pathDisplayer.corner.transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
                    }
                }
                else
                {
                    begin.GetComponent<CellControl>().pathDisplayer.line.SetActive(true);
                }
            }
            last = begin;
            begin = begin.GetComponent<CellControl>().parentNode;
            if(begin == StartNode)
            return ;
        }
    }
    

    public void FindPath()
    {
        //ClearNode();
        OpenList.Clear();//ssss
        StartNode = gameManager.startCell;
        OpenList.Add(StartNode);
        for(int i=0;i<100;i++)
        {
            GameObject current = LowestCost(OpenList);
            OpenList.Remove(current);
            ClosedList.Add(current);
            if(current == EndNode)
            {
                Debug.Log("getway!");
                // ShowPathWay();
                return ;
            }
            
            List<GameObject> neighbours = current.GetComponent<CellControl>().GetNeighbour();
            foreach(var cell in neighbours)
            {
                if(ClosedList.Contains(cell))
                {
                    continue;
                }
                if(!ClosedList.Contains(cell))
                {
                    cell.GetComponent<CellControl>().CalculateCost();
                    cell.GetComponent<CellControl>().parentNode = current;
                    OpenList.Add(cell);//添加至代搜索节点
                }
            }
        }
        ClosedList.Clear();
    }
    public void rFindPath()
    {
        OpenList.Clear();
        ClosedList.Clear();
        OpenList.Add(StartNode);
        for(int i=0;i<100;i++)
        {
            GameObject current = LowestCost(OpenList);
            OpenList.Remove(current);
            ClosedList.Add(current);
            if(current == EndNode)
            {
                Debug.Log("getway!");
                // ShowPathWay();
                return ;
            }
            List<GameObject> neighbours = current.GetComponent<CellControl>().GetNeighbour();
            foreach(var cell in neighbours)
            {
                if(ClosedList.Contains(cell))
                {
                    continue;
                }
                if(!ClosedList.Contains(cell))
                {
                    cell.GetComponent<CellControl>().CalculateCost();
                    cell.GetComponent<CellControl>().parentNode = current;
                    OpenList.Add(cell);//添加至代搜索节点
                }
            }
        }
        OpenList.Clear();
        ClosedList.Clear();
    }

    public void GetrPath()
    {
        rPath.Clear();
        GameObject begin = EndNode;
        GameObject last = EndNode;
        GameObject next;
        for(int i=0;i<100;i++)
        {
            Debug.Log("rPath.name="+begin.name);
            next = begin.GetComponent<CellControl>().parentNode;
            rPath.Add(begin);
            last = begin;
            begin = begin.GetComponent<CellControl>().parentNode;
            if(begin == StartNode)
            return ;
        }
    }
}
