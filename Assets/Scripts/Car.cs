using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private GameManager     gameManager;
    public InputHandler     inputHandler;
    private Rigidbody       carRigidbody;

    public float            movespeed;
    public float            jumpForceMin;
    public float            jumpForceMax;

    public float            cameraLimitRotationMin, cameraLimitRotationMax;
    public float            carSmoothRotatiionFactor;

    private float           jumpForce;
    private bool            startedJumpForceCalculations = false;
    private float           yStartJumpforceCalculation;
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
        updateHorizontalPosition();
        doJumpCalculations(inputHandler.input.position, inputHandler.oldPosition, inputHandler.input.jump, inputHandler.yJumpLimits.y);
    }

    private void updateHorizontalPosition()
    {
        float newPositionX = inputHandler.mapValue(Screen.width, inputHandler.input.position.x, xMin, xMax);
        Vector3 target = new Vector3(newPositionX, transform.position.y, transform.position.z);
        Vector3 updatedPosition = Vector3.MoveTowards(transform.position, target, movespeed * Time.deltaTime);
        transform.position = updatedPosition;

        float rotationY = inputHandler.mapValue(Screen.width, inputHandler.input.position.x, cameraLimitRotationMin, cameraLimitRotationMax);
        Quaternion targetRotation = Quaternion.Euler(new Vector3(transform.rotation.x, -rotationY, transform.rotation.z));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * carSmoothRotatiionFactor);
    }

    private void doJumpCalculations(Vector3 aPosition, Vector3 aOldPosition, bool aJump, float xEndPosition)
    {
        if (aPosition.y > aOldPosition.y && !startedJumpForceCalculations)
        {
            startedJumpForceCalculations = true;

            yStartJumpforceCalculation = aPosition.y;
        }
        else if (aPosition.y < aOldPosition.y)
        {
            startedJumpForceCalculations = false;
        }

        if (aJump)
        {
            jumpForce = inputHandler.mapValue(xEndPosition, xEndPosition - yStartJumpforceCalculation, jumpForceMin, jumpForceMax);
        }
        
        if (groundCheck.isGrounded && aJump)
        {
            carRigidbody.AddForce(new Vector3(0, jumpForce, 0));
        }
    }
}
