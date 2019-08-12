using System.Collections;
using System.Linq;
using UnityEngine;

public class ItemsActivator : MonoBehaviour
{
	[SerializeField] float startDelay;
	[SerializeField] float secondsPerIncrement;
	[SerializeField] float xIncrement;

	CineItem[] items;

	void Start()
	{
		items = GetComponentsInChildren<CineItem>(true);
		Debug.Log("ItemCount:" + items.Length);
		items = items.OrderBy(i => i.transform.localPosition.x).ToArray();
		StartCoroutine(Activate());
	}

	IEnumerator Activate()
	{
		int index = 0;
		float currentX = items[0].transform.localPosition.x;
		
		yield return new WaitForSeconds(startDelay);
		while (index < items.Length)
		{
			if (items[index].transform.localPosition.x <= currentX)
			{
				items[index].gameObject.SetActive(true);
				index++;
			}

			currentX += xIncrement;
			yield return new WaitForSeconds(secondsPerIncrement);
		}
	}
}
