using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public static bool isInternetConneted ()
	{
		if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			return true;
		}else
		{
			GuiManager.Instance.ShowMessageBox("Network","Please check network again!",MessageBoxType.OK);
			return false;
		}
	}
}
