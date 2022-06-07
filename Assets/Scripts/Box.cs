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

    private float _lasttime;
    private bool _create;
    private AudioSource _audioSource;

    public event UnityAction DeactiveBox;
    public event UnityAction DeactiveBoxEfect;

    private void OnEnable()
    {
        _lasttime = 0;
        _create = true;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _lasttime += Time.deltaTime;
        if (_lasttime >= _timerSecond-1)
        {
            DeactiveBoxEfect?.Invoke();
        }
        if (_lasttime >= _timerSecond)
        {
            DeactiveBox?.Invoke();
            _lasttime = 0;
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
