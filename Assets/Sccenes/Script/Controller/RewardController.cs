using UnityEngine;
using System.Collections;

public class RewardController : Singleton<RewardController> {

	/// <summary>
	/// Gets the reward.
	/// </summary>
	/// <returns>The reward.</returns>
	/// <param name="id">Identifier.</param>
	public float GetReward(int id)
	{
		ConfigRewardItems configrewardItem = AvConfigManager.configReward.GetRewardItem (id);
		return configrewardItem.money;
	}
}
