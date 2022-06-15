using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(GenerationChicken))]

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private Slider _barCaptureChicken;
    [SerializeField] private GameObject _nextLevelButton;
    [SerializeField] private GameObject _freezButton;
    [SerializeField] private TextMeshProUGUI _lecelCurrentText;
    [SerializeField] private TextMeshProUGUI _levelNextText;

    private GenerationChicken _spawnChicken;

    private void OnEnable()
    {
        _spawnChicken = GetComponentInParent<GenerationChicken>();
    }

    private void Update()
    {
        SetCaptudeBar();
    }

    public void ActiveateNextLevel()
    {
        _spawnChicken.SetWave(1+_spawnChicken.IndexWave);
        Time.timeScale = 1;
    }

    public void GetAgainButton()
    {
        _spawnChicken.ZeroingLevel();
    }

    private void SetCaptudeBar()
    {
        _barCaptureChicken.maxValue = _spawnChicken.CurrentWave.CountChicken;
        _barCaptureChicken.value = _spawnChicken.CountChickenInBox;
    }

    private void ChangeTextPanelNextLevel()
    {
        _lecelCurrentText.text = "Level " + (1 + _spawnChicken.IndexWave);
        _levelNextText.text = "Level " + (2 + _spawnChicken.IndexWave);
    }

    public void EndWave()
    {
        ChangeTextPanelNextLevel();
        _nextLevelButton.SetActive(true);
        _freezButton.SetActive(false);
    }

}
