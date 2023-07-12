using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject deathExplosion;
    public GameObject[] rings;
    public Transform spawnPoint;
    public Material offRingMaterial;
    public GravityAttractor world;
    public int spawnInterval = 3;
    public float health = 200;
    private float maxHealth;
    private float ringCount;
    private float maxRings;

    private bool dying = false;


    // Start is called before the first frame update
    void Start()
    {
        maxRings = rings.Length;
        ringCount = maxRings;
        maxHealth = health;
    }

    public void TakeDamage(float amount, GameObject target)
    {
        if (world.currentWorld)
        {
            health -= amount;
            if (health / maxHealth < (ringCount - 1.0f) / maxRings)
            {
                if (ringCount != 0)
                {
                    ringCount -= 1;
                }
                rings[(int)ringCount].GetComponent<MeshRenderer>().material = offRingMaterial;
            }
            if (health <= 0f || ringCount == 0)
            {
                if (!dying)
                {
                    Die();
                }
            }
        }
    }

    public void StartSpawning()
    {
        StartCoroutine("SpawnEnemies");
    }

    private void Die()
    {
        GameObject explosion = Instantiate(deathExplosion, transform);
        explosion.transform.Translate(new Vector3(0, -2, 0), Space.Self);
        dying = true;
        StartCoroutine("Death");
        world.RemoveSpawner();
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            GameObject enemyInstance = Instantiate(enemy, spawnPoint.position, enemy.transform.rotation);
            world.enemies.Add(enemyInstance.GetComponent<Enemy>());
            enemyInstance.transform.Rotate(Vector3.forward, Random.Range(0, 360));
            enemyInstance.GetComponent<GravityEnemyBody>().planet = world;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
