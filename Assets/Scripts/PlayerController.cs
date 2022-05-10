using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;

    protected bool _grounded;

    private float _upForce = 5f;
    private Vector2 _moveVelocity;
    private const string JumpTrigger = "Jump";

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 moveVelocity = new Vector2(Input.GetAxis("Horizontal"), 0);
        _moveVelocity = moveVelocity.normalized * _speed;

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && _grounded)
        {
            StartCoroutine(Jump());
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _moveVelocity * Time.deltaTime);

        // _grounded = false;

        // if (_moveVelocity.y >= 0)
        // {
        _grounded = true;
        // }
    }

    private IEnumerator Jump()
    {
        Debug.Log("Jump");
        _rigidbody2D.AddForce(Vector2.up * _upForce * Time.deltaTime, ForceMode2D.Impulse);
        _animator.SetTrigger(JumpTrigger);
        Debug.Log($"rigidBody2d {_rigidbody2D.velocity.normalized}");
        yield return new WaitForFixedUpdate();
    }
}