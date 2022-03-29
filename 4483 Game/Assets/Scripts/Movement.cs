using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private bool grounded;
    public float teleportDistance = 10;

    public float health = 3;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        if (Input.GetKey(KeyCode.Q))
        {
            Teleport();
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;

        if (collision.gameObject.tag == "Enemy")
        {
            health -= 1;
        }
    }

    private void Teleport()
    {
        Player.transform.position = new Vector2(Player.transform.position.x + teleportDistance, Player.transform.position.y);
    }
}
