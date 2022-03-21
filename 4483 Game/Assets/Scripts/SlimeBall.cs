using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    void Start()
    {
        //move bullet
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //deal damage
       if (collider.name != "GreenSlime")
        {
            Destroy(gameObject);
        }
    }
}
