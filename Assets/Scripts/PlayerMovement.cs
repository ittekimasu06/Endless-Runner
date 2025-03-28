using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 8;
    public float horizontalSpeed = 10;
    public float leftLimit = -6.5f;
    public float rightLimit = 6.5f;
    public float jumpForce = 7f; // Lực nhảy
    public bool isJumping = false;
    public bool isGrounded = false;
    public GameObject player;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Lấy Rigidbody của nhân vật
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.Self);

        // Di chuyển trái
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > leftLimit)
            {
                Debug.Log("Moving Left");
                transform.Translate(Vector3.left * Time.deltaTime * horizontalSpeed);
            }
            else
            {
                Debug.Log("Left Limit Reached");
            }
        }

        // Di chuyển phải
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < rightLimit)
            {
                Debug.Log("Moving Right");
                transform.Translate(Vector3.right * Time.deltaTime * horizontalSpeed);
            }
            else
            {
                Debug.Log("Right Limit Reached");
            }
        }

        // Nhảy
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            isJumping = true;
            isGrounded = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            player.GetComponent<Animator>().Play("Jump");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Kiểm tra va chạm với mặt đất
        {
            isGrounded = true;
            isJumping = false;
        }
    }
}
