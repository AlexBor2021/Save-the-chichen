using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _spawnChicken;

    private int _countChcken;

    private Transform _target;

    private void Start()
    {
        _target = _spawnChicken.transform.GetChild(0);
        _countChcken = _spawnChicken.transform.childCount;
    }

    private void Update()
    {
        if (_countChcken == _spawnChicken.transform.childCount)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        }
        else if (_countChcken == 0)
        {
            transform.position = transform.position;
        }
        else 
        {
            _countChcken = _spawnChicken.transform.childCount;
            _target = _spawnChicken.transform.GetChild(0);
        }

        
    }
}
