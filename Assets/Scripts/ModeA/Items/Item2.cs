using UnityEngine;
using System.Collections;

public class Item2 : Item{
	public override void Effect() {
		GameObject player = PlayerScript.GetPlayer();
		PlayerScript.ChangeLayer("Player2");
		Transform body = player.transform.FindChild("body");
		body.renderer.material.color = Color.red;
		Destroy(gameObject);;
	}
}
