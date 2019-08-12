using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundActivator : MonoBehaviour
{
	[SerializeField] float startDelay;
	[SerializeField] float fadeSpeed;
	[SerializeField] bool activate;
	[SerializeField] SpriteRenderer background;
	
	void Start()
	{
		StartCoroutine(Fade());
	}

	IEnumerator Fade()
	{
		float progress = 0f;
		Color c = background.color;
		c.a = activate ? 0f : 1f;
		background.color = c;
		
		yield return new WaitForSeconds(startDelay);
		
		while (progress < 1f)
		{
			progress += Time.deltaTime * fadeSpeed;
			c.a = activate ? progress : 1f - progress;
			background.color = c;
			yield return null;
		}
		
		background.color = new Color(background.color.r, background.color.g, background.color.b, 1f);
	}
}
