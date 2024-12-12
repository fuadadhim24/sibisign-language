using UnityEngine;
using UnityEngine.SceneManagement;

public class ClosePopupButton1 : MonoBehaviour
{
    public void ClosePopup()
    {
        SceneManager.UnloadSceneAsync("sibisign_page4");
    }
}
