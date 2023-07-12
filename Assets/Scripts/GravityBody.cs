using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour
{
    [SerializeField] public GravityAttractor planet;

    private Rigidbody rb;

    private void Awake()
    {
        if (planet == null)
        {
            planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        }
        planet.StartWorld();
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate()
    {
        if (transform.gameObject.GetComponent<FirstPersonController>())
        {
            if (!transform.gameObject.GetComponent<FirstPersonController>().onFloor)
            {
                planet.Attract(transform);
            }
        }
        planet.AddGravity(transform);
    }

    public void ChangePlanet(GravityAttractor newPlanet)
    {
        planet.currentWorld = false;
        planet = newPlanet;
        planet.StartWorld();
    }

    


}
