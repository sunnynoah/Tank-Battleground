using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawn : MonoBehaviour
{
    public GameObject missImpactParticles;
    public GameObject hitImpactParticles;

    float lifeTime = 0;
    float maxLifeTime = 5;

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;

        if (lifeTime > maxLifeTime)
        {
            DestroyBullet(null);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestroyBullet(collision.gameObject.tag);
    }

    void DestroyBullet(string tag)
    {
        GameObject particle = missImpactParticles;
        if (tag == "Tank")
        {
            particle = hitImpactParticles;
        }
        GameObject impactPars = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(impactPars, 2);

        Destroy(gameObject);
    }
}
