using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Platform[] _platforms;
    [SerializeField] private Coin _coin;
    [SerializeField] private float _upLevitation = 0.0f;

    private void Start()
    {
        SpawnCoins();
    }

    private void SpawnCoins()
    {
        if (_platforms.Length == 0)
        {
            return;
        }

        Collider2D squareCollider = _platforms[0].transform.GetChild(0).GetComponent<Collider2D>();

        for (int i = 0; i < _platforms.Length; i++)
        {
            Platform platform = _platforms[i];
            Collider2D _coinCollider = _coin.GetComponent<Collider2D>();
            int squaresSize = platform.transform.childCount;

            float verticalOffset = squareCollider.bounds.max.y / 2 + _coinCollider.bounds.max.y / 2 +
                                   _upLevitation;
            float y = platform.transform.position.y + verticalOffset;

            for (int j = 0; j < squaresSize; j++)
            {
                Transform square = platform.transform.GetChild(j);
                Instantiate(_coin, new Vector3(square.position.x, y), Quaternion.identity);
            }
        }
    }
}