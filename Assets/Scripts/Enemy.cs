using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float health = 50.0f;
    GravityEnemyBody geb;

    public GameObject deathExplosion;
    public GameObject bullet;
    private bool dying = false;

    private int bulletDelay = 3;

    private void Start()
    {
        geb = gameObject.GetComponent<GravityEnemyBody>();
    }

    private void Update()
    {
        if (geb.planet.spawnerCount <= 0)
        {
            if (!dying)
            {
                Die();
            }
        }
    }

    public void TakeDamage(float amount, GameObject target)
    {
        if (geb.planet.currentWorld)
        {
            health -= amount;
            geb.targetObject(target);
            if (health <= 0f)
            {
                if (!dying)
                {
                    Die();
                }
            }
        }
    }

    public void Attack()
    {
        StartCoroutine("shootLoop");
    }

    public void Die()
    {
        GameObject explosion = Instantiate(deathExplosion, transform);
        explosion.transform.Translate(new Vector3(1, -4, 0), Space.Self);
        dying = true;
        StartCoroutine("Death");
        geb.freeze = true;
        // gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    IEnumerator shootLoop()
    {
        while (!dying)
        {
            Debug.Log("bullet hit");
            //GameObject bulletInstance = Instantiate(bullet, transform.position + (transform.forward * 2), transform.localRotation);
            GameObject bulletInstance = Instantiate(bullet);
            bulletInstance.transform.position = transform.position + (transform.forward * 2);
            bulletInstance.transform.forward = transform.forward;
            //bulletInstance.transform.LookAt(geb.target.transform.position);
            //bulletInstance.transform.Rotate(Vector3.right, Space.Self);
            yield return new WaitForSeconds(bulletDelay);
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
