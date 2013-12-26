using UnityEngine;
using System.Collections;

public static class Score {
	static int score = 0;

	static public int get() {
		return score;
	}

	static public void NewGame() {
		score = 0;
	}
	static public void add(int value) {
		score += value;
	}
}
