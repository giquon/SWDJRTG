using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputUI : MonoBehaviour
{
    private InputHandler inputHandler;

    public GameObject moveRegion;
    public GameObject jumpRegion;

    private Image jumpImageRegion;
    private Image moveImageRegion;

    // Use this for initialization
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();

        //get image component
        moveImageRegion = moveRegion.GetComponent<Image>();
        jumpImageRegion = jumpRegion.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //render the move image and scale it to size
        moveImageRegion.rectTransform.sizeDelta = new Vector2(Screen.width, inputHandler.yMoveLimit);
        moveRegion.transform.position = new Vector3(Screen.width / 2, inputHandler.yMoveLimit / 2, 0);

        //render the jump image and scale it to size
        jumpImageRegion.rectTransform.sizeDelta = new Vector2(Screen.width, inputHandler.yJumpSize);
        jumpRegion.transform.position = new Vector3(Screen.width / 2, inputHandler.yMoveLimit + inputHandler.yJumpSize / 2, 0);
    }
}
