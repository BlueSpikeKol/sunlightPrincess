using UnityEngine;

public class Floatable : MonoBehaviour
{
    public bool isFloating;

    new Transform transform;
    new Rigidbody2D rigidbody;

    void Awake()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void ActivateFloating(bool _activate, float _waterEdge)
    {
        //if (isFloating == _activate) return;

        isFloating = _activate;

        if (isFloating)
        {
            float upGrav = -(Mathf.Abs(transform.position.y - _waterEdge)*0.1f);
            rigidbody.gravityScale = Mathf.Clamp(upGrav, -1, 0);
        }
        else
        {
            rigidbody.gravityScale = 1f;
        }

        
        rigidbody.gravityScale = _activate ? Mathf.Clamp(transform.position.y - _waterEdge, -1, 0) : 1;
    }
}
