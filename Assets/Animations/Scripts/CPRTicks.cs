using UnityEngine;

public class CPRMetronome : MonoBehaviour
{
    public AudioSource tickSource;
    public float bpm = 100f;

    private float interval;
    private float timer;

    void Start()
    {
        interval = 60f / bpm;  // e.g., 0.6 seconds for 100 BPM
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            tickSource.Play();
            timer = 0f;
        }
    }
}
