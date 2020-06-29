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
    public Inputs       input;

    [Header("Movement")]
    public float        moveLimitPercentage = 30.0f;
    public float        moveLimitMax;
    public float        moveLimitMin;
    public float        moveLimitPosition;
    public float        moveLimitSize;

    [Header("Jumping")]
    public float        jumpLimitPercentage = 30.0f;
    public float        jumpLimitMax;
    public float        jumpLimitMin;
    public float        jumpLimitPosition;
    public float        jumpLimitSize;
    public bool         isInJumpZone = false;

    public Vector3      oldPosition;

    private void Awake()
    {
        //get percentage factor
        moveLimitPercentage     /= 100.0f;
        jumpLimitPercentage     /= 100.0f;

        moveLimitSize   = moveLimitPercentage * Screen.height;
        moveLimitMax    = moveLimitPosition + moveLimitSize/2;
        moveLimitMin    = moveLimitPosition - moveLimitSize/2;

        jumpLimitSize       = jumpLimitPercentage * Screen.height;
        jumpLimitMax        = moveLimitMax + jumpLimitSize;
        jumpLimitMin        = moveLimitMax;
        jumpLimitPosition   = moveLimitMax + jumpLimitSize/2;

        moveLimitMax    += jumpLimitSize;
    }

    private void Update()
    {
        input.position  = CheckMovementBounds   (new Vector2(Screen.width, Screen.height));
        input.jump      = CheckJumpBounds       (new Vector2(Screen.width, Screen.height));
    }

    private Vector2 GetTouchPosition()
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
        oldPosition             = input.position;
        Vector2 returnPosition  = GetTouchPosition();
        
        if (returnPosition.x > aBounds.x)
            returnPosition.x = aBounds.x;

        if (returnPosition.x < 0)
            returnPosition.x = 0;

        if (returnPosition.y > moveLimitMax || returnPosition.y < moveLimitMin)
            returnPosition = oldPosition;

        return returnPosition;
    }

    /// <summary>
    /// Checks if the user input is within the jump bounds
    /// if it is then a flag is set 
    /// when leaving the bound the flag is reset
    /// </summary>
    private bool CheckJumpBounds(Vector2 aBounds)
    {
        //check if the input is within bounds
        if (!isInJumpZone
            && input.position.y < jumpLimitMax 
            && input.position.y > jumpLimitMin)
        {            
            return isInJumpZone = true; 
        }

        if (input.position.y < jumpLimitMin)
        {
            return isInJumpZone = false;
        }

        return false;
    }


    //copied from arduino :)
    public float mapValue(float in_max, float update_this, float out_min, float out_max)
    {
        float finalPos;
        float factor = (out_max - out_min) / (in_max);

        finalPos = update_this * factor + out_min;

        if (finalPos < out_min)
            finalPos = out_min;

        if (finalPos > out_max)
            finalPos = out_max;

        return finalPos;
    }
}

































