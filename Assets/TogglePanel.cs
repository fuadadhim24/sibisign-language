using UnityEngine;

public class TogglePageController : MonoBehaviour
{
    public GameObject senyapkanPanel; // Panel untuk Senyapkan
    public GameObject bunyikanPanel; // Panel untuk Bunyikan

    private bool isSenyapkanActive = true; // Menyimpan status halaman aktif

    // Fungsi untuk toggle antara Senyapkan dan Bunyikan
    public void ToggleScene()
    {
        // Cek apakah Senyapkan sedang aktif
        if (isSenyapkanActive)
        {
            // Sembunyikan panel Senyapkan, tampilkan panel Bunyikan
            senyapkanPanel.SetActive(false);
            bunyikanPanel.SetActive(true);
        }
        else
        {
            // Sembunyikan panel Bunyikan, tampilkan panel Senyapkan
            senyapkanPanel.SetActive(true);
            bunyikanPanel.SetActive(false);
        }

        // Perbarui status halaman aktif
        isSenyapkanActive = !isSenyapkanActive;
    }
}
