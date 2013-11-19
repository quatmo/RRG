using UnityEngine;
using System.Collections;

public class StageSelect : MonoBehaviour {
	void OnGUI(){
		if(GUI.Button(new Rect(90, 100, 100, 100), "Stage 1"))
		{
			Debug.Log("stage 1");
			Application.LoadLevel("ModeA");
		}else if(GUI.Button(new Rect(210, 100, 100, 100), "Stage 2"))
		{
			Debug.Log("stage 2");
		}
	}
}
