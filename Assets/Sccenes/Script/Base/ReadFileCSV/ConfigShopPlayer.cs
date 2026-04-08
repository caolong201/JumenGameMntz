using System.Text;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;
using System.IO;

[DelimitedRecord("\t")]
[IgnoreFirst(1)]
[IgnoreCommentedLines("//")]
[IgnoreEmptyLines(true)]

public class ConfigShopPlayerItems {

	public int id;
	public string name;
	public float money;
}
public class ConfigShopPlayer:GConfigDataTable<ConfigShopPlayerItems>
{
	public ConfigShopPlayer():base("ConfigShopPlayer")
	{
		
	}
	protected override void OnDataLoaded ()
	{
		//rebuild data index
		RebuildIndexField<int>("id");
	}
	
	public ConfigShopPlayerItems GetPlayerItem(int ID)
	{
		return FindRecordByIndex<int>("id",ID);
	}
	public List<ConfigShopPlayerItems> GetAllPlayerItem()
	{
		return records;
	}

}

