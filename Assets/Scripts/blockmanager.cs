using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using static blockmanager;

public class blockmanager : MonoBehaviour
{
    [Serializable]
    public class JsonBlockDataClass
    {
        public List<int> blockList;
    }

    public List<int> blockList;

    public GameObject dirtBlock;
    public GameObject grassBlock;

    public List<GameObject> blockTemplatesList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blockTemplatesList.Clear();
        blockTemplatesList.Add(GameObject.Find("AirBlock"));
        blockTemplatesList.Add(GameObject.Find("DirtBlock"));
        blockTemplatesList.Add(GameObject.Find("GrassBlock"));
        blockTemplatesList.Add(GameObject.Find("StoneBlock"));
        if (!System.IO.File.Exists(UnityEngine.Application.persistentDataPath + "/level.json"))
        {
            GenerateBlocks();

        }
        LoadChunk();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.PageUp))
        {
            GenerateBlocks();
            
        }
        if (Input.GetKey(KeyCode.PageDown))
        {
            LoadChunk();
        }
        if (Input.GetKey(KeyCode.End))
        {
            SaveChunk();
        }
    }

    void GenerateBlocks()
    {
        blockList.Clear();
        int blockNumber = 0;
        while (blockNumber < 16384)
        {
            blockList.Add(1);
            blockNumber++;
            //blockList.Add(0);
            //blockNumber++;
        }
        int grassBlockNumber = 0;
        while (grassBlockNumber < 256)
        {
            blockList.Add(2);
            grassBlockNumber++;
            blockNumber++;
        }
        while (blockNumber < 32768)
        {
            blockList.Add(0);
            blockNumber++;
            //blockList.Add(0);
            //blockNumber++;
        }
        SaveChunk();
        LoadChunk();
    }

    void SaveChunk()
    {
        JsonBlockDataClass blockData = new JsonBlockDataClass
        {
            blockList = blockList
        };
        string json = JsonUtility.ToJson(blockData);
        System.IO.File.WriteAllText(UnityEngine.Application.persistentDataPath + "/level.json", json);
    }
    void CreateBlocks()
    {
        int currentBlock = 0;
        int currentBlockX = 0;
        int currentBlockY = 0;
        int currentBlockZ = 0;
        while (currentBlock < 32768)
        {
            GameObject blockObject = Instantiate(blockTemplatesList[blockList[currentBlock]]);
            blockObject.transform.position = new Vector3(currentBlockX, currentBlockY, currentBlockZ);
            blockObject.name = "block" + currentBlock;

            //if (blockList[currentBlock] == 1)
            //{
            //    GameObject blockObject = Instantiate(dirtBlock);
            //    blockObject.transform.position = new Vector3(currentBlockX, currentBlockY, currentBlockZ);
            //    blockObject.name = "block" + currentBlock;
            //}
            //if (blockList[currentBlock] == 2)
            //{
            //    GameObject blockObject = Instantiate(grassBlock);
            //    blockObject.transform.position = new Vector3(currentBlockX, currentBlockY, currentBlockZ);
            //    blockObject.name = "block" + currentBlock;
            //}

            currentBlockX++;
            if (currentBlockX == 16)
            {
                currentBlockX = 0;
                currentBlockZ++;
            }
            if (currentBlockZ == 16)
            {
                currentBlockZ = 0;
                currentBlockY++;
            }
            currentBlock++;
        }
    }

    void LoadChunk()
    {
        string json = System.IO.File.ReadAllText(UnityEngine.Application.persistentDataPath + "/level.json");
        JsonBlockDataClass blockData = JsonUtility.FromJson<JsonBlockDataClass>(json);
        blockList = blockData.blockList;
        CreateBlocks();
    }
}
