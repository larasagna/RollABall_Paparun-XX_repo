using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI finalTimeText;
        public TextMeshProUGUI bestTimeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        panel.SetActive(false);
    }

 
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(float time)
    {
        float best = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        bool newRecord = time < best;
        if (newRecord)
        {
            best = time;
            PlayerPrefs.SetFloat("BestTime", best);
            PlayerPrefs.Save();
        }
        finalTimeText.text = "Tu tiempo " + FormatTime(time) + (newRecord ? " ¡Nuevo record!" : "");
        bestTimeText.text = "Mejor tiempo: " + FormatTime(best);

        panel.SetActive(true);
        Time.timeScale = 0f; //se detiene el tiempo en la pantalla final
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    string FormatTime(float t)
    {
        int minutes = Mathf.FloorToInt(t / 60f);
        int seconds = Mathf.FloorToInt(t % 60f);
        int centis = Mathf.FloorToInt((t * 100f) % 100f);
        return $"{minutes:00}:{seconds:00}.{centis:00}";
    }
}
