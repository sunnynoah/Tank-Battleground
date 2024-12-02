using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float health = 100;

    public GameObject deathPars;

    public UIManager uiManager;
    float invulnerable = 0;

    private void Update()
    {
        if (invulnerable > 0)
        {
            invulnerable -= Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        if (invulnerable <= 0)
        {
            invulnerable = 0.1f;
            health -= damage;
            if (health <= 0)
            {
                if (this.name == "P1 Tank")
                {
                    uiManager.EndGame("Player 2");
                }
                else
                {
                    uiManager.EndGame("Player 1");
                }
                Instantiate(deathPars, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(20);
        }
    }
}
