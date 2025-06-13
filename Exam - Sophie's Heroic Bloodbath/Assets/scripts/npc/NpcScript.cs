using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NpcScript : MonoBehaviour
{
    [Header("player")]
    public GameObject player;

    [Header("gameObjects")]
    public Rigidbody2D rigidBody;
    public GameObject firePoint;

    [Header("values")]
    public int detectionRadius = 10;
    [HideInInspector] public float distance;

    [HideInInspector]
    public bool hasLineOfSight = false;

    Vector2 playerPos;

    /*
    private bool isIsometric = false;
    public GameObject render;
    */

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        /*
        if (isIsometric)
        {
            render.transform.rotation = Quaternion.Euler(0f, 0f, gameObject.transform.rotation.z * -1f);
        }
        */

        if (player != null)
        {
            CollisionDetection();
            playerPos = player.transform.position;
        }
        else
            gameObject.GetComponent<NpcScript>().enabled = false;
    }

    public void FixedUpdate()
    {
        if (player != null)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance < detectionRadius && hasLineOfSight && player != null)
            {
                Vector2 lookDirection = playerPos - rigidBody.position;
                float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
                rigidBody.rotation = angle;
            }
        }
    }

    public void CollisionDetection()
    {
        RaycastHit2D ray = Physics2D.Raycast(firePoint.transform.position, player.transform.position - firePoint.transform.position);

        if (ray.collider != null)
        {
            hasLineOfSight = ray.collider.CompareTag(player.tag);

            if (hasLineOfSight && distance < detectionRadius)
            {
                Debug.DrawRay(firePoint.transform.position, player.transform.position - firePoint.transform.position, Color.green);
            }
            else
            {
                Debug.DrawRay(firePoint.transform.position, player.transform.position - firePoint.transform.position, Color.red);
            }
        }
    }
}
