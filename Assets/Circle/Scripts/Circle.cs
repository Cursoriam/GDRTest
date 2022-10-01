using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle: MonoBehaviour
{
    private List<Vector3> _coordinatesToVisit;
    private bool _isMoving;
    private List<GameObject> _lines;
    public Action IncreaseCountAction;
    public Action CircleDestroyedAction;
    private GameObject _line;
    private GameObject _oldLine;
    private IEnumerator _moveCoordinatesCoroutine;

    private void Awake()
    {
        _coordinatesToVisit = new List<Vector3>();
        _lines = new List<GameObject>();
        GameManager.LoseAction += ShutDown;
        GameManager.WinAction += ShutDown;
        _moveCoordinatesCoroutine = null;
    }
    
    public IEnumerator Move()
    {
        if (!_isMoving)
        {
            _isMoving = true;
            while (_coordinatesToVisit.Count != 0)
            {
                var line = _lines[0];
                _lines.RemoveAt(0);
                Destroy(line);
                _moveCoordinatesCoroutine = MoveToCoordinate(_coordinatesToVisit[0]);
                yield return StartCoroutine(_moveCoordinatesCoroutine);
                _coordinatesToVisit.RemoveAt(0);
            }
            _isMoving = false;
        }
    }

    public void AddCoordinate(Vector3 coordinate)
    {
        var initialPoint = _coordinatesToVisit.Count == 0 ? 
            transform.position : _coordinatesToVisit[_coordinatesToVisit.Count - 1];
        _lines.Add(Utilities.DrawLine(initialPoint, coordinate));
        _coordinatesToVisit.Add(coordinate);
    }

    private IEnumerator MoveToCoordinate(Vector2 coordinate)
    {
        var currentPosition = transform.position;
        var elapsedTime = Constants.FloatBaseValue;
        var distance = Vector3.Distance(currentPosition, coordinate);
        var movementTime = distance / Constants.CircleVelocity;
        while (elapsedTime < movementTime)
        {
            _oldLine = _line;
            Destroy(_oldLine);
            _line = Utilities.DrawLine(transform.position, coordinate);
            transform.position = Vector3.Lerp(currentPosition, coordinate, (elapsedTime / movementTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = coordinate;
        Destroy(_line);
        yield return null;
    }
    
    private void ShutDown()
    {
        if(_moveCoordinatesCoroutine != null)
            StopCoroutine(_moveCoordinatesCoroutine);
        _moveCoordinatesCoroutine = null;
        Destroy(_line);
        Destroy(_oldLine);
        foreach (var line in _lines)
        {
            Destroy(line);
        }
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        switch (otherCollider.gameObject.name)
        {
            case "Coin":
                Destroy(otherCollider.gameObject);
                IncreaseCountAction?.Invoke();
                break;
            case "Thorn":
                CircleDestroyedAction?.Invoke();
                Destroy(this.gameObject);
                break;
        }
    }
}
