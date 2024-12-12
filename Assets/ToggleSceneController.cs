using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleSceneController : MonoBehaviour
{
    private string currentScene = "sibisign_page3"; // Awali dengan scene aktif

    // Fungsi untuk toggle scene
    public void ToggleScene()
    {
        // Tentukan scene berikutnya
        string nextScene = currentScene == "sibisign_page3" ? "sibisign_page4" : "sibisign_page3";

        // Cek apakah scene aktif benar-benar sedang dimuat
        if (SceneManager.GetSceneByName(currentScene).isLoaded)
        {
            // Unload scene saat ini
            SceneManager.UnloadSceneAsync(currentScene).completed += (op) =>
            {
                // Setelah scene saat ini di-unload, load scene berikutnya
                SceneManager.LoadScene(nextScene, LoadSceneMode.Additive);
                currentScene = nextScene; // Perbarui state
            };
        }
        else
        {
            Debug.LogWarning($"Scene {currentScene} tidak ditemukan atau belum dimuat!");
        }
    }
}
