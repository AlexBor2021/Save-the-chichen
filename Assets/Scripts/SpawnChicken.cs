using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class SpawnChicken : ObjectPool
{
    [SerializeField] private List<Wave> _waves = new List<Wave>();
    [SerializeField] private List<Transform> _targetWay = new List<Transform>();
    [SerializeField] private Slider _barCaptureChicken;
    [SerializeField] private GameObject _nextLevelButton;
    [SerializeField] private GameObject _freezButton;
    [SerializeField] private TextMeshProUGUI _lecelCurrentText;
    [SerializeField] private TextMeshProUGUI _levelNextText;

    private Wave _currentWave;
    private float _pastTime;
    private int _indexWave = 0;
    private int _activeChikenNow = 0;
    private float timer = 0;

    

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
       
    }

    void Start()
    {
        SetWave(_indexWave);

        _currentWave.ChickenTemplate.ClearListChickenList();

        SetWayForChicken();

        InitializeChicken(_currentWave.ChickenTemplate);
    }

    private void Update()
    {
        SetChickenWave();

        ExitcurrentWave();
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


    private void SetCaptudeBar(int countChickenInBox)
    {
        _barCaptureChicken.maxValue = _currentWave.CountChicken;
        _barCaptureChicken.value = _countChickenInBox;
    }

    private void ChangeTextPanelNextLevel()
    {
        _lecelCurrentText.text = "Level " + (1+_indexWave);
        _levelNextText.text = "Level " + (2+_indexWave);
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

    private void ExitcurrentWave()
    {
        if (_countChickenInBox == _currentWave.CountChicken)
        {
            timer += Time.deltaTime;

            if (timer >= 1)
            {
                ChangeTextPanelNextLevel();
                TurnOffChickenTemplate();
                _nextLevelButton.SetActive(true);
                _countChickenInBox = 0;
                _activeChikenNow = 0;
                timer = 0;
                _freezButton.SetActive(false);
                Time.timeScale = 0;
            }
        }

        SetCaptudeBar(_countChickenInBox);
    }

    public void ActiveateNextLevel()
    {
        SetWave(++_indexWave);
        Time.timeScale = 1;
    }

    public void GetAgainButton()
    {
        TurnOffChickenTemplate();
        _countChickenInBox = 0;
        _activeChikenNow = 0;
    }
}

[System.Serializable]

public class Wave
{
    public Chicken ChickenTemplate;
    public float CountChicken;
    public int Delay;
}
