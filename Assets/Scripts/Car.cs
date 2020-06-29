using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    private GameManager     gameManager;
    public InputHandler     inputHandler;
    private Rigidbody       carRigidbody;
    public Transform        carTranform;
    private BoxCollider     collider;

    [Header("Movement/Jumping")]
    public float        movespeed;
    private float       jumpForce;
    public float        jumpForceMin;
    public float        jumpForceMax;
    private bool        startedJumpForceCalculations = false;
    private float       yStartJumpforceCalculation;
    private GrounCheck  groundCheck;

    [Header("Camera Movement")]
    public float        carLimitRotationMin;
    public float        carLimitRotationMax;
    public float        carSmoothRotatiionFactor;

    private float       minMovementBoundX, maxMovementBoundX;

    [Header("Death Parameters")]
    public float        deathForceY;
    public float        deathForceMinX, deathForceMaxX;
    public float        deathTorgueMin, deathTorgueMax;
    public float        dieTime;
    public float        deathZoneY;
    private bool        hasDied;
    private float       time;
    private float       timer;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        carRigidbody = GetComponent<Rigidbody>();
        groundCheck = GetComponentInChildren<GrounCheck>();
        collider = GetComponent<BoxCollider>();

        minMovementBoundX = gameManager.minXPosition;
        maxMovementBoundX = gameManager.maxXPosition;
    }

    // Update is called once per frame
    void Update()
    {
        updateHorizontalPosition();
        doJumpCalculations(inputHandler.input.position, inputHandler.oldPosition, inputHandler.input.jump, inputHandler.jumpLimitMax);

        if (isVeryMuchDead())
            doDeathSequence();
        else
            RunMainAnimations();
    }

    private bool isVeryMuchDead()
    {
        if (hasDied)
            return hasDied;

        if (transform.position.y < deathZoneY)
        {
            hasDied = true;

            time = Time.deltaTime;

            carRigidbody.constraints = RigidbodyConstraints.None;

            //animation
            float randomForce = Random.Range(deathForceMinX, deathForceMaxX);
            carRigidbody.AddForce(new Vector3(randomForce, deathForceY, randomForce), ForceMode.Impulse);

            float randomTorgue = Random.Range(deathTorgueMin, deathTorgueMax);
            carRigidbody.AddTorque(new Vector3(randomTorgue, randomTorgue, randomTorgue));

            return true;
        }

        return false;
    }

    private void doDeathSequence()
    {
        timer += Time.deltaTime;

        if (timer > time + dieTime || transform.position.y < deathZoneY - 1)
            SceneManager.LoadScene("Deathscreen", LoadSceneMode.Single);

        if (carRigidbody.velocity.y > 5)
            collider.isTrigger = true;
        else
            collider.isTrigger = false;


        //get the score needs to be handled here
        PlayerPrefs.SetInt("tempscore", gameManager.playerScore);
        if (gameManager.playerScore > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", gameManager.playerScore);
            PlayerPrefs.Save();
        }
    }

    private void updateHorizontalPosition()
    {
        float newPositionX = inputHandler.mapValue(Screen.width, inputHandler.input.position.x, minMovementBoundX, maxMovementBoundX);
        Vector3 target = new Vector3(newPositionX, transform.position.y, transform.position.z);
        Vector3 updatedPosition = Vector3.MoveTowards(transform.position, target, movespeed * Time.deltaTime);
        transform.position = updatedPosition;

        float rotationY = inputHandler.mapValue(Screen.width, inputHandler.input.position.x, carLimitRotationMin, carLimitRotationMax);
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

    private void RunMainAnimations()
    {
        if (carRigidbody.velocity.y < -2)
        {
            Quaternion targetRotation = Quaternion.Euler(new Vector3(-20, carTranform.rotation.y, carTranform.rotation.z));
            carTranform.rotation = Quaternion.RotateTowards(carTranform.rotation, targetRotation, Time.deltaTime * 100);
        }
        else if (carRigidbody.velocity.y > 2)
        {
            Quaternion targetRotation = Quaternion.Euler(new Vector3(20, carTranform.rotation.y, carTranform.rotation.z));
            carTranform.rotation = Quaternion.RotateTowards(carTranform.rotation, targetRotation, Time.deltaTime * 100);
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, carTranform.rotation.y, carTranform.rotation.z));
            carTranform.rotation = Quaternion.RotateTowards(carTranform.rotation, targetRotation, Time.deltaTime * 100);
        }
    }
}
