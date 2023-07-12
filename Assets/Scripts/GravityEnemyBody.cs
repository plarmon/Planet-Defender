using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityEnemyBody : MonoBehaviour
{
    public GravityAttractor planet;
    public Transform visionStart;
    public float moveSpeed = 0.15f;

    private Rigidbody rb;
    public GameObject target;
    public bool searching = true;
    public float maxVelocity = 5.0f;
    public float force = 5.0f;

    public bool freeze = false;

    private void Awake()
    {
        if (planet == null)
        {
            planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        }
        rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        if (searching)
        {
            for (float i = 1; i >= -1; i -= 0.2f)
            {
                Vector3 direction = transform.forward + (transform.right * i) + (transform.up * -0.8f);
                
                Ray visionNode = new Ray(visionStart.position, direction * 20);
                RaycastHit hit;
                if (Physics.Raycast(visionNode, out hit, 20))
                {
                    if (hit.transform.gameObject.CompareTag("Player"))
                    {
                        i = -1.1f;
                        targetObject(hit.transform.gameObject);
                    }
                }
            }
        }
        else
        {
            transform.LookAt(target.transform);
        }
    }

    private void FixedUpdate()
    {
        if (!freeze)
        {
            if (searching)
            {
                planet.Attract(transform);
            }
            transform.Translate(Vector3.forward * moveSpeed * Time.fixedDeltaTime);
        } else
        {
            planet.Attract(transform);
        }
    }

    public void targetObject(GameObject targetObj)
    {
        target = targetObj;
        transform.LookAt(target.transform);
        searching = false;
        gameObject.GetComponent<Enemy>().Attack();
    }




}
