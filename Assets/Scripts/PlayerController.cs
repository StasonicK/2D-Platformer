using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    [SerializeField] private float _upForce = 5f;

    private Vector2 _moveVelocity;
    private const string JumpTrigger = "Jump";
    private const string SpeedFloat = "SpeedFloat";

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float getAxisHorizontal = Input.GetAxis("Horizontal");
        Vector2 moveVelocity = new Vector2(getAxisHorizontal, 0);
        _moveVelocity = moveVelocity.normalized * _speed;
        Debug.Log($"SpeedFloat {getAxisHorizontal}");
        _animator.SetFloat(SpeedFloat, getAxisHorizontal);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _moveVelocity * Time.deltaTime);
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _upForce, ForceMode2D.Force);
        _animator.SetTrigger(JumpTrigger);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<EnemyController>(out EnemyController enemyController))
        {
            Destroy(gameObject);
        }
    }
}