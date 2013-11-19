using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			Application.LoadLevel("ModeSelect");
		}
	}
}
