using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _containerChicken;
    [SerializeField] private int _capasity;

    private List<Chicken> _poolChicken = new List<Chicken>();

    protected void InitializeChicken(Chicken prefab)
    {
        for (int i = 0; i < _capasity; i++)
        {
            Chicken spawned = Instantiate(prefab, _containerChicken.transform);
            spawned.CreateBox();
            spawned.gameObject.SetActive(false);
            _poolChicken.Add(spawned);
        }
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
