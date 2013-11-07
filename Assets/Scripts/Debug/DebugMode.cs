using UnityEngine;
using System.Collections;

/**
 *  for Debug
 * 
 * */

public class DebugMode : MonoBehaviour {
	private bool isVisible = false;
	private float zoom = 4.0f;
	Camera mainCamera;
	PlayerScript playerScript;
	

	void Start () {
		playerScript = GameObject.Find("Rabbit").GetComponent<PlayerScript>();
		
		if(playerScript.isDebugMode){
			mainCamera = GameObject.Find("MainCamera").camera;
			if(DebugManager.isInit) {
				LoadManager();
				DebugManager.isInit = false;
			}else{
				SaveManager();
			}
		}
	}
	
	void Update () {
		if(playerScript.isDebugMode){
			guiText.text = EditText();
		}
	}
	
	/**
	 * @return string - status about basic game settings
	 * */
	string EditText () {
		return "speed : " + playerScript.runningSpeed + "\n" +
						"gravity : " + playerScript.gravity + "\n" +
						"jumpSpeed : " + playerScript.jumpSpeed + "\n" +
						"zoom : " + zoom;
		
	}
	
	void LoadManager(){
		playerScript.runningSpeed = DebugManager.runningSpeed;
		playerScript.jumpSpeed = DebugManager.jumpSpeed;
		playerScript.gravity = DebugManager.gravity;
		mainCamera.orthographicSize = DebugManager.zoom;
	}
	void SaveManager(){
		DebugManager.runningSpeed = playerScript.runningSpeed;
		DebugManager.jumpSpeed = playerScript.jumpSpeed;
		DebugManager.gravity = playerScript.gravity;
		DebugManager.zoom = mainCamera.orthographicSize;
	}
	
	#region GUI method
	void OnGUI(){
		if(playerScript.isDebugMode){
			
			if(isVisible) {
				GUI.Box(new Rect(10,10,300,300), "DEBUG MENU");
				GUI.Label(new Rect(20,50,70,20), "SPEED");
				if(GUI.Button(new Rect(100,50,40,20), "UP")){
					DebugManager.runningSpeed += 0.5f;
				}
				if(GUI.Button(new Rect(150,50,80,20), "DOWN")){
					DebugManager.runningSpeed -= 0.5f;
				}
			
				GUI.Label(new Rect(20,90,70,20), "JUMP");
				if(GUI.Button(new Rect(100,90,40,20), "UP")){
					DebugManager.jumpSpeed += 0.5f;
				}
				
				if(GUI.Button(new Rect(150,90,80,20), "DOWN")){
					DebugManager.jumpSpeed -= 0.5f;
					
				}
			
				GUI.Label(new Rect(20,140,70,20), "GRAVITY");
				if(GUI.Button(new Rect(100,140,40,20), "UP")){
					DebugManager.gravity += 0.5f;
					
				}
				if(GUI.Button(new Rect(150,140,80,20), "DOWN")){
					DebugManager.gravity -= 0.5f;
				
				}
				GUI.Label(new Rect(20,190,80,20), "CAMERA");
				DebugManager.zoom = GUI.HorizontalSlider(new Rect(80,190,200,20),DebugManager.zoom,2.0f,10.0f );
			
				if(GUI.Button(new Rect(20,270, 80,20), "RESET")){
					DebugManager.runningSpeed = 4.0f;
					DebugManager.gravity = 20.0f;
					DebugManager.jumpSpeed = 8.0f;
					DebugManager.zoom = 4.0f;
				}
				if(GUI.Button(new Rect(110,270,80,20), "RESTART")){
					DebugManager.isInit = true;
					Application.LoadLevel("Main");
				}
				if(GUI.Button(new Rect(200,270,100,20), "EXIT")){
					isVisible = false;
				}
			}else{
				if(GUI.Button(new Rect(100,0,180,30), "DEBUG MENU")){
					isVisible = true;
				}
			
			}
			LoadManager();
		}
	}
	#endregion
}
