using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPro;
using System;

public class SpikeRespawner : Hazard {

    public GameObject Character;
    public GameObject Respawner;
    public Animator animator;
    private float timer = 0.8f;

    



    protected override void DoHit(Collider2D other)
    {
        base.DoHit(other);
        StartCoroutine(DoMove());
        

    }

    IEnumerator DoMove()
    {
        animator.SetBool("attente", false);
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(timer);
        Character.transform.position = Respawner.transform.position;
        if (Character.transform.position == Respawner.transform.position)
        {
            animator.SetTrigger("FadeIn");
            animator.SetBool("attente", true);

        }
    
    }
    

}
