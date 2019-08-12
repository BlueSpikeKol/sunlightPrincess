using System.Linq;
using System.Collections;
using UnityEngine;

public class TileActivator : MonoBehaviour
{
	[SerializeField] float startDelay;
	[SerializeField] float rate;
	[SerializeField] bool activate;

	SpriteRenderer[] tiles;

	void Start()
	{
		tiles = GetComponentsInChildren<SpriteRenderer>(true);
		tiles = tiles.OrderBy(t => t.transform.localPosition.y).ToArray();
		tiles = tiles.Reverse().ToArray();
		StartCoroutine(Rollin());
	}

	IEnumerator Rollin()
	{
		yield return new WaitForSeconds(startDelay);

		int currentY = Mathf.FloorToInt(tiles[0].transform.localPosition.y);
		for (int i = 0; i < tiles.GetLength(0); i++)
		{
			tiles[i].gameObject.SetActive(activate);
			
			if (currentY != Mathf.FloorToInt(tiles[i].transform.localPosition.y))
			{
				currentY = Mathf.FloorToInt(tiles[i].transform.localPosition.y);
				yield return new WaitForSeconds(rate);
			}
		}
	}
}
