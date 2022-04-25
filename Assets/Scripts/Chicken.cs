using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chicken : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    [SerializeField] private List<Transform> _targetsWay = new List<Transform>();
    private int _pointTarget;

    private void OnMouseDown()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(10,4), _speed * Time.deltaTime);
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _targetsWay[_pointTarget].transform.position) < 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetsWay[_pointTarget = Random.Range(0, 7)].transform.position, _speed * Time.deltaTime);
        }
    }

    public void GetTarget(Transform targetWay)
    {
        _targetsWay.Add(targetWay);
    }

    public void ClearListChickenList()
    {
        _targetsWay.Clear();
    }
}
