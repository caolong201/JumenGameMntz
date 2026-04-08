using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AvConfigManager : Singleton<AvConfigManager>
{
    public static ConfigString configString;    
	public static ConfigShopPlayer configShopPlayer;
	public static ConfigReward configReward;
	public static Dictionary<string, IConfigDataTable> configList = new Dictionary<string, IConfigDataTable>();


    private void LoadDataConfig<TConfigTable>(ref TConfigTable configTable, params string[] dataPaths) where TConfigTable : IConfigDataTable, new()
	{
       
		try
		{
			//if (configTable == null)//alway news
				configTable = new TConfigTable();
			#if !USING_ASSETLOADER
			configTable.BeginLoadAppend();
			#endif
			foreach (var path in dataPaths)
			{
				#if !USING_ASSETLOADER
				configTable.LoadFromAssetPath(path);
				#endif
				configList[path] = configTable;

			}
			#if !USING_ASSETLOADER
			configTable.EndLoadAppend();
			#endif

		}
		catch (System.Exception ex)
		{
            Debug.LogError("Load Config Error:"+ configTable.GetName()+", "+ ex.ToString());
		}
	}
	
	public void LoadAll()
    {
		Debug.LogWarning("log data config");

		LoadDataConfig<ConfigString> (ref configString,"Config/ConfigString_Normal");
		LoadDataConfig<ConfigShopPlayer>(ref configShopPlayer,"Config/ConfigShopPlayer");
		LoadDataConfig<ConfigReward>(ref configReward,"Config/ConfigReward");
    }

	public void UnLoad()
	{
		foreach(KeyValuePair<string, IConfigDataTable>item in configList)
			item.Value.Clear();
		configList.Clear();
	}


#if UNITY_EDITOR

	private void LoadDataConfigEditor<TConfigTable>(ref TConfigTable configTable, params string[] dataPaths) where TConfigTable : IConfigDataTable, new()
	{
		
		try
		{
			configTable = new TConfigTable();
			
			configTable.BeginLoadAppend();
			foreach (var path in dataPaths)
			{
				configTable.LoadFromAssetPath(path);
			}
			configTable.EndLoadAppend();
		}
		catch (System.Exception ex)
		{
			Debug.LogError("Load Config Error:" + configTable.GetName() + ", " + ex.ToString());
		}
	}

#endif


}
