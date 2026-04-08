using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using DG.Tweening;
//using System.Diagnostics.Eventing.Reader;

public enum DirecShowDialog
{
    None,
    Left,
    Right,
    Top,
    Bottom
}
public class GuiManager : SingletonMonoAwake<GuiManager>
{


    public GameObject iconAd;
    public ScreenFader fader;
    public GameObject loading;
    public GameObject blackBolder;
    public GameObject panelReplay;
    public Text lbScore;

    public GameObject dialogShop;
    public GameObject panelStart;
    public GameObject playerState;
    public MessageBoxDlg messagebox;
    public FloatingText floatText;

    public Text lblScore, lblHighScore, lblMoney;
    Animator ani;
    bool isSettingOpen = false;

    public override void OnAwake()
    {
        base.OnAwake();
        ShowDialog(panelStart, false);
        PlayerState.Instance.playerCurrState = State.Idle;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //show score
        if (lbScore != null)
        {
            lbScore.text = ScoreManager.Instance.Score.ToString();

        }

        if (PlayerState.Instance.playerCurrState == State.Death)
        {
            PlayerState.Instance.playerCurrState = State.Idle;
            ShowPanelRePlay();
        }
    }


  


    public void InitScore()
    {
        float initalScore = ScoreManager.Instance.Score;
        DOTween.To(() => 0, x => UpdateScoreValue(x), initalScore, 1.5f);
    }
    void UpdateScoreValue(float val)
    {
        lblScore.text = ((int)val).ToString();
    }

    ///==========high score=======
    public void InitHighScore()
    {
        float initalHighScore = ScoreManager.Instance.HighScore;
        DOTween.To(() => 0, x => UpdateHighScoreValue(x), initalHighScore, 1.5f);

    }
    void UpdateHighScoreValue(float val)
    {
        lblHighScore.text = ((int)val).ToString();
    }
    ///==========money=======
    public void InitMoney()
    {
        float initialMoney = AvPlayerManager.Instance.Money; 
        DOTween.To(() => 0, x => UpdateMoneyValue(x), initialMoney, 1.5f);
    }
    void UpdateMoneyValue(float val)
    {
        lblMoney.text = ((int)val).ToString();
    }
    public void CheckShowNewIcon(GameObject obj)
    {
        //====== icon !========
        if (obj != null)
            if (IsShowIconNew())
                obj.SetActive(true);
            else
                obj.SetActive(false);
    }
    /// <summary>
    /// Determines whether this instance is show icon new.
    /// </summary>
    /// <returns><c>true</c> if this instance is show icon new; otherwise, <c>false</c>.</returns>
    private bool IsShowIconNew()
    {

        for (int i = 0; i < AvPlayerManager.Instance.PlayerCount; i++)
        {
            //=====check show icon new========
            ConfigShopPlayerItems configItem = AvPlayerManager.Instance.ListConfigShopPlayerItems[i];
            if (AvPlayerManager.Instance.Money >= configItem.money && SaveDataManager.Instance.GetConfigureInfo(configItem.id) == 0)//0 chua mua
                return true;
        }
        return false;

    }
    /// <summary>
    /// Shows the black bolder.
    /// </summary>
    public void ShowBlackBolder()
    {
        if (blackBolder == null)
        {
            Debug.LogError("Black bolder was null");
            return;
        }
        blackBolder.SetActive(true);
    }
    /// <summary>
    /// Hides the black bolder.
    /// </summary>
    public void HideBlackBolder()
    {
        if (blackBolder == null)
        {
            Debug.LogError("Black bolder was null");
            return;
        }
        blackBolder.SetActive(false);
    }
    public void ShowIconAds()
    {
        if (iconAd == null)
        {
            Debug.LogError("Null ads icon");
            return;
        }
        //iTween.MoveTo(iconAd, iTween.Hash("x", 250, "islocal", true, "time", 0.5f, "loopType", "none"
        //                                 , "delay", 1, "easeType", "easeInOutQuad"));

    }
    public void HideIconAds()
    {
        if (iconAd == null)
        {
            Debug.LogError("Null ads icon");
            return;
        }
        iconAd.transform.DOLocalMoveX(0, 0.25f);

        //iTween.MoveTo(iconAd, iTween.Hash("x", 600, "islocal", true, "time", 0.5f, "loopType", "none"
        //                                 , "delay", 1, "easeType", "easeInOutQuad"));
    }

