using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCMenu : MonoBehaviour
{
    public GameObject escMenu;
    [SerializeField] bool isShow = true;
    [SerializeField] private AudioSource bgm;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isShow)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                escMenu.SetActive(true);
                isShow = false;
                Time.timeScale = 0f;
                bgm.Pause();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            escMenu.SetActive(false);
            isShow=true;
            Time.timeScale = 1f;
            bgm.Play();
        }
    }
    public void ReturnGame()
    {
        escMenu.SetActive(false);
        isShow = true;
        Time.timeScale = 1f;
        bgm.Play();
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene("Start");
        Time.timeScale = 1f;
    }
    public  void ExitGame()
    {
        Application.Quit();
        Debug.Log("exitgame!");
    }
    public void ReturnMap()
    {
        SceneManager.LoadScene("Map");
        Time.timeScale = 1f;
    }

}

