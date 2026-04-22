using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static Action<MoveDirection> OnSwipe;
    public float swipeRange = 50f;
    private Vector2 startTouchPosition;
    private bool isSwiping = false;
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        if(GameManager.Instance.IsUIShow) return; // Nếu UI đang hiển thị, không xử lý input
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
            isSwiping = true;
        }
        if(Input.GetMouseButton(0) && isSwiping)
        {
            Vector2 currentPosition = Input.mousePosition;
            Vector2 swipeDelta = currentPosition - startTouchPosition;

            if (swipeDelta.magnitude > swipeRange)
            {
                float x = swipeDelta.x;
                float y = swipeDelta.y;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x > 0)
                    {
                        OnSwipe?.Invoke(MoveDirection.Right);
                    }
                    else
                    {
                        OnSwipe?.Invoke(MoveDirection.Left);
                    }
                }
                else
                {
                    if (y > 0)
                    {
                        OnSwipe?.Invoke(MoveDirection.Up);

                    }
                    else
                    {
                        OnSwipe?.Invoke(MoveDirection.Down);
                    }
                }
                isSwiping = false; 
            }
        }
    }

}
