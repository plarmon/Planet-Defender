using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private int raiseToHash;

    public GravityAttractor nextPlanet;

    public GameObject trigger;

    // Start is called before the first frame update
    void Start()
    {
        raiseToHash = Animator.StringToHash("raise");
    }

    public void RaisePlatform()
    {
        gameObject.GetComponent<Animator>().SetTrigger(raiseToHash);
        trigger.SetActive(true);
    }

    // public void ChangePlanet(GameObject player)
    // {
    //     player.GetComponent<GravityBody>().planet.currentWorld = false;
    //     player.GetComponent<GravityBody>().planet = nextPlanet;
    //     nextPlanet.currentWorld = true;
    // }
}
