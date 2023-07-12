using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameTrigger : MonoBehaviour
{
    public Teleporter teleporter;
    public GameManager gm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<GravityBody>().ChangePlanet(teleporter.nextPlanet);
            other.gameObject.GetComponent<FirstPersonController>().health = other.gameObject.GetComponent<FirstPersonController>().maxHealth;

            gm.SceneTransition("WinGame");
        }
    }
}
