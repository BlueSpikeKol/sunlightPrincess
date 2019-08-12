using System.Linq;
using UnityEngine;
using SXP;

public class BackgroundLayout : MonoBehaviour
{
    Vector2 backgroundSize = new Vector2(8, 4);

    Transform camTransform;
    Transform CamTransform
    {
        get
        {
            if(camTransform == null)
            {
                camTransform = Camera.main.transform;
            }
            return camTransform;
        }
    }

    Vector2 camMinPos;
    Vector2 camMaxPos;

    void Start()
    {
        var sp = GetComponentInChildren<SpriteRenderer>();
        if(sp != null && sp.sprite != null)
        {
            backgroundSize = (Vector2)sp.sprite.bounds.extents;
        }

        camMinPos = new Vector2(float.MaxValue, float.MaxValue);
        camMaxPos = new Vector2(float.MinValue, float.MinValue);

        var transforms = GameObject.Find("TilesetsMapperObject").GetComponentsInChildren<Transform>();

        for(int i = 0; i < transforms.Length; i++)
        {
            if(transforms[i].GetComponent<RectTransform>() == null)
            {
                if(transforms[i].position.x < camMinPos.x) camMinPos.x = transforms[i].position.x;
                if(transforms[i].position.x > camMaxPos.x) camMaxPos.x = transforms[i].position.x;
                if(transforms[i].position.y < camMinPos.y) camMinPos.y = transforms[i].position.y;
                if(transforms[i].position.y > camMaxPos.y) camMaxPos.y = transforms[i].position.y;
            }
        }
    }

    private void LateUpdate()
    {
        UpdateBackground();
    }

    void UpdateBackground()
    {
        var camSize = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);

        transform.localScale = Vector3.one * Camera.main.orthographicSize * 0.6f;

        if(CamTransform.position.x < camMinPos.x) camMinPos.x = camTransform.position.x;
        if(CamTransform.position.y < camMinPos.y) camMinPos.y = camTransform.position.y;
        if(CamTransform.position.x > camMaxPos.x) camMaxPos.x = camTransform.position.x;
        if(CamTransform.position.y > camMaxPos.y) camMaxPos.y = camTransform.position.y;

        var camMin = camMinPos - new Vector2(40, 20);
        var camMax = camMaxPos + new Vector2(40, 20);

        var backgroundMinPos = camMin - camSize + backgroundSize * transform.lossyScale;
        var backgroundMaxPos = camMax + camSize - backgroundSize * transform.lossyScale;

        var normalizedPos = ((Vector2)CamTransform.position - camMin) / (camMax - camMin);

        Vector2 nextPos = new Vector2(
            Mathf.LerpUnclamped(backgroundMinPos.x, backgroundMaxPos.x, normalizedPos.x),
            Mathf.LerpUnclamped(backgroundMinPos.y, backgroundMaxPos.y, normalizedPos.y)
        );

        transform.position = nextPos;
    }
}
