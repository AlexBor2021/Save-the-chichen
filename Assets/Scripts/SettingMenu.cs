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

    private GenerationChicken _generationChicken;
    private int _levelNextTekst => 1 + _generationChicken.IndexWave;
    private int _normalTime = 1;

    private void OnEnable()
    {
        _generationChicken = GetComponentInParent<GenerationChicken>();
    }

    private void Update()
    {
        SetCaptudeBar();
    }

    public void ActiveateNextLevel()
    {
        _generationChicken.SetWave(_generationChicken.IndexWave);
        _generationChicken.ActivateCorotne();
        Time.timeScale = _normalTime;
    }

    public void GetAgainButton()
    {
        _generationChicken.ZeroingLevel();
    }

    private void SetCaptudeBar()
    {
        _barCaptureChicken.maxValue = _generationChicken.CurrentWave.CountChicken;
        _barCaptureChicken.value = _generationChicken.CountChickeninBox;
    }

    private void ChangeTextPanelNextLevel()
    {
        _lecelCurrentText.text = "Level " + (_generationChicken.IndexWave);
        _levelNextText.text = "Level " + (_levelNextTekst);
    }

    public void EndWave()
    {
        ChangeTextPanelNextLevel();
        _nextLevelButton.SetActive(true);
        _freezButton.SetActive(false);
    }

}
