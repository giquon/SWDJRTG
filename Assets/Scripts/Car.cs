using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private GameManager     gameManager;
    public InputHandler     inputHandler;
    private Rigidbody       carRigidbody;

    public float            movespeed;
    public float            jumpForce;
    private bool            jumpFlag = false;
    private GrounCheck      groundCheck;

    private float           xMin, xMax;

    // Start is called before the first frame update
    void Start()
    {
        gameManager     = GameManager.instance;
        carRigidbody    = GetComponent<Rigidbody>();
        groundCheck     = GetComponentInChildren<GrounCheck>();

        xMin            = gameManager.minXPosition;
        xMax            = gameManager.maxXPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float newPositionX = mapValue(inputHandler.input.position.x, xMin, xMax);
        Vector3 target = new Vector3(newPositionX, transform.position.y, transform.position.z);
        Vector3 updatedPosition = Vector3.MoveTowards(transform.position, target, movespeed * Time.deltaTime);
        transform.position = updatedPosition;

        Debug.Log(target);

        if (groundCheck.isGrounded && inputHandler.input.jump)
        {
            jumpFlag = false;

            carRigidbody.AddForce(new Vector3(0, jumpForce, 0));
        }

        if (!inputHandler.isInJumpZone)
        {
            jumpFlag = true;
        }
    }

    //copied from arduino :)
    private float mapValue(float in_max, float out_min, float out_max)
    {
        float finalPos;
        float factor = (out_max - out_min) / (Screen.width);

        finalPos = inputHandler.input.position.x * factor + out_min;

        if (finalPos < out_min)
            finalPos = out_min;

        if (finalPos > out_max)
            finalPos = out_max;

        return finalPos;
    }
}
