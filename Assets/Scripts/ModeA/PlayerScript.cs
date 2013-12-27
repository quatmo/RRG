using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public bool jump = false;
	public bool secondJump = false;
	public bool isSecondJumpActive = true;
	public float jumpForce = 800f;
	public float m_runningSpeed = 5.0f;
	public float runningSpeed {get {return m_runningSpeed;} set{m_runningSpeed = value;}}

	private Transform groundCheck;
	private Animator anim;
	private bool grounded = false;

	#region static members
	private static int playerType;
	public static GameObject player;
	#endregion

	void Awake()
	{
		groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator>();
		playerType = LayerMask.NameToLayer("Player");
		player = gameObject;
	}
	void Start()
	{
		Status.NewGame();
	}

	void Update()
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		if(grounded) isSecondJumpActive = true;
		if(Input.GetButtonDown("Jump") && grounded)
		{
			jump = true;
		}
		if(Input.GetButtonDown("Jump") && !grounded && playerType == LayerMask.NameToLayer("Player1") && isSecondJumpActive)
		{
			secondJump = true;
			isSecondJumpActive = false;
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
		if(secondJump)
		{
			anim.SetTrigger("Jump");
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
			rigidbody2D.AddForce(new Vector2(0f, 2 * jumpForce/3));
			secondJump= false;
		}
		rigidbody2D.velocity = new Vector2(runningSpeed,  rigidbody2D.velocity.y);

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		int layerNum = other.gameObject.layer;
		if(layerNum == LayerMask.NameToLayer("Food") || layerNum == LayerMask.NameToLayer("Item")) {

			 other.gameObject.SendMessage("Effect");

		}else if(layerNum ==  LayerMask.NameToLayer("WeakPoint")) {

			Destroy(other.gameObject.transform.parent.gameObject);
			anim.SetTrigger("Jump");
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
			rigidbody2D.AddForce(new Vector2(0f, jumpForce / 2));
			jump = false;

		}else if(layerNum ==  LayerMask.NameToLayer("Enemy")) {

			Destroy(other.gameObject);
			if(playerType == LayerMask.NameToLayer("Player1")) {
				ToPlayer();
			}else if(playerType == LayerMask.NameToLayer("Player2")) {

			}else{
				Status.Damaged();
			}

		}else if(layerNum ==  LayerMask.NameToLayer("Bomb")) {

			Destroy(other.gameObject);
			if(playerType == LayerMask.NameToLayer("Player1") || playerType == LayerMask.NameToLayer("Player2")) {
				ToPlayer();
			}else{
				Status.Damaged();
			}

		}else if(layerNum ==  LayerMask.NameToLayer("EndPoint")) {

			Status.FalledDie();

		}else if(layerNum ==  LayerMask.NameToLayer("Goal")) {
			Status.GameClear();

		}
	}


	public void ToPlayer() {
		isSecondJumpActive = false;
		ChangeLayer("Player");
		Transform body = transform.FindChild("body");
		body.renderer.material.color = Color.white;
	}

	#region static methods
	public static GameObject GetPlayer() {
		return player;
	}

	public static void ChangeLayer(string layerName){
		player.layer = LayerMask.NameToLayer(layerName);
		foreach(Transform  child in player.transform) {
			child.gameObject.layer = LayerMask.NameToLayer(layerName);
		}
		playerType = LayerMask.NameToLayer(layerName);
	}
	#endregion
}

