using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(10, -10), 4 * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(10, -10), 4 * Time.deltaTime);
    }
}
