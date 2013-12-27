using UnityEngine;
using System.Collections;

public class Item1 : Item{
	public override void Effect() {
		GameObject player = PlayerScript.GetPlayer();
		PlayerScript.ChangeLayer("Player1");
		Transform body = player.transform.FindChild("body");
		body.renderer.material.color = Color.green;
		Destroy(gameObject);
	}

}
