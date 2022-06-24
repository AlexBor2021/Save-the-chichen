using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class AnimationChicken : MonoBehaviour
{
    private SpriteRenderer _chickenSpriteRender;
    private Animator _animator;
    private float _axisY;
    private float _horizontalUp = 1;
    private float _horizontalDown = -1;

    public const string Horizontal = "Horizontal";

    private void OnEnable()
    {
        _chickenSpriteRender = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _axisY = transform.position.y;
    }

    public void SetAnimation(float _axisYpointTarget)
    {
        if (_axisY > _axisYpointTarget)
        {
            _animator.SetFloat(Horizontal, _horizontalDown);
            _chickenSpriteRender.flipY = false;
        }
        else
        {
            _animator.SetFloat(Horizontal, _horizontalUp);
            _chickenSpriteRender.flipY = true;
        }

        _axisY = transform.position.y;
    }
}
