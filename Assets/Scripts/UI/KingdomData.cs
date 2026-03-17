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

public enum CharacterClass
{
    Scrapper,
    Pickpocket,
    Defender,
    Crafter,
    Druid,
    Bard
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
    CharacterCreation,
    ClassSelection,
    StatAllocation,
    CharacterOverview
}

public enum StatGenerationMethod
{
    PointBuy,
    StandardArray,
    FreeSelection
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
    private StatGenerationMethod selectedStatMethod = StatGenerationMethod.PointBuy;

    [SerializeField] private GameObject ClassSelectorCanvas;
    [SerializeField] private GameObject ClassOverviewCanvas;
    [SerializeField] private TMP_Text ClassName;
    [SerializeField] private TMP_Text ClassInfo;

    [SerializeField] private GameObject StatAllocationCanvas;
    [SerializeField] private GameObject statScreen;
    [SerializeField] private TMP_Text[] abilityTexts;
    [SerializeField] private TMP_Text[] modifierTexts;
    [SerializeField] private GameObject[] plusButtons;
    [SerializeField] private GameObject[] minusButtons;
    [SerializeField] private GameObject[] upButtons;
    [SerializeField] private GameObject[] downButtons;

    private int[] statValues = new int[6] { 10, 10, 10, 10, 10, 10 };

    [SerializeField] private GameObject CharacterConfirmationCanvas;

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

    [SerializeField] private GameObject confirmationExitPrompt;

    [SerializeField] private GameObject confirmationSelectionPrompt;
    [SerializeField] private TMP_Text selectionConfirmText;

    // Currently selected class
    private CharacterClass selectedClass = CharacterClass.Scrapper;

    private void Start()
    {
        // Show only the Kingdom Selection panel
        KingdomSelectionCanvas.SetActive(true);

        // Make sure all others start hidden
        KingdomOverviewCanvas.SetActive(false);
        CharacterSelectorCanvas.SetActive(false);
        CharacterOverviewCanvas.SetActive(false);
        ClassSelectorCanvas.SetActive(false);
        ClassOverviewCanvas.SetActive(false);
        confirmationExitPrompt.SetActive(false);
        confirmationSelectionPrompt.SetActive(false);
        
        UpdateStatControlVisibility();
        UpdateAllStatsUI();
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
            confirmationExitPrompt.SetActive(true);
        }
        else if (currentStep == CreationStep.CharacterCreation)
        {
            ClearPreview();

            CharacterSelectorCanvas.SetActive(false);
            CharacterOverviewCanvas.SetActive(false);
            KingdomOverviewCanvas.SetActive(false);
            ClassSelectorCanvas.SetActive(false);
            ClassOverviewCanvas.SetActive(false);
            StatAllocationCanvas.SetActive(false);
            KingdomSelectionCanvas.SetActive(true);

            currentStep = CreationStep.KingdomSelection;
        }
        else if (currentStep == CreationStep.ClassSelection)
        {
            CharacterOverviewCanvas.SetActive(false);
            KingdomOverviewCanvas.SetActive(false);
            ClassSelectorCanvas.SetActive(false);
            ClassOverviewCanvas.SetActive(false);
            KingdomSelectionCanvas.SetActive(false);
            StatAllocationCanvas.SetActive(false);
            CharacterSelectorCanvas.SetActive(true);

            currentStep = CreationStep.CharacterCreation;
        }
        else if (currentStep == CreationStep.StatAllocation)
        {
            CharacterOverviewCanvas.SetActive(false);
            KingdomOverviewCanvas.SetActive(false);
            ClassSelectorCanvas.SetActive(false);
            ClassOverviewCanvas.SetActive(false);
            KingdomSelectionCanvas.SetActive(false);
            CharacterSelectorCanvas.SetActive(true);
            StatAllocationCanvas.SetActive(false);

            currentStep = CreationStep.ClassSelection;
        }
        else if (currentStep == CreationStep.CharacterOverview)
        {
            CharacterOverviewCanvas.SetActive(false);
            KingdomOverviewCanvas.SetActive(false);
            ClassSelectorCanvas.SetActive(false);
            ClassOverviewCanvas.SetActive(false);
            KingdomSelectionCanvas.SetActive(false);
            CharacterSelectorCanvas.SetActive(false);
            StatAllocationCanvas.SetActive(true);
            CharacterConfirmationCanvas.SetActive(false);
            currentStep = CreationStep.StatAllocation;
        }
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
        else if (currentStep == CreationStep.CharacterCreation)
        {
            currentStep = CreationStep.ClassSelection;
            CharacterSelectorCanvas.SetActive(false);
            CharacterOverviewCanvas.SetActive(false);
            KingdomOverviewCanvas.SetActive(false);
            KingdomSelectionCanvas.SetActive(false);

            ClassSelectorCanvas.SetActive(true);
            ClassOverviewCanvas.SetActive(false); // hidden until a class is clicked
        }

