using UnityEngine;
using System.Collections;
using MiniJSON;
using System.Collections.Generic;

public class UserNameRegistration : MonoBehaviour {

  private string UserName = "";
  private int UserId;
  private bool HasName;

	void Start () {
    PlayerPrefs.DeleteKey("UserName");
    if (PlayerPrefs.HasKey("UserName")) {
      HasName = true;
      UserName = PlayerPrefs.GetString("UserName");
    }else{
      HasName = false;
    }
	}

	void Update () {

	}

  void OnGUI () {
    GUI.Label (new Rect (10, 10, 100, 20),"Player Name");
    UserName = GUI.TextField(new Rect (10, 40, 100, 20),UserName, 10);
    if (GUI.Button(new Rect (10, 70, 100, 20),"OK") && UserName!=""){
      PlayerPrefs.SetString("UserName", UserName);
      if(HasName==true){
        //名前更新のための処理（あとから追加）
      }else{
        //名前登録処理
        ResistData(UserName);
      }
    }
  }

  void ResistData (string UserName){
    string url = "http://team1.vmlocal.crooz.local/RRG/UserNameRegistration";
    WWWForm form = new WWWForm();
    form.AddField("username", UserName);
    WWW www = new WWW(url, form);
    StartCoroutine(WaitForRequest(www));
  }

  IEnumerator WaitForRequest(WWW www) {
    yield return www;
    if (www.error == null) {
      Debug.Log("WWW Ok!: " + www.text);
      GetJson(www);
    } else {
      Debug.Log("WWW Error: "+ www.error);
    }
  }

  void GetJson (WWW www) {
    string json = www.text;
    json="["+json+"]";
    IList ResultData = (IList)Json.Deserialize(json);
    foreach(IDictionary Result in ResultData){
      string result = (string)Result["result"];
      Debug.Log("result:"+result);
      UserId = int.Parse(result);
      PlayerPrefs.SetInt("UserId", UserId);
      Application.LoadLevel("ModeSelect");
    }
  }
}