using Unity.VisualScripting;
using UnityEngine;

public class EnemyShootingScript : MonoBehaviour
{
    [Header("player")] 
    public GameObject player;

    [HideInInspector]
    public bool hasLineOfSight = false;

    [Header("weapon gameObjects")]
    public GameObject firePoint;
    //public LineRenderer lineRenderer;
    public GameObject bulletPrefab;
    public ParticleSystem muzzleflash;

    [Header("animations")]
    public Animator anim;

    [Header("values")]
    public float bulletForce = 20f;

    private float nextTimeToFire = 0f;
    private float fireRate = 6.5f;

    private bool isShooting = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (player != null)
        {
            Shooting();
        }
        else
            gameObject.GetComponent<EnemyShootingScript>().enabled = false;
    }

    public void Shooting()
    {
        isShooting = false;

        RaycastHit2D ray = Physics2D.Raycast(firePoint.transform.position, player.transform.position - firePoint.transform.position);

        if (ray.collider != null)
        {
            EnemyControllerScript enemyMove = gameObject.GetComponent<EnemyControllerScript>();

            hasLineOfSight = ray.collider.CompareTag(player.tag);

            if (hasLineOfSight && enemyMove.distance < enemyMove.detectionRadius)//if player is in the enemies line of sight, and within the detecttion radius
            {
                if (Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f / fireRate;//adds a bit of delay before the enemy fires (by dividing 1 by the fire rate and adding that to the time.time, which is the current "game" time)

                    MuzzleFlash();
                    isShooting = true;

                    FindAnyObjectByType<AudioManager>().Play("enemy shoot");

                    GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(firePoint.transform.up * bulletForce, ForceMode2D.Impulse);
                }

                /*
                lineRenderer.SetPosition(0, firePoint.transform.position);
                lineRenderer.SetPosition(1, ray.point);
                */

                Debug.DrawRay(firePoint.transform.position, player.transform.position - firePoint.transform.position, Color.green);
            }
            else
            {
                isShooting = false;

                Debug.DrawRay(firePoint.transform.position, player.transform.position - firePoint.transform.position, Color.red);
            }
        }
        anim.SetBool("isShooting", isShooting);
    }

    public void MuzzleFlash()
    {
        muzzleflash.Emit(30);
    }
}
