using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct Inputs
{
    public Vector2 position;
    public bool jump;
}

public class InputHandler : MonoBehaviour
{
    public Inputs input;

    public float yRegionBoundPercentage     = 30.0f;
    public float yMoveLimit;           

    public float yJumpBoundPercentage       = 20.0f;
    public bool isInJumpZone                = false;
    public Vector2 yJumpLimits;
    public float yJumpSize;

    private void Awake()
    {
        //get percentage factor
        yRegionBoundPercentage  /= 100.0f;
        yJumpBoundPercentage    /= 100.0f;
    }

    private void Update()
    {
        CheckMovementBounds(new Vector2(Screen.width, Screen.height));

        //input.jump = Input.GetKeyDown(KeyCode.Space);
        input.jump = CheckJumpBounds();
    }

    private Vector2 CheckInputDevice(Vector2 oldPosition)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Touch touch = Input.GetTouch(0);
            return touch.position;
        }
        else
        {
            if (Input.GetMouseButton(2)) //middle mouse button
            {
                return Input.mousePosition;
            }
        }

        return oldPosition;
    }

    /// <summary>
    /// Checks if the user is within the bounds for movement
    /// </summary>
    /// <param name="bounds">widht and height (in that order) of the screen</param>
    private void CheckMovementBounds(Vector2 bounds)
    {
        //get the old position
        Vector2 positionOld = input.position;

        yMoveLimit = bounds.y * yRegionBoundPercentage;

        input.position = CheckInputDevice(positionOld);

        //check if the user input is outsize a set bound
        //if (input.position.y > yMoveLimit)
        //{
        //    input.position.y = yMoveLimit;
        //    input.position.x = positionOld.x;
        //}

        if (input.position.x > bounds.x)
            input.position.x = bounds.x;

        if (input.position.x < 0)
            input.position.x = 0;
    }

    /// <summary>
    /// Checks if the user input is within the jump bounds
    /// if it is then a flag is set 
    /// when leaving the bound the flag is reset
    /// </summary>
    private bool CheckJumpBounds()
    {
        yJumpSize = yMoveLimit * yJumpBoundPercentage;
        yJumpLimits = new Vector2(yMoveLimit, yMoveLimit + yJumpSize);

        //check if the input is within bounds
        if (input.position.y > yJumpLimits.x && input.position.y < yJumpLimits.y && !isInJumpZone)
        {
            isInJumpZone = true;
            //Debug.Log("jumping");
            return true;
        }

        if (input.position.y < yJumpLimits.x)
        {
            isInJumpZone = false;
            //Debug.Log("not jumping");
            return false;
        }

        return false;
    }
}

































