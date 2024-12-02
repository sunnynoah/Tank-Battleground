using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    GameObject powerupSpawner;

    public GameObject display;
    public string[] powerups;

    private void Start()
    {
        powerupSpawner = GameObject.Find("PowerupSpawner");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tank"))
        {
            Destroy(gameObject);

            powerupSpawner.GetComponent<PowerupSpawner>().PowerupActive = false;

            TankMovement tankMovement = collision.gameObject.GetComponent<TankMovement>();
            string randomPowerup = powerups[Random.Range(0, powerups.Length)];
            tankMovement.powerup = randomPowerup;
            tankMovement.powerupTime = 8;

            GameObject displayText = Instantiate(display, new Vector3(transform.position.x, 5, transform.position.z), Quaternion.identity);

            displayText.GetComponent<TextMeshPro>().text = randomPowerup;

            Destroy(displayText, 3);
        }
    }
}
