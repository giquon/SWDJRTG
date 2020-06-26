using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Inputs
{
    public Vector2 position;
    public bool jump;
}

public class InputHandler : MonoBehaviour
{
    public Inputs input;

    public float yRegionBoundPercentage     = 30.0f;
    public float yTouchLimit;           

    public float yJumpBoundPercentage       = 20.0f;
    private bool isInJumpZone = false;
    private Vector2 yJumpLimits;

    private void Awake()
    {
        //get percentage factor
        yRegionBoundPercentage  /= 100.0f;
        yJumpBoundPercentage    /= 100.0f;
    }

    private void Update()
    {
        input.jump = Input.GetKeyDown(KeyCode.Space);

        CheckMoveMentBounds(new Vector2(Screen.width, Screen.height));
        CheckJumpBounds();
        input.position = CheckInputDevice();
    }

    private Vector2 CheckInputDevice()
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

        return Vector2.one;
    }

    /// <summary>
    /// Checks if the user is within the bounds for movement
    /// </summary>
    /// <param name="bounds">widht and height (in that order) of the screen</param>
    private void CheckMoveMentBounds(Vector2 bounds)
    {
        //get the old position
        Vector2 positionOld = input.position;

        yTouchLimit = bounds.y * yRegionBoundPercentage;

        //check if the user input is outsize a set bound
        if (input.position.y > yTouchLimit)
        {
            input.position.y = yTouchLimit;
            input.position.x = positionOld.x;
        }
    }

    /// <summary>
    /// Checks if the user input is within the jump bounds
    /// if it is then a flag is set 
    /// when leaving the bound the flag is reset
    /// </summary>
    private void CheckJumpBounds()
    {
        yJumpLimits = new Vector2(yTouchLimit * yJumpBoundPercentage, yTouchLimit);

        //check if the input is within bounds
        if (input.position.y > yJumpLimits.x && input.position.y < yTouchLimit && !isInJumpZone)
        {
            input.jump = true;
            //Debug.Log("jumping");
        }

        if (input.position.y < yJumpLimits.x)
        {
            input.jump = false;
            //Debug.Log("not jumping");
        }
    }
}

































