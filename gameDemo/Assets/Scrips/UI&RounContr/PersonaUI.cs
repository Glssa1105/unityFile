using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonaUI : MonoBehaviour
{
    [SerializeField]private Text player1_HP;
    [SerializeField]private Text player1_MP;
    [SerializeField]private Text player1_stamina;
    [SerializeField]private Text player1_Name;
    [SerializeField]private PlayerControl player1;
    // Start is called before the first frame update
  
    // Update is called once per frame
    void Update()
    {
        CheckState();
    }
    public void CheckState()
    {
        player1_Name.text = player1.name;
        player1_stamina.text = player1.stamina.ToString() + " / " + player1.Maxstamina.ToString(); 
        player1_HP.text = player1.blood.ToString()+ " / " + player1.maxBlood.ToString(); 
        player1_MP.text = player1.Mp.ToString()+ " / " + player1.MaxMP.ToString();  
    }
}
