using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellControl : MonoBehaviour
{
    [SerializeField] private GameManager GameManager;
    [SerializeField] private GameObject persona;
    public bool personaInsist;


    public int fCost;
    public int hCost;
    public int gCost;
    public GameObject parentNode;
    public PathDisplayer pathDisplayer;
    public PathManager pathManager;

    
    public bool Moveable;
    public GameObject moveCell;
    public GameObject attackCell;
 
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        pathManager = GameObject.Find("PathManager").GetComponent<PathManager>();
        if(gameObject.tag=="Cell")
        pathDisplayer = transform.Find("PathDisplayer").gameObject.GetComponent<PathDisplayer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            personaInsist = true;
            persona = other.gameObject;
            pathManager.StartNode = gameObject;
            
        }    
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            personaInsist = false;
        }    
    }
    
   
    private void OnMouseDown()
    {
        if(personaInsist&&persona.GetComponent<PlayerControl>().ableToMove)
        {
            GameManager.selected = persona;
            GameManager.startCell = gameObject;
            GameManager.ShowMoveRange();
            pathManager.ClearNode();
        }
        Debug.Log("这是一个单元格,属性为" + this.tag+"位置为"+transform.position.x+","+transform.position.y);
        if(Moveable&&GameManager.selected.GetComponent<PlayerControl>().ableToMove)
        {
            pathManager.EndNode=gameObject; 
            pathManager.FindPath();
            pathManager.ShowPathWay();
            GameManager.selected.GetComponent<PlayerControl>().Move(pathManager.Path);
        }
    }

    public List<GameObject>GetNeighbour()
    {
        List<GameObject> neightbours = new List<GameObject>();
        Vector2 rayPoint = new Vector2(transform.position.x,transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(rayPoint + Vector2.up,Vector2.up);
        if(hit.collider !=null && hit.collider.CompareTag("Cell"))
        {
            neightbours.Add(hit.collider.gameObject);
        }

        hit = Physics2D.Raycast(rayPoint + Vector2.right,Vector2.right);
        if(hit.collider !=null && hit.collider.CompareTag("Cell"))
        {
            neightbours.Add(hit.collider.gameObject);
        }

        hit = Physics2D.Raycast(rayPoint + Vector2.left,Vector2.left);
        if(hit.collider !=null && hit.collider.CompareTag("Cell"))
        {
            neightbours.Add(hit.collider.gameObject);
        }

        hit = Physics2D.Raycast(rayPoint + Vector2.down,Vector2.down);
        if(hit.collider !=null && hit.collider.CompareTag("Cell"))
        {
            neightbours.Add(hit.collider.gameObject);
        }

        return neightbours;
    }

    public void CalculateCost()
    {
        gCost = (int)Mathf.Round((Mathf.Abs(transform.position.x-pathManager.StartNode.transform.position.x)+Mathf.Abs(transform.position.y-pathManager.StartNode.transform.position.y)));
        hCost = (int)Mathf.Round((Mathf.Abs(transform.position.x-pathManager.EndNode.transform.position.y)+Mathf.Abs(transform.position.y-pathManager.EndNode.transform.position.y)));
        fCost = gCost + hCost;
    }
}
