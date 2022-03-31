using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour
{
    private bool disabled = false;
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    public GameObject[] bullets;

    public Transform[] LaunchOffset;

    private Rigidbody2D _rigidbody;

    public WeaponScript weapons;

    public float health = 3;
    private bool grounded;

    public float dashSpeed = 50;
    private float dashTime;
    public float startDashTime = 0.1f;
    private int direction;



    private float teleportDistance = 2;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (!disabled)
        {
            UpdateMovement();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Quaternion projectileEuler;
            if (transform.rotation.eulerAngles.y > 0)
            {
                projectileEuler = Quaternion.Euler(0, 0, -90);
            }
            else
            {
                projectileEuler = Quaternion.Euler(0, 0, 90);
            }
            Instantiate(bullets[weapons.currentWeaponIndex], LaunchOffset[weapons.currentWeaponIndex].position, projectileEuler);
        }

        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.Q) && horizontalInput < -0.01f)
            {
                direction = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Q) && horizontalInput > 0.01f)
            {
                direction = 2;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                _rigidbody.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (direction == 1)
                {
                    _rigidbody.velocity = Vector2.left * dashSpeed;
                }
                else if (direction == 2)
                {
                    _rigidbody.velocity = Vector2.right * dashSpeed;
                }
            }
        }
    }


    private void UpdateMovement()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        if (!Mathf.Approximately(0, movement))
        {
            transform.rotation = movement > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }

        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;

        if (collision.gameObject.tag == "Enemy")
        {
            health -= 1;
        }
    }




}
