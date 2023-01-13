using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellControl : MonoBehaviour
{
    [SerializeField] private GameManager GameManager;
    [SerializeField] private UIManager uIManager;

    public GameObject persona;
    
    public bool personaInsist;


    public int fCost;
    public float hCost;
    public float gCost;
    public GameObject parentNode;
    public PathDisplayer pathDisplayer;
    public PathManager pathManager;
    public int PlayerNumber;
    public bool Moveable;
    public GameObject moveCell;
    public GameObject attackCell;
 
    // Start is called before the first frame update
    void Start()
    {
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        pathManager = GameObject.Find("PathManager").GetComponent<PathManager>();
        if(gameObject.tag=="Cell")
        pathDisplayer = transform.Find("PathDisplayer").gameObject.GetComponent<PathDisplayer>();
    }


    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" || other.tag =="Enemy")
        {
            //personaInsist = true;
            persona = other.gameObject;
            if(other.tag == "Player")
            PlayerNumber = other.GetComponent<PlayerControl>().number;
            transform.tag = "Block";
        }    
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag=="Player" || other.tag == "Enemy")
        {
            personaInsist = true;
        } 
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player" || other.tag == "Enemy")
        {
            PlayerNumber = 0;
            personaInsist = false;
            transform.tag = "Cell";
            persona =null;
        }    
    }
    
   
    private void OnMouseDown()
    {
        if(personaInsist&&persona.tag == "Player")
        {

            if(persona.GetComponent<PlayerControl>().ableToMove)
            {  
                if(GameManager.startCell)
                GameManager.startCell.GetComponent<CellControl>().moveCell.SetActive(false);
                GameManager.CloseMoveRange();
                GameManager.CloseAttackRange();
                GameManager.selected = persona;
                GameManager.startCell = gameObject;
                Debug.Log(GameManager.startCell.name);
                GameManager.startCell.GetComponent<CellControl>().moveCell.SetActive(true);
                uIManager.CreateOption();
                uIManager.ClossSkillBoard();
                pathManager.ClearNode();
                GameManager.startCell.GetComponent<CellControl>().moveCell.SetActive(true);
            }
        }
        Debug.Log("这是一个单元格,属性为" + this.tag+"位置为"+transform.position.x+","+transform.position.y);
        if(Moveable&&GameManager.selected.GetComponent<PlayerControl>().ableToMove)
        {
            if(GameManager.selected.GetComponent<PlayerControl>().status == 1)
            {
                GameManager.CloseMoveRange();
                GameManager.CloseAttackRange();
                pathManager.EndNode=gameObject; 
                pathManager.FindPath();
                pathManager.ShowPathWay();
                GameManager.selected.GetComponent<PlayerControl>().Move(pathManager.Path);
            }
            else if(GameManager.selected.GetComponent<PlayerControl>().status == 2)
            {
                GameManager.CloseMoveRange();
                GameManager.CloseAttackRange();
                if(persona!=null)
                GameManager.startCell.GetComponent<CellControl>().persona.GetComponent<PlayerControl>().Attack(persona.GetComponent<EnemyAI>());
                GameManager.CloseAttackRange();
            }
            else if(GameManager.selected.GetComponent<PlayerControl>().status == 3)
            {
                GameManager.CloseAttackRange();
                if(persona!=null)
                {
                    GameManager.target = gameObject;
                    GameManager.dischargeMagic();
                    GameManager.CloseAttackRange();
                }
            }
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

    public List<GameObject>GetNeighbourAttack()
    {

        List<GameObject> neightbours = new List<GameObject>();
        Vector2 rayPoint = new Vector2(transform.position.x,transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(rayPoint + Vector2.up,Vector2.up);
        if(hit.collider !=null && (hit.collider.CompareTag("Cell")||hit.collider.CompareTag("Block")))
        {
            neightbours.Add(hit.collider.gameObject);
        }

        hit = Physics2D.Raycast(rayPoint + Vector2.right,Vector2.right);
        if(hit.collider !=null && (hit.collider.CompareTag("Cell")||hit.collider.CompareTag("Block")))
        {
            neightbours.Add(hit.collider.gameObject);
        }

        hit = Physics2D.Raycast(rayPoint + Vector2.left,Vector2.left);
        if(hit.collider !=null && (hit.collider.CompareTag("Cell")||hit.collider.CompareTag("Block")))
        {
            neightbours.Add(hit.collider.gameObject);
        }

        hit = Physics2D.Raycast(rayPoint + Vector2.down,Vector2.down);
        if(hit.collider !=null && (hit.collider.CompareTag("Cell")||hit.collider.CompareTag("Block")))
        {
            neightbours.Add(hit.collider.gameObject);
        }

        return neightbours;
    }

    public void CalculateCost()
    {
        gCost = Mathf.Abs(transform.position.x-pathManager.StartNode.transform.position.x)+Mathf.Abs(transform.position.y-pathManager.StartNode.transform.position.y);
        hCost = Mathf.Abs(transform.position.x-pathManager.EndNode.transform.position.y)+Mathf.Abs(transform.position.y-pathManager.EndNode.transform.position.y);
        fCost = Mathf.RoundToInt(gCost + hCost);
    }
}
