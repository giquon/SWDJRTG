using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public InputHandler inputHandler;

    public float CameraSpeedSmoothingPosition;
    public float CameraSpeedSmoothingRotation;

    public float CameraLimitPositionX, CameraLimitPositionY;
    public float CameraLimitRotationX, CameraLimitRotationY;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //positions
        float positionX             = inputHandler.mapValue(Screen.width, inputHandler.input.position.x, CameraLimitPositionX, CameraLimitPositionY);
        Vector3 targetPosition      = new Vector3(positionX, transform.position.y, transform.position.z);
        transform.position          = Vector3.MoveTowards(transform.position, targetPosition, CameraSpeedSmoothingPosition * Time.deltaTime);

        //rotation
        float rotationY             = inputHandler.mapValue(Screen.width, inputHandler.input.position.x, CameraLimitRotationX, CameraLimitRotationY);
        Quaternion targetRotation   = Quaternion.Euler(new Vector3(18, -rotationY, transform.rotation.z));
        transform.rotation          = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * CameraSpeedSmoothingRotation);
    }
}
