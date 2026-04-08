using System.Text;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;
using System.IO;

[DelimitedRecord("\t")]
[IgnoreFirst(1)]
[IgnoreCommentedLines("//")]
[IgnoreEmptyLines(true)]

public class ConfigRewardItems {

	public int id;
	public string name;
	public float money;
}
public class ConfigReward:GConfigDataTable<ConfigRewardItems>
{
	public ConfigReward():base("ConfigShopPlayer")
	{
		
	}
	protected override void OnDataLoaded ()
	{
		//rebuild data index
		RebuildIndexField<int>("id");
	}
	
	public ConfigRewardItems GetRewardItem(int ID)
	{
		return FindRecordByIndex<int>("id",ID);
	}
	public List<ConfigRewardItems> GetAllRewardItem()
	{
		return records;
	}

}

