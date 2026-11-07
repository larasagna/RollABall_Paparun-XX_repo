using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float elapsedTime;
    private bool running = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!running) return;
        elapsedTime += Time.deltaTime;
        UpdateText();
    }
    void UpdateText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int centis = Mathf.FloorToInt((elapsedTime * 100f) % 100f);
        timerText.text = $"{minutes:00}:{seconds:00}.{centis:00}";
    }
    public void StopTimer()
    {
        running = false;
    }
    public float GetTime()
    {
        return elapsedTime;
    }
}
