using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveDataManager : Singleton<SaveDataManager>
{
    private List<int> listK_Player = new List<int>();
    private Dictionary<int, int> listV_Player = new Dictionary<int, int>();
    /// <summary>
    /// Init this instance, listK_Player is list contain all id of player.
    /// </summary>
    public bool Init()
    {
        for (int i = 0; i < AvPlayerManager.Instance.PlayerCount; i++)
        {
            ConfigShopPlayerItems player = AvPlayerManager.Instance.ListConfigShopPlayerItems[i];
            listK_Player.Add(player.id);
        }
        return true;
    }
    /// <summary>
    /// Loads all.
    /// </summary>
    /// <returns><c>true</c>, if all was loaded, <c>false</c> otherwise.</returns>
    public bool LoadAll()
    {
        listV_Player.Clear();
        //load danh sach player 
        foreach (int name in listK_Player)
        {
            int valueOnLoaded = 0;// gia tri 0 co nghia la player nay chua mua (1 la mua roi)
            valueOnLoaded = PlayerPrefs.GetInt(name.ToString());
            if (name == GameConstance.PlayerIdDefault)//id default xem nhu la da mua roi
                valueOnLoaded = 1;
            listV_Player.Add(name, valueOnLoaded);
        }
        return true;
    }
    /// <summary>
    /// Saves all setting.
    /// </summary>
    public void SaveAllSetting()
    {
        foreach (int name in listK_Player)
        {
            int valueToSave = 0;
            listV_Player.TryGetValue(name, out valueToSave);
            PlayerPrefs.SetInt(name.ToString(), valueToSave);
        }
    }
    /// <summary>
    /// Saves the value with key.
    /// </summary>
    /// <param name="inKeyName">In key name.</param>
    /// <param name="inValue">In value.</param>
    public void SaveValueWithKey (int inKeyName, int inValue)
    {
        SetConfigureInfo(inKeyName, inValue);
        PlayerPrefs.SetInt(inKeyName.ToString(), inValue);
    }
    //==================================================
    //
    //==================================================
    public int GetConfigureInfo(int inKeyName)
    {
        int configureInfo = 0;
        listV_Player.TryGetValue(inKeyName, out configureInfo);
        return configureInfo;
    }
    //==================================================
    //set only, not save yet
    //==================================================
    public bool SetConfigureInfo(int inKeyName, int inValue)
    {
        bool isResult = false;
        if (listV_Player.ContainsKey(inKeyName))
        {
            listV_Player[inKeyName] = inValue;
            isResult = true;
        }
        return isResult;
    }
    /// <summary>
    /// Set/get the current player.
    /// </summary>
    public void SetCurrentPlayerID()
    {
        PlayerPrefs.SetInt(GameConstance.KeyCurrentPlayerID, AvPlayerManager.Instance.CurrentPlayerID);
    }

    public int GetCurrentPlayerID()
    {
        return PlayerPrefs.GetInt(GameConstance.KeyCurrentPlayerID, 100);
    }
    /// <summary>
    /// Set/get money.
    /// </summary>
    public void SaveMoney()
    {
        PlayerPrefs.SetFloat(GameConstance.moneyID, AvPlayerManager.Instance.Money);
    }
    public float GetMoney()
    {
        return PlayerPrefs.GetFloat(GameConstance.moneyID);
    }
    /// <summary>
    /// Saves/get the tutoial.
    /// </summary>
    /// <returns><c>true</c>, if tutoial was saved, <c>false</c> otherwise.</returns>
    public bool SaveTutoial()
    {
        PlayerPrefs.SetString(GameConstance.kTutorial, "true");
        return true;
    }
    public bool GetTutorial()
    {
        string s = PlayerPrefs.GetString(GameConstance.kTutorial, "");
        if (s == "true")
            return true;
        return false;
    }
    /// <summary>
    /// Saves/Get the name of the user.
    /// </summary>
    /// <returns><c>true</c>, if user name was saved, <c>false</c> otherwise.</returns>
    /// <param name="name">Name.</param>
    public bool SaveUserName(string name)
    {
        string s = Base64Encode(name);
        //Debug.LogError ("encode: "+s);
        PlayerPrefs.SetString(GameConstance.kUserName, s);
        return true;
    }
    public string GetUserName()
    {
        return PlayerPrefs.GetString(GameConstance.kUserName, "");
    }
    /// <summary>
    /// Base64s the encode.
    /// </summary>
    /// <returns>The encode.</returns>
    /// <param name="plainText">Plain text.</param>
    public string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
    /// <summary>
    /// Base64s the decode.
    /// </summary>
    /// <returns>The decode.</returns>
    /// <param name="base64EncodedData">Base64 encoded data.</param>
    public string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
    /// <summary>
    /// Saves/get the sound.
    /// </summary>
    /// <returns><c>true</c>, if sound was saved, <c>false</c> otherwise.</returns>
    /// <param name="value">Value.</param>
    public bool SaveSound(int value)
    {
        PlayerPrefs.SetInt(GameConstance.kSound, value);
        return true;
    }
    public bool GetSound()
    {
        if (PlayerPrefs.GetInt(GameConstance.kSound, 1) == 1)
            return true;
        return false;
    }
    /// <summary>
    /// Saves/get the BGM.
    /// </summary>
    /// <returns><c>true</c>, if sound was saved, <c>false</c> otherwise.</returns>
    /// <param name="value">Value.</param>
    public bool SaveBGM(int value)
    {
        PlayerPrefs.SetInt(GameConstance.kBgm, value);
        return true;
    }
    public bool GetBGM()
    {
        if (PlayerPrefs.GetInt(GameConstance.kBgm, 1) == 1)
            return true;
        return false;
    }
}
