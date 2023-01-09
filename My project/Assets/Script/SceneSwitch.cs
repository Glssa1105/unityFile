using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //切换到事件地图
    public void SwitchEventScene()
    {
        SceneManager.LoadScene("EventMap");
    }
    //切换到战斗地图
    public void SwitchFightingScene()
    {
        SceneManager.LoadScene("FightingMap");
    }
    public void SwitchEventToMainScene()
    {
        SceneManager.LoadScene("MainMap");
    }
    public void SwitchFightingToMainScene()
    {
        SceneManager.LoadScene("MainMap");
    }
}
