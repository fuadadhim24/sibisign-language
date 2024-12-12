using UnityEngine;
using UnityEngine.SceneManagement;

public class ClosePopupButton : MonoBehaviour
{
    public void ClosePopup()
    {
        SceneManager.UnloadSceneAsync("sibisign_page3");
    }
}
