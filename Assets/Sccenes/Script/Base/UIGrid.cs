using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIGrid : MonoBehaviour {
	public int itemHeight = 50;
	public int itemWidth = 0;
	public Transform parent;
	private float width = 0;
	private int compensation = 20;
	// Use this for initialization
	void Start () {
		RectTransform r1 = (RectTransform)transform;
		r1.pivot = new Vector2 (0.5f, 1f);
		r1.anchorMin = new Vector2(0.5f, 0.5f);
		r1.anchorMax = new Vector2(0.5f, 0.5f);
		parent = transform.parent;
		RectTransform r = (RectTransform)parent.transform;
		width = r.rect.width;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Reposition()
	{
		if (itemWidth > 0) 
		{
			int amountItemWidth = Mathf.FloorToInt(width / itemWidth);
			int index = 0;
			int countItemWidth = 0;
			foreach(Transform child in gameObject.transform)
			{
				if(countItemWidth < amountItemWidth)
				{
					child.transform.localPosition = new Vector3(countItemWidth * itemWidth, -index * itemHeight, 0);
					countItemWidth++;
				}
				else
				{
					countItemWidth = 0;
					index++;
				}
			}
		}
		else
		{
			int index = 0;
			foreach(Transform child in gameObject.transform)
			{
				child.transform.localPosition = new Vector3(0, -index * itemHeight, 0);
				index++;
			}
			RectTransform r = (RectTransform)transform;
			r.sizeDelta = new Vector2(r.rect.width, gameObject.transform.childCount * itemHeight + compensation);
		}
	}
}
