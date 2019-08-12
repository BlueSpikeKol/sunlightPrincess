using UnityEngine;

using PlatformerPro;

public class DoorSet_Locker : MonoBehaviour
{
    [SerializeField] DoorSet myDoorset;

    void OnTriggerEnter2D(Collider2D other)
    {
        var characterRef = other.gameObject.GetComponent<ICharacterReference>();

        if(characterRef != null)
        {
            myDoorset.TryOpenDoor();
        }
    }
}
