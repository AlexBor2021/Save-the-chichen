using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    void Start()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(10, -10), 4 * Time.deltaTime);
    }

    
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(10, -10), 4 * Time.deltaTime);
    }
}
