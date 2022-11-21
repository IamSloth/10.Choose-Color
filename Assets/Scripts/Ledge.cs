using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Ledge : MonoBehaviour
{

    public int blockCount;
    public float blockSize;
    public int nowBlock;
    
    Block[] blocks;
    // Start is called before the first frame update
    void Start()
    {
        blocks = GetComponentsInChildren<Block>();
    }

    public void Align()
    {
        blockCount = blocks.Length;

        if (blockCount == 0)
        {
            Debug.Log("블록이 없어요");
            return;
        }

        blockSize = blocks[0].GetComponentInChildren<BoxCollider>().transform.localScale.z;

        for (int i = 0; i < blockCount; i++)
        {
            blocks[i].transform.Translate(0, 0, i * blockSize * -1);
            blocks[i].Init();
        }

    }

    IEnumerator Move()
    {
        float nextZ = transform.position.z + 2;
        while (transform.position.z < nextZ)
        {
            yield return null;    
            transform.Translate(0,0,Time.deltaTime * 10);
            
        }

        transform.position = Vector3.forward * nextZ;
    }
    
    public void Select(int selectType)
    {
        bool result = blocks[nowBlock].Check(selectType);

        if (result)
        {//정답
            GameManager.Success();
            StartCoroutine((Move()));    
            
            nowBlock = (nowBlock + 1) % blockCount;
        }

        else
        {//오답
            GameManager.Fail();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
