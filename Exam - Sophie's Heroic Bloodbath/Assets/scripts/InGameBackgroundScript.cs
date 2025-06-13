using UnityEngine;

public class InGameBackgroundScript : MonoBehaviour
{
    public static InGameBackgroundScript instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(gameObject);
    }
}
