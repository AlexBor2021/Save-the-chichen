using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class MoveChicken : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _restartSpeed;
    [SerializeField] private Chicken _chicken;
    [SerializeField] private List<Transform> _targetsWay = new List<Transform>();

    private SpriteRenderer _chickenSpriteRender;
    private Animator _animator;
    private int _pointTarget;
    private int _oldPointTarget;
    private float _axisY;
    private float _oldSpeed;
    private int _degreeTurn = 90;
    private float _valueUpSpeed = 0.5f;
    private float _distanceToPoint = 1f;

    public const string Horizontal = "Horizontal";

    private void OnEnable()
    {
        _oldSpeed = _speed;
        _chickenSpriteRender = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _pointTarget = Random.Range(0, _targetsWay.Count);
        _axisY = transform.position.y;
        _chicken.StopChicken += Stopchicken;
    }

    private void OnDisable()
    {
        _speed = _oldSpeed;
        _chicken.StopChicken -= Stopchicken;
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Box>(out Box box))
        {
            _pointTarget = _oldPointTarget;
            Move();
        }
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

    public void ClearWayList()
    {
        _targetsWay.Clear();
    }

    private void Rotate()
    {
        var dirathion = _targetsWay[_pointTarget].transform.position - transform.position;
        var angle = Mathf.Atan2(dirathion.y, dirathion.x) * Mathf.Rad2Deg;
        angle += _degreeTurn;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Move()
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
        if (_axisY > _targetsWay[_pointTarget].transform.position.y)
        {
            _animator.SetFloat(Horizontal, -1);
            _chickenSpriteRender.flipY = false;
        }
        else
        {
            _animator.SetFloat(Horizontal, 1);
            _chickenSpriteRender.flipY = true;
        }

        _axisY = transform.position.y;
    }

    private void Stopchicken(bool stop)
    {
        if (stop)
        {
            _speed = 0;
        }
        else
        {
            _speed = _oldSpeed;
        }
    }
}
