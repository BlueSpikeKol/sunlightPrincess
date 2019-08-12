using UnityEngine;

using PlatformerPro;

public class DoorSet_Key : MonoBehaviour
{
    [SerializeField] DoorSet myDoorset;

    void OnTriggerEnter2D(Collider2D other)
    {
        var characterRef = other.gameObject.GetComponent<ICharacterReference>();

        if(characterRef != null)
        {
            myDoorset.PlayerHaveKey = true;
            Destroy(this.gameObject);
        }
    }
}