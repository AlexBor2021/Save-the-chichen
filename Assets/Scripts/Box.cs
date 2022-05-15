using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Box : MonoBehaviour
{
    [SerializeField]private float _timerSecond = 10;

    public UnityAction DestroyBox;

    private float _lasttime;

    private void Update()
    {
        _lasttime += Time.deltaTime;

        if (_lasttime >= _timerSecond)
        {
            Destroy(gameObject);
            DestroyBox?.Invoke();
        }
    }
}
