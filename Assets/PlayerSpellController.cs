using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PlayerSpellController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private VoiceController voiceController;

    [SerializeField] private GameObject leftHandCanvas;
    [SerializeField] private GameObject rightHandCanvas;

    private SpellMono leftMainSpell = null;
    private SpellMono leftSideSpell = null;
    private SpellMono rightMainSpell = null;
    private SpellMono rightSideSpell = null;

    bool spellInputEnabled = false;
    void Start()
    {
        if (playerController == null) playerController = GetComponent<PlayerController>();
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (voiceController == null) voiceController = GetComponent<VoiceController>();

        rightHandCanvas.SetActive(false);
        leftHandCanvas.SetActive(false);
    }

    public void AddSpellToHand(SpellMono spell, bool rightHand)
    {
        if (rightHand)
        {
            if (rightMainSpell == null) rightMainSpell = (SpellMono)gameObject.AddComponent(spell.GetType());
            else if (rightMainSpell != null && rightSideSpell == null) rightSideSpell = (SpellMono)gameObject.AddComponent(spell.GetType());
            else if (rightMainSpell != null && rightSideSpell != null) rightMainSpell = (SpellMono)gameObject.AddComponent(spell.GetType());
        }
        else
        {
            if (leftMainSpell == null) leftMainSpell = (SpellMono)gameObject.AddComponent(spell.GetType());
            else if (leftMainSpell != null && leftSideSpell == null) leftSideSpell = (SpellMono)gameObject.AddComponent(spell.GetType());
            else if (leftMainSpell != null && leftSideSpell != null) leftMainSpell = (SpellMono)gameObject.AddComponent(spell.GetType());
        }
    }

    // Update is called once per frame
    private void HandleSpellInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerController.enabled = false;
            Time.timeScale = 0.2f;
            spellInputEnabled = true;
            rightHandCanvas.SetActive(true);
            leftHandCanvas.SetActive(true);
            voiceController.StartKeywordRecognizer();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerController.enabled = true;
            Time.timeScale = 1.0f;
            spellInputEnabled = false;
            rightHandCanvas.SetActive(false);
            leftHandCanvas.SetActive(false);
            voiceController.StopKeywordRecognizer();
            leftHandCanvas.transform.GetChild(0).GetComponent<SpellInputController>().GetCurrentOrder().Clear();
            rightHandCanvas.transform.GetChild(0).GetComponent<SpellInputController>().GetCurrentOrder().Clear();
        }
    }

    private void HandleCastingSpells()
    {
        if (spellInputEnabled) return;

        if (leftMainSpell == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            // Left Fire
            leftMainSpell.OnPress();
        }

        if (Input.GetMouseButton(0))
        {
            leftMainSpell.OnHold();
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Left Release
            leftMainSpell.OnRelease();
            Destroy(GetComponent(leftMainSpell.GetType()));
            leftMainSpell = leftSideSpell;
            leftSideSpell = null;
        }

        if (rightMainSpell == null) return;

        if (Input.GetMouseButtonDown(1))
        {
            // Right Fire
        }

        if (Input.GetMouseButtonUp(1))
        {
            // Right Release
            Destroy(GetComponent(rightMainSpell.GetType()));
            rightMainSpell = rightSideSpell;
            rightSideSpell = null;
        }

    }

    void Update()
    {
        HandleSpellInput();

        if (spellInputEnabled && Input.GetKeyDown(KeyCode.Space))
        {
            //Swap Spells
            if (Input.GetKey(KeyCode.Q))
            {
                //Swap Left Hand Spells
                SpellMono temp = leftMainSpell;
                leftMainSpell = leftSideSpell;
                leftSideSpell = temp;
            }
            if (Input.GetKey(KeyCode.E))
            {
                //Swap Right Hand Spell
                SpellMono temp = rightMainSpell;
                rightMainSpell = rightSideSpell;
                rightSideSpell = temp;
            }
        }

        HandleCastingSpells();
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (leftMainSpell != null) Debug.Log("Left hand main is " + leftMainSpell.GetType().Name + " with a spell name of " + leftMainSpell.spellName);
            if (leftSideSpell != null) Debug.Log("Left hand side is " + leftSideSpell.GetType().Name + " with a spell name of " + leftSideSpell.spellName);
            if (rightMainSpell != null) Debug.Log("Right hand main is " + rightMainSpell.GetType().Name + " with a spell name of " + rightMainSpell.spellName);
            if (rightSideSpell != null) Debug.Log("Right hand side is " + rightSideSpell.GetType().Name + " with a spell name of " + rightSideSpell.spellName);
        }
    }
}
