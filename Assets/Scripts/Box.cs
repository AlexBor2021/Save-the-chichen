using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]

public class Box : MonoBehaviour
{
    [SerializeField] private float _timerSecond;
    [SerializeField] private AudioClip _crashBox;
    [SerializeField] private AudioClip _fellBox;
    [SerializeField] private AudioClip _chickenSounds;

    private bool _isWork;
    private float _lastTime;
    private float _timerSecondForEffect => _timerSecond-1;
    private AudioSource _audioSource;

    public event UnityAction BoxDestroaed;
    public event UnityAction EffectActivated;

    private void OnEnable()
    {
        _isWork = true;
        _lastTime = 0;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _lastTime += Time.deltaTime;

        if (_lastTime >= _timerSecond)
        {
            StartDestroyBox();
        }
        else if (_lastTime >= _timerSecondForEffect && _isWork)
        {
            EffectActivated?.Invoke();
            _isWork = false;
        }
    }

    public void StartingAudio()
    {
        _audioSource.PlayOneShot(_fellBox);
        _audioSource.PlayOneShot(_chickenSounds);
    }

    private void StartDestroyBox()
    {
        BoxDestroaed?.Invoke();
        gameObject.SetActive(false);
        AudioSource.PlayClipAtPoint(_crashBox, transform.position);
    }
}
