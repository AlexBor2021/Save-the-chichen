using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChicken : ObjectPool
{
    [SerializeField] private Chicken _chicken;
    [SerializeField] private float _countChickenActive;
    [SerializeField] private float _timeBetweenSpawns;
    [SerializeField] private List<Transform> _targetWay = new List<Transform>();

    private float _pastTime;

    void Start()
    {
        _chicken.ClearListChickenList();

        for (int i = 0; i < _targetWay.Count; i++)
        {
            _chicken.GetTarget(_targetWay[i]);
        }

        Initialize(_chicken);
    }

    private void Update()
    {
        _pastTime += Time.deltaTime;

        if (TryGetGameObject(out _chicken) && _pastTime >= _timeBetweenSpawns)
        {
            if (_countChickenActive > 0)
            {
                SetChicken(_chicken, transform.position);
                _pastTime = 0;
                _countChickenActive--;
            }
        }
    }

    private void SetChicken(Chicken chicken, Vector3 spawnPostion)
    {
        chicken.gameObject.SetActive(true);
        _chicken.transform.position = spawnPostion;
    }
}
