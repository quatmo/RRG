using UnityEngine;
using System.Collections;

public class Food : Item {
	public int score = 0;

	public override void Effect() {
		Score.add(score);
		Destroy(gameObject);
	}
}
