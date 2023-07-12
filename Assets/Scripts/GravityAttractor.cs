using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float gravity = -10f;

    public int spawnerCount;

    public Teleporter worldTeleporter;
    public GravityAttractor nextWorld;

    public GameObject[] spawners;
    public List<Enemy> enemies;

    public bool currentWorld;

    public void Attract(Transform body)
    {
        Vector3 finalTargetDir = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;
        Vector3 targetDir = Vector3.Lerp(bodyUp, finalTargetDir, 0.2f);

        body.rotation = Quaternion.FromToRotation(bodyUp, targetDir)* body.rotation;
    }

    public void AddGravity(Transform body)
    {
        Vector3 targetDir = (body.position - transform.position).normalized;

        body.gameObject.GetComponent<Rigidbody>().AddForce(targetDir * gravity);
    }

    public void RemoveSpawner()
    {
        spawnerCount -= 1;
        if (spawnerCount <= 0)
        {
            worldTeleporter.RaisePlatform();
        }
    }

    public void StartWorld()
    {
        foreach(GameObject obj in spawners)
        {
            obj.GetComponent<EnemySpawner>().StartSpawning();
            currentWorld = true;
        }
    }
}
