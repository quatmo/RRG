using UnityEngine;
using System.Collections;

public class GameMode : MonoBehaviour {
	public int life = 3;
	public int maxLife = 5;
	
	private bool gameOver = false;

	void Start () {
		DrawLife ();
	}

	void Update () {
		
	}
	
	void Die () {
		life--;
		DrawLife();
		checkGameOver();
	}
	
	void checkGameOver () {
		if (life <= 0) {
			gameOver = true;
			GameOver();
		}
	}
	
	void DrawLife () {
		GameObject hp = GameObject.Find("HP");
		foreach (Transform obj in hp.transform){
			Destroy(obj.gameObject);
		}
		for(int i=0; i<life; i++) {
			GameObject lifeTexture = (GameObject)Object.Instantiate(Resources.Load("Prefabs/Life"));
				lifeTexture.transform.position = new Vector3 (0 + 0.03f * i, 1, 0);
				lifeTexture.transform.parent = hp.transform;
			}
	}
	
	void OnGUI (){
		if(gameOver) {
			if(GUI.Button(new Rect(140,200,100,30), "RETRY")) {
					Application.LoadLevel("Main");
					Time.timeScale = 1;
			}
		}
	}
	
	void GameOver () {
		Time.timeScale = 0;
		GUITexture.Instantiate(Resources.Load("Prefabs/End"));
	}
	
}
