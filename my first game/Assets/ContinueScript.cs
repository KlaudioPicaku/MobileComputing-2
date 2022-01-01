using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueScript : MonoBehaviour
{
    [SerializeField] GameObject dialog;
    [SerializeField] Button continueButton;
    [SerializeField] ExpandedInventoryObject expanded;
    [SerializeField] InventoryObject inventory;
    [SerializeField] SpecialSlots specialInventory;
    private void Start()
    {
        dialog.SetActive(false);
        if (!File.Exists(Application.persistentDataPath + "/save.data"))
        {
            continueButton.enabled = false;
        }
    }
    public void PlayNewGame()
    {
        if (File.Exists(Application.persistentDataPath + "/save.data"))
        {
            dialog.SetActive(true);
        }
        else
        {
                for (int i = 0; i < inventory.Container.Items.Length; i++)
                {
                    if (inventory.Container.Items[i].ID >= 0)
                    {
                        inventory.Container.Items[i].ID = -1;
                        inventory.Container.Items[i].item.Id = -1;
                        inventory.Container.Items[i].item.Name = "";
                        inventory.Container.Items[i].amount = 0;
                    }
                }
                for (int i = 0; i < expanded.Container.Items.Length; i++)
                {
                    if (expanded.Container.Items[i].ID >= 0)
                    {
                        expanded.Container.Items[i].ID = -1;
                        expanded.Container.Items[i].item.Id = -1;
                        expanded.Container.Items[i].item.Name = "";
                        expanded.Container.Items[i].amount = 0;
                    }
                }
                for (int i = 0; i < specialInventory.Container.Items.Length; i++)
                {
                    if (specialInventory.Container.Items[i].ID >= 0)
                    {
                        specialInventory.Container.Items[i].ID = -1;
                        specialInventory.Container.Items[i].item.Id = -1;
                        specialInventory.Container.Items[i].item.Name = "";
                        specialInventory.Container.Items[i].amount = 0;
                    }
                }
                SceneManager.LoadScene("Persistent");
            //SceneManager.UnloadScene("MainMenu");
        }

    }
    public void confirmNewGame()
    {
        if (File.Exists(Application.persistentDataPath + "/save.data"))
        {
            File.Delete(Application.persistentDataPath + "/save.data");
            File.Delete(Application.persistentDataPath + "/inventory.save");
            File.Delete(Application.persistentDataPath + "/expanded.save");
            File.Delete(Application.persistentDataPath + "/specialInventory.save");
            for (int i = 0; i < inventory.Container.Items.Length; i++)
            {
                if (inventory.Container.Items[i].ID >= 0)
                {
                    inventory.Container.Items[i].ID = -1;
                    inventory.Container.Items[i].item.Id = -1;
                    inventory.Container.Items[i].item.Name = "";
                    inventory.Container.Items[i].amount = 0;
                }
            }
            for (int i = 0; i < expanded.Container.Items.Length; i++)
            {
                if (expanded.Container.Items[i].ID >= 0)
                {
                    expanded.Container.Items[i].ID = -1;
                    expanded.Container.Items[i].item.Id = -1;
                    expanded.Container.Items[i].item.Name = "";
                    expanded.Container.Items[i].amount = 0;
                }
            }
            for (int i = 0; i < specialInventory.Container.Items.Length; i++)
            {
                if (specialInventory.Container.Items[i].ID >= 0)
                {
                    specialInventory.Container.Items[i].ID = -1;
                    specialInventory.Container.Items[i].item.Id = -1;
                    specialInventory.Container.Items[i].item.Name = "";
                    specialInventory.Container.Items[i].amount = 0;
                }
            }
            SceneManager.LoadScene("Persistent");
            //SceneManager.UnloadScene("MainMenu");

        }
        else
        {
            Debug.Log("Error findig save file");
        }
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/save.data"))
        {
            // File exists 
            SceneManager.LoadScene("Persistent");
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
