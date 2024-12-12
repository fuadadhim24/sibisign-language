using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator1 : MonoBehaviour
{
    // Fungsi untuk pindah ke scene berikutnya
    public void menu()
    {
        // Load scene berikutnya (page5)
        SceneManager.LoadScene("page5");
    }
}