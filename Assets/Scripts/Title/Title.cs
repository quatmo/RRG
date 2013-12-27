using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

	void Update () {
    if (PlayerPrefs.HasKey("UserName")) {
      Application.LoadLevel("ModeSelect");
    }else{
			Application.LoadLevel("Tutorial");
		}
	}
}
