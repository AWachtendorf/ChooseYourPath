using UnityEngine;
using System.Collections;

public enum Swipe { None, Up, Down, Left, Right, UpRight, UpLeft, DownRight, DownLeft };

public class SwipeManager : MonoBehaviour
{
    public float MinSwipeLength = 5;
    Vector2 _firstPressPos;
    Vector2 _secondPressPos;
    Vector2 _currentSwipe;

    public static Swipe SwipeDirection;

    void Update()
    {
        DetectSwipe();
    }

    class GetCardinalDirections
    {
        public static readonly Vector2 Up = new Vector2(0, 1);
        public static readonly Vector2 Down = new Vector2(0, -1);
        public static readonly Vector2 Right = new Vector2(1, 0);
        public static readonly Vector2 Left = new Vector2(-1, 0);

        public static readonly Vector2 UpRight = new Vector2(1, 1);
        public static readonly Vector2 UpLeft = new Vector2(-1, 1);
        public static readonly Vector2 DownRight = new Vector2(1, -1);
        public static readonly Vector2 DownLeft = new Vector2(-1, -1);
    }


    public void DetectSwipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                _firstPressPos = new Vector2(t.position.x, t.position.y);
            }

            if (t.phase == TouchPhase.Ended)
            {

                _secondPressPos = new Vector2(t.position.x, t.position.y);
                _currentSwipe = new Vector3(_secondPressPos.x - _firstPressPos.x, _secondPressPos.y - _firstPressPos.y);


                // Make sure it was a legit swipe, not a tap
                if (_currentSwipe.magnitude < MinSwipeLength)
                {
                    SwipeDirection = Swipe.None;
                    return;
                }


                _currentSwipe.Normalize();

                // use dot product against 4 cardinal directions.
                // return if one of them is > 0.5f;


                //compare north
                if (Vector2.Dot(_currentSwipe, GetCardinalDirections.Up) > 0.5f)
                {
                    SwipeDirection = Swipe.Up;
                    print("Up!");
                    return;
                }
                if (Vector2.Dot(_currentSwipe, GetCardinalDirections.Down) > 0.5f)
                {
                    SwipeDirection = Swipe.Down;
                    print("Down!");
                    return;
                }
                if (Vector2.Dot(_currentSwipe, GetCardinalDirections.Left) > 0.5f)
                {
                    SwipeDirection = Swipe.Left;
                    print("Left");
                    return;
                }
                if (Vector2.Dot(_currentSwipe, GetCardinalDirections.Right) > 0.5f)
                {
                    SwipeDirection = Swipe.Right;
                    print("Right");
                    return;
                }

                if (Vector2.Dot(_currentSwipe, GetCardinalDirections.UpRight) > 0.25f)
                {
                    SwipeDirection = Swipe.UpRight;
                    print("UpRight");
                    return;
                }

                if (Vector2.Dot(_currentSwipe, GetCardinalDirections.UpLeft) > 0.25f)
                {
                    SwipeDirection = Swipe.UpLeft;
                    print("UpLeft");
                    return;
                }

                if (Vector2.Dot(_currentSwipe, GetCardinalDirections.DownLeft) > 0.25f)
                {
                    SwipeDirection = Swipe.DownLeft;
                    print("DownLeft");
                    return;
                }

                if (Vector2.Dot(_currentSwipe, GetCardinalDirections.DownRight) > 0.25f)
                {
                    SwipeDirection = Swipe.DownRight;
                    print("DownRight");
                    return;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                _firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            else
            {
                SwipeDirection = Swipe.None;
                //Debug.Log ("None");
            }
            if (Input.GetMouseButtonUp(0))
            {
                _secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                _currentSwipe = new Vector3(_secondPressPos.x - _firstPressPos.x, _secondPressPos.y - _firstPressPos.y);

                // Make sure it was a legit swipe, not a tap
                if (_currentSwipe.magnitude < MinSwipeLength)
                {
                    SwipeDirection = Swipe.None;
                    return;
                }

                _currentSwipe.Normalize();

                //Swipe directional check and it is not working at the moment.
                // Swipe up
                if (Vector2.Dot(_currentSwipe, GetCardinalDirections.Up) > 0.5f)
                {
                    if (Vector2.Dot(_currentSwipe, GetCardinalDirections.UpRight) > 0.5f)
                    {
                        SwipeDirection = Swipe.UpRight;
                        print("UpRight");
                        return;
                    }
                    if (Vector2.Dot(_currentSwipe, GetCardinalDirections.UpLeft) > 0.5f)
                    {
                        SwipeDirection = Swipe.UpLeft;
                        print("UpLeft");
                        return;
                    }
                    SwipeDirection = Swipe.Up;
                    print("Up!");
                    return;
                }
                if (Vector2.Dot(_currentSwipe, GetCardinalDirections.Down) > 0.5f)
                {
                    if (Vector2.Dot(_currentSwipe, GetCardinalDirections.DownLeft) > 0.5f)
                    {
                        SwipeDirection = Swipe.DownLeft;
                        print("DownLeft");
                        return;
                    }
                    if (Vector2.Dot(_currentSwipe, GetCardinalDirections.DownRight) > 0.5f)
                    {
                        SwipeDirection = Swipe.DownRight;
                        print("DownRight");
                        return;
                    }
                    SwipeDirection = Swipe.Down;
                    print("Down!");
                    return;
                }
                if (Vector2.Dot(_currentSwipe, GetCardinalDirections.Left) > 0.5f)
                {
                    SwipeDirection = Swipe.Left;
                    print("Left");
                    return;
                }
                if (Vector2.Dot(_currentSwipe, GetCardinalDirections.Right) > 0.5f)
                {
                    SwipeDirection = Swipe.Right;
                    print("Right");
                    return;
                }
            }
        }
    }
}