        else if (currentStep == CreationStep.ClassSelection)
        {
            currentStep = CreationStep.StatAllocation;

            ClassSelectorCanvas.SetActive(false);
            ClassOverviewCanvas.SetActive(false);

            StatAllocationCanvas.SetActive(true);
        }

        else if (currentStep == CreationStep.StatAllocation)
        {
            currentStep = CreationStep.CharacterOverview;

            StatAllocationCanvas.SetActive(false);
            CharacterConfirmationCanvas.SetActive(true);
        }

        else if (currentStep == CreationStep.CharacterOverview)
        {
            confirmationSelectionPrompt.SetActive(true);
            ShowSelectionConfirmation();
        }
    }

    public void ConfirmExit()
    {
        confirmationExitPrompt.SetActive(false);
        KingdomSelectionCanvas.SetActive(false);
        LevelManager.Instance.LoadMainMenu();
    }

    public void CancelExit()
    {
        confirmationExitPrompt.SetActive(false);
        confirmationSelectionPrompt.SetActive(false);
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

    public void OnClassButtonClick(int classIndex)
    {
        selectedClass = (CharacterClass)classIndex;
        SelectClass(selectedClass);
    }
    private void SelectClass(CharacterClass c)
    {
        // Update the right-side panel text
        switch (c)
        {
            case CharacterClass.Scrapper: SetScrapperInfo(); break;
            case CharacterClass.Pickpocket: SetPickpocketInfo(); break;
            case CharacterClass.Defender: SetDefenderInfo(); break;
            case CharacterClass.Crafter: SetCrafterInfo(); break;
            case CharacterClass.Druid: SetDruidInfo(); break;
            case CharacterClass.Bard: SetBardInfo(); break;
        }

        // Make sure the overview is visible on selection
        ClassOverviewCanvas.SetActive(true);
    }

    private void SetScrapperInfo()
    {
        ClassName.text = "Scrapper";
        ClassInfo.text = "Frontline fighter. High defense and melee damage. Str based damager";
    }
    private void SetPickpocketInfo()
    {
        ClassName.text = "Pickpocket";
        ClassInfo.text = "Stealthy striker. Crits, evasion, and traps. Dex based damager";
    }
    private void SetDefenderInfo()
    {
        ClassName.text = "Defender";
        ClassInfo.text = "Heavy defender. Tanky with any sheild. Con based damager";
    }
    private void SetCrafterInfo()
    {
        ClassName.text = "Crafter";
        ClassInfo.text = "Gadgets and turrets. Controls space and utilities. Int based damager";
    }
    private void SetDruidInfo()
    {
        ClassName.text = "Druid";
        ClassInfo.text = "Nature caster. Heals, dots, and shapeshifts. Wis based damager";
    }
    private void SetBardInfo()
    {
        ClassName.text = "Bard";
        ClassInfo.text = "Buffs & debuffs through music. Team enabler. Cha based damager";
    }

    private void ShowSelectionConfirmation()
    {
        // Only kingdom + class (no size, no blurbs)
        string kingdom = selectedKingdom.ToString();          // e.g., "Dog"
        string cls = selectedClass.ToString();            // e.g., "Scrapper"

        if (selectionConfirmText != null)
            selectionConfirmText.text = $"Are you sure you want to be a {cls} from the {kingdom} kingdom?";

        confirmationSelectionPrompt.SetActive(true);
    }

    // Hook these to the Yes/No buttons on the confirmationSelectionPrompt:
    public void OnSelectionConfirmYes()
    {
        confirmationSelectionPrompt.SetActive(false);
        // TODO: proceed (save choices, load next scene, etc.)
    }

    public void SelectPointBuy()
    {
        selectedStatMethod = StatGenerationMethod.PointBuy;

        statValues[0] = 10;
        statValues[1] = 10;
        statValues[2] = 10;
        statValues[3] = 10;
        statValues[4] = 10;
        statValues[5] = 10;

        UpdateAllStatsUI();
        UpdateStatControlVisibility();
        statScreen.SetActive(true);
        Debug.Log("Selected Stat Method: " + selectedStatMethod);
    }

    public void SelectStandardArray()
    {
        selectedStatMethod = StatGenerationMethod.StandardArray;
        UpdateStatControlVisibility();
        SetStandardArrayDefaults();
        statScreen.SetActive(true);
        Debug.Log("Selected Stat Method: " + selectedStatMethod);
    }

    public void SelectFreeSelection()
    {
        selectedStatMethod = StatGenerationMethod.FreeSelection;

        statValues[0] = 10;
        statValues[1] = 10;
        statValues[2] = 10;
        statValues[3] = 10;
        statValues[4] = 10;
        statValues[5] = 10;

        UpdateAllStatsUI();
        UpdateStatControlVisibility();
        statScreen.SetActive(true);
        Debug.Log("Selected Stat Method: " + selectedStatMethod);
    }

    private int GetModifier(int score)
    {
        return Mathf.FloorToInt((score - 10) / 2f);
    }

    private string FormatModifier(int modifier)
    {
        return modifier >= 0 ? $"+{modifier}" : modifier.ToString();
    }

    private void UpdateStatUI(int statIndex)
    {
        if (abilityTexts != null && statIndex >= 0 && statIndex < abilityTexts.Length)
            abilityTexts[statIndex].text = statValues[statIndex].ToString();

        if (modifierTexts != null && statIndex >= 0 && statIndex < modifierTexts.Length)
            modifierTexts[statIndex].text = FormatModifier(GetModifier(statValues[statIndex]));
    }

    private void UpdateAllStatsUI()
    {
        for (int i = 0; i < statValues.Length; i++)
        {
            UpdateStatUI(i);
        }
    }

    public void OnStatPlus(int statIndex)
    {
        if (selectedStatMethod == StatGenerationMethod.StandardArray) return;

        statValues[statIndex]++;
        UpdateStatUI(statIndex);
    }

    public void OnStatMinus(int statIndex)
    {
        if (selectedStatMethod == StatGenerationMethod.StandardArray) return;

        statValues[statIndex]--;
        UpdateStatUI(statIndex);
    }
    public void OnStatUp(int statIndex)
    {
        if (selectedStatMethod != StatGenerationMethod.StandardArray) return;
        if (statIndex <= 0) return;

        SwapStats(statIndex, statIndex - 1);
    }

    public void OnStatDown(int statIndex)
    {
        if (selectedStatMethod != StatGenerationMethod.StandardArray) return;
        if (statIndex >= statValues.Length - 1) return;

        SwapStats(statIndex, statIndex + 1);
    }

    private void SwapStats(int firstIndex, int secondIndex)
    {
        int temp = statValues[firstIndex];
        statValues[firstIndex] = statValues[secondIndex];
        statValues[secondIndex] = temp;

        UpdateStatUI(firstIndex);
        UpdateStatUI(secondIndex);
    }
    private void SetStandardArrayDefaults()
    {
        statValues[0] = 18;
        statValues[1] = 16;
        statValues[2] = 14;
        statValues[3] = 12;
        statValues[4] = 10;
        statValues[5] = 10;

        UpdateAllStatsUI();
    }
    private void UpdateStatControlVisibility()
    {
        bool isStandardArray = selectedStatMethod == StatGenerationMethod.StandardArray;

        for (int i = 0; i < 6; i++)
        {
            if (plusButtons != null && i < plusButtons.Length && plusButtons[i] != null)
                plusButtons[i].SetActive(!isStandardArray);

            if (minusButtons != null && i < minusButtons.Length && minusButtons[i] != null)
                minusButtons[i].SetActive(!isStandardArray);

            if (upButtons != null && i < upButtons.Length && upButtons[i] != null)
                upButtons[i].SetActive(isStandardArray);

            if (downButtons != null && i < downButtons.Length && downButtons[i] != null)
                downButtons[i].SetActive(isStandardArray);
        }

        if (isStandardArray)
        {
            // top row can't go up
            if (upButtons != null && upButtons.Length > 0 && upButtons[0] != null)
                upButtons[0].SetActive(false);

            // bottom row can't go down
            if (downButtons != null && downButtons.Length > 5 && downButtons[5] != null)
                downButtons[5].SetActive(false);
        }
    }
}