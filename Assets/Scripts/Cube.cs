using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
    public InputHandler inputHandler;

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(inputHandler.input.jump);
        this.transform.position = inputHandler.input.position/10;
        text.text = inputHandler.input.position.ToString();
    }
}
