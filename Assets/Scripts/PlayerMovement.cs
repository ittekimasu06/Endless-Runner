using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 8;
    public float horizontalSpeed = 10;
    public float leftLimit = -6.5f;
    public float rightLimit = 6.5f;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.Self);

        //move left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.gameObject.transform.position.x > leftLimit)
            {
                Debug.Log("Moving Left");
                transform.Translate(Vector3.left * Time.deltaTime * horizontalSpeed);
            }
            else
            {
                Debug.Log("Left Limit Reached");
            }
        }

        //move right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (this.gameObject.transform.position.x < rightLimit)
            {
                Debug.Log("Moving Right");
                transform.Translate(Vector3.right * Time.deltaTime * horizontalSpeed);
            }
            else
            {
                Debug.Log("Right Limit Reached");
            }
        }
    }
}
