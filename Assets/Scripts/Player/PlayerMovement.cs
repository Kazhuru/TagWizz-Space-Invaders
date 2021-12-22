using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float padding = 0f;
    [SerializeField] BoxCollider2D rightTouch;
    [SerializeField] BoxCollider2D leftTouch;

    // Object variables
    Vector2 minPoint;
    Vector2 maxPoint;

    // Cached variables
    Rigidbody2D rigBody2d;

    void Start()
    {
        SetUpMovementLimits();
        rigBody2d = GetComponent<Rigidbody2D>();
        Debug.Log(rigBody2d);
    }

    void Update()
    {
        GetHorizontalAxisInput();
        GetTouchInput();
        PlayerMovementLimits();
    }

    private void SetUpMovementLimits()
    {
        minPoint = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        maxPoint = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
    }

    private void GetHorizontalAxisInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            if (horizontalInput > 0.25)
            {
                transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            }
            if (horizontalInput < -0.25)
            {
                transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
            }
        }
    }

    private void GetTouchInput()
    {
        foreach (Touch touch in Input.touches)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            if (rightTouch.bounds.Contains(touchPosition))
            {
                transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
                break;
            }
            if (leftTouch.bounds.Contains(touchPosition))
            {
                transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
                break;
            }
        }
    }

    private void PlayerMovementLimits()
    {
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, minPoint.x + padding, maxPoint.x - padding),
            transform.position.y);
    }
}
