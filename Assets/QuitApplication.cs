using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    // Fungsi ini akan dipanggil ketika tombol ditekan
    public void QuitGame()
    {
        #if UNITY_EDITOR
            // Jika dijalankan di editor Unity, keluar dari play mode
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Jika dijalankan di build game, keluar dari aplikasi
            Application.Quit();
        #endif
    }
}
