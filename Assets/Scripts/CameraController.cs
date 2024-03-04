using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform character;
    [SerializeField] private Camera cam;
    [SerializeField] private Vector3 cameraPosition;
    [SerializeField] private float minCameraVertical;
    [SerializeField] private float maxCameraVertical;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float camSpeed;
    [SerializeField] private float zoomModifierSpeed;

    public Vector3 previousPosition;
    private float xRotation;

    void Update()
    {
        CameraRotation();
        CameraZoom();
    }

    private void CameraRotation()
    {
        Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

        transform.position = character.position;

        xRotation += direction.y * (rotationSpeed / 2);
        xRotation = Mathf.Clamp(xRotation, minCameraVertical, maxCameraVertical);

        //x축 회전
        transform.localRotation = Quaternion.Euler(xRotation, transform.localEulerAngles.y, transform.localEulerAngles.z);

        //y축 회전
        transform.Rotate(new Vector3(0, 1, 0), -direction.x * rotationSpeed, Space.World);
        transform.Translate(new Vector3(0, 0, -10));

        previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
    }

    private void CameraZoom()
    {
        float zoomModifier = Input.GetAxis("Mouse ScrollWheel");
        float zoom = cam.fieldOfView - (zoomModifier * zoomModifierSpeed);
        zoom = Mathf.Clamp(zoom, 30, 90);

        cam.fieldOfView = zoom;
    }
}