using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectDestroyBox : MonoBehaviour
{
    [SerializeField] private ParticleSystem _destroyBoxEffectTemplate;
    
    private Box _box;
    private ParticleSystem _destroyBoxEffect;
    private Chicken _chicken;
    private float _timerPlayEffect = 2f;

    private void Awake()
    {
        _destroyBoxEffect = Instantiate(_destroyBoxEffectTemplate, gameObject.transform);
    }

    private void OnEnable()
    {
        _destroyBoxEffectTemplate.gameObject.SetActive(false);
        _box = GetComponentInParent<Chicken>().Box;
        _box.EffectActivated += StartEffectDestroyBox;
    }

    private void OnDisable()
    {
        _box.EffectActivated -= StartEffectDestroyBox;
    }

    private void OffEffect()
    {
        _destroyBoxEffect.gameObject.SetActive(false);
        Debug.Log(1);
    }

    private void StartEffectDestroyBox()
    {
        _destroyBoxEffect.transform.position = transform.position;
        _destroyBoxEffect.gameObject.SetActive(true);
        Invoke("OffEffect", _timerPlayEffect);
    }
}
