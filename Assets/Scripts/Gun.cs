using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damageSemi = 10.0f;
    public float damageShotgun = 20.0f;
    public float damagePellet = 5.0f;
    public float range = 100f;

    public Camera fpsCam;

    public ParticleSystem muzzleFlashSemi;
    public ParticleSystem muzzleFlashShotgun;

    public GameObject gunImpactEffect;
    public GameObject groundImpactEffect;

    public AudioSource semiGunshotSound;
    public AudioSource shotgunShotSound;

    public Animator gunAnim;

    private int gunType = 0;

    private int shotToHash;
    private int swapToHash;
    private int gunTypeToHash;

    public float shotDelay;
    public float shotDelaySemi = 0.25f;
    public float shotDelayShotgun = 1.0f;
    public int pellets;
    public int shotgunBloom;

    private bool swapping;

    private FirstPersonController fps;

    void Start()
    {
        shotToHash = Animator.StringToHash("shot");
        swapToHash = Animator.StringToHash("swap");
        gunTypeToHash = Animator.StringToHash("GunType");
        shotDelay = shotDelaySemi;
        fps = gameObject.GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!fps.dead)
        {
            shotDelay += Time.deltaTime;
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!swapping)
                {
                    SwapGuns();
                }
            }
        }
    }

    void Shoot()
    {
        if (gunType == 0)
        {
            if (shotDelay >= shotDelaySemi)
            {
                semiGunshotSound.Play();
                shotDelay = 0;
                muzzleFlashSemi.Play();
                gunAnim.SetTrigger(shotToHash);
                RaycastHit hit;
                if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
                {
                    if (hit.transform.GetComponent<Enemy>())
                    {
                        Instantiate(gunImpactEffect, hit.point, Quaternion.Euler(hit.transform.position - transform.position));
                        hit.transform.GetComponent<Enemy>().TakeDamage(damageSemi, gameObject);
                    }
                    else if (hit.transform.gameObject.CompareTag("Spawner"))
                    {
                        Instantiate(gunImpactEffect, hit.point, Quaternion.Euler(hit.transform.position - transform.position));
                        hit.transform.GetComponent<EnemySpawner>().TakeDamage(damageSemi, gameObject);
                    }
                }
            }
        } else if (gunType == 1)
        {
            if (shotDelay >= shotDelayShotgun)
            {
                shotgunShotSound.Play();
                shotDelay = 0;
                muzzleFlashShotgun.Play();
                gunAnim.SetTrigger(shotToHash);

                for (int i = 0; i < pellets; i++)
                {
                    Vector3 t_bloom = fpsCam.transform.position + fpsCam.transform.forward * range;
                    t_bloom += Random.Range(-shotgunBloom, shotgunBloom) * fpsCam.transform.up;
                    t_bloom += Random.Range(-shotgunBloom, shotgunBloom) * fpsCam.transform.right;
                    t_bloom -= fpsCam.transform.position;
                    t_bloom.Normalize();

                    RaycastHit hit;
                    if (Physics.Raycast(fpsCam.transform.position, t_bloom, out hit, range))
                    {
                        if (hit.transform.GetComponent<Enemy>())
                        {
                            Instantiate(gunImpactEffect, hit.point, Quaternion.Euler(hit.transform.position - transform.position));
                            hit.transform.GetComponent<Enemy>().TakeDamage(damagePellet, gameObject);
                        }
                        else if (hit.transform.gameObject.CompareTag("Spawner"))
                        {
                            Instantiate(gunImpactEffect, hit.point, Quaternion.Euler(hit.transform.position - transform.position));
                            hit.transform.GetComponent<EnemySpawner>().TakeDamage(damagePellet, gameObject);
                        }
                        else
                        {
                            Instantiate(groundImpactEffect, hit.point, Quaternion.Euler(hit.transform.position - transform.position));
                        }
                    }

                    // Still Single Shot
                    //RaycastHit hit;
                    //if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
                    //{
                    //    if (hit.transform.GetComponent<Enemy>())
                    //    {
                    //        Instantiate(gunImpactEffect, hit.point, Quaternion.Euler(hit.transform.position - transform.position));
                    //        hit.transform.GetComponent<Enemy>().TakeDamage(damageShotgun, gameObject);
                    //    }
                    //    else if (hit.transform.gameObject.CompareTag("Spawner"))
                    //    {
                    //        Instantiate(gunImpactEffect, hit.point, Quaternion.Euler(hit.transform.position - transform.position));
                    //        hit.transform.GetComponent<EnemySpawner>().TakeDamage(damageShotgun, gameObject);
                    //    }
                    //    else
                    //    {
                    //        Instantiate(groundImpactEffect, hit.point, Quaternion.Euler(hit.transform.position - transform.position));
                    //    }
                    //}
                }
            }
        }
    }

    private void SwapGuns()
    {
        swapping = true;
        gunType = (gunType == 0) ? 1 : 0;
        gunAnim.SetInteger(gunTypeToHash, gunType);
        gunAnim.SetTrigger(swapToHash);
    }

    public void endSwap()
    {
        swapping = false;
    }
}
