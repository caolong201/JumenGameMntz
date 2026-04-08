//#define TEST

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Language
{
	English = 0,
	Vietnamese,
	Japanese,
	korea
}

public class AvLocalizationManager
{	
#if LANG_VN
	public static Language currLang = Language.Vietnamese;
#else
	public static Language currLang = Language.Japanese;
#endif
	public static string GetString(int id)
	{
		if (AvConfigManager.configString == null || !AvConfigManager.configString.isLoaded)
			return "@error@";

		ConfigStringItem item = AvConfigManager.configString.GetStringItem(id);
		if (item == null)
			return string.Format("({0})", id);

		string reString = item.en;
		switch (currLang)
		{
			case Language.English:
				reString = item.en;
				break;
			case Language.Vietnamese:
				reString = item.vn;
				break;
			case Language.Japanese:
				reString = item.jp;
				break;
		}


		string[] regex = { @"\n" };
		string[] temp2 = reString.Split(regex,StringSplitOptions.None);
		reString = "";
		for (int i = 0; i < temp2.Length; i++)
		{
			if (i == temp2.Length - 1)
			{
				reString += temp2[i];
				break;
			}
			reString += temp2[i] + "\n";
		}

#if TEST
		reString += "(" + id + ")";
#endif
		return reString;
	}
}