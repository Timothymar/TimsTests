using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum KingdomRace
{
    Bird,
    Cat,
    Dog,
    Reptile,
    Monkey
}
public class KingdomData : MonoBehaviour
{
    [SerializeField] private GameObject KingdomOverviewCanvas;
    [SerializeField] private TMP_Text KingdomTitle;
    [SerializeField] private TMP_Text KingdomInfo;

    public void OnKingdomButtonClick(int kingdomIndex)
    {
        KingdomRace selected = (KingdomRace)kingdomIndex;
        SelectKingdom(selected);
    }

    private void SelectKingdom(KingdomRace race)
    {
        switch (race)
        {
            case KingdomRace.Bird:
                GetBirdInfo();
                break;
            case KingdomRace.Cat:
                GetCatInfo();
                break;
            case KingdomRace.Dog:
                GetDogInfo();
                break;
            case KingdomRace.Reptile:
                GetReptileInfo();
                break;
            case KingdomRace.Monkey:
                GetMonkeyInfo();
                break;
        }
    }

    private void GetBirdInfo()
    {
        KingdomTitle.text = "Bird Kingdom";
        KingdomInfo.text = "Welcome to the Bird Kingdom! Here, you can find various species of birds.";
        KingdomOverviewCanvas.SetActive(true);
    }
    private void GetCatInfo()
    {
        KingdomTitle.text = "Cat Kingdom";
        KingdomInfo.text = "Welcome to the Cat Kingdom! Here, you can find various species of cats.";
        KingdomOverviewCanvas.SetActive(true);
    }
    private void GetDogInfo()
    {
        KingdomTitle.text = "Dog Kingdom";
        KingdomInfo.text = "Welcome to the Dog Kingdom! Here, you can find various species of dogs.";
        KingdomOverviewCanvas.SetActive(true);
    }
    private void GetReptileInfo()
    {
        KingdomTitle.text = "Reptile Kingdom";
        KingdomInfo.text = "Welcome to the Reptile Kingdom! Here, you can find various species of reptiles.";
        KingdomOverviewCanvas.SetActive(true);
    }
    private void GetMonkeyInfo()
    {
        KingdomTitle.text = "Monkey Kingdom";
        KingdomInfo.text = "Welcome to the Monkey Kingdom! Here, you can find various species of monkeys.";
        KingdomOverviewCanvas.SetActive(true);
    }
}