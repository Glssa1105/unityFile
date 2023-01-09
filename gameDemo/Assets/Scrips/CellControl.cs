using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellControl : MonoBehaviour
{
    [SerializeField] private GameManager GameManager;
    [SerializeField] private GameObject persona;
    public bool personaInsist;
    public GameObject moveCell;
    public GameObject attackCell;
    public bool Moveable;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            personaInsist = true;
            persona = other.gameObject;
        }    
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            personaInsist = false;
        }    
    }
    
    
    // Update is called once per frame
  
    private void OnMouseDown()
    {
        if(personaInsist&&persona.GetComponent<PlayerControl>().ableToMove)
        {
            GameManager.selected = persona;
            GameManager.ShowMoveRange();
        }
        Debug.Log("这是一个单元格,属性为" + this.tag+"位置为"+transform.position.x+","+transform.position.y);
        if(Moveable)
        {
            GameManager.selected.GetComponent<PlayerControl>().Move(transform.position.x,transform.position.y);
        }
    }
}
