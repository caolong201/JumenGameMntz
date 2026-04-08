using UnityEngine;
using System.Collections;

public static class GameConstance {
	/// <summary>
	/// The tag.AutoRun
	/// </summary>
	/// 
	public static string PlayerTag = "Player";
	public static string EnemyTag = "Enemy";
	public static string BonusTag = "Bonus";
	public static string ChangeCameraTag = "ChangeCamera";
	/// <summary>
	/// The time platorm down.
	/// </summary>
	public static float TimePlatformDown_Start = 3f;
	public static float TimePlatformDown_Over50 = .7f;
	public static float TimePlatformDown_Over200 = .6f;
	public static float TimePlatformDown_Over300 = .5f;

	public static float TimePlatformAutodown = .4f;
	/// <summary>
	/// The name of the platform.
	/// </summary>
	public static string PlatformName = "Platform_";
	/// <summary>
	/// The time to player auto run.
	/// </summary>
	public static float TimeToPlayerAutoRun = 2f;
	public static float TimeToPlayerAutoRunNextStep = .1f;
	/// <summary>
	/// The path of player prefabs.
	/// </summary>
	public static string PathOfPlayerPrefabs = "PlayerPrefabs/Player_";
	public static string PathOfAvatarPlayer = "AvatarPlayer/";
	public static string KeyCurrentPlayerID = "currentPlayerID";
	/// <summary>
	/// The Player identifier default.
	/// </summary>
	public static int PlayerIdDefault = 100;

	public static string moneyID = "MONEY";
	public static string kTutorial = "Tutorial";
	public static string kUserName = "USERNAME";
	public static string kUserId = "USERID";
	//sound, bm
	public static string kSound ="SOUND";
	public static string kBgm = "BGM";
}
