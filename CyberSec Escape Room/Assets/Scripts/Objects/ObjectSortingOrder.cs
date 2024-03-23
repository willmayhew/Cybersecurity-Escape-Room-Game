using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSortingOrder : MonoBehaviour
{
    public SpriteRenderer objectSpriteRenderer;
    public SpriteRenderer playerSpriteRenderer;

    void Start()
    {
        if(objectSpriteRenderer == null)
        {
            objectSpriteRenderer = GetComponent<SpriteRenderer>();
        }
        
    }

    void Update()
    {
        if (transform.position.y < playerSpriteRenderer.transform.position.y)
        {
            objectSpriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder + 1;
        }
        else
        {
            objectSpriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder - 1;
        }
    }
}
