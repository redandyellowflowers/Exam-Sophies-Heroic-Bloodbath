using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    /*
    Title: How to Make Camera Follow In UNITY 2D
    Author: Anup Prasad
    Date: 08 April 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=FXqwunFQuao

    ^^^this is referencing the camera follow feature
     */

    /*
    Title: Simple Scroll Zoom Unity Tutorial
    Author: Gatsby
    Date: 17 May 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=Iz9AMF2dZdw

    ^^^this is referencing the camera zoom feature
    */

    [Header("player")]
    public GameObject player;
    public Camera cam;

    [Header("values")]
    public float followSpeed = 2f;
    public float yOffset = 1f;
    public float xOffset = 0f;

    [field: SerializeField, Header("camera zoom")]

    private float zoomTarget;

    [SerializeField] private float multiplier = 2f;//increase rate as which we can zoom
    [SerializeField] private float minZoom = 1f;
    [SerializeField] private float maxZoom = 10f;
    [SerializeField] private float smoothTime = .1f;
    private float Velocity = 0f;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        cam = gameObject.GetComponentInChildren<Camera>();

        cam.orthographicSize = 20f;
    }

    void Start()
    {
        zoomTarget = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        zoomTarget -= Input.GetAxisRaw("Mouse ScrollWheel") * multiplier;
        zoomTarget = Mathf.Clamp(zoomTarget, minZoom, maxZoom);//mathf.clamp clamps the zoom between two values (min, and max)
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoomTarget, ref Velocity, smoothTime);

        if (player == null)
        {
            gameObject.GetComponent<CameraFollowScript>().enabled = false;
        }
        else
        {
            Vector3 newPosition = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, -10);//defines the position (the current x, y axes, or the "newPosition") of the object to be followed (which is executed in the following line)
            transform.position = Vector3.Slerp(transform.position, newPosition, followSpeed * Time.deltaTime);
        }
    }
}
