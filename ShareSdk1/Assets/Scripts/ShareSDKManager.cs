using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cn.sharesdk.unity3d;
using cn.SMSSDK.Unity;

public class ShareSDKManager : MonoBehaviour {
	private static ShareSDKManager _instance;
	public static ShareSDKManager Instance{
		get{
			return _instance;
		}

	}
	public PlatformType userPlat = PlatformType.Unknown;
	public string userId = "";
	[HideInInspector]
	public ShareSDK ssdk;
	public SMSSDK sMSSDK;
	// Use this for initialization
	void Awake () {
		_instance = this;
		DontDestroyOnLoad(gameObject);
		sMSSDK = GetComponent<SMSSDK>();
		//ssmsdk需要手动初始化
		sMSSDK.init("moba6b6c6d6","b89d2427a3bc7ad1aea1e1e8c1d36bf3",true);
		//sharesdk已经初始化过了
		ssdk = GetComponent<ShareSDK>();
		UnityEngine.SceneManagement.SceneManager.LoadScene(1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
