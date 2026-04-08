using UnityEngine;
using System.Collections;

public class AvLoading : SingletonMonoAwake<AvLoading> {
	public override void OnAwake ()
	{
		base.OnAwake ();
		LoadAllConfig ();
	}

	/// <summary>
	/// Loads all config.
	/// </summary>
	private void LoadAllConfig()
	{
		AvConfigManager.Instance.LoadAll ();
		SaveDataManager.Instance.Init ();
		SaveDataManager.Instance.LoadAll ();
		//load current playerid
		AvPlayerManager.Instance.CurrentPlayerID = SaveDataManager.Instance.GetCurrentPlayerID ();
		Debug.Log ("PLayerID: "+AvPlayerManager.Instance.CurrentPlayerID);
		//load money
		AvPlayerManager.Instance.Money = SaveDataManager.Instance.GetMoney ();

	}
	void OnDestroy()
	{
		//save money
		SaveDataManager.Instance.SaveMoney ();
	}
}

