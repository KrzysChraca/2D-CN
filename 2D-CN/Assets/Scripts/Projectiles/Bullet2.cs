using UnityEngine;
using System.Collections;

public class Bullet2 : Projectile
{

    void Awake()
    {
        damage = 20;
        deathTimer = 1;
        speed = 55;
        fireRate = 0.1f;
        StartCoroutine(DeathTimer());
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += (Time.deltaTime * speed) * moveDir;
    }
}
