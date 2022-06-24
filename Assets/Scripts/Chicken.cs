using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Chicken : MonoBehaviour
{
    [SerializeField] private BoxChicken _boxChicken;
   
    private int _click = 0;

    public Box Box => _boxChicken.Box;

    public event UnityAction<int> ChickenCaught;
    public event UnityAction<bool> StopChicken;
    
    enum ÑaughtChicken
    {
        yes = 1,
        no = -1,
    }

    private void OnEnable()
    {
        _boxChicken.Box = _boxChicken.TemplayBox.GetComponent<Box>();
        _boxChicken.Box.BoxDestroaed += ReleaseChicken;
    }

    private void OnDisable()
    {
        _boxChicken.Box.BoxDestroaed -= ReleaseChicken;
        _click = 0;
        _boxChicken.TemplayBox.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (_click == 0)
        {
            StopChicken?.Invoke(true);
            _boxChicken.TemplayBox.transform.position = transform.position;
            _boxChicken.ActiveTemplayBox(true);
            ChickenCaught?.Invoke((int)ÑaughtChicken.yes);
            _click = 1;
        }
    }
    
    public void SetBox(GameObject templayBox)
    {
        _boxChicken.TemplayBox = templayBox;
    }

    public void ActivateGameobject(bool work)
    {
        gameObject.SetActive(work);
    }

    private void ReleaseChicken()
    {
        _click = 0;
        StopChicken?.Invoke(false);
        ChickenCaught?.Invoke((int)ÑaughtChicken.no);
    }
}

[System.Serializable]

public class BoxChicken
{
    public GameObject TemplayBox;
    public Box Box;

    public void ActiveTemplayBox(bool work)
    {
        TemplayBox.SetActive(work);
    }
}