    public void OnbtnAdClicked()
    {
        HideIconAds();
    }
    private void ShowPanelRePlay()
    {
        lbScore.gameObject.SetActive(false);
        panelReplay.SetActive(true);
        if (ani == null)
            ani = panelReplay.GetComponent<Animator>();
        ani.SetBool("PanelShow", true);
        panelReplay.GetComponent<PanelRePlayDialog>().Init();

    }
    public void OnbtnRePlayClicked()
    {
        GuiManager.Instance.Hideloading();
        SoundManager.Instance.PlayButtonClick();
        ScoreManager.Instance.Score = 0;
        fader.ScreenFadeOut(0);
        StartCoroutine(WaitingClosePanelReplay(.3f));
    }
    IEnumerator WaitingClosePanelReplay(float time)
    {
        yield return new WaitForSeconds(time);
        panelReplay.SetActive(false);
        lbScore.gameObject.SetActive(true);
    }
    public void OnbtnSettingClicked()
    {
        SoundManager.Instance.PlayButtonClick();
        isSettingOpen = !isSettingOpen;
        ani.SetBool("SettingShow", isSettingOpen);
        if (isSettingOpen == false)
        {

        }
    }
    public void OnbtnPLAYclicked()
    {
        SoundManager.Instance.PlayButtonClick();
        HideDialog(panelStart, false);
        PlayerState.Instance.playerCurrState = State.Live;
    }
    /// <summary>
    /// Shows the dialog.
    /// </summary>
    public void ShowDialog(GameObject dialog, bool tween = true, float time = .5f, float delay = 0)
    {
        if (dialog.activeSelf == false)
            dialog.SetActive(true);
        dialog.transform.DOLocalMoveX(0, 0.25f);
      

        //if(tween)
        //{
        //	iTween.MoveTo(dialog,iTween.Hash("x",0,"islocal", true,"time",time,"loopType","none"
        //	                                 ,"delay",delay,"easeType","easeInOutQuad"));
        //}

    }
    /// <summary>
    /// Hides the dialog.
    /// </summary>
    public void HideDialog(GameObject dialog, bool tween = true, bool hide = true, float time = .5f, float delay = 0, string axis = "x", float pos = -700f)
    {
        if (tween)
        {
            //iTween.MoveTo(dialog,iTween.Hash(axis,pos,"islocal", true,"time",time,"loopType","none"
            //                                 ,"delay",delay,"easeType","easeInOutQuad"));
            //iTween.MoveTo(dialog,iTween.Hash(axis,pos,"islocal", true,"time",time,"loopType","none"
            //                                 ,"delay",delay,"easeType","easeInOutQuad"));
            StartCoroutine(OnCompletedHidedialog(time, dialog));
        }
        else if (hide)
            dialog.SetActive(false);


    }
    IEnumerator OnCompletedHidedialog(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
    public void ShowLoading()
    {
        if (loading == null)
        {
            Debug.LogError("Loading was null");
            return;
        }
        loading.SetActive(true);
    }
    public void Hideloading()
    {
        if (loading == null)
        {
            Debug.LogError("Loading was null");
            return;
        }
        else if (loading.activeSelf == true)
            loading.SetActive(false);
    }
    /// <summary>
    /// Shows the message box.
    /// </summary>
    public void ShowMessageBox(string title, string content, MessageBoxType type, System.Action okCallback = null, System.Action cancelCallback = null)
    {
        messagebox.gameObject.SetActive(true);
        messagebox.title.text = title;
        messagebox.content.text = content;
        messagebox.type = type;
        messagebox.OkCallback = okCallback;
        messagebox.CancelCallback = cancelCallback;
        messagebox.transform.DOLocalMoveX(0, 0.25f);
        //iTween.MoveTo(messagebox.gameObject,iTween.Hash("y",0,"islocal", true,"time",.3f,"loopType","none"
        //	                                 ,"delay",0,"easeType","easeInOutQuad"));
    }
    /// <summary>
    /// Hides the message box.
    /// </summary>
    public void HideMessageBox(bool tween = true, bool hide = true, float time = .5f, float delay = 0, string axis = "x", float pos = -700f)
    {
        HideDialog(messagebox.gameObject, tween, hide, time, delay, axis, pos);
    }
    /// <summary>
    /// Shows the floating text.
    /// </summary>
    /// <param name="content">Content.</param>
    public void ShowFloatingText(string content)
    {
        floatText.gameObject.SetActive(false);
        floatText.content.text = content;
        floatText.gameObject.SetActive(true);
    }
    public void OnbtnSHOPclicked()
    {
        SoundManager.Instance.PlayButtonClick();
        ShowDialog(dialogShop);
        dialogShop.GetComponent<ShopPlayerDialog>().Init();
    }

}


































