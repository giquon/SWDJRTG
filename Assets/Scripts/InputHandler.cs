using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct Inputs
{
    public Vector2  position;
    public bool     jump;
}

public class InputHandler : MonoBehaviour
{
    public Inputs   input;

    public float        yRegionBoundPercentage     = 30.0f;
    public float        yJumpBoundPercentage       = 20.0f;

    public float        yMoveLimit;
    public Vector2      yJumpLimits;
    public bool         isInJumpZone                = false;
    public float        yJumpSize;


    public Vector3      oldPosition;

    private void Awake()
    {
        //get percentage factor
        yRegionBoundPercentage  /= 100.0f;
        yJumpBoundPercentage    /= 100.0f;
    }

    private void Update()
    {
        input.position  = CheckMovementBounds   (new Vector2(Screen.width, Screen.height));
        input.jump      = CheckJumpBounds       (new Vector2(Screen.width, Screen.height));
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

        return oldPosition;
    }

    /// <summary>
    /// Checks if the user is within the bounds for movement
    /// </summary>
    /// <param name="bounds">widht and height (in that order) of the screen</param>
    private Vector2 CheckMovementBounds(Vector2 aBounds)
    {
        Vector2 returnPosition;

        oldPosition = input.position;
        returnPosition  = CheckInputDevice();
        
        if (returnPosition.x > aBounds.x)
            returnPosition.x = aBounds.x;

        if (returnPosition.x < 0)
            returnPosition.x = 0;

        if (returnPosition.y > yJumpLimits.y)
            returnPosition = oldPosition;

        yMoveLimit      = aBounds.y * yRegionBoundPercentage;

        return returnPosition;
    }

    /// <summary>
    /// Checks if the user input is within the jump bounds
    /// if it is then a flag is set 
    /// when leaving the bound the flag is reset
    /// </summary>
    private bool CheckJumpBounds(Vector2 aBounds)
    {
        yJumpSize = aBounds.x * yJumpBoundPercentage;
        yJumpLimits = new Vector2(yMoveLimit, yMoveLimit + yJumpSize);

        //check if the input is within bounds
        if (input.position.y > yJumpLimits.x 
        &&  input.position.y < yJumpLimits.y
        && !isInJumpZone)
        {            
            return isInJumpZone = true; 
        }

        if (input.position.y < yJumpLimits.x)
        {
            return isInJumpZone = false;
        }

        return false;
    }
}

































