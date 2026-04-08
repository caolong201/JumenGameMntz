using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemShopPlayer : MonoBehaviour {

	public Image avatar;
	public Text name;
	public Text txtMoney;
	public Text playerCurrMoney;
	public GameObject btnBuy;
	public GameObject checkIcon;
	public GameObject selected;
	public int mId;
	private int idCurrSelected;
	private float mMoney;
	public void Init(int id, string name, float money)
	{
		//load avatar
		//=======================
		avatar.sprite = AvGameObjectUtils.LoadSpriteInAsset (GameConstance.PathOfAvatarPlayer+id);
		this.name.text = name;
		txtMoney.text = money.ToString ();
		this.mId = id;
		this.mMoney = money;
		if (AvPlayerManager.Instance.CurrentPlayerID == id) {
			selected.SetActive (true);
		} else
			selected.SetActive (false);
		if(SaveDataManager.Instance.GetConfigureInfo (id) == 0)// chua mua
		{
			btnBuy.SetActive(true);
			checkIcon.SetActive(false);
		}
		else
		{
			btnBuy.SetActive(false);
			checkIcon.SetActive(true);
		}
	}
	public void OnbtnBUYClicked()
	{
		//kiem tra xem co du tien hk trc
		if(AvPlayerManager.Instance.Money >= this.mMoney)
		{
			btnBuy.SetActive(false);
			checkIcon.SetActive(true);
			//select item
			SelectItem();			
			AvPlayerManager.Instance.CurrentPlayerID = mId;
			SaveDataManager.Instance.SaveValueWithKey (mId,1);
			//tinh lai tien
			float m = AvPlayerManager.Instance.Money - this.mMoney;
			//iTween.PunchScale(playerCurrMoney.gameObject, new Vector3(.5f,.5f,1), 2f);
			//iTween.ValueTo(gameObject, iTween.Hash(
			//	"from",AvPlayerManager.Instance.Money,
			//	"to",m,
			//	"time", 0.5f,
			//	"onupdate", "UpdateValue"
			//	));
			AvPlayerManager.Instance.Money = m;
			GuiManager.Instance.InitMoney ();
		}
		else
		{
            //ShowMessageBox.DG
            //show message boxx      
            GuiManager.Instance.ShowBlackBolder();
			GuiManager.Instance.ShowMessageBox("Infomation","Dont have enought money!\nYou can get money by Wacth movies!",MessageBoxType.OK,()=>{
			});
		}

	}
	void UpdateValue( float val ) {
		playerCurrMoney.text = ((int)val).ToString();
	}

	public void OnItemClicked()
	{
		if (SaveDataManager.Instance.GetConfigureInfo(mId)==1) 
		{
			SelectItem ();
			AvPlayerManager.Instance.CurrentPlayerID = mId;
		}
	}
	private void SelectItem()
	{
		//enanble off backgroud before
		for(int i =0; i <transform.parent.transform.childCount;i++)
		{
			//Debug.Log("fgerg"+transform.parent.transform.GetChild(i).GetComponent<ItemShopPlayer>().mId);
			//Debug.Log("av"+AvPlayerManager.Instance.CurrentPlayerID);
			if(transform.parent.transform.GetChild(i).GetComponent<ItemShopPlayer>().mId ==AvPlayerManager.Instance.CurrentPlayerID)
			{
				GameObject obj =transform.parent.transform.GetChild(i).Find("Selected").gameObject;
				if(obj.activeSelf==true)
					obj.SetActive(false);
				//Debug.Log("bgfn");
			}
		}
		//enable on backgroud selected
		selected.SetActive (true);
	}
	 
}
