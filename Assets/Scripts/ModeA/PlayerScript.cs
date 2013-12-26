using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public bool jump = false;
	public float jumpForce = 800f;
	public float m_runningSpeed = 5.0f;
	public float runningSpeed {get {return m_runningSpeed;} set{m_runningSpeed = value;}}

	private GameMode newGame;
	private Transform groundCheck;
	private Animator anim;
	private bool grounded = false;

	void Awake()
	{
		groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator>();
	}
	void Start()
	{
		newGame = GameObject.Find("GameMode").GetComponent<GameMode>();
	}

	void Update()
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		if(Input.GetButtonDown("Jump") && grounded)
		{
			jump = true;
		}
		if(transform.position.y < -2) {
			newGame.SendMessage("FalledDie");
		}
	}

	void FixedUpdate()
	{

		if(jump)
		{
			anim.SetTrigger("Jump");
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
		else
		{
			anim.SetBool("Jump", false);
		}
		rigidbody2D.velocity = new Vector2(runningSpeed,  rigidbody2D.velocity.y);

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.layer ==  LayerMask.NameToLayer("Food"))
		{
			Destroy(other.gameObject);
		}
		if(other.gameObject.layer ==  LayerMask.NameToLayer("Enemy"))
		{
			Destroy(other.gameObject);
			newGame.SendMessage("Damaged");
		}
		if(other.gameObject.layer ==  LayerMask.NameToLayer("Bomb"))
		{
			Destroy(other.gameObject);
			newGame.SendMessage("Damaged");
		}
	}

}
