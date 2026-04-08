using UnityEngine;
using System.Collections;

public class PlatformItem : MonoBehaviour
{
    public GameObject barrier;
    public GameObject itemBonus;
    public GameObject itemChangeCamra;
    public bool isFall = false;
    public bool isCreate = false;
    private float _nexPos;
    private const float pos1 = 1.47f, pos2 = 0.075f;
    public void Init()
    {
        if (barrier != null)
        {
            //check neu lan dau thi tutorials
            if (!SaveDataManager.Instance.GetTutorial())
            {
                PlatformManager.countPos += 1;

                //Debug.LogError(PlatformManager.countPos);
                GameObject newBarrier = Instantiate(barrier) as GameObject;
                newBarrier.transform.SetParent(gameObject.transform);
                if (PlatformManager.countPos == 1 || PlatformManager.countPos == 3)
                {
                    newBarrier.transform.position = new Vector3(transform.position.x, newBarrier.transform.position.y + 4, pos2);
                    newBarrier.GetComponent<BarrierItem>().Init(newBarrier.transform.position, transform.position.y);
                }
                else
                {
                    newBarrier.transform.position = new Vector3(transform.position.x, newBarrier.transform.position.y + 4, pos1);
                    newBarrier.GetComponent<BarrierItem>().Init(newBarrier.transform.position, transform.position.y);
                }
            }
            else
            {
                int rand = UnityEngine.Random.Range(0, 10);
                if (rand >= 3)
                {
                    GameObject newBarrier = Instantiate(barrier) as GameObject;
                    newBarrier.transform.SetParent(gameObject.transform);
                    if (rand >= 6)
                    {//pos 1
                        newBarrier.transform.position = new Vector3(transform.position.x, newBarrier.transform.position.y + 4, pos1);
                        newBarrier.GetComponent<BarrierItem>().Init(newBarrier.transform.position, transform.position.y);
                        if (rand == 6)
                        { //truong hop nay randdom itemchangecamera
                            int r = UnityEngine.Random.Range(0, 5);
                            if (r == 1)
                            {
                                GameObject itemChange = Instantiate(itemChangeCamra) as GameObject;
                                itemChange.transform.SetParent(gameObject.transform);
                                itemChange.transform.position = new Vector3(transform.position.x, itemChange.transform.position.y, pos2);
                            }
                        }
                    }
                    else
                    { //pos 2
                        newBarrier.transform.position = new Vector3(transform.position.x, newBarrier.transform.position.y + 4, pos2);
                        newBarrier.GetComponent<BarrierItem>().Init(newBarrier.transform.position, transform.position.y);
                        if (rand == 3)
                        { //truong hop nay randdom itemchangecamera
                            int r = UnityEngine.Random.Range(0, 5);
                            if (r == 1)
                            {
                                GameObject itemChange = Instantiate(itemChangeCamra) as GameObject;
                                itemChange.transform.SetParent(gameObject.transform);
                                itemChange.transform.position = new Vector3(transform.position.x, itemChange.transform.position.y, pos1);
                            }
                        }
                    }
                }
                else if (rand == 1)
                {//truong hop nay random item bonus
                    if (PlayerState.Instance.playerCurrState != State.AutoRun)
                    {// neu dang chay auto thi hk tao nua
                        int r = UnityEngine.Random.Range(0, 5);
                        if (r == 1)
                        {
                            GameObject item = Instantiate(itemBonus) as GameObject;
                            item.transform.SetParent(gameObject.transform);
                            item.transform.position = new Vector3(transform.position.x, item.transform.position.y, transform.position.z);
                        }
                    }
                }
            }
        }
        else
            Debug.LogError(" barrier was null");
    }
    public void InitNewPlatform(float nexPos)
    {
        _nexPos = nexPos;
    }
    void Update()
    {
        if (transform.position.x > _nexPos)
        {
            transform.position -= new Vector3(.1f, 0, 0);
        }
        else
            //lam tron vi tri cho de tinh sau nay
            transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
        if (isFall)
        {
            //Debug.LogError("dfghrt");
            gameObject.transform.position -= new Vector3(0f, .07f, 0f);
        }
    }
}
