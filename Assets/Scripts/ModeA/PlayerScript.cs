using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public bool jump = false;
	public float jumpForce = 800f;
	public float m_runningSpeed = 5.0f;
	public float runningSpeed {get {return m_runningSpeed;} set{m_runningSpeed = value;}}

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
		Status.NewGame();
	}

	void Update()
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		if(Input.GetButtonDown("Jump") && grounded)
		{
			jump = true;
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
			Food food = other.gameObject.GetComponent<Food>();
			Score.add(food.score);
		}
		if(other.gameObject.layer ==  LayerMask.NameToLayer("Enemy"))
		{
			Destroy(other.gameObject);
			Status.Damaged();
		}
		if(other.gameObject.layer ==  LayerMask.NameToLayer("Bomb"))
		{
			Destroy(other.gameObject);
			Status.Damaged();
		}
		if(other.gameObject.layer ==  LayerMask.NameToLayer("EndPoint"))
		{
			Status.FalledDie();
		}
	}

}
