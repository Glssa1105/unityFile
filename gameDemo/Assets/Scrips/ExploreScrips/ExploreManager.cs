using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreManager : MonoBehaviour
{
    //此处从GameMessage中读取BeginNode与Player的各种状态
    [SerializeField]private GameMessage gameMessage;
    [SerializeField]private ExploreUI exploreUI;
    [SerializeField]private GameObject player;
    [SerializeField]private float Speed;
    [SerializeField]GameObject[] StageList;
    public float waittime;
    public GameObject BeginNode;
    public GameObject selected;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public Camera MainCamera;
    public bool Moveable;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        StageList = GameObject.FindGameObjectsWithTag("Stage");
        gameMessage = GameObject.Find("GameMessage").GetComponent<GameMessage>();
    }
    void Start()
    {
        exploreUI = GameObject.Find("UIManager").GetComponent<ExploreUI>();
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.Find("Player");
        foreach(var item in StageList)
        {
            if(item.GetComponent<StageScrip>().stageNumber == gameMessage.stageNum)
            {
                player.transform.position = item.transform.position;
                BeginNode = item;
                break;
            }
        }
        // player.transform.position = BeginNode.transform.position;
        if(gameMessage.Player1Insist)
        player1.SetActive(true);
        if(gameMessage.Player2Insist)
        player2.SetActive(true);
        if(gameMessage.Player3Insist)
        player3.SetActive(true);
        if(gameMessage.Player4Insist)
        player4.SetActive(true);
    }
    void Update()
    {
        MainCamera.transform.position = player.transform.position;
        if(!Moveable)
        {
            Move();
        }
    }

    public void Run()
    {
        StartCoroutine(counter(waittime));
    }
    private void Move()
    {
        player.transform.position = Vector2.MoveTowards(player.transform.position,selected.transform.position,Speed*Time.deltaTime);
        gameMessage.stageNum = selected.GetComponent<StageScrip>().stageNumber;
    }

    IEnumerator counter(float waitTime)
    {
        Moveable =false;
        Debug.Log("在动");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("结束");
        BeginNode = selected;
        if(selected.GetComponent<StageScrip>().stageStatus!=0)
        {
            exploreUI.ShowFightScene();
        }
        else
        {
            exploreUI.ShowEventScene();
        }
    }
}
