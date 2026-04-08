using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AvPlayerManager : Singleton<AvPlayerManager> {
	/// <summary>
	/// my money.
	/// </summary>
	private float mMoney;
	public float Money
	{
		set{mMoney =value;}
		get{return mMoney;}
	}
	/// <summary>
	/// The my player count.
	/// </summary>
	public int PlayerCount
	{
		get{return mListConfigShopPlayerItems.Count;}
	}
	/// <summary>
	///  current player (find buy id).
	/// </summary>
	private int mCurrentPlayerID =100; //default
	public int CurrentPlayerID
	{
		set{mCurrentPlayerID = value;}
		get{return mCurrentPlayerID;}
	}
	/// <summary>
	/// The m list config shop player items.
	/// </summary>
	private List<ConfigShopPlayerItems> mListConfigShopPlayerItems = AvConfigManager.configShopPlayer.GetAllPlayerItem ();
	public List<ConfigShopPlayerItems> ListConfigShopPlayerItems
	{
		get{return mListConfigShopPlayerItems;}

	}
	/// <summary>
	/// Caculators the money.
	/// </summary>
	/// <param name="score">Score.</param>
	public void CaculatorMoney(int score)
	{
		mMoney += (int)(score/10);
		Debug.Log("+"+(int)(score/10)+"$ Sum: "+Money+"$");
	}
	private bool mIsTutorials;
	public bool Istutorials
	{
		set{mIsTutorials = value;}
		get{return mIsTutorials;}
	}

}
