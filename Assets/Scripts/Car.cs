using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public InputHandler inputHandler;
    public Vector2 bounds;
    private bool jumpFlag = false;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        playerVelocity.x = ClampOneOne(inputHandler.input.position.x, Screen.width/2);

        if (transform.position.x < bounds.x && playerVelocity.x < 0)
            playerVelocity.x = 0;

        if (transform.position.x > bounds.y && playerVelocity.x > 0)
            playerVelocity.x = 0;

        Vector3 move = new Vector3(playerVelocity.x, 0, 0);
        controller.Move(move * Time.deltaTime * playerSpeed);

        transform.position = new Vector3(0, 0, 0);


        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}

        // Changes the height position of the player..
        //if (inputHandler.input.jump && groundedPlayer)
        //{
        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //}

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    /// <summary>
    /// Depricated
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    //public bool checkIfCanJump(int i)
    //{
    //    float range = 1;
    //    RaycastHit hit;
    //    //float xOrigin = transform.position.x - bodyBounds.size.x / 2 * i;
    //    //float yOrigin = transform.position.y - bodyBounds.size.y / 2;
    //    //float zOrigin = transform.position.z - bodyBounds.size.z / 2;
    //    //Vector3 rayOrigin = new Vector3(xOrigin, yOrigin, zOrigin);

    //    Vector3 direction = transform.TransformDirection(Vector3.down) * range;

    //    if (Physics.Raycast(rayOrigin, direction, out hit))
    //    {
    //        Debug.DrawRay(rayOrigin, direction, Color.blue, 0.02f);
    //    }
    //    else
    //    {
    //        Debug.DrawRay(rayOrigin, direction, Color.red, 0.02f);
    //    }

    //    return false;
    //}

    private float mapValue(float in_max, float out_min, float out_max)
    {
        float finalPos;
        float factor = (out_max - out_min) / (Screen.width);

        finalPos = inputHandler.input.position.x * factor + out_min;

        if (finalPos < -3)
            finalPos = -3;

        if (finalPos > 3)
            finalPos = 3;

        return finalPos;
    }

    private float ClampOneOne(float value, float threshold)
    {
        if (value > threshold)
            return 1;

        return -1;
    }
}
