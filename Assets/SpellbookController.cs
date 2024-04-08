using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SpellbookController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private List<Sprite> bookImages = new List<Sprite>();
    [SerializeField] private Image doubleImg;
    [SerializeField] private Image img1;
    [SerializeField] private Image img2;
    private int maxPages;
    private int currentPage = 0;

    private bool spellbookOpen = false;

    [SerializeField] private bool doublePages = false;
    void Start()
    {
        if (doublePages)
            maxPages = bookImages.Count;
        else
            maxPages = bookImages.Count / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (spellbookOpen)
            SpellbookOpen();
        else
            SpellbookClosed();
    }

    private void SpellbookOpen()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            spellbookOpen = false;
            HideBook();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentPage = ClampPage(currentPage - 1);
            ChangePage();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentPage = ClampPage(currentPage + 1);
            ChangePage();
        }
    }

    private void SpellbookClosed()
    {
        if (Input.GetKeyDown (KeyCode.Tab))
        {
            spellbookOpen = true;
            ShowBook();
        }
    }

    private int ClampPage(int newVal)
    {
        if (newVal > maxPages) return maxPages;
        if (newVal < 0) return 0;
        return newVal;
    }

    private void ChangePage()
    {
        if (doublePages)
        {
            doubleImg.sprite = bookImages[currentPage];
        }
        else
        {
            img1.sprite = bookImages[currentPage*2];
            img2.sprite = bookImages[currentPage*2+1];
        }
    }

    private void ShowBook()
    {
        if (doublePages)
            doubleImg.enabled = true;
        else
        {
            img1.enabled = true;
            img2.enabled = true;
        }
    }

    private void HideBook()
    {
        if (doublePages)
            doubleImg.enabled = false;
        else
        {
            img1.enabled = false;
            img2.enabled = false;
        }
    }
}
