using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * commponents of GameField
 * */
public class StageBlockSet : ScriptableObject
{
	public List<StageBlock> list = null;
	
	void OnEnable()
	{
		hideFlags = HideFlags.NotEditable;
		
		if(list == null)
		{
			Debug.Log("new List<StageBlock>();");
			list = new List<StageBlock>();
		}
	}
	
	public void Add(StageBlock _data)
	{
		if(list != null)
		{
			list.Add(_data);
		}
	}
	
	public void Clear()
	{
		if(list != null)
		{
			list.Clear();
		}
	}
}

[System.SerializableAttribute]
public class StageBlock
{
	public string  name = "";
	public enum BlockType{
		Normal
	};
	public Vector3 position = Vector3.zero;
}
