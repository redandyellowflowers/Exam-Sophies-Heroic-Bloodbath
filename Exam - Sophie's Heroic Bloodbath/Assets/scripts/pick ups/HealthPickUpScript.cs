using TMPro;
using TMPro.Examples;
using UnityEngine;

public class HealthPickUpScript : MonoBehaviour
{
    [Header("values")]
    private int health = 25;

    [Header("gameObjects")]
    public GameObject interactText;

    private void Awake()
    {
        interactText.GetComponent<TextMeshPro>().enabled = false;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        interactText.GetComponent<TextMeshPro>().enabled = true;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = true;

        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.Space))
        {
            PlayerHealthScript playerHealth = collision.GetComponent<PlayerHealthScript>();

            if (playerHealth.currentHealth < playerHealth.maxHealth)
            {
                FindAnyObjectByType<AudioManager>().Play("health pick");

                playerHealth.AddHealth(health);
                Destroy(gameObject);
            }
            else
                playerHealth.currentHealth = playerHealth.maxHealth;//meaning nothing happens
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactText.GetComponent<TextMeshPro>().enabled = false;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = false;
    }
}
