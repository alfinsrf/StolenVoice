using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public bool moveToLeft;

    public float speed = 1f;
    private Vector3 startPos;
    private float tileSize;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        tileSize = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float newPosition = Mathf.Repeat(Time.fixedTime * speed, tileSize);

        if (moveToLeft == false)
        {
            transform.position = startPos + Vector3.right * newPosition;
        }
        else if (moveToLeft == true)
        {
            transform.position = startPos + Vector3.left * newPosition;
        }
    }
}
