using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance {get; private set;}
    [SerializeField] private PlayerController player;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color unselectedColor;

    [SerializeField] private ScriptableRendererFeature darkVisionOne;
    [SerializeField] private ScriptableRendererFeature darkVisionTwo;
    [SerializeField] private ScriptableRendererFeature darkVisionThree;
    [SerializeField] private UniversalRenderPipelineAsset UniversalRenderPipelineAsset;

    [Header("UI")]
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private SpellAudio spellAudio;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetDarkVision(bool val)
    {
        //UniversalRenderPipelineAsset.
        darkVisionOne.SetActive(val);
        darkVisionTwo.SetActive(val);
        darkVisionThree.SetActive(val);
    }

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        SetDarkVision(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (darkVisionOne.isActive)
                SetDarkVision(false);
            else
                SetDarkVision(true);
        }
    }

    public PlayerController GetPlayer()
    {
        return player;
    }

    public Color SelectedColor()
    {
        return selectedColor;
    }
    public Color UnselectedColor()
    {
        return unselectedColor;
    }

    public void SetMainUI(bool val)
    {
        mainUI.SetActive(val);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        mainUI.SetActive(false);
        gameOverUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.enabled = false;
        player.gameObject.GetComponent<PlayerSpellController>().enabled = false;
    }

    public void GameOverWin()
    {
        gameOverUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "You Win!";
        GameOver();
    }

    public SpellAudio GetSpellAudio() { return spellAudio; }
}
