using UnityEngine;
using System.Collections;
using PlatformerPro;
using SXP;

public class YKillTrigger : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") ||
                other.gameObject.layer == LayerMask.NameToLayer("PlayerHitObject"))
        {
            FindObjectOfType<CharacterHealth>().Damage(1000);
        }
    }
}
