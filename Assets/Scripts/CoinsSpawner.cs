using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    // [SerializeField] private float _spaceBetweenCoins;
    [SerializeField] private Platform[] _platforms;
    [SerializeField] private Coin _coin;
    [SerializeField] private float _upLevitation = 0.0f;

    private SpriteRenderer _platformSpriteRenderer;
    // private const int DigitsAfterDot = 0;

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

        for (int i = 0; i < _platforms.Length; i++)
        {
            Platform platform = _platforms[i];
            _platformSpriteRenderer = platform.GetComponent<SpriteRenderer>();
            int squaresSize = platform.transform.childCount;
            SpriteRenderer squareSpriteRenderer = platform.transform.GetChild(0).GetComponent<SpriteRenderer>();

            for (int j = 0; j < squaresSize; j++)
            {
                Transform square = platform.transform.GetChild(j);
                float verticalOffset = _platformSpriteRenderer.bounds.max.y / 2 + _platformSpriteRenderer.bounds.max.y / 2 +
                                       _upLevitation;
                float y = platform.transform.position.y + verticalOffset;
                Instantiate(_coin, new Vector3(square.position.x, y), Quaternion.identity);
                Debug.Log($"square {square.position}");
            }
        }
    }
}