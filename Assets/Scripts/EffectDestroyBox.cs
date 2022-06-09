using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroyBox : MonoBehaviour
{
    [SerializeField] private ParticleSystem _destroyBoxEffectTemplate;
    
    private Box _box;
    private ParticleSystem _destroyBoxEffect;

    private void Awake()
    {
        _destroyBoxEffect = Instantiate(_destroyBoxEffectTemplate, gameObject.transform);
    }

    private void OnEnable()
    {
        _destroyBoxEffectTemplate.gameObject.SetActive(false);
        _box = GetComponentInParent<Chicken>().Box;
        _box.DeactiveBoxEfect += StartEffectDestroyBox;
    }

    private void OnDisable()
    {
        _box.DeactiveBoxEfect -= StartEffectDestroyBox;
    }

    private void Update()
    {
        OffEffect();
    }

    private void OffEffect()
    {
        if (_destroyBoxEffect.isStopped)
        {
            _destroyBoxEffect.gameObject.SetActive(false);
        }
    }

    private void StartEffectDestroyBox()
    {
        _destroyBoxEffect.transform.position = transform.position;
        _destroyBoxEffect.gameObject.SetActive(true);
    }
}
