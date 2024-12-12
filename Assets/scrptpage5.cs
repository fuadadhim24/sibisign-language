using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scrptpage5 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void buttonBelajar(string sceneName)
    {
        // Corrected method call
        SceneManager.LoadScene(sceneName);
    }
}
