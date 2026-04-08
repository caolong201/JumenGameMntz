using UnityEngine;
using System.Collections;

public enum TutorialState
{
    None,
    Step1,
    Step2,
    Step3,
    Congratulation
}
public class TutorialsManager : SingletonMono<TutorialsManager>
{
    public TutorialStep1 step1;
    public TutorialStep2 step2;
    public TutorialStep3 step3;
    public GameObject congratulation;
    [HideInInspector]
    public int
        currentStepCount = 0;
    [HideInInspector]
    public bool
        isTutorialing = false;
    [HideInInspector]
    public bool
        isCreated = false;
    GameObject ObjStep1;
    GameObject ObjStep2;
    GameObject ObjStep3;
    GameObject congratu;
    [HideInInspector]
    public TutorialState tutorialSate;
    // Use this for initialization
    void Start()
    {
        if (!SaveDataManager.Instance.GetTutorial())
        {//chua tutorial
            isTutorialing = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTutorialing == true)
        {
            if (currentStepCount == 0 && !isCreated)
            {
                isCreated = true;
                ObjStep1 = LoadgameObject(step1.gameObject);
                RectTransform rect = ObjStep1.GetComponent<RectTransform>();
                rect.offsetMin = new Vector2(320, 0);
                rect.offsetMax = new Vector2(0, 0);

                tutorialSate = TutorialState.Step1;
            }
            else
            if (currentStepCount == 2 && !isCreated)
            {
                isCreated = true;
                Destroy(ObjStep1);
                ObjStep2 = LoadgameObject(step2.gameObject);
                RectTransform rect = ObjStep2.GetComponent<RectTransform>();
                rect.offsetMin = new Vector2(0, 0);
                rect.offsetMax = new Vector2(-320, 0);

                tutorialSate = TutorialState.Step2;
            }
            else
            if (currentStepCount == 5 && !isCreated)
            {
                isCreated = true;
                Destroy(ObjStep2);
                ObjStep3 = LoadgameObject(step3.gameObject);
                RectTransform rect = ObjStep3.GetComponent<RectTransform>();
                rect.offsetMin = new Vector2(320, 0);
                rect.offsetMax = new Vector2(0, 0);

                tutorialSate = TutorialState.Step3;
            }
            else
                if (currentStepCount == 6)
            {
                Destroy(ObjStep3);
                congratu = LoadgameObject(congratulation);
                RectTransform rect = congratu.GetComponent<RectTransform>();
                rect.offsetMin = new Vector2(0, 0);
                rect.offsetMax = new Vector2(160, 30);
                Destroy(congratu, 1f);
                tutorialSate = TutorialState.Congratulation;
                GuiManager.Instance.ShowFloatingText("+5$");
                AvPlayerManager.Instance.Money += 5;
                isTutorialing = false;
                SaveDataManager.Instance.SaveTutoial();
            }

        }
    }

    private GameObject LoadgameObject(GameObject obj)
    {
        GameObject ObjStep = Instantiate(obj.gameObject) as GameObject;
        ObjStep.transform.SetParent(transform);
        ObjStep.transform.localScale = Vector3.one;
        ObjStep.transform.localScale = Vector3.one;
        return ObjStep;
    }
}
