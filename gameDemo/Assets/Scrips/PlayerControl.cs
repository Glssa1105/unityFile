using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]private GameManager GameManager;
    [SerializeField]private PathManager pathManager;
    public float waitTime;
    public int moveRange;
    public float speed;
    public Vector2 PaTarget;
    public bool ableToMove;
    private bool Moving;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
   
    void Update()
    {
        if(Moving)
        {
           Running(PaTarget);
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("这是一个玩家,属性为" + this.tag+"位置为"+transform.position.x+","+transform.position.y);
    }

    
    public void Move(List<GameObject> Path)
    {
        int times = Path.Count;
        Debug.Log(times);
        ableToMove = false;
        StartCoroutine(counter(waitTime,times,Path));        
    }

    private void Running(Vector2 target)
    {
        transform.position = Vector3.MoveTowards(transform.position,target,speed*Time.deltaTime);
    }

    IEnumerator counter(float waitTime,int times,List<GameObject> target)
    {
        Moving = true;
        for(int i=times-1;i>=0;i--)
        {
            yield return new WaitForSeconds(waitTime);
            PaTarget= new Vector2(target[i].transform.position.x,target[i].transform.position.y);
            //gameObject.transform.position = target[i].transform.position;
            //Running(new Vector2(target[i].transform.position.x,target[i].transform.position.y));
            PathDisplayer pathDisplayer = target[i].transform.Find("PathDisplayer").GetComponent<PathDisplayer>();
            pathDisplayer.CancelNode();
        }
        yield return new WaitForSeconds(waitTime);
        Moving = false;
        Debug.Log("Moving");
        GameManager.CloseMoveRange();
        ableToMove =true;
    }

}
