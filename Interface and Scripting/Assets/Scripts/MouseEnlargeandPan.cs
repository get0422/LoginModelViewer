using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEnlargeandPan : MonoBehaviour {

    // Private Variables
    private float distance;
    private Vector3 speed;
    private GameObject mainCamera;
    private Vector3 MouseOldPosition;

    // Use this for Initialization
	void Start () {
        // Sets Main Camera
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        // Distance from Camera to Object
        distance = mainCamera.transform.position.z - transform.position.z;
    }

    // Update is called once per frame
    void Update() {
        // Enlarge Model
        if (Input.mouseScrollDelta.y != 0)
        {
            // Distance from Camera to Object
            distance = mainCamera.transform.position.z - transform.position.z;

            if (Input.mouseScrollDelta.y > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x + 0.3f, transform.localScale.y + 0.3f, transform.localScale.z + 0.3f);
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x - 0.3f, transform.localScale.y - 0.3f, transform.localScale.z - 0.3f);
            }
        }

        if (Input.GetMouseButtonDown(2))
        {
            // Gets Current Mouse Position
            MouseOldPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            // Gets the Distance Moved
            speed = Input.mousePosition - MouseOldPosition;

            // Moves it by the Distance / (Width or Height)
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x - (speed.x / (Screen.width/2)), mainCamera.transform.position.y - (speed.y / (Screen.height / 2)), mainCamera.transform.position.z);

            // Sets Old Mouse Position to the Current Position
            MouseOldPosition = Input.mousePosition;
        }
    }
}
