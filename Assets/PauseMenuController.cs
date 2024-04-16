using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    bool pauseOpen = false;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject controls;

    public static bool IsPaused = false;

    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerSpellController spellController;
    float oldTimescale;
    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        controls.SetActive(false);
        IsPaused = false;
        spellController = player.GetComponent<PlayerSpellController>();
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
                PauseMenuController.IsPaused = true;
                menu.SetActive(true);
                oldTimescale = Time.timeScale;
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                player.enabled = false;
                spellController.enabled = false;
            }
            

        }
    }

    public void Resume()
    {
        PauseMenuController.IsPaused = false;
        pauseOpen = false;
        menu.SetActive(false);
        Time.timeScale = oldTimescale;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.enabled = true;
        spellController.enabled = true;
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
        Time.timeScale = 1f;
    }
}
