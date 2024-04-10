using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    bool pauseOpen = false;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject controls;
    float oldTimescale;
    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        controls.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseOpen) {
                Resume();
            }
            else
            {
                pauseOpen = true;
                menu.SetActive(true);
                oldTimescale = Time.timeScale;
                Time.timeScale = 0f;
            }
            

        }
    }

    public void Resume()
    {
        pauseOpen = false;
        menu.SetActive(false);
        Time.timeScale = oldTimescale;
    }

    public void ShowControls()
    {
        controls.SetActive(true);
    }

    public void HideControls()
    {
        controls.SetActive(false);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
