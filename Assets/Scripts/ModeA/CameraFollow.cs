using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public float xMargin = 1f;
	public float yMargin = 2f;
	public float xSmooth = 1f;
	public float ySmooth = 1f;

	private Transform player;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	bool CheckXMargin ()
	{
		return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
	}
	bool CheckYMargin ()
	{
//		return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
		return player.position.y - transform.position.y > yMargin;
	}
	void FixedUpdate ()
	{
		TrackPlayer();
	}
	void TrackPlayer()
	{
		float targetX = transform.position.x;
		float targetY = transform.position.y;

//		if(CheckXMargin())
//		{
//			targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);
//		}
		targetX = player.position.x + 2.73555f;
		if(CheckYMargin())
		{
			targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);
		}else{
			targetY = Mathf.Lerp(transform.position.y, player.position.y + 2, ySmooth * Time.deltaTime);
		}

		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
