using UnityEngine;
using System.Collections;

public static class Status{
	static int life = 3;
	static int initLife = 3;
	static bool isGameOver = false;
	static bool isGameClear = false;

	static public void Damaged() {
		life--;
		CheckGameOver();
	}

	static private void CheckGameOver() {
		if(life < 1) {
			GameOver();
		}
	}

	static public void FalledDie() {
		life = 0;
		GameOver();
	}

	static public int GetLife() {
		return life;
	}
	static public bool IsGameOver() {
		return isGameOver;
	}
	static public bool IsGameClear() {
		return isGameClear;
	}


	static void GameOver () {
		Time.timeScale = 0;
		isGameOver = true;
	}

	static public void GameClear () {
		Time.timeScale = 0;
		isGameClear = true;
	}

	static public void NewGame() {
		life = initLife;
		isGameOver = false;
		isGameClear = false;
		Time.timeScale = 1;
	}
}
