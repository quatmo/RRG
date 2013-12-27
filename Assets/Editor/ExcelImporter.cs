using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;
//using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class ExcelImporter : AssetPostprocessor {
	
	static void OnPostprocessAllAssets (
			string[] importedAssets, 
			string[] deletedAssets, 
			string[] movedAssets, 
			string[] movedFromAssetPaths)
	{


		foreach (string file in importedAssets) 
		{			
			if (file.EndsWith(".xls"))
			{
				ClearGameField();
				Debug.Log("FilePath:"+file);
				
				string asset_path = "Assets/Data/" + Path.GetFileNameWithoutExtension(file) + ".asset";
				Debug.Log("AssetPath:"+asset_path);
				
				using(FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					// Load or Create Asset
					StageBlockSet stageBlockSet = AssetDatabase.LoadAssetAtPath(asset_path, typeof(StageBlockSet)) as StageBlockSet;					
					if(stageBlockSet == null)
					{
						stageBlockSet = ScriptableObject.CreateInstance<StageBlockSet>();
						AssetDatabase.CreateAsset(stageBlockSet, asset_path);
					}
					stageBlockSet.Clear();

					// Open Excel
					IWorkbook book = new HSSFWorkbook(fs);
					int sheetNum = book.NumberOfSheets;
					for(int i=0; i<sheetNum; ++i)
					{ 
						ISheet sheet = book.GetSheetAt(i);
						Debug.Log("Sheet:" + sheet.SheetName);
						
						int firstRow = sheet.FirstRowNum;
						int lastRow = sheet.LastRowNum;
						for(int j=0; j<UnityEditorInternal.InternalEditorUtility.tags.Length ; j++) {
							Debug.Log("id: " + j + "name:" + UnityEditorInternal.InternalEditorUtility.tags[j]);
						}

						for(int rowIdx=firstRow; rowIdx<=lastRow; ++rowIdx)
						{
							IRow row = sheet.GetRow(rowIdx);
							if(row == null){ continue; }
							
							int firstCell = row.FirstCellNum;
							int lastCell = row.LastCellNum;
							
							for(int cellIdx=firstCell; cellIdx<=lastCell; ++cellIdx)
							{
								ICell cell = row.GetCell(cellIdx);
								if(cell == null){ continue;}
								if(cell.NumericCellValue == -1) {
									StageBlock stageBlock = new StageBlock();
									stageBlock.name = "EndPoint";
									stageBlock.position = new Vector3((float) cellIdx-5, (float) 14 - rowIdx,0);
									stageBlockSet.Add(stageBlock);
									CreateOrUpdateObject(stageBlock, cellIdx); 
								}else if(cell.NumericCellValue == -2) {
									StageBlock stageBlock = new StageBlock();
									stageBlock.name = "Goal";
									stageBlock.position = new Vector3((float) cellIdx-5, (float) 14 - rowIdx,0);
									stageBlockSet.Add(stageBlock);
									CreateOrUpdateObject(stageBlock, cellIdx); 
								}else{
 									if(cell.NumericCellValue < 1) {continue;}
									int cellNum = (int)cell.NumericCellValue ; 
									Debug.Log("X :" + cellIdx + " Y : " + rowIdx);
									Debug.Log("cellNum : " + cellNum);
									StageBlock stageBlock = new StageBlock();
									 stageBlock.name = UnityEditorInternal.InternalEditorUtility.tags[cellNum];
									stageBlock.position = new Vector3((float) cellIdx-5, (float) 14 - rowIdx,0);
									stageBlockSet.Add(stageBlock);
									CreateOrUpdateObject(stageBlock, cellIdx);
								}
							}
						}
					}

					// Apply Data
					ScriptableObject obj = AssetDatabase.LoadAssetAtPath(asset_path, typeof(ScriptableObject)) as ScriptableObject;
					EditorUtility.SetDirty(obj);
				}
			}
			
			if (file.EndsWith(".xlsx"))
			{
				Debug.Log("FilePath:"+file);
				Debug.LogWarning(".xlsx file is not spported now.");
			}

		}
	}
	
	static private void CreateOrUpdateObject(StageBlock _data, int cellIdx)
	{
		if(_data == null){ return; }
		
		GameObject objRoot = GameObject.Find("GameField");
		if(objRoot == null){ return; }
		Debug.Log("Prefabs/"+_data.name);

		GameObject objStageBlock = (GameObject)Object.Instantiate(Resources.Load("Prefabs/"+_data.name));
		if(objStageBlock.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			objRoot = GameObject.Find("Floor");
		}else if(objStageBlock.gameObject.layer == LayerMask.NameToLayer("Food")) {
			objRoot = GameObject.Find("Foods");
		}else if(objStageBlock.gameObject.layer == LayerMask.NameToLayer("Enemy")){
			objRoot = GameObject.Find("Enemies");
		}else if(objStageBlock.gameObject.layer == LayerMask.NameToLayer("Bomb")){
			objRoot = GameObject.Find("Bombs");
		}else if(objStageBlock.gameObject.layer == LayerMask.NameToLayer("Item")){
			objRoot = GameObject.Find("Items");
		}else if(objStageBlock.gameObject.layer == LayerMask.NameToLayer("EndPoint")){
			objRoot = GameObject.Find("EndPoints");
		}else if(objStageBlock.gameObject.layer == LayerMask.NameToLayer("Goal")){
			objRoot = GameObject.Find("Goals");
		}

		if(objStageBlock == null)
		{
			objStageBlock = GameObject.CreatePrimitive(PrimitiveType.Cube);
			objStageBlock.name = _data.name;
		}
		objStageBlock.transform.parent = objRoot.transform;
		objStageBlock.transform.position = new Vector3(_data.position.x, _data.position.y, _data.position.z);
	}

	static private void ClearGameField() {
		GameObject gameField = GameObject.Find("GameField");
		foreach(Transform child in gameField.transform) {
			foreach(Transform go in child.gameObject.transform) {
				GameObject.DestroyImmediate(go.gameObject);
			}
		}
	}

}
