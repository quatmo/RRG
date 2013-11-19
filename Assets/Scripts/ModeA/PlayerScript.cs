using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
//	
//	#region basic game settings
//	public float m_jumpSpeed = 8.0f;
//	public float jumpSpeed {get {return m_jumpSpeed;} set{m_jumpSpeed = value;}}
//	public float m_gravity = 20.0f;
//	public float gravity {get {return m_gravity;} set {m_gravity = value;}}
//	#endregion
//	
//	#region sound
//	SoundManager sm;
//	#endregion
//	
//	#region for debug
//	public bool isDebugMode = false;
//	#endregion
//
//	private Vector3 moveDirection = Vector3.zero;
//	private CharacterController controller;
//	private GameObject rabbit;
//	
//	private int LAYER_Player = 8;
//	private int LAYER_PlayerOnBlock = 9;
//	private int LAYER_Block = 10;


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
//		RunFrameAnimation = GetComponent<Animator>();
	}
	void Start()
	{
		newGame = GameObject.Find("GameMode").GetComponent<GameMode>();
	}

	void Update()
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		if(Input.GetButtonDown("Jump"))
		{
			Debug.Log(grounded);
		}
		if(Input.GetButtonDown("Jump") && grounded)
		{
			jump = true;
		}
	}

	void FixedUpdate()
	{

		if(jump)
		{
			    rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
		rigidbody2D.velocity = new Vector2(runningSpeed,  rigidbody2D.velocity.y);

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Item")
		{
			Destroy(other.gameObject);
		}
		else if (other.gameObject.tag == "Enemy")
		{
			Destroy(other.gameObject);
			newGame.SendMessage("Die");
		}
	}

//	void Start () {
//		controller = GetComponent<CharacterController>();
//		StartCoroutine("RunFrameAnimation");
//		rabbit = GameObject.Find("Rabbit");
//		newGame = GameObject.Find("GameMode").GetComponent<GameMode>();
//		sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
//		sm.PlayBGM();
//	}
//	void FixedUpdate() {
//		if(controller.velocity.y < 0 && rabbit.layer == LAYER_Player) {
//			rabbit.layer = LAYER_PlayerOnBlock;
//		}
//		RaycastHit hit;
//		if(Physics.Raycast(rabbit.transform.position, Vector3.down, out hit, 2)) {
//			if(hit.transform.gameObject.layer == LAYER_Block) {
//			}else{
//				rabbit.layer = LAYER_Player;
//			}
//		}
//		if(controller.isGrounded) {
//			if(Input.GetButtonDown("Jump")){
//				moveDirection.y = jumpSpeed;
//				sm.PlayJump();
//			}
//		}else{
//			moveDirection.y -= gravity * Time.deltaTime;
//		}
//		
//		moveDirection.x = runningSpeed;
//		controller.Move(moveDirection * Time.deltaTime);
//		
//		// for debug only
//		if (isDebugMode) {
//			if (controller.transform.position.x > 200){
//				controller.transform.position = new Vector3(0,1.01f,0 );
//			}
//		}
//	}
//	
//
//
//	private IEnumerator RunFrameAnimation(){
//		float runFrameSpeed = 0.2f;
//		float[,] offsets = new float[,] {{0.333f,0.0f},{0.000f,0.0f},{0.333f,0.0f},{0.667f,0.0f}};
//		while(true){
//			for (int i=0;i<4;i++) {
//				Vector2 offsetxy = new Vector2(offsets[i,0],offsets[i,1]);
//				renderer.material.mainTextureOffset = offsetxy;
//				yield return new WaitForSeconds(runFrameSpeed);
//			}
//		}
//	}		
	
}
