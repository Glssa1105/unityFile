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
    //�л����¼���ͼ
    public void SwitchEventScene()
    {
        SceneManager.LoadScene("EventMap");
    }
    //�л���ս����ͼ
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
