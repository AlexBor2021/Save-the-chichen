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
    [SerializeField] private Box _box;
    [SerializeField] private ParticleSystem _destroyBoxEffectTemplate;

    private SpriteRenderer _chickenSpriteRender;
    private Animator _animator;
    private int _pointTarget;
    private int _oldPointTarget;
    private float _axisY;
    private float _oldSpeed;
    private int _click = 0;
    private ParticleSystem _destroyBoxEffect;

    public const string Horizontal = "Horizontal";

    public event UnityAction<int> ChickenInBox;

    private void Awake()
    {
        _destroyBoxEffect = Instantiate(_destroyBoxEffectTemplate, gameObject.transform);
        _destroyBoxEffectTemplate.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _destroyBoxEffectTemplate.gameObject.SetActive(false);
        _oldSpeed = _speed;
        _box.DeactiveBox += ReleaseChicken;
        _box.DeactiveBoxEfect += StartEffectDestroyBox;
    }

    private void OnDisable()
    {
        _box.DeactiveBox -= ReleaseChicken;
        _box.DeactiveBoxEfect -= StartEffectDestroyBox;
        Restart();
    }

    private void Start()
    {
        _chickenSpriteRender = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _pointTarget = Random.Range(0, _targetsWay.Count);
        _axisY = transform.position.y;
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
            _destroyBoxEffect.gameObject.SetActive(false);
            _speed = 0;
            _box.transform.position = transform.position;
            _box.gameObject.SetActive(true);
            ChickenInBox?.Invoke(1);
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

    private void StartEffectDestroyBox()
    {
        _destroyBoxEffect.transform.position = transform.position;
        _destroyBoxEffect.gameObject.SetActive(true);
    }

    private void ReleaseChicken()
    {
        _click = 0;
        _speed = _oldSpeed;
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
            _oldPointTarget = _pointTarget;
            _pointTarget = Random.Range(0, _targetsWay.Count);
            transform.position = Vector2.MoveTowards(transform.position, _targetsWay[_pointTarget].transform.position, _speed * Time.deltaTime);
            SetAnimation();
        }
    }

    private void SetAnimation()
    {
        var time = +Time.deltaTime;

        if (_axisY > _targetsWay[_pointTarget].transform.position.y || time == 1)
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

    private void Restart()
    {
        _box.gameObject.SetActive(false);
        _click = 0;
    }

    public void UpSpeed()
    {
        if (_speed == 0)
        {
            _speed = _oldSpeed;
        }

        _speed += 0.5f;
    }

    public void RestartSpeed()
    {
        _speed = _restartSpeed;
    }

    public void GetTarget(Transform targetWay)
    {
        _targetsWay.Add(targetWay);
    }

    public void ClearListChickenList()
    {
        _targetsWay.Clear();
    }

    public void SetBox(Box box)
    {
        _box = box;
        _box.gameObject.SetActive(false);
    }
}
