using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	#region basic game settings
	public float m_jumpSpeed = 8.0f;
	public float jumpSpeed {get {return m_jumpSpeed;} set{m_jumpSpeed = value;}}
	public float m_gravity = 20.0f;
	public float gravity {get {return m_gravity;} set {m_gravity = value;}}
	public float m_runningSpeed = 4.0f;
	public float runningSpeed {get {return m_runningSpeed;} set{m_runningSpeed = value;}}
	#endregion
	
	
	#region for debug
	public bool isDebugMode = false;
	#endregion

	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	private GameObject rabbit;
	
	private int LAYER_Player = 8;
	private int LAYER_PlayerOnBlock = 9;
	private int LAYER_Block = 10;

	void Start () {
		controller = GetComponent<CharacterController>();
		StartCoroutine("RunFrameAnimation");
		rabbit = GameObject.Find("Rabbit");
	}
	
	void FixedUpdate() {
		if(controller.velocity.y < 0 && rabbit.layer == LAYER_Player) {
			rabbit.layer = LAYER_PlayerOnBlock;
		}
		RaycastHit hit;
		if(Physics.Raycast(rabbit.transform.position, Vector3.down, out hit, 2)) {
			if(hit.transform.gameObject.layer == LAYER_Block) {
			}else{
				rabbit.layer = LAYER_Player;
			}
		}
		if(controller.isGrounded) {
			if(Input.GetButtonDown("Jump")){
				moveDirection.y = jumpSpeed;
			}
		}else{
			moveDirection.y -= gravity * Time.deltaTime;
		}
		
		moveDirection.x = runningSpeed;
		controller.Move(moveDirection * Time.deltaTime);
		
		// for debug only
		if (isDebugMode) {
			if (controller.transform.position.x > 200){
				controller.transform.position = new Vector3(0,1.01f,0 );
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Item") {
			Destroy(other.gameObject);
		}
	}

	private IEnumerator RunFrameAnimation(){
		float runFrameSpeed = 0.2f;
		float[,] offsets = new float[,] {{0.333f,0.0f},{0.000f,0.0f},{0.333f,0.0f},{0.667f,0.0f}};
		while(true){
			for (int i=0;i<4;i++) {
				Vector2 offsetxy = new Vector2(offsets[i,0],offsets[i,1]);
				renderer.material.mainTextureOffset = offsetxy;
				yield return new WaitForSeconds(runFrameSpeed);
			}
		}
	}		
}
