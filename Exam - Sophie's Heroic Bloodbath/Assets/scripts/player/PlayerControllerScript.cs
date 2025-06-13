using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerControllerScript : MonoBehaviour
{
    //bool isIsometric = false;

    [Header("gameobjects")]
    public Rigidbody2D rigidBody;
    public Camera cam;

    [Header("base movement")]
    public float moveSpeed = 12f;
    
    [Header("bullet time")]
    public float currentStamina;
    public float MaxStamina;
    public float slowMoRate;
    //public Slider bulletTimeBar;
    public TextMeshProUGUI slowMoText;

    /*
    [Header("rendering")]
    public GameObject render;
    */

    Vector2 mousePos;

    [Header("animation/s")]
    private Animator anim;
    bool isWalking = false;
    //bool bulletTime = false;

    [Header("volume")]
    //[SerializeField] private Volume volume;
    public GameObject volume;

    private void Awake()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }

        volume = GameObject.Find("Global Volume");

        anim = gameObject.GetComponentInChildren<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
        bulletTimeBar.maxValue = MaxStamina;
        bulletTimeBar.value = currentStamina;
        */
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        MoveMent();

        /*
        if (isIsometric)
        {
            render.transform.rotation = Quaternion.Euler(0f, 0f, gameObject.transform.rotation.z * -1f);//locks rotation of child object by countering the rotation of the parent of object, important for potential walk anim
        }
        */

        slowMoText.text = currentStamina.ToString("0.00000") + "%";//REMEMBER, the "0" ensures that the value is shown as an integer
    }

    void FixedUpdate()
    {
        /*
        Title: TOP DOWN SHOOTING in Unity
        Author: Asbjørn Thirslund / Brackeys
        Date: 06 April 2025
        Code version: 1
        Availability: https://www.youtube.com/watch?v=LNLVOjbrQj4

        ^^^it is the "character controller facing the cursor" mechanic part of the above tutorial being referenced
        */
        Vector2 lookDirection = mousePos - rigidBody.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rigidBody.rotation = angle;
    }

    public void MoveMent()
    {
        isWalking = false;
        //bulletTime = false;

        Vector3 movePosition = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movePosition.y += moveSpeed;

            isWalking = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movePosition.y -= moveSpeed;

            isWalking = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movePosition.x -= moveSpeed;

            isWalking = true;
            //transform.localScale = new Vector3(-1, transform.localScale.y);//flip the sprite left
        }
        if (Input.GetKey(KeyCode.D))
        {
            movePosition.x += moveSpeed;

            isWalking = true;
            //transform.localScale = new Vector3(1, transform.localScale.y);//flip the sprite left
        }

        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (currentStamina != 0)
            {
                float velocity = 0f;
                cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, 45, ref velocity, .1f);
            }

            if (volume.GetComponent<Volume>().profile.TryGet(out ColorAdjustments colorAdjust))
            {
                colorAdjust.hueShift.value = -30;
            }

            //bulletTime = true;

            Time.timeScale = .5f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            currentStamina -= slowMoRate;

            if (currentStamina <= 0)
            {
                if (volume.GetComponent<Volume>().profile.TryGet(out ColorAdjustments colorAdjustment))
                {
                    colorAdjustment.hueShift.value = 0;
                }

                //bulletTime = false;

                currentStamina = 0;
                Time.timeScale = 1f;
            }

            //bulletTimeBar.value = currentStamina;
            slowMoText.text = currentStamina.ToString("0.00000") + "%";
        }
        else
            if (currentStamina != MaxStamina)
        {
            //DISABLE/RUN BULLET TIME VISUALIZER HERE***
            //StartCoroutine(Flash());

            if (volume.GetComponent<Volume>().profile.TryGet(out ColorAdjustments colorAdjust))
            {
                colorAdjust.hueShift.value = 0;
            }

            //bulletTime = false;

            Time.timeScale = 1f;

            currentStamina += slowMoRate;

            //bulletTimeBar.value = currentStamina;

            if (currentStamina >= MaxStamina)
            {
                currentStamina = MaxStamina;
            }
        }
        

        rigidBody.transform.position += movePosition.normalized * moveSpeed * Time.deltaTime;//this line (more so the "normalized" and "time.deltatime") essentially stops the controller from building up an unfixed amount of momentum

        anim.SetBool("isWalking", isWalking);
        //anim.SetBool("slow motion", bulletTime);
    }
}
