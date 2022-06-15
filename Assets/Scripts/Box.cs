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

    private Coroutine _destroyBox;
    private float _timerSecondForEffect => _timerSecond-1;
    private AudioSource _audioSource;

    public event UnityAction BoxDestroaed;
    public event UnityAction EffectActivated;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        _destroyBox = StartCoroutine(DestroyBox());
    }

    private IEnumerator DestroyBox()
    {
        yield return new WaitForSecondsRealtime(_timerSecondForEffect);
        EffectActivated?.Invoke();
        yield return new WaitForSecondsRealtime(_timerSecond-_timerSecondForEffect);
        StartDestroyBox();
    }

    public void LaunchgAudio()
    {
        _audioSource.PlayOneShot(_fellBox);
        _audioSource.PlayOneShot(_chickenSounds);
    }

    private void StartDestroyBox()
    {
        BoxDestroaed?.Invoke();
        StopCoroutine(_destroyBox);
        gameObject.SetActive(false);
        AudioSource.PlayClipAtPoint(_crashBox, transform.position);
    }
}
