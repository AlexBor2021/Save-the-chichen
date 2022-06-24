using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _containerChicken;
    [SerializeField] private int _capasity;
    [SerializeField] private GameObject _templayBox;

    protected int ActiveChikenNow = 0;
    protected int CountChickenInBox;
    private List<Chicken> _poolChicken = new List<Chicken>();
    
    public int CountChickeninBox => CountChickenInBox;

    private void OnDisable()
    {
        foreach (var chicken in _poolChicken)
        {
            chicken.ChickenCaught -= SetCaughtChicken;
        }
    }

    protected void InitializeChicken(Chicken prefab)
    {
        for (int i = 0; i < _capasity; i++)
        {
            Chicken spawned = Instantiate(prefab, _containerChicken.transform);
            spawned.ChickenCaught += SetCaughtChicken;
            var templayBox = Instantiate(_templayBox, spawned.transform);
            spawned.SetBox(templayBox);
            _poolChicken.Add(spawned);
            spawned.ActivateGameobject(false);
        }
    }

    protected void SetCaughtChicken(int index)
    {
        CountChickenInBox += index;
    }

    protected void TurnOffChickenTemplate()
    {
        foreach (var chicken in _poolChicken)
        {
            if (chicken.gameObject)
            {
                chicken.ActivateGameobject(false);
            }
        }
    }

    protected bool TryGetGameObject(out Chicken result)
    {
        result = _poolChicken.First(p => p.gameObject.activeSelf == false);

        return result != null;
    }
}
