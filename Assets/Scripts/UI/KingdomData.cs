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
    // ---- UI PANELS ----
    [SerializeField] private GameObject KingdomSelectionCanvas;
    [SerializeField] private GameObject KingdomOverviewCanvas;
    [SerializeField] private TMP_Text KingdomTitle;
    [SerializeField] private TMP_Text KingdomInfo;

    [SerializeField] private GameObject CharacterSelectorCanvas;
    [SerializeField] private GameObject CharacterOverviewCanvas;
    [SerializeField] private TMP_Text CharacterSubrace;
    [SerializeField] private TMP_Text CharacterSubraceInfo;

    private KingdomRace selectedKingdom = KingdomRace.Dog;
    private CharacterSize selectedSize = CharacterSize.Medium;

    // ---- UI THUMBNAILS (the 3 images you show on Character Creation) ----
    [SerializeField] private Image[] sizePortraitSlots; // left=Small, middle=Medium, right=Large

    // Per-kingdom portrait sprites (thumbnails)
    [SerializeField] private Sprite birdSmallSprite;
    [SerializeField] private Sprite birdMediumSprite;
    [SerializeField] private Sprite birdLargeSprite;

    [SerializeField] private Sprite catSmallSprite;
    [SerializeField] private Sprite catMediumSprite;
    [SerializeField] private Sprite catLargeSprite;

    [SerializeField] private Sprite dogSmallSprite;
    [SerializeField] private Sprite dogMediumSprite;
    [SerializeField] private Sprite dogLargeSprite;

    [SerializeField] private Sprite reptileSmallSprite;
    [SerializeField] private Sprite reptileMediumSprite;
    [SerializeField] private Sprite reptileLargeSprite;

    [SerializeField] private Sprite monkeySmallSprite;
    [SerializeField] private Sprite monkeyMediumSprite;
    [SerializeField] private Sprite monkeyLargeSprite;

    // Prefabs for Character Models
    [Header("Prefabs by Kingdom and Size")]
    [SerializeField] private GameObject birdSmall;
    [SerializeField] private GameObject birdMedium;
    [SerializeField] private GameObject birdLarge;

    [SerializeField] private GameObject catSmall;
    [SerializeField] private GameObject catMedium;
    [SerializeField] private GameObject catLarge;

    [SerializeField] private GameObject dogSmall;
    [SerializeField] private GameObject dogMedium;
    [SerializeField] private GameObject dogLarge;

    [SerializeField] private GameObject reptileSmall;
    [SerializeField] private GameObject reptileMedium;
    [SerializeField] private GameObject reptileLarge;

    [SerializeField] private GameObject monkeySmall;
    [SerializeField] private GameObject monkeyMedium;
    [SerializeField] private GameObject monkeyLarge;

    // Character Preview
    [SerializeField] private Transform PreviewModelSpot;

    //[SerializeField] private GameObject ClassSelector;
    //[SerializeField] private GameObject ClassSelectorCanvas;
    //[SerializeField] private TMP_Text ClassName;
    //[SerializeField] private TMP_Text ClassInfo;

    [SerializeField] private GameObject confirmationPrompt;

    private void Start()
    {
        // Show only the Kingdom Selection panel
        KingdomSelectionCanvas.SetActive(true);

        // Make sure all others start hidden
        KingdomOverviewCanvas.SetActive(false);
        CharacterSelectorCanvas.SetActive(false);
        CharacterOverviewCanvas.SetActive(false);
        confirmationPrompt.SetActive(false);
    }

    public void OnKingdomButtonClick(int kingdomIndex)
    {
        selectedKingdom = (KingdomRace)kingdomIndex;   // store it
        SelectKingdom(selectedKingdom);                // still update the overview text
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
            ClearPreview();

            CharacterSelectorCanvas.SetActive(false);
            CharacterOverviewCanvas.SetActive(false);
            KingdomOverviewCanvas.SetActive(false);
            KingdomSelectionCanvas.SetActive(true);

            currentStep = CreationStep.KingdomSelection;
        }
    }

    private void SelectSize(CharacterSize size)
    {
        selectedSize = size;
        SpawnSelectedModel();

        // Call the correct info function
        switch (selectedKingdom)
        {
            case KingdomRace.Dog:
                GetDogSubraceInfo(size);
                break;
            case KingdomRace.Bird:
                GetBirdSubraceInfo(size);
                break;
            case KingdomRace.Cat:
                GetCatSubraceInfo(size);
                break;
            case KingdomRace.Reptile:
                GetReptileSubraceInfo(size);
                break;
            case KingdomRace.Monkey:
                GetMonkeySubraceInfo(size);
                break;
        }
    }
    private void GetBirdSubraceInfo(CharacterSize size)
    {
        CharacterSubrace.text = $"Bird - {size}";
        switch (size)
        {
            case CharacterSize.Small:
                CharacterSubraceInfo.text = "Swift and agile.";
                break;
            case CharacterSize.Medium:
                CharacterSubraceInfo.text = "Balanced glider.";
                break;
            case CharacterSize.Large:
                CharacterSubraceInfo.text = "Powerful guardian.";
                break;
        }
        CharacterOverviewCanvas.SetActive(true);
    }

    private void GetCatSubraceInfo(CharacterSize size)
    {
        CharacterSubrace.text = $"Cat - {size}";
        switch (size)
        {
            case CharacterSize.Small:
                CharacterSubraceInfo.text = "Sneaky scout.";
                break;
            case CharacterSize.Medium:
                CharacterSubraceInfo.text = "Quick striker.";
                break;
            case CharacterSize.Large:
                CharacterSubraceInfo.text = "Tough brawler.";
                break;
        }
        CharacterOverviewCanvas.SetActive(true);
    }

    private void GetDogSubraceInfo(CharacterSize size)
    {
        CharacterSubrace.text = $"Dog - {size}";

        switch (size)
        {
            case CharacterSize.Small:
                CharacterSubraceInfo.text = "Charismatic Yapper.";
                break;
            case CharacterSize.Medium:
                CharacterSubraceInfo.text = "Loyal Chewer.";
                break;
            case CharacterSize.Large:
                CharacterSubraceInfo.text = "Bulky Growler.";
                break;
        }
        CharacterOverviewCanvas.SetActive(true);
    }

    private void GetReptileSubraceInfo(CharacterSize size)
    {
        CharacterSubrace.text = $"Reptile - {size}";
        switch (size)
        {
            case CharacterSize.Small:
                CharacterSubraceInfo.text = "Slimy Scale.";
                break;
            case CharacterSize.Medium:
                CharacterSubraceInfo.text = "Eccentric Nibbler.";
                break;
            case CharacterSize.Large:
                CharacterSubraceInfo.text = "Hardened Slasher.";
                break;
        }
        CharacterOverviewCanvas.SetActive(true);
    }

    private void GetMonkeySubraceInfo(CharacterSize size)
    {
        CharacterSubrace.text = $"Monkey - {size}";
        switch (size)
        {
            case CharacterSize.Small:
                CharacterSubraceInfo.text = "Tricky Peanut.";
                break;
            case CharacterSize.Medium:
                CharacterSubraceInfo.text = "Gifted Climber.";
                break;
            case CharacterSize.Large:
                CharacterSubraceInfo.text = "Stoic Boulder.";
                break;
        }
        CharacterOverviewCanvas.SetActive(true);
    }


    public void OnConfirm()
    {
      if (currentStep == CreationStep.KingdomSelection)
        {
            currentStep = CreationStep.CharacterCreation;
            CharacterSelectorCanvas.SetActive(true);
            KingdomSelectionCanvas.SetActive(false);

            PopulateSizeThumbnails(selectedKingdom); // swap images to Bird/Cat/Dog...
            ClearPreview(); // clear any old model
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

    // Hook these to your three UI buttons: Small / Medium / Large
    public void OnSelectSmall() { SelectSize(CharacterSize.Small); }
    public void OnSelectMedium() { SelectSize(CharacterSize.Medium); }
    public void OnSelectLarge() { SelectSize(CharacterSize.Large); }

   
    // Spawns the prefab that matches (selectedKingdom, selectedSize) at previewAnchor
    private void SpawnSelectedModel()
    {
        ClearPreview();
        var prefab = GetPrefab(selectedKingdom, selectedSize);
        if (prefab != null && PreviewModelSpot != null)
        {
            var go = Instantiate(prefab, PreviewModelSpot);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
        }

        // (Optional) update right-side text panel
        if (CharacterSubrace != null)
            CharacterSubrace.text = $"{selectedKingdom} - {selectedSize}";
    }

    // Remove any previously displayed model
    private void ClearPreview()
    {
        if (PreviewModelSpot == null) return;
        for (int i = PreviewModelSpot.childCount - 1; i >= 0; i--)
            Destroy(PreviewModelSpot.GetChild(i).gameObject);
    }

    // Map (kingdom,size) -> prefab
    private GameObject GetPrefab(KingdomRace k, CharacterSize s)
    {
        switch (k)
        {
            case KingdomRace.Bird:
                return s == CharacterSize.Small ? birdSmall :
                       s == CharacterSize.Medium ? birdMedium : birdLarge;
            case KingdomRace.Cat:
                return s == CharacterSize.Small ? catSmall :
                       s == CharacterSize.Medium ? catMedium : catLarge;
            case KingdomRace.Dog:
                return s == CharacterSize.Small ? dogSmall :
                       s == CharacterSize.Medium ? dogMedium : dogLarge;
            case KingdomRace.Reptile:
                return s == CharacterSize.Small ? reptileSmall :
                       s == CharacterSize.Medium ? reptileMedium : reptileLarge;
            case KingdomRace.Monkey:
                return s == CharacterSize.Small ? monkeySmall :
                       s == CharacterSize.Medium ? monkeyMedium : monkeyLarge;
        }
        return null;
    }

    private void PopulateSizeThumbnails(KingdomRace k)
    {
        if (sizePortraitSlots == null || sizePortraitSlots.Length < 3) return;
        Sprite small, medium, large;

        switch (k)
        {
            case KingdomRace.Bird:
                small = birdSmallSprite; medium = birdMediumSprite; large = birdLargeSprite; break;
            case KingdomRace.Cat:
                small = catSmallSprite; medium = catMediumSprite; large = catLargeSprite; break;
            case KingdomRace.Dog:
                small = dogSmallSprite; medium = dogMediumSprite; large = dogLargeSprite; break;
            case KingdomRace.Reptile:
                small = reptileSmallSprite; medium = reptileMediumSprite; large = reptileLargeSprite; break;
            case KingdomRace.Monkey:
                small = monkeySmallSprite; medium = monkeyMediumSprite; large = monkeyLargeSprite; break;
            default: return;
        }

        sizePortraitSlots[0].sprite = small; sizePortraitSlots[0].enabled = small != null;
        sizePortraitSlots[1].sprite = medium; sizePortraitSlots[1].enabled = medium != null;
        sizePortraitSlots[2].sprite = large; sizePortraitSlots[2].enabled = large != null;
    }

}