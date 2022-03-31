using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour
{
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    public GameObject[] bullets;

    public Transform[] LaunchOffset;

    private Rigidbody2D _rigidbody;

    public WeaponScript weapons;

    public float health = 3;
    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.paused){
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        if(!Mathf.Approximately(0, movement)){
            transform.rotation = movement > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }

        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f){
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }

        if(Input.GetButtonDown("Fire1")){
            Quaternion projectileEuler;
            if (transform.rotation.eulerAngles.y > 0){
                projectileEuler = Quaternion.Euler(0, 0, -90);
            } else{
                projectileEuler = Quaternion.Euler(0, 0, 90);
            }
            Instantiate(bullets[weapons.currentWeaponIndex], LaunchOffset[weapons.currentWeaponIndex].position, projectileEuler);
        }

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
