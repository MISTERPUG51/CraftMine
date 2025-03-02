using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public CameraController CameraController;
    public BlockEdit BlockEdit;

    public GameObject BlockSelectUI;
    public TMP_InputField itemTextBox;

    public int blockInHand = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Cursor.lockState = CursorLockMode.None;
            CameraController.enabled = false;
            BlockEdit.enabled = false;
            BlockSelectUI.SetActive(true);
        }
    }

    public void InventoryClose()
    {
        CameraController.enabled = true;
        BlockEdit.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        blockInHand = int.Parse(itemTextBox.text);
        BlockSelectUI.SetActive(false);

    }
}
