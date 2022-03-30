using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character2DController : MonoBehaviour
{
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    public GameObject[] bullets;

    public Transform[] LaunchOffset;

    private Rigidbody2D _rigidbody;

    public WeaponScript weapons;

    public AudioClip[] gunSFX;

    public float health;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        if(!Mathf.Approximately(0, movement)){
            transform.rotation = movement > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }

        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f){
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }

        if(Input.GetButtonDown("Fire1")){
            // Update the ammo count for the rifle and shotgun
            if (weapons.currentWeaponIndex != 0){
                weapons.bulletCount[weapons.currentWeaponIndex-1] -= 1;
            }

            // Instantiate the bullet and have it travel in the x direction
            Quaternion projectileEuler;
            if (transform.rotation.eulerAngles.y > 0){
                projectileEuler = Quaternion.Euler(0, 0, -90);
            } else{
                projectileEuler = Quaternion.Euler(0, 0, 90);
            }

            // Load and play the correct bullet sound
            AudioSource gunShot = GetComponent<AudioSource>();
            if (weapons.currentWeaponIndex == 0){
                gunShot.clip = gunSFX[0];
                gunShot.Play();
            }
            else if (weapons.currentWeaponIndex == 1){
                gunShot.clip = gunSFX[1];
                gunShot.Play();
            }
            else if (weapons.currentWeaponIndex == 2){
                gunShot.clip = gunSFX[2];
                gunShot.Play();
            }
            Instantiate(bullets[weapons.currentWeaponIndex], LaunchOffset[weapons.currentWeaponIndex].position, projectileEuler);
        }
    }

    private void Die(){
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
    public void dealDamage(int damage){
        health -= damage;
        if (health <= 0f){
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "GreenSlime")
        {
            dealDamage(1);
        }
        if (collision.gameObject.name == "GoldSlime")
        {
            dealDamage(3);
        }
    }
}
