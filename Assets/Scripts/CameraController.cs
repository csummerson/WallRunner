using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 targetPosition, offset;
    public float smoothTime = .25f;
    Vector3 velocity = Vector3.zero;

    bool spectatingBoss;

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition + offset, ref velocity, smoothTime);

        if (zoomIn) {
            Camera cam = GetComponent<Camera>();
            cam.orthographicSize = Utilities.Damp(cam.orthographicSize, 0.1f, 0.4f, Time.unscaledDeltaTime);
        }
    }

    bool zoomIn = false;

    public void ZoomIn() {
        zoomIn = true;
    }
}
