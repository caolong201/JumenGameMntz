using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
public class ShopPlayerDialog : MonoBehaviour {
	public  Text txt;
	public UIGrid content;
	public GameObject itemsPref;
	public Text money;
	public PanelRePlayDialog panelreplay;
	

    public Transform playerState;
	
	public void Init()
	{
		money.text = AvPlayerManager.Instance.Money.ToString();
		AvGameObjectUtils.ClearAllChild (content.gameObject);
		for (int i = 0; i <  AvPlayerManager.Instance.PlayerCount; i++)
		{
			GameObject item = Instantiate(itemsPref) as GameObject;
			item.transform.SetParent(content.gameObject.transform);
			item.transform.localScale =Vector3.one;
			ConfigShopPlayerItems configItem =AvPlayerManager.Instance.ListConfigShopPlayerItems[i];
			item.GetComponent<ItemShopPlayer>().Init(configItem.id,configItem.name,configItem.money);
		}
		content.Reposition ();
	}
	public void OnbtnBACKclicked()
	{
		SoundManager.Instance.PlayButtonClick ();
		//save current player id
		SaveDataManager.Instance.SetCurrentPlayerID ();
		Destroy (AvGameObjectUtils.FindGameObjectWithTagPlayer ());
		GameObject go =	AvGameObjectUtils.LoadGameObject (playerState, GameConstance.PathOfPlayerPrefabs+AvPlayerManager.Instance.CurrentPlayerID);
		GuiManager.Instance.HideDialog (gameObject);
		panelreplay.CheckShowIconNew ();

        //close dialog shop 
     
      
    }
}
