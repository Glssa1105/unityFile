using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        //初始化颜色
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color32(255, 255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //鼠标触碰到单元格发生颜色变化以及大小缩放
    private void OnMouseEnter()
    {
        spriteRenderer.color = new Color32(255, 153, 153, 255);
        transform.localScale += Vector3.one * 0.04f;

    }
    //鼠标移开时颜色恢复大小恢复
    private void OnMouseExit()
    {
        spriteRenderer.color = new Color32(255, 255, 255, 255);
        transform.localScale -= Vector3.one * 0.04f;
    }
}
