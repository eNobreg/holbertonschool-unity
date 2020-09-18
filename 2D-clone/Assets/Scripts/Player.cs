using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public ParticleSystem dust;

	[Header("Horizonttal Movement")]
	public float moveSpeed = 10f;
	public Vector2 direction;
	private bool facingRight = true;


	[Header("Components")]
	public Rigidbody2D rb;
    public GameObject cam;
	public LayerMask groundLayer;
	public GameObject character;
    public GameManager manager;

    public Animator animator;


	[Header("Vertical Movement")]
	public float jumpSpeed = 15f;
    public float launchSpeed = 20f;
	public float jumpDelay = 0.25f;
	private float jumpTimer;
	private float wasGrounded = 0.2f;
	private float wasGroundedTime = 0.2f;


	[Header("Physics")]
	public float maxSpeed = 7f;
	public float linearDrag = 4f;
	public float gravity = 1f;
	public float fallMultiplier = 5f;


	[Header("Collision")]
    public float Double = 1;
	public bool onGround = false;
	public float groundLength = 0.6f;
	public Vector3 colliderOffset;

    public float xSqeeze1 = 0.5f;
    public float ySqueeze1 = 1.2f;
    public float xSqeeze2 = 1.25f;
    public float ySqueeze2 = 0.8f;

    void Update()
    {
        float saved_double = Double;
		bool wasOnGround = onGround;

		onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);
		wasGrounded -= Time.deltaTime;
		if (onGround)
		{
			wasGrounded = wasGroundedTime;
		}
		if (!wasOnGround && onGround)
		{
			StartCoroutine(jumpSqueeze(xSqeeze2, ySqueeze2, 0.08f));
            if (saved_double > 0)
                Double = 1;
		}

		if (Input.GetButtonDown("Jump"))
		{
			jumpTimer = Time.time + jumpDelay;
		}
			

        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        
        
    }
	void FixedUpdate()
	{
		moveCharacter(direction.x);	
        animator.SetFloat("Speed X", Mathf.Abs(direction.x));
        animator.SetFloat("Speed Y", rb.velocity.y);

        if (rb.velocity.y < 0)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", true);
        }
        else
            animator.SetBool("Falling", false);
        

		if (jumpTimer > Time.time && wasGrounded > 0)
        {
			Jump();
        }
		modifyPhysics();
	}
	void moveCharacter(float horizontal)
	{
		rb.AddForce(Vector2.right * horizontal * moveSpeed);

		if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
		{
			Flip();
		}

		if (Mathf.Abs(rb.velocity.x) > maxSpeed)
		{
			rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
		}
	}

	void modifyPhysics()
	{
		bool changingDirection = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

		if (onGround) {
			if (Mathf.Abs(direction.x) < 0.4f || changingDirection)
				rb.drag = linearDrag;
			else
				rb.drag = 0f;
			rb.gravityScale = 0;
		}
		else
		{
			rb.gravityScale = gravity;
			rb.drag = linearDrag * 0.15f;
			if (rb.velocity.y < 0)
			{
				rb.gravityScale = gravity * fallMultiplier;
			}
			else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
			{
				rb.gravityScale = gravity * (fallMultiplier / 2);
			}
		}
	}
	void Jump()
	{
		rb.velocity = new Vector3(rb.velocity.x, 0);
		rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        animator.SetBool("Jumping", true);
		jumpTimer = 0;
		StartCoroutine(jumpSqueeze(xSqeeze1, ySqueeze1, 0.1f));
		CreateDust();
	}
	void Flip()
	{
		if (onGround)
			CreateDust();
		facingRight = !facingRight;
		transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
	}

	/// <summary>
	/// Callback to draw gizmos that are pickable and always drawn.
	/// </summary>
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
		Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
	}

	IEnumerator jumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
	{
		Vector3 originalSize = Vector3.one;
		Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
		float t = 0f;
		while (t <= 1.0)
		{
			t += Time.deltaTime / seconds;
			character.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
			yield return null;
		}
		t = 0f;
		while (t <= 1.0)
		{
			t += Time.deltaTime / seconds;
			character.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
			yield return null;
		}

	}

	void CreateDust()
	{
		dust.Play();
	}

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("StartTrigger"))
        {
            cam.gameObject.GetComponent<CameraMove>().enabled = true;
            LevelGeneration.doGeneration = true;
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("JumpPad"))
		{
			if (rb.velocity.y < 0)
				rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * launchSpeed, ForceMode2D.Impulse);
		}
		
		if (other.gameObject.CompareTag("DeathTrigger"))
        {
            manager.restartGame();
        }
    }
}
