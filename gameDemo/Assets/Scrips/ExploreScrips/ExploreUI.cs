using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ExploreUI : MonoBehaviour
{
    [SerializeField]private GameObject FightScene;
    [SerializeField]private GameObject EventScene; 
    
    public Text Player_1_HP;
    public Text Player_1_MP;
    public Text Player_2_HP;
    public Text Player_2_MP;
    public Text Player_3_HP;
    public Text Player_3_MP;
    public Text Player_4_HP;
    public Text Player_4_MP;
    public int SceneNumber;
    [SerializeField]private GameMessage gameMessage;
    [SerializeField]private ExploreManager exploreManager;
    // Start is called before the first frame update
    void Start()
    {
        exploreManager=GameObject.Find("ExploreManager").GetComponent<ExploreManager>();
        gameMessage = GameObject.Find("GameMessage").GetComponent<GameMessage>();
    }

    // Update is called once per frame
    void Update()
    {
        Player_1_HP.text = gameMessage.Player_1_Hp.ToString() + "/"+gameMessage.Player_1_MaxHp.ToString();
        Player_1_MP.text = gameMessage.Player_1_Mp.ToString()+ "/"+gameMessage.Player_1_MaxMp.ToString();;
        Player_2_HP.text = gameMessage.Player_2_Hp.ToString()+ "/"+gameMessage.Player_2_MaxHp.ToString();;
        Player_2_MP.text = gameMessage.Player_3_Mp.ToString()+ "/"+gameMessage.Player_2_MaxMp.ToString();;
        Player_3_HP.text = gameMessage.Player_3_Hp.ToString()+ "/"+gameMessage.Player_3_MaxHp.ToString();;
        Player_3_MP.text = gameMessage.Player_3_Mp.ToString()+ "/"+gameMessage.Player_3_MaxMp.ToString();;
        Player_4_HP.text = gameMessage.Player_4_Hp.ToString()+ "/"+gameMessage.Player_4_MaxHp.ToString();;
        Player_4_MP.text = gameMessage.Player_4_Mp.ToString()+ "/"+gameMessage.Player_4_MaxMp.ToString();;
    }
    public void ShowFightScene()
    {
        FightScene.SetActive(true);
    }

    public void CloseFightScene()
    {
        FightScene.SetActive(false);
    }

    public void ShowEventScene()
    {
        EventScene.SetActive(true);
    }

    public void CloseEventScene()
    {
        EventScene.SetActive(false);
        exploreManager.Moveable = true;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneNumber);
    }

}
