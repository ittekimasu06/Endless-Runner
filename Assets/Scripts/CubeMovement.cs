using UnityEngine;
using System.Collections;

public class CubeMovement : MonoBehaviour
{
    public float moveDistance = 4.5f; // Khoảng cách di chuyển mỗi lần nhấn phím
    public float jumpHeight = 3.6f; // Chiều cao của cú nhảy
    public float sideJumpHeight = 0.8f; // Chiều cao của cú nhảy sang bên
    public float jumpSpeed = 0.62f; // Tốc độ của cú nhảy
    public float sideJumpSpeed = 0.14f; // Tốc độ của cú nhảy sang bên
    public float leftLimit = -6.5f;
    public float rightLimit = 6.5f;

    private bool isJumping = false;
    private bool isAtPeak = false;
    private Vector3 originalPosition;
    private Vector3 targetPosition;

    private Vector2 touchStartPos;
    private float minSwipeDistance = 50f; // Khoảng cách tối thiểu để nhận diện vuốt

    void Start()
    {
        originalPosition = transform.position;
        //reset tốc độ khi bắt đầu màn chơi mới
        GameManager.ResetSpeed();
    }

    void Update()
    {
        HandleKeyboardInput();
        HandleTouchInput();
    }

    private void HandleKeyboardInput()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && !isJumping)
        {
            targetPosition = originalPosition;
            StartCoroutine(Jump(targetPosition, jumpHeight, jumpSpeed));
        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && !isJumping)
        {
            Vector3 tentativePosition = originalPosition + Vector3.left * moveDistance;
            if (tentativePosition.x >= leftLimit)
            {
                targetPosition = tentativePosition;
                StartCoroutine(Jump(targetPosition, sideJumpHeight, sideJumpSpeed));
            }
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && !isJumping)
        {
            Vector3 tentativePosition = originalPosition + Vector3.right * moveDistance;
            if (tentativePosition.x <= rightLimit)
            {
                targetPosition = tentativePosition;
                StartCoroutine(Jump(targetPosition, sideJumpHeight, sideJumpSpeed));
            }
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 swipeVector = touch.position - touchStartPos;

                if (swipeVector.magnitude < minSwipeDistance) return;

                if (Mathf.Abs(swipeVector.x) > Mathf.Abs(swipeVector.y))
                {
                    // Swipe Left or Right
                    if (swipeVector.x > 0) TryMoveRight();
                    else TryMoveLeft();
                }
                else
                {
                    // Swipe Up
                    if (swipeVector.y > 0) TryJump();
                }
            }
        }
    }

    private void TryJump()
    {
        if (!isJumping)
        {
            targetPosition = originalPosition;
            StartCoroutine(Jump(targetPosition, jumpHeight, jumpSpeed));
        }
    }

    private void TryMoveLeft()
    {
        if (!isJumping)
        {
            Vector3 tentativePosition = originalPosition + Vector3.left * moveDistance;
            if (tentativePosition.x >= leftLimit)
            {
                targetPosition = tentativePosition;
                StartCoroutine(Jump(targetPosition, sideJumpHeight, sideJumpSpeed));
            }
        }
    }

    private void TryMoveRight()
    {
        if (!isJumping)
        {
            Vector3 tentativePosition = originalPosition + Vector3.right * moveDistance;
            if (tentativePosition.x <= rightLimit)
            {
                targetPosition = tentativePosition;
                StartCoroutine(Jump(targetPosition, sideJumpHeight, sideJumpSpeed));
            }
        }
    }

    private IEnumerator Jump(Vector3 targetPosition, float height, float speed)
    {
        isJumping = true;
        float elapsedTime = 0f;

        // Ascend
        while (elapsedTime < speed / 2)
        {
            transform.position = Vector3.Lerp(originalPosition, originalPosition + Vector3.up * height, (elapsedTime / (speed / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isAtPeak = true;

        // Check for side movement at peak
        if (isAtPeak && ((Input.GetKey(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.D))))))
        {
            Vector3 tentativePosition = originalPosition;

            if ((Input.GetKey(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.A))))
            {
                tentativePosition += Vector3.left * moveDistance;
                if (tentativePosition.x >= leftLimit)
                {
                    targetPosition = tentativePosition;
                }
            }
            else if ((Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.D))))
            {
                tentativePosition += Vector3.right * moveDistance;
                if (tentativePosition.x <= rightLimit)
                {
                    targetPosition = tentativePosition;
                }
            }
            isAtPeak = false;
        }

        // Descend
        elapsedTime = 0f;
        while (elapsedTime < speed / 2)
        {
            transform.position = Vector3.Lerp(originalPosition + Vector3.up * height, targetPosition, (elapsedTime / (speed / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        originalPosition = targetPosition;
        isJumping = false;
    }
}
