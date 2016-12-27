using UnityEngine;
using System.Collections;

public class DefaultBullet : Projectile {


    // Use this for initialization
    void Awake()
    {
        damage = 5;
        deathTimer = 3;
        speed = 15;
        fireRate = 0.3f;
        StartCoroutine(DeathTimer());
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += (Time.deltaTime * speed) * moveDir;
    }
}
