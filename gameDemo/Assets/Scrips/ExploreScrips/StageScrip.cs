using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScrip : MonoBehaviour
{
    public List<GameObject> MotherList;
    public int stageNumber;

    public int stageStatus;//0为探索节点 非0为为战斗节点 数字对应加载场景
    [SerializeField]private ExploreManager exploreManager;
    [SerializeField]private ExploreUI exploreUI;
 
    void Start()
    {
        exploreUI = GameObject.Find("UIManager").GetComponent<ExploreUI>();
        exploreManager = GameObject.Find("ExploreManager").GetComponent<ExploreManager>();
    }
    void OnMouseDown()
    {
        Debug.Log("点了" + gameObject.name);
        if(exploreManager.Moveable)
        {
            exploreManager.selected = gameObject;
            foreach(var item in MotherList)
            {
                Debug.Log("开搜");
                if(item == exploreManager.BeginNode)
                {
                    Debug.Log("niu");
                    exploreManager.Run();
                    exploreUI.SceneNumber = stageStatus;
                    break;
                }
            }
        }
    }
}
