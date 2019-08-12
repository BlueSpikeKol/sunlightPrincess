using System;
using UnityEngine;

public class WaterMove : MonoBehaviour
{
    [SerializeField] float height;
    [SerializeField] float speed;

    float progress = 0;
    bool shouldMove;

    Vector3 startPos;
    Vector3 endPos;

    new Transform transform;

    void Awake()
    {
        transform = GetComponent<Transform>();
    }

    void OnEnable()
    {
        progress = 0;
        shouldMove = Math.Abs(transform.position.y - height) > 0.05f;

        startPos = transform.position;
        endPos = new Vector3(startPos.x, height, startPos.z);
    }

    void Update()
    {
        if (shouldMove)
        {
            progress += Time.deltaTime*speed;
            transform.position = Vector3.Lerp(startPos, endPos, progress);

            if (progress >= 1)
            {
                transform.position = endPos;
                shouldMove = false;
            }
        }
        else
        {
            this.enabled = false;
        }
    }
}
