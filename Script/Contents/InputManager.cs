using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class InputManager
{
    public Action<Keys> _inputEvent;
    public Action<Vector2, Mouse> _mouseEvent;
    
    public void Update()
    {
        if (Input.GetKeyDown(keyBinding[Keys.Jump]))
            _inputEvent?.Invoke(Keys.Jump);
        if (Input.GetKey(keyBinding[Keys.down]))
            _inputEvent?.Invoke(Keys.down);
        if (Input.GetKey(keyBinding[Keys.left]))
            _inputEvent?.Invoke(Keys.left);
        if (Input.GetKey(keyBinding[Keys.right]))
            _inputEvent?.Invoke(Keys.right);
        if (Input.GetKey(keyBinding[Keys.attack]))
            _inputEvent?.Invoke(Keys.attack);
        if (Input.GetKey(keyBinding[Keys.interaction]))
            _inputEvent?.Invoke(Keys.interaction);
        if (Input.GetMouseButtonDown(1))
            _mouseEvent?.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition), Mouse.RightClick);
        if (Input.GetMouseButtonUp(0))
            _mouseEvent?.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition), Mouse.LeftClickUp);
        if (Input.GetMouseButton(0))
            _mouseEvent?.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition), Mouse.LeftClick);
        if (!Input.anyKey)
            _inputEvent?.Invoke(Keys.None);

    }

}
