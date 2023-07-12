using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 20.0f;
    private int damage = 5;

    private void Start()
    {
        StartCoroutine("TooLong");
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<FirstPersonController>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    IEnumerator TooLong()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
