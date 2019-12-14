using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItemPerso : MonoBehaviour
{
    [SerializeField] private string itemName;

    protected Collider2D myCollider;

    void Start()
    {
        Init();
    }

    /// <summary>
    /// Init this item.
    /// </summary>
    virtual public void Init()
    {
        myCollider = GetComponent<Collider2D>();
        if (myCollider == null)
        {
            Debug.LogError("An Item must be on the same GameObject as a Collider2D");
        }
    }

    void OnTriggeEnter2D(Collider2D other)
    {
        Debug.Log("Item collected: " + itemName);
        Destroy(this.gameObject);
    }
}
