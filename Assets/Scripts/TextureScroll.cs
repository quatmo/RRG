using UnityEngine;
using System.Collections;

public class TextureScroll: MonoBehaviour {

	public float ScrollSpeed = 0.1f;

	void Update () {
		float offset = Time.time * ScrollSpeed;
		renderer.material.mainTextureOffset = new Vector2 (offset, 0);
	}
}
