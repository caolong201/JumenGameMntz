using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public enum MessageBoxType
{
	OK,
	OKCancel
}
public class MessageBoxDlg : MonoBehaviour,UIDialogBase {

	// Use this for initialization
	public Text title;
	public Text content;
	public MessageBoxType type;
	public Button TbtnOk,TbtnCancel;
	public Action OkCallback;
	public Action CancelCallback;
	public void OnBeginShow (object para)
	{
		Debug.LogError ("vo hk : "+para);
		MessageBoxType type = (MessageBoxType)para  ;
		if(type == MessageBoxType.OK)
		{
			TbtnOk.gameObject.SetActive(true);
			TbtnCancel.gameObject.SetActive(false);

			TbtnOk.transform.localPosition = new Vector3(0,-97.4f,0);
			//add button listioner
			TbtnOk.onClick.AddListener(()=> OnbtnCancelClicked());

		}
		else
		{
			TbtnOk.gameObject.SetActive(true);
			TbtnCancel.gameObject.SetActive(true);
			
			TbtnOk.transform.localPosition = new Vector3(110,-101,0);
			TbtnCancel.transform.localPosition = new Vector3(-96,-98,0);

			//add button listioner
			TbtnOk.onClick.AddListener(()=> OnbtnOKclicked());
			TbtnCancel.onClick.AddListener(()=>OnbtnCancelClicked());

		}
	}

	public void OnbtnOKclicked()
	{
		GuiManager.Instance.HideBlackBolder();
		GuiManager.Instance.HideMessageBox (true,true, .3f, 0, "y", 1500);
		Debug.Log("OK");
		if(OkCallback!=null)
		{
			OkCallback();
		}
		ResetCallback ();
	}
	public void OnbtnCancelClicked()
	{
		GuiManager.Instance.HideBlackBolder();
		GuiManager.Instance.HideMessageBox (true,true, .3f, 0, "y", 1500);
		Debug.Log("Cancel");
		if(CancelCallback!=null)
		{
			CancelCallback();
		}
		ResetCallback ();

	}

	void ResetCallback()
	{
		OkCallback = null;
		CancelCallback = null;
	}

	void OnDisable()
	{
		TbtnOk.onClick.RemoveAllListeners();
		TbtnCancel.onClick.RemoveAllListeners();
	}
}
