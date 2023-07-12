using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float mouseSensitivityX = 250f;
    public float mouseSensitivityY = 250f;
    public float walkSpeed = 8f;
    public float jumpForce = 10f;
    public LayerMask groundedMask;

    public int health;
    public int maxHealth = 200;

    Rigidbody rb;

    Transform cameraT;
    float verticalLookRotation;

    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    float smoothTime = 0.15f;

    bool grounded = true;
    bool moving = false;

    public bool onFloor = false;

    public Animator gunAnim;
    int movingToHash;

    public bool testing;

    public GameManager gm;

    public bool dead = false;

    public RectTransform healthBar;

    void Start()
    {
        movingToHash = Animator.StringToHash("moving");
        cameraT = Camera.main.transform;
        rb = gameObject.GetComponent<Rigidbody>();
        health = maxHealth;
    }

    void Update()
    {
        if (!dead)
        {
            healthBar.sizeDelta = new Vector2(((float)health / (float)maxHealth) * 100, 100);
            // Vector3 targetLookDir = Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivityX;
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivityX);
            verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivityY;
            verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
            cameraT.localEulerAngles = Vector3.left * verticalLookRotation;

            Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            Vector3 targetMoveAmount = moveDir * walkSpeed;
            moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, smoothTime);

            if (moveDir != Vector3.zero)
            {
                if (moving == false)
                {
                    moving = true;
                    gunAnim.SetBool(movingToHash, true);
                }
            }
            else
            {
                if (moving == true)
                {
                    moving = false;
                    gunAnim.SetBool(movingToHash, false);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (grounded)
                {
                    rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                }
            }

            grounded = false;
            onFloor = false;
            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask))
            {
                if (hit.transform.gameObject.CompareTag("Floor"))
                {
                    onFloor = true;
                    Vector3 bodyUp = transform.up;
                    Vector3 targetDir = Vector3.Lerp(bodyUp, hit.normal, 0.2f);
                    transform.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * transform.rotation;
                }
                grounded = true;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damage)
    {
        if (!testing)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        gm.SceneTransition("GameOver");
        dead = true;
        Debug.Log("Death");
    }
}
