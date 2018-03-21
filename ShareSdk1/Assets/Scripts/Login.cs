using UnityEngine;
using System.Collections;
using System;  
using System.Collections.Generic;  
using System.IO;  
using System.Text;
using System.Reflection;
using cn.sharesdk.unity3d;
using cn.SMSSDK.Unity;
public class Login : MonoBehaviour,SMSSDKHandler{
	ShareSDK ssdk;
	SMSSDK sMSSDK;
	// Use this for initialization
	void Start () {
		ssdk = ShareSDKManager.Instance.ssdk;
		//指定授权结果的回调函数
		ssdk.authHandler+=OnAuthResultHandler;
		sMSSDK = ShareSDKManager.Instance.sMSSDK;
		sMSSDK.setHandler(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnSinaLoginButtonClick(){
		//是否有授权信息
		
		if(ssdk.IsAuthorized(PlatformType.SinaWeibo)){
			Util.WriteFile(Application.persistentDataPath,"AuthInfo.dat",ssdk.GetAuthInfo(PlatformType.SinaWeibo).toJson());
			Util.MakeToast("微博用户："+ssdk.GetAuthInfo(PlatformType.SinaWeibo)["userName"]+"登录成功");
			ShareSDKManager.Instance.userPlat = PlatformType.SinaWeibo;
			UnityEngine.SceneManagement.SceneManager.LoadScene(2);
		}else{
			//授权
			ssdk.Authorize(PlatformType.SinaWeibo);
		}

	}
	//state为状态，type为平台类型，data为返回的数据（本次授权过程中的data数据）
	void OnAuthResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable data){
		if(state == ResponseState.Success){
			//ssdk.GetAuthInfo(type);

			Util.MakeToast("微博用户："+ssdk.GetAuthInfo(PlatformType.SinaWeibo)["userName"]+"登录成功");
			Util.WriteFile(Application.persistentDataPath,"AuthResult.dat",data.toJson());
			Util.WriteFile(Application.persistentDataPath,"AuthInfo.dat",ssdk.GetAuthInfo(PlatformType.SinaWeibo).toJson());
			UnityEngine.SceneManagement.SceneManager.LoadScene(2);
		}else if(state == ResponseState.Fail){
			//授权失败清空指定平台授权信息
			ssdk.CancelAuthorize(type);
			Util.MakeToast("登录失败");
		}else if(state == ResponseState.Cancel){
			//授权失败清空指定平台授权信息
			ssdk.CancelAuthorize(type);
			Util.MakeToast("登录取消");
		}
	}
	public void OnSmsLoginButtonClick(){
		ShareSDKManager.Instance.userPlat = PlatformType.SMS;
		sMSSDK.showRegisterPage(CodeType.TextCode);
	}
	public void onComplete(int action, object resp){
		ActionType act = (ActionType) action;
		Debug.Log(act);
		if(act == ActionType.CommitCode){
			ShareSDKManager.Instance.userPlat = PlatformType.SMS;
			ShareSDKManager.Instance.userId = ((string)resp).hashtableFromJson()["phone"].ToString();
		}
	}
	public void onError(int action, object resp){
		Util.MakeToast("短信登录失败");
	}
}
