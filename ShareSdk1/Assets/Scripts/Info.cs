using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using cn.sharesdk.unity3d;
public class Info : MonoBehaviour {
	public Image userIcon;
	public Text userName;
	public Text userId;
	private ShareSDK ssdk;
	// Use this for initialization
	void Start () {
		ssdk = ShareSDKManager.Instance.ssdk;
		ssdk.showUserHandler+=OnGetUserInfoResultHandler;
		Hashtable authInfo = ssdk.GetAuthInfo(PlatformType.SinaWeibo).toJson().hashtableFromJson();
		StartCoroutine(LoadUserIcon(authInfo["userIcon"].ToString()));
		userName.text = authInfo["userName"].ToString();
		userId.text = "ID:" + authInfo["userID"].ToString();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator LoadUserIcon(string url){
		WWW www = new WWW(url);
		yield return www;
		if(www.isDone&&www.error == null){
			Texture2D texture2D = www.texture;
			userIcon.sprite = Sprite.Create(texture2D,new Rect(0,0,texture2D.width,texture2D.height),Vector2.zero);
		}
	}
	public void OnEnterButtonClick(){
		UnityEngine.SceneManagement.SceneManager.LoadScene(3);
	}
	public void OnDetailButtonClick(){
		ssdk.GetUserInfo(PlatformType.SinaWeibo);
	}
	public void OnSignOutButtonClick(){
		ssdk.CancelAuthorize(PlatformType.SinaWeibo);
		UnityEngine.SceneManagement.SceneManager.LoadScene(1);
	}
	void OnGetUserInfoResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable data){
		if(state == ResponseState.Success){
			Debug.Log("???");
			Util.MakeToast("您的位置：");
			Util.MakeToast("您的位置："+Util.UnicodeToString(data["location"].ToString()) );
			Util.WriteFile(Application.persistentDataPath,"UserInfo.dat",data.toJson());

		}else if(state == ResponseState.Fail){
			
			Util.MakeToast("获取用户详情失败");
		}else if(state == ResponseState.Cancel){
			
			Util.MakeToast("获取用户详情失败");
		}
	}
}
