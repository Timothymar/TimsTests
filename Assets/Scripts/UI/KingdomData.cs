using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum KingdomRace
{
    Bird,
    Cat,
    Dog,
    Reptile,
    Monkey
}

public enum CharacterSize
{
    Small,
    Medium,
    Large
}

public enum CreationStep
{
    KingdomSelection,
    CharacterCreation
    //ClassSelection
}

public class KingdomData : MonoBehaviour
{
    [SerializeField] private GameObject KingdomSelectionCanvas;
    [SerializeField] private GameObject KingdomOverviewCanvas;
    [SerializeField] private TMP_Text KingdomTitle;
    [SerializeField] private TMP_Text KingdomInfo;

    [SerializeField] private GameObject CharacterSelectorCanvas;
    [SerializeField] private GameObject CharacterOverviewCanvas;
    [SerializeField] private TMP_Text CharacterSubrace;
    [SerializeField] private TMP_Text CharacterSubraceInfo;

    //[SerializeField] private GameObject ClassSelector;
    //[SerializeField] private GameObject ClassSelectorCanvas;
    //[SerializeField] private TMP_Text ClassName;
    //[SerializeField] private TMP_Text ClassInfo;

    [SerializeField] private GameObject confirmationPrompt;

    private void Start()
    {
        KingdomSelectionCanvas.SetActive(true);
    }

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

    private CreationStep currentStep = CreationStep.KingdomSelection;

    public void OnBack()
    {
        if (currentStep == CreationStep.KingdomSelection)
        {
            confirmationPrompt.SetActive(true);
        }
        else if (currentStep == CreationStep.CharacterCreation)
        {
            currentStep = CreationStep.KingdomSelection;
            CharacterSelectorCanvas.SetActive(false);
            KingdomSelectionCanvas.SetActive(true);
        }
    }

    public void OnConfirm()
    {
      if (currentStep == CreationStep.KingdomSelection)
        {
            currentStep = CreationStep.CharacterCreation;
            CharacterSelectorCanvas.SetActive(true);
            KingdomSelectionCanvas.SetActive(false);
        }
        //else if (currentStep == CreationStep.CharacterCreation)
        //{
        //    //ClassSelector.SetActive(true);
        //}
        //else if (currentStep == CreationStep.ClassSelection)
        //{
        //    // Handle confirm action for other steps
        //}
    }
    public void ConfirmExit()
    {
        confirmationPrompt.SetActive(false);
        KingdomSelectionCanvas.SetActive(false);
        LevelManager.Instance.LoadMainMenu();
    }

    public void CancelExit()
    {
        confirmationPrompt.SetActive(false);
    }

    //private void ShowKingdomSelection()
    //{
    //    kingdomSelectionCanvas.SetActive(true);
    //    characterCustomizationCanvas.SetActive(false);
    //    currentStep = CreationStep.KingdomSelection;
    //}

    //private void ShowCharacterCustomization()
    //{
    //    kingdomSelectionCanvas.SetActive(false);
    //    characterCustomizationCanvas.SetActive(true);
    //    currentStep = CreationStep.CharacterCustomization;
    //}
}