using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingMenu;
    public AudioMixer AudioMixer;
    [SerializeField] private bool isShow;
    public void Startgame()
    {
        PlayerPrefs.SetInt("INum", 0);
        SceneManager.LoadScene("MainMap");
    }
    // Start is called before the first frame update
    public void Quitgame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        isShow = false;
        Time.timeScale = 1f;
    }
    public void SetVolume(float value)
    {
        AudioMixer.SetFloat("MainVolume", value);

    }
    void Start()
    {
        isShow = false;
    }

    // Update is called once per frame
    void Update()
    {
        SettingsMenu();
    }
    //按ESC打开菜单
    public void SettingsMenu()
    {
        if (isShow)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SettingMenu.SetActive(true);
                isShow = false;
                Time.timeScale = 0f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingMenu.SetActive(false);
            isShow = true;
            Time.timeScale = 1f;
        }
    }


}
