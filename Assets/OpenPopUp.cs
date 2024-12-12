using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupController : MonoBehaviour
{
    private bool isPopupActive = false; // Status apakah popup sedang aktif

    private void Start()
    {
        // Mendaftarkan event saat scene di-unload
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDestroy()
    {
        // Membersihkan event saat objek ini dihancurkan
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    // Fungsi untuk membuka popup
    public void OpenPopup()
    {
        if (!isPopupActive)
        {
            SceneManager.LoadScene("sibisign_page3", LoadSceneMode.Additive);
            isPopupActive = true; // Tandai bahwa popup sedang aktif
        }
    }

    // Fungsi untuk menutup popup
    public void ClosePopup()
    {
        if (isPopupActive)
        {
            SceneManager.UnloadSceneAsync("sibisign_page3");
        }
    }

    // Callback saat scene di-unload
    private void OnSceneUnloaded(Scene scene)
    {
        if (scene.name == "sibisign_page3")
        {
            isPopupActive = false; // Reset status saat scene popup dihapus
        }
    }
}
