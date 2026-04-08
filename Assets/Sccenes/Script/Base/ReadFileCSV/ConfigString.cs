using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;


[DelimitedRecord("\t")]
[IgnoreFirst(1)]
[IgnoreCommentedLines("//")]
[IgnoreEmptyLines(true)]
public class ConfigStringItem
{
	public int id;

	public string jp;
	public string en;
	public string vn;


	[FieldOptional]
	public string note;
}

public class ConfigString : GConfigDataTable<ConfigStringItem>
{
	public ConfigString()
		: base("ConfigString")
	{
	}

	protected override void OnDataLoaded()
	{
//		Debug.LogError("ConfigString");
		RebuildIndexField<int>("id");
	}

	public ConfigStringItem GetStringItem(int ID)
	{
		return FindRecordByIndex<int>("id", ID);
	}

	public List<ConfigStringItem> GetAllStringItem()
	{
		return records;
	}
}
