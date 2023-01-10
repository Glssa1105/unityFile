using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]private GameManager GameManager;
    public Vector2 target;
    public float waitTime;
    public int moveRange;
    public float speed;
    public bool ableToMove;
    // Start is called before the first frame update
    void Start()
    {
        ableToMove = true;
        waitTime = 3;
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
   
    void Update()
    {
        if(!ableToMove)
        {
            Running(target);
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("这是一个玩家,属性为" + this.tag+"位置为"+transform.position.x+","+transform.position.y);
    }
    
    public void Move(float x, float y)
    {
    //    transform.position = new Vector2(x,y);
        target = new Vector2(x,y);
        ableToMove = false;
        GameManager.CloseMoveRange();
        StartCoroutine(counter(waitTime));
    }

    private void Running(Vector2 target)
    {
        transform.position = Vector3.MoveTowards(transform.position,target,2*Time.deltaTime);
    }

    IEnumerator counter(float time)
    {
        yield return new WaitForSeconds(time);
        ableToMove =true;
    }

}
