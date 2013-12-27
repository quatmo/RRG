using UnityEngine;
using System.Collections;

public class StatusGUI : MonoBehaviour {

	void Start () {
		NewGame();
	
	}

	void OnGUI() {
		// draw score
		GUIStyle textStyle = new GUIStyle();
		textStyle.normal.textColor = Color.black;
		textStyle.fontSize = 34;
		GUILayout.BeginArea(new Rect(Screen.width /2 - 50,  0,  100, 50));
		GUILayout.Label(Score.get()  + "kg", textStyle);
		GUILayout.EndArea();

		//draw life
		GUILayout.BeginArea(new Rect(0,  0,  100, 50));
		GUILayout.Label("LIFE:" + Status.GetLife(), textStyle);
		GUILayout.EndArea();

		if(Status.IsGameOver()) {
			textStyle.normal.textColor = Color.red;
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 250,  Screen.height / 2,  500, 50));
			GUILayout.Label("TOUCH TO RESTART", textStyle);
			GUILayout.EndArea();
			if(Input.GetMouseButtonDown(0))
			{
				Application.LoadLevel("ModeA");
			}
		}
		if(Status.IsGameClear()) {
			textStyle.normal.textColor = Color.red;
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 250,  Screen.height / 2,  500, 50));
			GUILayout.Label("TOUCH TO RESTART", textStyle);
			GUILayout.EndArea();
			textStyle.normal.textColor = Color.yellow;
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 250,  Screen.height / 2 - 50,  500, 50));
			GUILayout.Label("GAME CLEAR", textStyle);
			GUILayout.EndArea();
			if(Input.GetMouseButtonDown(0))
			{
				Application.LoadLevel("ModeA");
			}
		}

	}

	void NewGame() {
		Score.NewGame();
	}
}
