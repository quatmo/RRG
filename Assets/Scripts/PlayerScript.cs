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

	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	void FixedUpdate() {
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
	
}
