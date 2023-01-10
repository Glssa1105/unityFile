using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private bool canWalk;
    public LayerMask oblayermask;
    [SerializeField] private Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
        int randomNum = Random.Range(0,sprites.Length);
        spriteRenderer.sprite = sprites[randomNum];
        CheckObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        CheckObstacle();
    }
    private void OnMouseEnter()
    {
        transform.localScale += Vector3.one * 0.06f;
        spriteRenderer.sortingOrder = 25;
    }
    private void OnMouseExit()
    {
        transform.localScale -= Vector3.one * 0.06f;
        spriteRenderer.sortingOrder = 0;
    }
    private void CheckObstacle()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position,spriteRenderer.bounds.extents.x,oblayermask);
        if(collider != null)
        {
            canWalk = false;
        }
        else
        {
            canWalk = true;
        }
    }
}
