using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    public DirectionEvent ON_DIRECTION = new DirectionEvent();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow)) ON_DIRECTION.Invoke(new Vector2(-1, 0));
        else if (Input.GetKeyUp(KeyCode.RightArrow)) ON_DIRECTION.Invoke(new Vector2(1, 0));
        else if (Input.GetKeyUp(KeyCode.UpArrow)) ON_DIRECTION.Invoke(new Vector2(0, -1));
        else if (Input.GetKeyUp(KeyCode.DownArrow)) ON_DIRECTION.Invoke(new Vector2(0, 1));
    }
}

