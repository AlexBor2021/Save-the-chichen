using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class TimeFreezButton : MonoBehaviour
{
    [SerializeField] private float _recoveryFreezTime;
    [SerializeField] private float _actionFreez;

    private Image _image;
    private Coroutine _timerForFreezTime;
    private float _click;
    private float _startValue = 1;
    private float _endValue = 0;
    private float _speedByFreez = 0.2f;
    
    private void OnEnable()
    {
        _click = 0;
        _image = GetComponent<Image>();
        _image.fillAmount = 1;
    }

    private IEnumerator ActiveTimerForFreezTime(float duration, float startValue, float endValue)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            var nextvalue = Mathf.Lerp(startValue, endValue, elapsedTime/duration);
            _image.fillAmount = nextvalue;
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= _actionFreez)
            {
                Time.timeScale = 1;
            }
            yield return null;
        }
        _click = 0;
        StopCoroutine(_timerForFreezTime);
    }

    public void FreezTime()
    {
        if (_click == 0)
        {
            Time.timeScale = _speedByFreez;
            _timerForFreezTime = StartCoroutine(ActiveTimerForFreezTime(_recoveryFreezTime, _startValue, _endValue));
            _click++;
            _image.fillAmount = 0;
        }
    }
}
