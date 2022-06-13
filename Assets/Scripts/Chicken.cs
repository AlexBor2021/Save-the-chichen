using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class Chicken : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _restartSpeed;
    [SerializeField] private List<Transform> _targetsWay = new List<Transform>();
    [SerializeField] private BoxChicken _boxChicken;

    private SpriteRenderer _chickenSpriteRender;
    private Animator _animator;
    private int _pointTarget;
    private int _oldPointTarget;
    private float _axisY;
    private float _oldSpeed;
    private int _degreeTurn = 90;
    private float _timeAnimation = 1;
    private float _valueUpSpeed = 0.5f;
    private float _distanceToPoint = 1f;
    private int _click = 0;

    public Box Box => _boxChicken.Box;

    public const string Horizontal = "Horizontal";

    public event UnityAction<int> ChickenCaught;
    
    enum �aughtChicken
    {
        yes = 1,
        no = -1,
    }

    private void OnEnable()
    {
        _oldSpeed = _speed;
        _boxChicken.Box = _boxChicken.TemplayBox.GetComponent<Box>();
        _boxChicken.Box.BoxDestroaed += ReleaseChicken;
        _chickenSpriteRender = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _pointTarget = Random.Range(0, _targetsWay.Count);
        _axisY = transform.position.y;
    }

    private void OnDisable()
    {
        _boxChicken.Box.BoxDestroaed -= ReleaseChicken;
        _click = 0;
        _boxChicken.TemplayBox.SetActive(false);
    }

    private void Update()
    {
        MoveChicken();
        Rotate();
    }

    private void OnMouseDown()
    {
        if (_click == 0)
        {
            _speed = 0;
            _boxChicken.TemplayBox.transform.position = transform.position;
            _boxChicken.TemplayBox.SetActive(true);
            ChickenCaught?.Invoke((int)�aughtChicken.yes);
            _click = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Box>(out Box box))
        {
            _pointTarget = _oldPointTarget;
            MoveChicken();
        }
    }
    
    public void SetTemplayBox(GameObject templayBox)
    {
        _boxChicken.TemplayBox = templayBox;
    }

    public void UpSpeed()
    {
        if (_speed == 0)
        {
            _speed = _oldSpeed;
        }

        _speed += _valueUpSpeed;
    }

    public void GetTarget(Transform targetWay)
    {
        _targetsWay.Add(targetWay);
    }

    public void ClearListChickenList()
    {
        _targetsWay.Clear();
    }

    private void ReleaseChicken()
    {
        _click = 0;
        _speed = _oldSpeed;
        ChickenCaught?.Invoke((int)�aughtChicken.no);
    }

    private void Rotate()
    {
        var dirathion = _targetsWay[_pointTarget].transform.position - transform.position;
        var angle = Mathf.Atan2(dirathion.y, dirathion.x) * Mathf.Rad2Deg;
        angle += _degreeTurn;
        transform.rotation = Quaternion.Euler(0 , 0, angle);
    }

    private void MoveChicken()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetsWay[_pointTarget].transform.position, _speed * Time.deltaTime);
        SetAnimation();

        if (Vector2.Distance(transform.position, _targetsWay[_pointTarget].transform.position) < _distanceToPoint)
        {
            _oldPointTarget = _pointTarget;
            _pointTarget = Random.Range(0, _targetsWay.Count);
            transform.position = Vector2.MoveTowards(transform.position, _targetsWay[_pointTarget].transform.position, _speed * Time.deltaTime);
            SetAnimation();
        }
    }

    private void SetAnimation()
    {
        var time = +Time.deltaTime;

        if (_axisY > _targetsWay[_pointTarget].transform.position.y || time == _timeAnimation)
        {
            _axisY = transform.position.y;
            _chickenSpriteRender.flipY = false;
            _animator.SetFloat(Horizontal, -1);
            time = 0;
        }
        else
        {
            _axisY = transform.position.y;
            _animator.SetFloat(Horizontal, 1);
            _chickenSpriteRender.flipY = true;
            time = 0;
        }
    }
}

[System.Serializable]

public class BoxChicken
{
    public GameObject TemplayBox;
    public Box Box;
}
