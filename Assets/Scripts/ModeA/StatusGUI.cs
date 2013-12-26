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
		GUILayout.Label("LIFE : " + Status.GetLife(), textStyle);
		GUILayout.EndArea();

		if(Status.IsGameOver()) {
			GUI.Label(new Rect(100, 100, 200, 40), "TOUCH to RESTART");
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
