using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public string sceneToLoad;  // Nama scene tujuan
    public Slider progressBar; // Slider untuk progress bar (opsional)
    public Text progressText;  // Text untuk menampilkan persentase progress (opsional)

    private void Start()
    {
        // Mulai proses loading
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        // Mulai memuat scene secara asinkron
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);

        // Mencegah scene langsung aktif sebelum selesai
        operation.allowSceneActivation = false;

        // Proses loading
        while (!operation.isDone)
        {
            // Hitung progress (operation.progress berada di 0.0 hingga 0.9)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // Update UI jika ada
            if (progressBar != null)
                progressBar.value = progress; // Update progress bar

            if (progressText != null)
                progressText.text = Mathf.RoundToInt(progress * 100) + "%"; // Update teks progress

            // Jika progress sudah selesai (0.9), aktifkan scene
            if (operation.progress >= 0.9f)
            {
                if (progressBar != null)
                    progressBar.value = 1f; // Pastikan progress bar penuh

                if (progressText != null)
                    progressText.text = "100%";

                yield return new WaitForSeconds(1f); // Opsional: Delay sebelum berpindah scene
                operation.allowSceneActivation = true; // Pindah ke scene berikutnya
            }

            yield return null;
        }
    }
}
