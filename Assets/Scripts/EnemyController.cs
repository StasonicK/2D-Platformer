using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject _platform;
    [SerializeField] private float _speed;
    [SerializeField] private float _levitationHeight = 1.0f;
    [SerializeField] private float _platformEdgeOffset = 4.0f;


    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _platformSpriteRenderer;
    private bool _moveLeft;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _platformSpriteRenderer = _platform.GetComponent<SpriteRenderer>();
        float verticalOffset =
            _platformSpriteRenderer.bounds.size.y / 2 + _spriteRenderer.bounds.size.y / 2 + _levitationHeight;
        transform.position = new Vector3(transform.position.x, _platform.transform.position.y + verticalOffset);
    }

    private void FixedUpdate()
    {
        float offset = _moveLeft ? -_platformEdgeOffset : _platformEdgeOffset;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + offset, transform.position.y),
            Vector2.down);

        if (hit.collider != null)
        {
            if (hit.distance >= _levitationHeight)
            {
                _moveLeft = !_moveLeft;
            }
        }

        Move(_moveLeft);
    }

    private void Move(bool moveLeft)
    {
        float step = moveLeft ? _speed * -Time.deltaTime : _speed * Time.deltaTime;

        transform.position = new Vector3(transform.position.x + step, transform.position.y);
    }
}