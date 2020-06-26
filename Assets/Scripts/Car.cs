using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public InputHandler inputHandler;
    public Vector2 bounds;
    public float jumpForce;

    private Rigidbody rb;
    private Bounds bodyBounds;

    private bool jumpFlag = false;
    private GrounCheck groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        bodyBounds = GetComponent<Collider>().bounds;

        groundCheck = GetComponentInChildren<GrounCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        float newPositionX = mapValue(inputHandler.input.position.x, bounds.x, bounds.y);
        transform.position = new Vector2(newPositionX, transform.position.y);

        Debug.Log(groundCheck.isGrounded);
        
        if (groundCheck.isGrounded && inputHandler.input.jump)
        {
            jumpFlag = false;

            rb.AddForce(new Vector3(0, jumpForce, 0));
        }

        if (!inputHandler.isInJumpZone)
        {
            jumpFlag = true;
        }
    }

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
}
