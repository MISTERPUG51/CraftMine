using System;
using UnityEngine;

public class BlockEdit : MonoBehaviour
{
    public Inventory Inventory;
    public blockmanager blockmanager;
    public Camera playerCamera;


    public int blockNumberToEdit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.name.Substring(0,5));
                if (hit.collider.name.Substring(0,5) == "block")
                {
                    blockNumberToEdit = int.Parse(hit.collider.name.Substring(5));
                    Destroy(hit.collider.gameObject);

                } else
                {
                    Debug.Log("hit.collider.name = " + hit.collider.name);
                    GameObject hitObjectParent = hit.collider.gameObject.transform.parent.gameObject;
                    Debug.Log("hitObjectParent.name = " + hitObjectParent.name);
                    blockNumberToEdit = int.Parse(hitObjectParent.name.Substring(5));
                    Destroy(hitObjectParent);
                }
                Debug.Log("Block to edit: " + hit.collider.name);
                Debug.Log("Block # to edit: " + hit.collider.name.Substring(5));
                
                blockmanager.blockList[blockNumberToEdit] = 0;
                
                
            }
        }

        if (Input.GetMouseButtonDown(1))
        {

            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name.Substring(0,5) == "block")
                {
                    GameObject newBlock = Instantiate(blockmanager.blockTemplatesList[Inventory.blockInHand]);
                    var newBlockPostion = hit.transform.position + hit.normal;
                    var newBlockRotation = hit.transform.rotation;
                    int newBlockNumber = Convert.ToInt32(newBlockPostion.x) + (Convert.ToInt32(newBlockPostion.z) * 16) + (Convert.ToInt32(newBlockPostion.y) * 256);

                    

                    blockmanager.blockList[newBlockNumber] = Inventory.blockInHand;
                    newBlock.transform.position = newBlockPostion;
                    newBlock.transform.rotation = newBlockRotation;
                    newBlock.name = "block" + newBlockNumber;
                } else
                {
                    GameObject newBlock = Instantiate(blockmanager.blockTemplatesList[Inventory.blockInHand]);
                    var newBlockPostion = hit.transform.parent.gameObject.transform.position + hit.normal;
                    var newBlockRotation = hit.transform.parent.gameObject.transform.rotation;
                    int newBlockNumber = Convert.ToInt32(newBlockPostion.x) + (Convert.ToInt32(newBlockPostion.z) * 16) + (Convert.ToInt32(newBlockPostion.y) * 256);

                    blockmanager.blockList[newBlockNumber] = Inventory.blockInHand;
                    newBlock.transform.position = newBlockPostion;
                    newBlock.transform.rotation = newBlockRotation;
                    newBlock.name = "block" + newBlockNumber;
                }
                
            }
        }

    }
}
