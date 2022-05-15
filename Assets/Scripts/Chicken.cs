using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chicken : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private List<Transform> _targetsWay = new List<Transform>();
    [SerializeField] private List<Box> _box = new List<Box>();


    private SpriteRenderer _chickenSpriteRender;
    private Animator _animator;
    private int _pointTarget;
    private float _axisY;
    private int _clikMouse = 0;
    private float _oldSpeed;

    public  UnityAction<int> ChickenInBox;

    private void OnEnable()
    {
        _box[0]._destroyBox += ReleaseChicken;
    }

    private void OnDisable()
    {
        _box[0]._destroyBox -= ReleaseChicken;
    }

    private void Start()
    {
        _chickenSpriteRender = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _pointTarget = Random.Range(0, _targetsWay.Count);
        _axisY = transform.position.y;
        _oldSpeed = _speed;
    }

    private void Update()
    {
        MoveChicken();
        Rotate();
    }


    private void OnMouseDown()
    {
        _clikMouse++;

        if (_clikMouse == 1)
        {
            _speed = 0;
            Instantiate(_box[0], transform.position, Quaternion.identity);
            ChickenInBox?.Invoke(1);
        }
    }

    private void ReleaseChicken()
    {
        _speed = _oldSpeed;
        _clikMouse = 0;
        ChickenInBox?.Invoke(-1);
    }

    private void Rotate()
    {
        var dirathion = _targetsWay[_pointTarget].transform.position - transform.position;
        var angle = Mathf.Atan2(dirathion.y, dirathion.x) * Mathf.Rad2Deg;
        angle += 90;
        transform.rotation = Quaternion.Euler(0 , 0, angle);
    }

    private void MoveChicken()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetsWay[_pointTarget].transform.position, _speed * Time.deltaTime);
        SetAnimation();

        if (Vector2.Distance(transform.position, _targetsWay[_pointTarget].transform.position) < 1)
        {
            _pointTarget = Random.Range(0, _targetsWay.Count);
            transform.position = Vector2.MoveTowards(transform.position, _targetsWay[_pointTarget].transform.position, _speed * Time.deltaTime);
            SetAnimation();
        }
    }

    public void GetTarget(Transform targetWay)
    {
        _targetsWay.Add(targetWay);
    }

    public void ClearListChickenList()
    {
        _targetsWay.Clear();
    }

    private void SetAnimation()
    {
        var time = +Time.deltaTime;

        if (_axisY > _targetsWay[_pointTarget].transform.position.y || time == 1)
        {
            _axisY = transform.position.y;
            _chickenSpriteRender.flipY = false;
            _animator.SetFloat("Horizontal", -1);
            time = 0;
        }
        else
        {
            _axisY = transform.position.y;
            _animator.SetFloat("Horizontal", 1);
            _chickenSpriteRender.flipY = true;
            time = 0;
        }
    }
}
