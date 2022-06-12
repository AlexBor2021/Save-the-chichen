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

    private float _lastTime;
    private float _timerSecondForEffect => _timerSecond-1;
    private AudioSource _audioSource;

    public event UnityAction DeactiveBox;
    public event UnityAction DeactiveBoxEfect;

    private void OnEnable()
    {
        _lastTime = 0;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _lastTime += Time.deltaTime;
        if (_lastTime >= _timerSecondForEffect)
        {
            DeactiveBoxEfect?.Invoke();
        }
        if (_lastTime >= _timerSecond)
        {
            DeactiveBox?.Invoke();
            _lastTime = 0;
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(_crashBox, transform.position);
        }
    }

    public void FellBoxAudio()
    {
        _audioSource.PlayOneShot(_fellBox);
        _audioSource.PlayOneShot(_chickenSounds);
    }
}
