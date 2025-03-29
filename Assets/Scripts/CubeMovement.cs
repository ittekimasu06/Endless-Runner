using UnityEngine;
using System.Collections;

public class CubeMovement : MonoBehaviour
{
    public float moveDistance = 4.5f; // Khoang cach di chuyen moi lan nhan phim
    public float jumpHeight = 3.4f; // Chieu cao cua cu nhay
    public float sideJumpHeight = 0.8f; // Chieu cao cua cu nhay sang ben
    public float jumpSpeed = 0.6f; // Toc do cua cu nhay
    public float sideJumpSpeed = 0.2f; // Toc do cua cu nhay sang ben
    public float leftLimit = -6.5f;
    public float rightLimit = 6.5f;

    private bool isJumping = false;
    private bool isAtPeak = false;
    private Vector3 originalPosition;
    private Vector3 targetPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
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
        if (isAtPeak && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            Vector3 tentativePosition = originalPosition;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                tentativePosition += Vector3.left * moveDistance;
                if (tentativePosition.x >= leftLimit)
                {
                    targetPosition = tentativePosition;
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
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