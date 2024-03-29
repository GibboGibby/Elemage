using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance {get; private set;}
    [SerializeField] private PlayerController player;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color unselectedColor;
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

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
