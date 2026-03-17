using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//public class MainMenu : MonoBehaviour
//{
//    public void NewGame()
//    {
//        LevelManager.Instance.LoadNewGame();
//    }

//}
public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainButtonsPanel;
    public GameObject saveSlotPanel;

    public void NewGame()
    {
        mainButtonsPanel.SetActive(false);
        saveSlotPanel.SetActive(true);
    }

    public void BackFromSaveSlots()
    {
        saveSlotPanel.SetActive(false);
        mainButtonsPanel.SetActive(true);
    }

    public void SelectSaveSlot(int slotNumber)
    {
        Debug.Log("Selected save slot: " + slotNumber);

        // For now, just start the new game after selecting a slot.
        // Later we’ll save this slot choice and use it properly.
        LevelManager.Instance.LoadNewGame();
    }

    public void LoadGame()
    {
        Debug.Log("Load Game clicked");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}