using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    const float spawnTime = 30;

    float currentTime = 20;

    public GameObject powerupPrefab;

    public bool PowerupActive = false;
    void Update()
    {
        if (!PowerupActive)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= spawnTime)
            {
                PowerupActive = true;
                currentTime = 0;

                Instantiate(powerupPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
