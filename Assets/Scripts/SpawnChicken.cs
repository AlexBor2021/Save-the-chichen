using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpawnChicken : ObjectPool
{
    [SerializeField] private List<Wave> _waves = new List<Wave>();
    [SerializeField] private List<Transform> _targetWay = new List<Transform>();
    [SerializeField] private Slider _barCaptureChicken;


    private Wave _currentWave;
    private float _pastTime;
    private int _indexWave = 0;
    private int _countChickenInBox = 0;
    private int _activeChikenNow = 0;
    

    void Start()
    {
        SetWave(_indexWave);

        _currentWave.ChickenTemplate.ClearListChickenList();

        Chicken.ChickenInBox += SetCountChickenInBox;

        SetWayForChicken();

        Initialize(_currentWave.ChickenTemplate);
    }

    private void Update()
    {
        _pastTime += Time.deltaTime;

        if (TryGetGameObject(out _currentWave.ChickenTemplate) && _pastTime >= _currentWave.Delay)
        {
            if (_currentWave.CountChicken != _activeChikenNow)
            {
                SetChicken(_currentWave.ChickenTemplate, transform.position);
                _pastTime = 0;
                _activeChikenNow++;
            }
        }

        if (_countChickenInBox == _currentWave.CountChicken )
        {
            Time.timeScale = 0;
        }

        SetCaptudeBar(_countChickenInBox);
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

    private void SetCaptudeBar(int countChickenInBox)
    {
        _barCaptureChicken.maxValue = _currentWave.CountChicken;
        _barCaptureChicken.value = _countChickenInBox;
    }


    private void OnDisable()
    {
        Chicken.ChickenInBox -= SetCountChickenInBox;
    }
}

[System.Serializable]

public class Wave
{
    public Chicken ChickenTemplate;
    public float CountChicken;
    public int Delay;
}
