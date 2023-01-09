using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        //��ʼ����ɫ
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color32(255, 255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //��괥������Ԫ������ɫ�仯�Լ���С����
    private void OnMouseEnter()
    {
        spriteRenderer.color = new Color32(255, 153, 153, 255);
        transform.localScale += Vector3.one * 0.04f;

    }
    //����ƿ�ʱ��ɫ�ָ���С�ָ�
    private void OnMouseExit()
    {
        spriteRenderer.color = new Color32(255, 255, 255, 255);
        transform.localScale -= Vector3.one * 0.04f;
    }
}
