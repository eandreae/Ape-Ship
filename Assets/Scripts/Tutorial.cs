using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public AudioSource buttonPress;

    //Records which page we're currently on
    int currentPage = 0;

    //So we know when to enable/disable the arrows
    [Tooltip("Set this to the total amount of pages - 1")]
    public int maxPages;

    public GameObject leftArrow;
    public GameObject rightArrow;

    //Collect all of the pages in an array so we can flip through them later
    public GameObject[] pages;
    public void ReturnToMenu()
    {
        buttonPress.Play();
        //Disables the tutorial panel.
        gameObject.SetActive(false);
    }

    //Goes to the next page
    public void MoveOn()
    {
        pages[currentPage].SetActive(false);
        pages[currentPage + 1].SetActive(true);
        currentPage++;
        if (currentPage > 0)
        {
            leftArrow.SetActive(true);
        }
        if (currentPage == maxPages)
        {
            rightArrow.SetActive(false);
        }
    }

    //Goes back a page
    public void GoBack()
    {
        pages[currentPage].SetActive(false);
        pages[currentPage - 1].SetActive(true);
        currentPage--;
        if (currentPage == 0)
        {
            leftArrow.SetActive(false);
        }
        if (currentPage < maxPages)
        {
            rightArrow.SetActive(true);
        }
    }
}
