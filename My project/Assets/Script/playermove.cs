using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove : MonoBehaviour
{
    public Rigidbody2D fox;
    public float speed;
    public float jumpforce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement();
    }
    void movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float direction = Input.GetAxisRaw("Horizontal");
        if(horizontalmove != 0)
        {
            fox.velocity = new Vector2(horizontalmove * speed * Time.deltaTime, fox.velocity.y);
        }
        if(direction != 0)
        {
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }
}
