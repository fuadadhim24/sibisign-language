using UnityEngine;

public class audio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource lagu;

    public void mulai()
    {
        lagu.Play();
    }
    public void stop()
    {
        lagu.Stop();
    }

}
