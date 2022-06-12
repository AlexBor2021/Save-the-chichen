using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _containerChicken;
    [SerializeField] private int _capasity;
    [SerializeField] private GameObject _templayBox;


    protected int _countChickenInBox;
    private List<Chicken> _poolChicken = new List<Chicken>();

    private void OnDisable()
    {
        foreach (var chicken in _poolChicken)
        {
            chicken.ChickenInBox -= SetCountChickenInBox;
        }
    }

    protected void InitializeChicken(Chicken prefab)
    {
        for (int i = 0; i < _capasity; i++)
        {
            Chicken spawned = Instantiate(prefab, _containerChicken.transform);
            spawned.ChickenInBox += SetCountChickenInBox;
            var templayBox = Instantiate(_templayBox, spawned.transform);
            spawned.SetTemplayBox(templayBox);
            _poolChicken.Add(spawned);
            spawned.gameObject.SetActive(false);
        }
    }

    protected void SetCountChickenInBox(int index)
    {
        _countChickenInBox += index;
    }

    protected void TurnOffChickenTemplate()
    {
        foreach (var chicken in _poolChicken)
        {
            chicken.UpSpeed();

            if (chicken.gameObject)
            {
                chicken.gameObject.SetActive(false);
            }
        }
    }

    protected bool TryGetGameObject(out Chicken result)
    {
        result = _poolChicken.First(p => p.gameObject.activeSelf == false);

        return result != null;
    }
}
