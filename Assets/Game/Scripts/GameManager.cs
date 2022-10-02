using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject thornPrefab;
    [SerializeField] private int n;
    [SerializeField] private Text countText;
    [SerializeField] private GameObject winGameOverBg;
    private Circle _circle;
    private GameObject _circleOnScene;
    private int _count;
    private int _coinsLeft;
    private List<GameObject> _coins;
    private List<GameObject> _thorns;
    public static Action LoseAction;
    public static Action WinAction;
    private Camera _mainCamera;

    // Start is called before the first frame update
    private void Start()
    {
        if(Camera.main != null)
            _mainCamera = Camera.main;
        Constants.ScreenWidth = _mainCamera.ScreenToWorldPoint(
            new Vector2(Screen.width, Screen.height)).x;
        Constants.ScreenHeight = _mainCamera.ScreenToWorldPoint(
            new Vector2(Screen.width, Screen.height)).y;
        BootsTrap();
    }

    private void InitializeVariables()
    {
        _coins = new List<GameObject>();
        _thorns = new List<GameObject>();
        _circleOnScene = Instantiate(circlePrefab);
        _circle = _circleOnScene.GetComponent<Circle>();
        _count = 0;
        countText.text = _count.ToString();
        _circle.IncreaseCountAction += IncreaseCount;
        _circle.CircleDestroyedAction += LoseGame;
        _coinsLeft = n;
        winGameOverBg.SetActive(false);
    }

    private void BootsTrap()
    {
        InitializeVariables();
        SpawnObjects();
    }

    private void DestroyObjects()
    {
        foreach (var coin  in _coins)
        {
            Destroy(coin);
        }

        foreach (var thorn in _thorns)
        {
            Destroy(thorn);
        }
        if(_circleOnScene != null)
            Destroy(_circleOnScene);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!Utilities.IsPointerOverUIObject() && _circle != null)
            {
                _circle.AddCoordinate(_mainCamera.ScreenToWorldPoint(Input.mousePosition));
                StartCoroutine(_circle.Move());
            }
        }
    }

    private void SpawnObjects()
    {
        for (var i = 0; i < n; i++)
        {
            var thornPosition = Utilities.CalculatePosition(thornPrefab);
            var thorn = Instantiate(thornPrefab, thornPosition,
                Quaternion.identity);
            thorn.name = Constants.ThornName;
            _thorns.Add(thorn);
        }

        for (var i = 0; i < n; i++)
        {
            var coinPosition = Utilities.CalculatePosition(coinPrefab);
            var coin = Instantiate(coinPrefab, coinPosition,
                Quaternion.identity);
            coin.name = Constants.CoinName;
            _coins.Add(coin);
        }
    }
    
    private void IncreaseCount()
    {
        _count++;
        countText.text = _count.ToString();
        _coinsLeft--;
        if (_coinsLeft == 0)
        {
            _circle = null;
            WinAction?.Invoke();
        }
    }

    public void RestartGame()
    {
        DestroyObjects();
        BootsTrap();
    }

    private void LoseGame()
    {
        LoseAction?.Invoke();
    }
}
