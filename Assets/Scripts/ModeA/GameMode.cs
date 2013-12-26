using UnityEngine;
using System.Collections;

public class GameMode : MonoBehaviour {
	public int life = 3;
	public int maxLife = 5;
	
	private bool gameOver = false;

	void Start () {
		Time.timeScale = 1;
		DrawLife ();
	}

	void Update () {
		
	}
	
	void Damaged () {
		life--;
		DrawLife();
		checkGameOver();
	}

	void FalledDie() {
		life = 0;
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
			GUI.Label(new Rect(100, 100, 200, 40), "TOUCH to RESTART");
			if(Input.GetMouseButtonDown(0))
			{
					Application.LoadLevel("ModeA");
			}
		}
	}
	
	void GameOver () {
		Time.timeScale = 0;
	}
	
}
