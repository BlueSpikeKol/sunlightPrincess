using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerEnter : MonoBehaviour
{
    public Animator animator;
    public string sceneName;
    

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
            FadeToLevel(sceneName);            
        }
        

    }

    public void FadeToLevel(string name) {
        

            animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(name);

    }
}
