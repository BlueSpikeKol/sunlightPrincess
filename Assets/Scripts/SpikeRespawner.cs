using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPro;
using System;

public class SpikeRespawner : Hazard {

    public GameObject Character;
    public GameObject Respawner;
    


    protected override void DoHit(Collider2D other)
    {
        base.DoHit(other);
        StartCoroutine(DoMove());
        
    }

    IEnumerator DoMove()
    {
        yield return new WaitForSeconds(0.8f);
        Character.transform.position = Respawner.transform.position;
    }
}
