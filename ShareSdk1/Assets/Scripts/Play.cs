using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using cn.sharesdk.unity3d;
public class Play : MonoBehaviour {
	public Text resultText;
	ShareSDK ssdk;
	int mineral;
	int gas;
	int friendsBuff = 0;

	// Use this for initialization
	void Start () {
		ssdk = ShareSDKManager.Instance.ssdk;
		ssdk.getFriendsHandler = OnGetFriendResultHandler;
		ssdk.shareHandler = OnShareResultHandler;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnPlayButtonClick(){
		mineral = Random.Range(50,1000)+friendsBuff;
		gas = Random.Range(50,1000)+friendsBuff; 
		resultText.text = "恭喜你获得了\n"+mineral+"晶矿\n"+gas+"瓦斯\n"+"好友加成"+friendsBuff;
	}
	public void OnShareButtonClick(){
		ScreenCapture.CaptureScreenshot("Screenshot.png");
		ShareContent content = new ShareContent();
		content.SetText(resultText.text);
		content.SetImagePath(Application.persistentDataPath+"/Screenshot.png");
		content.SetTitle("TestShader");
		content.SetTitleUrl("http://www.baidu.com");
		content.SetSite("随便");
		content.SetSiteUrl("http://www.baidu.com");
		content.SetUrl("http://www.baidu.com");
		content.SetShareType(ContentType.Image);



		ShareContent SinaWeiboContent = new ShareContent();
		SinaWeiboContent.SetText(resultText.text+"via 新浪微博");
		content.SetShareContentCustomize(PlatformType.SinaWeibo,SinaWeiboContent);
		//PlatformType[] platforms = {PlatformType.SinaWeibo,};
		//哪些平台不显示
		string[] platforms = {"0","2","3","4","5"};
		content.SetHidePlatforms(platforms);
		ssdk.ShowPlatformList(null,content,100,100);
	}
	public void OnFriendsButtonClick(){
		ssdk.GetFriendList(PlatformType.SinaWeibo,15,0);
	}
	void OnGetFriendResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable data){
		if(state == ResponseState.Success){
			//Debug.Log("???");
			//Util.MakeToast("好友详情失败");
			//Util.WriteFile(Application.persistentDataPath,"UserInfo.dat",data.toJson());
			friendsBuff =int.Parse(data["total_number"].ToString()) ;
			Util.MakeToast("获得好友加成:"+friendsBuff);
		}else if(state == ResponseState.Fail){
			
			Util.MakeToast("好友详情失败");
		}else if(state == ResponseState.Cancel){
			
			Util.MakeToast("好友详情失败");
		}
	}
	void OnShareResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable data){
		if(state == ResponseState.Success){
			//Debug.Log("???");
			//Util.MakeToast("好友详情失败");
			//Util.WriteFile(Application.persistentDataPath,"UserInfo.dat",data.toJson());
			Util.MakeToast("分享成功:");
		}else if(state == ResponseState.Fail){
			
			Util.MakeToast("分享失败");
		}else if(state == ResponseState.Cancel){
			
			Util.MakeToast("分享失败");
		}
	}
	public void OnSignOutButtonClick(){
		ssdk.CancelAuthorize(PlatformType.SinaWeibo);
		UnityEngine.SceneManagement.SceneManager.LoadScene(1);
	}
}
