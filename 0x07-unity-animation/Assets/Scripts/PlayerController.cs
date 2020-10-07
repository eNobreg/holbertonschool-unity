using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Speeds")]
    public float speed = 10f;
    public float jumpForce = 15f;
    public float maxSpeed = 20f;
    public Rigidbody rb;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;


    [Header("MovePosition")]
    private float moveHorizontal;
    private float moveVertical;
    private Vector3 respawnPos;
    private Vector3 camRespawnPos;


    [Header("Physics")]
    public float linearDrag = 5;
    public float jumpMod = 1.1f;
    public bool onGround = true;
    public bool useGravity = true;
    public float fallMultiplier = 2.5f;
    public float lowJump = 2.0f;


    [Header("Components")]
    public LayerMask ground;
    public float groundLength;
    public float sphereRadius = 0.5f;
    public float groundOffset;
    public Transform cam;

    public Animator player_anim;


    [Header("Timers")]
    private float jumpTimer;
    public float jumpTimerTime = 0.2f;
    public float groundedTimerTime = 0.2f;
    private float groundedTimer = 0.2f;
    private Vector3 zOffset;
    private Vector3 xOffset;
    // Update is called once per frame
    void Start()
    {
        respawnPos = transform.position;
        respawnPos.y += 30f;
        camRespawnPos = cam.position;
        camRespawnPos.y += 30f;
    }
    void Update()
    {
        zOffset = new Vector3(0, 0, groundOffset);
        xOffset = new Vector3(groundOffset, 0, 0);
        Vector3 sphereOffset = new Vector3(0, groundLength, 0);
        //onGround = Physics.Raycast(transform.position, Vector3.down, groundLength, ground) || 
        //           Physics.Raycast(transform.position - zOffset, Vector3.down, groundLength, ground) || 
        //           Physics.Raycast(transform.position + zOffset, Vector3.down, groundLength, ground) ||
        //           Physics.Raycast(transform.position - xOffset, Vector3.down, groundLength, ground) ||
        //           Physics.Raycast(transform.position + xOffset, Vector3.down, groundLength, ground);
        onGround = Physics.OverlapSphere(transform.position - sphereOffset, sphereRadius, ground).Length > 0 ? true: false;
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        groundedTimer -= Time.deltaTime;
        if (onGround)
        {
            player_anim.SetBool("isFalling", false);
            player_anim.SetBool("isJumping", false);
            groundedTimer = groundedTimerTime;
        }
    
        if (Input.GetButtonDown("Jump"))
            jumpTimer += Time.time + jumpTimerTime;
    }
    void FixedUpdate()
    {
        Vector3 direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (direction.magnitude >= 0.1f)
        {
            linearDrag = 0.75f;
            player_anim.SetBool("isRunning", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            moveCharacter(moveDir.normalized);
        }
        else if (rb.velocity.y == 0f)
        {
            rb.drag = 10;
            player_anim.SetBool("isRunning", false);
        }

        if (jumpTimer > Time.time && groundedTimer > 0)
            Jump();

        if (rb.velocity.y < 0)
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJump - 1) * Time.deltaTime;
        }
    }

    void moveCharacter(Vector3 move)
    {
        rb.drag = linearDrag;
        if (onGround)
            rb.AddForce(move * speed, ForceMode.Acceleration);
        else
        {
            rb.drag = linearDrag * 0.55f;
            float jumpMove = speed * jumpMod;
            rb.AddForce(move * jumpMove, ForceMode.Force);
        }
    }
    void Jump()
    {
        rb.drag = linearDrag;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpTimer = 0;
        player_anim.SetBool("isJumping", true);
    }

    void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position + zOffset, transform.position + zOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - zOffset, transform.position - zOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position + xOffset, transform.position + xOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - xOffset, transform.position - xOffset + Vector3.down * groundLength);
        Gizmos.DrawWireSphere(transform.position - new Vector3 (0, groundLength, 0), sphereRadius);
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fall"))
        {
            player_anim.SetBool("isFalling", true);
            transform.position = respawnPos;
            cam.position = camRespawnPos;
            rb.velocity = new Vector3(0.0f, rb.velocity.y, 0.0f);
        }
    }
}
