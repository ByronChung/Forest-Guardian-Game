using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float startHealth;
    private float hp;

    public PlayerHealthBar playerHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        hp = startHealth;
        playerHealthBar.SetMaxHealth(startHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage){
        hp -= damage;

        if (hp <= 0f){
            Die();
        }

        playerHealthBar.SetHealth(hp);
    }

    void Die(){
        Destroy(gameObject);
    }
}
