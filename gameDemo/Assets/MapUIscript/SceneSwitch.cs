using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadScene("BigMap");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level02");
    }
    public void Level3()
    {
        SceneManager.LoadScene("Level03");
    }
    public void ReturnStartMenu()
    {
        SceneManager.LoadScene("Start");
    }
    public void OverGame()
    {
        SceneManager.LoadScene("Over");
    }
}
