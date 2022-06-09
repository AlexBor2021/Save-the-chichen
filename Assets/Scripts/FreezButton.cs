using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class FreezButton : MonoBehaviour
{
    [SerializeField] private float _recoveryFreezTime;
    [SerializeField] private float _actonFreez;

    private Image _image;
    private float _click;
    private Coroutine _timerForFreezTime;
    
    private void OnEnable()
    {
        _click = 0;
        _image = GetComponent<Image>();
        _image.fillAmount = 1;
    }

    private IEnumerator ActiveTimerForFreezTime(float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            Debug.Log(elapsedTime);
            var nextvalue = Mathf.Lerp(0, 1, elapsedTime/duration);
            _image.fillAmount = nextvalue;
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= _actonFreez)
            {
                Time.timeScale = 1f;
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
            Time.timeScale = 0.2f;
            _timerForFreezTime = StartCoroutine(ActiveTimerForFreezTime(_recoveryFreezTime));
            _click++;
            _image.fillAmount = 0;
        }
    }
}
