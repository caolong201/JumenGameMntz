using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelRePlayDialog : MonoBehaviour {

	public GameObject btnFbOff,btnTwOff;
	public GameObject btnFb, btnTw;
	public GameObject iconNew;
	public GameObject UpdateLater;
	// Use this for initialization
	public void Init()
	{
		btnFb.GetComponent<Button> ().enabled = true;
		btnFbOff.SetActive (false);
		btnTw.GetComponent<Button> ().enabled = true;
		btnTwOff.SetActive (false);
		GuiManager.Instance.InitScore ();
		GuiManager.Instance.InitHighScore ();
		GuiManager.Instance.InitMoney ();

		CheckShowIconNew ();
	}

	public void UpdateLatert()
	{
		UpdateLater.SetActive(true);

	}


	public void CheckShowIconNew()
	{
		if (iconNew != null)
			GuiManager.Instance.CheckShowNewIcon (iconNew);
	}
	///========== score=======

	/// <summary>
	/// On button fbclicked.
	/// </summary>
	public void OnbtnShareFbClicked()
	{
		SoundManager.Instance.PlayButtonClick ();
		if (NetworkManager.isInternetConneted ()) {

			btnFb.GetComponent<Button> ().enabled = false;
			btnFbOff.SetActive (true);
		}
	}
    /// <summary>
    /// On button fbclicked.
    /// </summary>
    /// 



    public void ClosUpdateLatert()
    {
        UpdateLater.SetActive(false);

    }
    public void OnbtnShareTWClicked()
	{
		SoundManager.Instance.PlayButtonClick ();
		if (NetworkManager.isInternetConneted ()) {

			btnTw.GetComponent<Button> ().enabled = false;
			btnTwOff.SetActive (true);
		}
	}
}
