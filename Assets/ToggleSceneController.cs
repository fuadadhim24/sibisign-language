using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleSceneController : MonoBehaviour
{
    private string currentScene = "sibisign_page3"; // Scene aktif saat ini

    // Fungsi untuk mengganti scene
    public void ToggleScene()
    {
        // Tentukan scene berikutnya
        string nextScene = currentScene == "sibisign_page3" ? "sibisign_page4" : "sibisign_page3";

        // Pastikan scene berikutnya dimuat terlebih dahulu
        if (!SceneManager.GetSceneByName(nextScene).isLoaded)
        {
            SceneManager.LoadScene(nextScene, LoadSceneMode.Additive);
            Debug.Log($"Scene {nextScene} sedang dimuat...");

            // Tunggu hingga scene berikutnya dimuat
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            // Jika sudah dimuat, lanjutkan untuk unload scene lama
            UnloadCurrentScene();
        }
    }

    // Event handler untuk memastikan scene baru dimuat
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == currentScene)
        {
            // Hapus event listener untuk menghindari pemanggilan ganda
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        // Unload scene aktif setelah scene berikutnya selesai dimuat
        UnloadCurrentScene();
    }

    private void UnloadCurrentScene()
    {
        // Unload scene aktif jika dimuat
        if (SceneManager.GetSceneByName(currentScene).isLoaded)
        {
            SceneManager.UnloadSceneAsync(currentScene);
            Debug.Log($"Scene {currentScene} berhasil di-unload.");
        }

        // Perbarui scene aktif setelah proses selesai
        string nextScene = currentScene == "sibisign_page3" ? "sibisign_page4" : "sibisign_page3";
        currentScene = nextScene;
        Debug.Log($"Scene aktif sekarang: {currentScene}");
    }
}
