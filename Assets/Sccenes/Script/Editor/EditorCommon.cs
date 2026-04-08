

using UnityEngine;

using System.Collections;
using UnityEditor;
public class EditorCommon : MonoBehaviour
{

    [MenuItem("CaoLong/Clear All Prefs")]
    static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Clear all Prefabs success");
    }
}
