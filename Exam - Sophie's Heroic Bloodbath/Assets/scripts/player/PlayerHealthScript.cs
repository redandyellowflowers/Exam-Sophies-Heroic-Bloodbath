using EZCameraShake;
using TMPro;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [Header("values")]
    public int currentHealth = 100;
    public int maxHealth = 100;
    private int lowHealthAlert = 25;

    [Header("UI")]
    //public Slider healthBar;
    public TextMeshProUGUI healthText;
    public GameObject damageScreenUI;

    [Header("gameObjects")]
    public GameObject bloodSplatter;
    public GameObject bloodExplosionEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthText.text = currentHealth + "%".ToString();

        /*
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= lowHealthAlert)
        {
            damageScreenUI.SetActive(true);
        }
        else
            damageScreenUI.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        //healthBar.value = currentHealth;
        healthText.text = currentHealth + "%".ToString();

        GameObject groundBloodSplatter = Instantiate(bloodSplatter, gameObject.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        //Destroy(groundBloodSplatter, 3f);

        if (currentHealth <= 0)
        {
            FindAnyObjectByType<AudioManager>().Play("enemy death");

            CameraShaker.Instance.ShakeOnce(6f, 1f, .1f, .4f);

            GameObject BloodExplosion = Instantiate(bloodExplosionEffect, gameObject.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(BloodExplosion, 1f);
            damageScreenUI.SetActive(false);

            Destroy(gameObject);
        }
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
        //healthBar.value = currentHealth;
        healthText.text = currentHealth + "%".ToString();

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
