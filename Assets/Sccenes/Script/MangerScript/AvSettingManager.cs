using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public enum SettingName
{
	SoundEffect2 = 0,
	BGM3,
	Vibration,
	PushNotification,
	EnablePushOnNight,
	//chat Content
	ChatContent,
	NoticeEvent,
	ShowHideName
}
public class AvSettingManager : Singleton<AvSettingManager>
{	
	private List<SettingName> listNameConfigure = new List<SettingName> ();
	private Dictionary<SettingName, string> listConfigureInfo = new Dictionary<SettingName, string> ();
	//==================================================
	//
	//==================================================
	public AvSettingManager ()
	{
		// add Name to list
		listNameConfigure.Add (SettingName.SoundEffect2);				
		listNameConfigure.Add (SettingName.BGM3);	
		listNameConfigure.Add (SettingName.PushNotification);
		listNameConfigure.Add (SettingName.EnablePushOnNight);
		listNameConfigure.Add (SettingName.ChatContent);
		listNameConfigure.Add (SettingName.NoticeEvent);
		listNameConfigure.Add (SettingName.ShowHideName);
		listNameConfigure.Add (SettingName.Vibration);
	}
	//==================================================
	//
	//==================================================
	public string GetDefaultValue (SettingName inKeyName)
	{
		switch (inKeyName) {
		case  SettingName.SoundEffect2:
			{
				return true.ToString ();
			}
		case  SettingName.BGM3:
			{
				return true.ToString ();
			}
		case  SettingName.PushNotification:
			{
				return true.ToString ();
			}
		case  SettingName.EnablePushOnNight:
			{
				return true.ToString ();
			}

		case SettingName.ChatContent:
			{
				return true.ToString ();
			}
		case SettingName.ShowHideName:
			{
				return true.ToString ();
			}
		case SettingName.Vibration:
			{
				return true.ToString ();
			}
		default:
			{
				return "";
			}
		}
	}
	//==================================================
	// load from disk * when application started, once execute
	//==================================================
	public bool Load ()
	{
		//Debug.LogWarning("Load setting");
		//
		listConfigureInfo.Clear ();	
		foreach (SettingName name in listNameConfigure) {
			string valueOnLoaded = "";
			valueOnLoaded = PlayerPrefs.GetString (name.ToString ());
			// not saved value
			if (valueOnLoaded == "") 
			{
				if(name==SettingName.BGM3 || name==SettingName.SoundEffect2)
				{
					valueOnLoaded = "10";
				}
				else
				valueOnLoaded = GetDefaultValue (name);
			}
			listConfigureInfo.Add (name, valueOnLoaded);

			//Debug.Log(name + "-" + valueOnLoaded);
		}

		return true;
	}
	//==================================================
	// save to disk(?)
	//==================================================
	public void SaveAllSetting ()
	{
		foreach (SettingName name in listNameConfigure) {
			string valueToSave = "";
			listConfigureInfo.TryGetValue (name, out valueToSave);
			
			PlayerPrefs.SetString (name.ToString (), valueToSave);
			//Debug.LogError("name save: "+name.ToString()+" value: "+valueToSave);
		}
	}
	//==================================================
	// save to disk(?)
	//==================================================
	public void SaveValueWithKey (SettingName inKeyName, string inValue)
	{
		SetConfigureInfo (inKeyName, inValue);
		PlayerPrefs.SetString (inKeyName.ToString (), inValue);
	}
	
	//==================================================
	//
	//==================================================
	public string GetConfigureInfo (SettingName inKeyName)
	{
		string configureInfo = "";
		listConfigureInfo.TryGetValue (inKeyName, out configureInfo);
		return configureInfo;
	}
	//==================================================
	//
	//==================================================
	public bool GetConfigAsBool (SettingName inKeyName)
	{
		bool outValue;
		string text = GetConfigureInfo (inKeyName);
		if (bool.TryParse (text, out outValue))
			return outValue;
		return true;
	}


	//==================================================
	//set only, not save yet
	//==================================================
	public bool SetConfigureInfo (SettingName inKeyName, string inValue)
	{
		bool isResult = false;
		if (listConfigureInfo.ContainsKey (inKeyName)) {
			listConfigureInfo [inKeyName] = inValue;
			isResult = true;
		}
		return isResult;
	}
	public float ConvertToVolume(float value)
	{
		return value/10;
	}
}