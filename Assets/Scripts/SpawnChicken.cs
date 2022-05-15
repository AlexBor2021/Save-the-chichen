using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnChicken : ObjectPool
{
    [SerializeField] private List<Wave> _waves = new List<Wave>();
    [SerializeField] private List<Transform> _targetWay = new List<Transform>();
    
    private Wave _currentWave;
    private float _pastTime;
    private int _indexWave = 0;
    private int _countChickenInBox = 0;
    private int _activeChikenNow = 0;

    //private void OnEnable()
    //{
    //    _currentWave.ChickenTemplate.ChickenInBox += SetCountChickenInBox;
    //}

    //private void OnDisable()
    //{
    //    _currentWave.ChickenTemplate.ChickenInBox -= SetCountChickenInBox;
    //}

    void Start()
    {
        SetWave(_indexWave);

        _currentWave.ChickenTemplate.ClearListChickenList();

        SetWayForChicken();

        Initialize(_currentWave.ChickenTemplate);
    }

    private void Update()
    {
        Debug.Log(_countChickenInBox);

        _pastTime += Time.deltaTime;

        if (TryGetGameObject(out _currentWave.ChickenTemplate) && _pastTime >= _currentWave.Delay)
        {
            if (_currentWave.CoundChicken != _activeChikenNow)
            {
                SetChicken(_currentWave.ChickenTemplate, transform.position);
                _pastTime = 0;
                _activeChikenNow++;
            }
        }

        if (_countChickenInBox == _currentWave.CoundChicken)
        {
            Time.timeScale = 0;
        }
    }

    private void SetWayForChicken()
    {
        for (int i = 0; i < _targetWay.Count; i++)
        {
            _currentWave.ChickenTemplate.GetTarget(_targetWay[i]);
        }
    }

    private void SetChicken(Chicken chicken, Vector2 spawnPostion)
    {
        chicken.gameObject.SetActive(true);
        chicken.transform.position = spawnPostion;
    }

    private void SetWave(int indexWave)
    {
        _currentWave = _waves[indexWave];
    }

    private void SetCountChickenInBox(int index)
    {
        _countChickenInBox += index;
    }

}

[System.Serializable]

public class Wave
{
    public Chicken ChickenTemplate;
    public int CoundChicken;
    public int Delay;
}
