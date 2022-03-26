using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 startPos;
    private float _deltaX = 0;

    private float _firstSpeed = -1;
    public float FirstSpeed { get { return _firstSpeed; } }
    [field: SerializeField]
    private float _speed = 5;

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public float DeltaX
    {
        get { return _deltaX; }
        set { _deltaX = value; }
    }
    private float _deltaY;

    public float DeltaY
    {
        get { return _deltaY; }
        set { _deltaY = value; }
    }

    private string collisionInfo;
    private void Start()
    {
        startPos = transform.position;
    }

    public void ResetBall()
    {
        _speed = _firstSpeed;
        transform.position = startPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionInfo = collision.transform.tag;
        if (collisionInfo == "Top" || collisionInfo == "Bottom")
        {
            _deltaY = -_deltaY;
        }
        else if (collisionInfo == "Paddle")
        {
            if (_firstSpeed == -1)
                _firstSpeed = _speed;
            _speed *= 1.03f;
            _deltaX = -_deltaX;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionInfo = "";
    }

    public string GetCollision()
    {
        return collisionInfo;
    }
}
