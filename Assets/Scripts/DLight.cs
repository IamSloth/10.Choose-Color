using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float changSpeed = 1;

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameManager.isGameOver)
            return;
        Debug.Log(GameManager.isGameOver + " aa");
        transform.Rotate(Vector3.right * (Time.deltaTime * changSpeed));
    }
}
