using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class GenerationChicken : ObjectPool
{
    [SerializeField] private List<Wave> _waves = new List<Wave>();
    [SerializeField] private List<Transform> _targetWay = new List<Transform>();

    private SettingMenu _settingMenu;
    private Wave _currentWave;
    private float _pastTime;
    private int _indexWave = 0;
    private float timer = 0;
    
    public int IndexWave => _indexWave;
    public Wave CurrentWave => _currentWave;

    private void Start()
    {
        _settingMenu = GetComponentInChildren<SettingMenu>();

        SetWave(_indexWave);

       _currentWave.MoveChicken.ClearWayList();

        SetWayForChicken();

        InitializeChicken(_currentWave.ChickenTemplate);

    }

    private void Update()
    {
        SetChickenWave();

        Exit—urrentWave();
    }

    public void ZeroingLevel()
    {
        TurnOffChickenTemplate();
        _countChickenInBox = 0;
        _activeChikenNow = 0;
    }

    public void SetWave(int indexWave)
    {
        _currentWave = _waves[indexWave];
    }

    private void SetWayForChicken()
    {
        for (int i = 0; i < _targetWay.Count; i++)
        {
            _currentWave.MoveChicken.GetTarget(_targetWay[i]);
        }
    }

    private void SetChicken(Chicken chicken, Vector2 spawnPostion)
    {
        chicken.gameObject.SetActive(true);
        chicken.transform.position = spawnPostion;
    }

    private void SetChickenWave()
    {
        _pastTime += Time.deltaTime;

        if (TryGetGameObject(out _currentWave.ChickenTemplate) && _pastTime >= _currentWave.Delay && Time.timeScale > 0)
        {
            if (_currentWave.CountChicken != _activeChikenNow)
            {
                SetChicken(_currentWave.ChickenTemplate, transform.position);
                _pastTime = 0;
                _activeChikenNow++;
            }
        }
    }

    private void Exit—urrentWave()
    {
        if (_countChickenInBox == _currentWave.CountChicken)
        {
            timer += Time.deltaTime;

            if (timer >= 1)
            {
                _settingMenu.EndWave();
                ZeroingLevel();
                timer = 0;
                Time.timeScale = 0;
            }
        }
    }
}

[System.Serializable]

public class Wave
{
    public Chicken ChickenTemplate;
    public MoveChicken MoveChicken;
    public float CountChicken;
    public int Delay;
}
