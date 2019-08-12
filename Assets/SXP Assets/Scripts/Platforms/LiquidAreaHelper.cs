using UnityEngine;
using PlatformerPro;

public class LiquidAreaHelper : MonoBehaviour
{
    public Vector2 size = Vector2.one;

    public BoxCollider2D Collider
    {
        get
        {
            return transform.GetOrAddComponent<BoxCollider2D>();
        }
    }

    public SpriteRenderer AreaRenderer
    {
        get
        {
            var t = transform.Find("AreaSprite");

            if(t != null)
            {
                return t.GetComponent<SpriteRenderer>();
            }
            else
            {
                return null;
            }
        }
    }

    public SpriteRenderer SurfaceRenderer
    {
        get
        {
            var t = transform.Find("SurfaceSprite");

            if(t != null)
            {
                return t.GetComponent<SpriteRenderer>();
            }
            else
            {
                return null;
            }
        }
    }
}
