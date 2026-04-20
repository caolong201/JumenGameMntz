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
                    }
                    else
                    { //pos 2
                        newBarrier.transform.position = new Vector3(transform.position.x, newBarrier.transform.position.y + 4, pos2);
                        newBarrier.GetComponent<BarrierItem>().Init(newBarrier.transform.position, transform.position.y);
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
            gameObject.transform.position -= new Vector3(0f, .14f, 0f);
        }
    }
}
