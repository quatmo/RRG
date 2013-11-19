using UnityEngine;
using System.Collections;

public class WorldMap : MonoBehaviour {

	void OnGUI(){
		if(GUI.Button(new Rect(90, 100, 100, 100), "map 1"))
		{
			Debug.Log("map 1");
			Application.LoadLevel("StageSelect");
		}else if(GUI.Button(new Rect(210, 100, 100, 100), "map 2"))
		{
			Debug.Log("map 2");
		}
	}

}
