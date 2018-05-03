using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour {

    // Public Variables
    public bool isMoving;

    // Private Variables
    private float distance;
    private GameObject mainCamera;

    // Use this for Initialization
    void Start() {

        // Sets if its moving
        isMoving = false;

        // Sets to Main Camera
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        // Distance from Camera to Object
        distance = mainCamera.transform.position.z - transform.position.z;
    }

    void OnMouseDrag()
    {
        // Sets if Object is moving
        isMoving = true;

        // Distance from Camera to Object
        distance = mainCamera.transform.position.z - transform.position.z;

        // Store Mouse Position
        Vector3 mousePosition = new Vector3(Input.mousePosition.x - transform.position.x, Input.mousePosition.y, -distance);

        // Store Objects Position
        Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Set Objects New Position
        transform.position = objectPosition;
    }

    void OnMouseUp()
    {
        // Turns Moving off
        isMoving = false;
    }
}
