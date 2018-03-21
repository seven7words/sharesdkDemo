using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text.RegularExpressions;
public static class Util  {
	public static void WriteFile(string path,string name,string info){
		StreamWriter sw;
		FileInfo fi = new FileInfo(path+"/"+name);
		sw = fi.CreateText();
		sw.WriteLine(info);
		sw.Close();
		sw.Dispose();
	}
	public static void ReadFile(string path,string name){
		StreamReader sr;
		FileInfo fi = new FileInfo(path+"/"+name);
		sr = fi.OpenText();
		string info = sr.ReadToEnd();
		sr.Close();
		sr.Dispose();
	}
	public static void MakeToast(string info){
		AndroidJavaObject currentActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
		currentActivity.Call("runOnUiThread",new AndroidJavaRunnable(()=>{
			Toast.CallStatic<AndroidJavaObject>("makeText",currentActivity,info,Toast.GetStatic<int>("LENGTH_LONG")).Call("show");
		}));
	}
	// public static string UnicodeToString(string unicode){
	// 	Regex reg = new Regex(@"(?i)\\[uU](0-9a-f){4})");
	// 	return reg.Replace(unicode,delegate (Match m){
	// 		return ((char)Convert.ToInt32(m.Groups[1].Value,16)).ToString();
	// 	});
	// }
	public static string UnicodeToString(string unicode)
    {
        string resultStr = "";
        string[] strList = unicode.Split ('u');
        for (int i = 1; i < strList.Length; i++) {
            resultStr += (char)int.Parse (strList[i], System.Globalization.NumberStyles.HexNumber);
        }
        return resultStr;
    }
}
