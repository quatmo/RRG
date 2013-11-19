using UnityEngine;
using System.Collections;

public class ModeSelect : MonoBehaviour {

	void OnGUI(){
		if(GUI.Button(new Rect(90, 100, 100, 100), "A MODE"))
		{
			Debug.Log("a mode");
			Application.LoadLevel("WorldMap");
		}else if(GUI.Button(new Rect(210, 100, 100, 100), "B MODE"))
		{
			Debug.Log("b mode");
		}
	}
}
