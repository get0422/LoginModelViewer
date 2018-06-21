using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour {

    // Private Variables
    private Vector3 mousePosition;
    private Vector3 mouseOffset;
    private bool isRotating;

    public Quaternion ModelTransform;

    // Use this for Initialization
    void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            OnRightClick();

        else if (Input.GetMouseButtonUp(1))
            OnMouseRightClickUp();

        // Checks if its Rotating
        if (isRotating)
        {
            // Offset Position
            mouseOffset = (Input.mousePosition - mousePosition);

            // Calculate Rotation
            Rotate(-mouseOffset.x, -mouseOffset.y);

            // Apply Rotation
            transform.rotation *= ModelTransform;

            // Store Old Mouse Position
            mousePosition = Input.mousePosition;
        }
    }

    void OnRightClick()
    {
        // Rotation Flag
        isRotating = true;

        // Store Mouse Position
        mousePosition = Input.mousePosition;
    }

    void OnMouseRightClickUp()
    {
        // Rotation Flag
        isRotating = false;
    }

    public void Rotate(float rotateLeftRight, float rotateUpDown)
    {
        //Unsure of how much below code changes outcome.
        float sensitivity = .5f;

        //Get Main camera in Use.
        Camera cam = Camera.main;
        //Gets the world vector space for cameras up vector 
        Vector3 relativeUp = cam.transform.TransformDirection(Vector3.up);
        //Gets world vector for space cameras right vector
        Vector3 relativeRight = cam.transform.TransformDirection(Vector3.right);

        //Turns relativeUp vector from world to objects local space
        Vector3 objectRelativeUp = transform.InverseTransformDirection(relativeUp);
        //Turns relativeRight vector from world to object local space
        Vector3 objectRelaviveRight = transform.InverseTransformDirection(relativeRight);

        ModelTransform = Quaternion.AngleAxis(rotateLeftRight / gameObject.transform.localScale.x * sensitivity, objectRelativeUp)
                * Quaternion.AngleAxis(-rotateUpDown / gameObject.transform.localScale.x * sensitivity, objectRelaviveRight);
    }
}
