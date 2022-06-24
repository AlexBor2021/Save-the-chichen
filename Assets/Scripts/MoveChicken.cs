using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class MoveChicken : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _restartSpeed;
    [SerializeField] private Chicken _chicken;
    [SerializeField] private AnimationChicken _animationChicken;
    [SerializeField] private List<Transform> _targetsWay = new List<Transform>();
   
    private int _pointTarget;
    private int _oldPointTarget;
    private float _oldSpeed;
    private int _degreeTurn = 90;
    private float _valueUpSpeed = 0.5f;
    private float _distanceToPoint = 1;

    private void OnEnable()
    {
        _oldSpeed = _speed;
        _pointTarget = Random.Range(0, _targetsWay.Count);
        _chicken.StopChicken += StopChicken;
    }

    private void OnDisable()
    {
        _speed = _oldSpeed;
        _chicken.StopChicken -= StopChicken;
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
        _animationChicken.SetAnimation(_targetsWay[_pointTarget].transform.position.y);

        if (Vector2.Distance(transform.position, _targetsWay[_pointTarget].transform.position) < _distanceToPoint)
        {
            _oldPointTarget = _pointTarget;
            _pointTarget = Random.Range(0, _targetsWay.Count);
            transform.position = Vector2.MoveTowards(transform.position, _targetsWay[_pointTarget].transform.position, _speed * Time.deltaTime);
            _animationChicken.SetAnimation(_targetsWay[_pointTarget].transform.position.y);
        }
    }

    private void StopChicken(bool stop)
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
