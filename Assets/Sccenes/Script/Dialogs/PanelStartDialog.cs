using UnityEngine;
using System.Collections;

public class PanelStartDialog : MonoBehaviour {
	public GameObject newIcon;
	// Use this for initialization
	void Start () {
	if (newIcon != null)
			GuiManager.Instance.CheckShowNewIcon (newIcon);
	}
	

}
