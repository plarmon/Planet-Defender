using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporterTrigger : MonoBehaviour
{
    public Teleporter teleporter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<GravityBody>().ChangePlanet(teleporter.nextPlanet);
            other.gameObject.GetComponent<FirstPersonController>().health = other.gameObject.GetComponent<FirstPersonController>().maxHealth;
        }
    }
}
