using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSetTrigger : MonoBehaviour {
    public Animator animator;
    

    // Use this for initialization
    void Start () {
        animator.SetTrigger("FadeOut");

    }
	
	
}
