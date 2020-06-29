using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputUI : MonoBehaviour
{
    private InputHandler inputHandler;

    public GameObject   moveRegion;
    public GameObject   jumpRegion;

    private Vector2     jumpRegionPosition;
    private Vector2     jumpRegionSize;

    private Vector2     moveRegionPosition;
    private Vector2     moveRegionSize;

    // Use this for initialization
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();

        jumpRegionPosition      = new Vector2(Screen.width / 2, inputHandler.jumpLimitPosition);
        moveRegionPosition      = new Vector2(Screen.width / 2, inputHandler.moveLimitPosition);

        jumpRegionSize          = new Vector2(Screen.width, inputHandler.jumpLimitSize);
        moveRegionSize          = new Vector2(Screen.width, inputHandler.moveLimitSize);

        //render the move image and scale it to size
        changeImageTransform(jumpRegion, jumpRegionSize, jumpRegionPosition);
        changeImageTransform(moveRegion, moveRegionSize, moveRegionPosition);
    }

    private void changeImageTransform(GameObject aGameObject, Vector2 aSize, Vector2 aPosition)
    {
        aGameObject.GetComponent<Image>().rectTransform.sizeDelta   = aSize;
        aGameObject.transform.position                              = aPosition;
    }
}
