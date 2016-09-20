using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public Vector3 moveDir;
    public int speed;
    public float deathTimer = 5;

	// Use this for initialization
	void Start () {
        StartCoroutine(DeathTimer());
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += (Time.deltaTime * speed) * moveDir;
	}

    public IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(deathTimer);
        Destroy(this.gameObject);
    }
}
