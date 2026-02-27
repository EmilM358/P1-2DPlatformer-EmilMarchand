using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D targetRb;
    [Header("Movement")]
    public float smoothSpeed = 5f;
    public float fixedY = 0f;


    [Header("Bounds")]
    public Transform leftBoundary;
    public Transform rightBoundary;


    private Camera cam;
    private float halfCameraWidth;
    public float yOffset = 5f;


    void Start()
    {
        cam = GetComponent<Camera>();
        targetRb = target.GetComponent<Rigidbody2D>();
        halfCameraWidth = cam.orthographicSize * cam.aspect;
    }


    void LateUpdate()
    {
        if (!target) return;


        float targetX = targetRb.position.x;


        float minX = leftBoundary.position.x + halfCameraWidth;
        float maxX = rightBoundary.position.x - halfCameraWidth;


        float clampedX = Mathf.Clamp(targetX, minX, maxX);


        Vector3 desiredPosition = new Vector3(
            clampedX,
            targetRb.position.y + yOffset,
            transform.position.z
        );


        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}
