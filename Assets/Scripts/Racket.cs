using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Racket : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    public FixedJoystick joystick;
    private Vector3 startPos;
    private Vector3 screenTop;
    private Vector3 screenBottom;

    private void Start()
    {
        startPos = transform.position;
        screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        screenBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
    }

    private void Update()
    {
        var vertical = joystick.Vertical > 0 ? 1 : joystick.Vertical < 0 ? -1 : 0;
        //error.text = joystick.Vertical.ToString();
        transform.Translate(new Vector3(0, vertical * speed * Time.deltaTime));
        if (vertical < 0) // moving bottom
        {
            //transform.position.Set(transform.position.x, Mathf.Max(transform.position.y - transform.localScale.y, screenBottom.y), 0);
            transform.position = Vector3.Max(transform.position, new Vector3(transform.position.x, screenBottom.y + transform.localScale.y / 2));
        }
        else
        {
            //transform.position.Set(transform.position.x, Mathf.Min(transform.position.y + transform.localScale.y, screenTop.y), 0);
            transform.position = Vector3.Min(transform.position, new Vector3(transform.position.x, screenTop.y - transform.localScale.y / 2));
        }
    }


    public void ResetPos()
    {
        transform.position = startPos;
    }
}
