using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    Ledge ledge;

    public Rigidbody[] characters;
    public int type;
    
    // Start is called before the first frame update
    void Start()
    {
        ledge = GetComponentInParent<Ledge>();
    }

    private void LateUpdate()
    {
        if (transform.position.z >= 4)
        {
            transform.Translate(0,0,ledge.blockCount * ledge.blockSize * -1);
            Init();
        }
    }

    public void Init()
    {
        type = Random.Range(0, 3);
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].gameObject.SetActive(type == i);
        }
        
        StartCoroutine(InitPhysics());
    }

    IEnumerator InitPhysics()
    {
        characters[type].isKinematic = true;
        yield return new WaitForFixedUpdate();

        characters[type].velocity = Vector3.zero;
        characters[type].angularVelocity = Vector3.zero;
        yield return new WaitForFixedUpdate();

        characters[type].transform.localPosition = Vector3.zero;
        characters[type].transform.localRotation = Quaternion.identity;
    }

    public bool Check(int selectType)
    {
        bool result = (type == selectType);
        if(type == selectType)
            StartCoroutine(Hit());
        return result;
    }

    IEnumerator Hit()
    {
        characters[type].isKinematic = false;
        yield return new WaitForFixedUpdate();
        
        int ran = Random.Range(0, 2);
        Vector3 forceVec;
        Vector3 torqueVec;
        switch (ran)
        {
            case 0:
                forceVec = (Vector3.right + Vector3.up) * 3f;
                torqueVec = (Vector3.forward + Vector3.down) * 3f;
                characters[type].AddForce(forceVec,ForceMode.Impulse);
                characters[type].AddTorque(torqueVec,ForceMode.Impulse);
                break;
            case 1:
                forceVec = (Vector3.left + Vector3.up) * 3f;
                torqueVec = (Vector3.back + Vector3.up) * 3f;
                characters[type].AddForce(forceVec,ForceMode.Impulse);
                characters[type].AddTorque(torqueVec,ForceMode.Impulse);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
