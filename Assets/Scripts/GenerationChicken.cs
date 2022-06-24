using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(SettingMenu))]

public class GenerationChicken : ObjectPool
{
    [SerializeField] private List<Wave> _waves = new List<Wave>();
    [SerializeField] private List<Transform> _targetWay = new List<Transform>();

    private SettingMenu _settingMenu;
    private Wave _currentWave;
    private Coroutine _setChickenWave;
    private int _indexWave = 0;
    private float timer = 0;
    private float _delay = 1; 
    private bool _workCorotine; 

    public int IndexWave => _indexWave;
    public Wave CurrentWave => _currentWave;

    private void OnEnable()
    {
        _settingMenu = GetComponentInChildren<SettingMenu>();
        _workCorotine = true;
    }

    private void Start()
    {
        SetWave(_indexWave);

       _currentWave.MoveChicken.ClearWayList();

        SetWayForChicken();

        InitializeChicken(_currentWave.ChickenTemplate);

        ActivateCorotineWave();
    }

    private void Update()
    {
        ExitÑurrentWave();
    }

    public void ActivateCorotineWave()
    {
        _workCorotine = true;
        _setChickenWave = StartCoroutine(SetChickenWave());
    }

    public void StopCorotineWave()
    {
        _workCorotine = false;
        StopCoroutine(_setChickenWave);
    }

    public void ZeroingLevel()
    {
        TurnOffChickenTemplate();
        CountChickenInBox = 0;
        ActiveChikenNow = 0;
    }

    public void SetWave(int indexWave)
    {
        _currentWave = _waves[indexWave];
    }

    private IEnumerator SetChickenWave()
    {
        while (_workCorotine)
        {
            if (TryGetGameObject(out Chicken chicken) && Time.timeScale > 0)
            {
                if (_currentWave.CountChicken != ActiveChikenNow)
                {
                    SetChicken(chicken, transform.position);
                    ActiveChikenNow++;
                }
                else
                {
                    StopCorotineWave();
                }
            }

            yield return new WaitForSeconds(_currentWave.Delay);
        }
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
        chicken.ActivateGameobject(true);
        chicken.transform.position = spawnPostion;
    }

    private void ExitÑurrentWave()
    {
        if (CountChickeninBox == _currentWave.CountChicken)
        {
            timer += Time.deltaTime;

            if (timer >= _delay)
            {
                _indexWave++;
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
    public float Delay;
}
