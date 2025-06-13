using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TargetHealthScript : MonoBehaviour
{
    public bool isEnemy = true;

    [Header("instantiated object")]
    public GameObject medicalKit;

    [Header("gameObjects")]
    public GameObject bloodSplatter;
    public GameObject bloodExplosionEffect;

    [Header("health indicators")]
    public Light2D lightOBJ;
    public TextMeshPro text;

    [Header("values")]
    private int health = 90;

    [Header("score")]
    public LogicScript logic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (text != null)
        {
            text.text = health.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (text != null)
        {
            text.transform.rotation = Quaternion.Euler(0f, 0f, gameObject.transform.rotation.z * -1f);
        }
    }

    public void takeDamage(int amount)
    {
        health -= amount;

        if (isEnemy) 
        {
            StartCoroutine(LightFX());
        }

        if (text != null)
        {
            text.text = health.ToString();
        }

        if (health <= 0)
        {
            if (logic != null)
            {
                logic.addScore(5);
            }

            FindAnyObjectByType<AudioManager>().Play("enemy death");

            GameObject groundBloodSplatter = Instantiate(bloodSplatter, gameObject.transform.position, gameObject.transform.rotation);

            GameObject BloodExplosion = Instantiate(bloodExplosionEffect, gameObject.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(BloodExplosion, 1f);

            Destroy(gameObject);

            if (isEnemy != true)
            {
                GameObject medkit = Instantiate(medicalKit, gameObject.transform.position, gameObject.transform.rotation);
            }
        }
        else
        {
            //GameObject groundBloodSplatter = Instantiate(bloodSplatter, gameObject.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            //Destroy(groundBloodSplatter, 1.5f);
        }
    }

    public IEnumerator LightFX()//when the enemy is hit, a red light is emmitted to indictate it
    {
        lightOBJ.color = Color.red;

        yield return new WaitForSeconds(.1f);

        lightOBJ.color = Color.white;
    }
}
