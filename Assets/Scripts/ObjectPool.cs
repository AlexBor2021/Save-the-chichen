using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capasity;

    private List<Chicken> _pool = new List<Chicken>();

    protected void Initialize(Chicken prefab)
    {
        for (int i = 0; i < _capasity; i++)
        {
            Chicken spawned = Instantiate(prefab, _container.transform);
            spawned.gameObject.SetActive(false);
            _pool.Add(spawned);
        }
    }

    protected bool TryGetGameObject(out Chicken result)
    {
        result = _pool.First(p => p.gameObject.activeSelf == false);

        return result != null;
    }
}
