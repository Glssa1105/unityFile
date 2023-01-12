using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField]private Text enemy_HP;
    [SerializeField]private Text enemy_MP;
    [SerializeField]private Text enemy_Name;
    [SerializeField]private EnemyAI enemy;
    // Start is called before the first frame update
    void Update()
    {
        CheckState();
    }
    public void CheckState()
    {
        enemy_Name.text = enemy.name;
        enemy_HP.text = enemy.blood.ToString(); 
        enemy_MP.text = enemy.Mp.ToString();  
    }
}
