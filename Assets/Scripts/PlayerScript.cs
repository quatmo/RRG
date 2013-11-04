using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	#region basic game settings
	public float jumpSpeed {get; set;}
	public float gravity {get; set;}
	public float runningSpeed {get; set;}
	#endregion
	
	
	#region for debug
	public bool isDebugMode = false;
	#endregion

	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;

	void Start () {
		jumpSpeed = 8.0f;
		gravity = 20.0f;
		runningSpeed = 4.0f;
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
