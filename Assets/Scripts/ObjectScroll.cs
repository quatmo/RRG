using UnityEngine;
using System.Collections;

public class ObjectScroll : MonoBehaviour {

	public float ScrollSpeed = 10.0f;
	
	void Update () {
		transform.Translate(Vector3.left * ScrollSpeed * Time.deltaTime, Space.World);
	}
}
