using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelect : MonoBehaviour
{
    public int KeyNum = 0;
    public bool isSelect =false;
    public GameObject locks;
    public GameObject opens;
    public int i = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("WholeKeyNum", i);
        locks.SetActive(true);
        opens.SetActive(false);
            //������������
        if (PlayerPrefs.GetInt("WholeKeyNum") >= KeyNum)
        {
            isSelect = true;
        }
        if (isSelect)
        {
            locks.SetActive(false);
            opens.SetActive(true);
        }
    }
    public void SwitchEventScene()
    {
        SceneManager.LoadScene("EventMap");
        i++;
        PlayerPrefs.SetInt("WholeKeyNum", i);
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
