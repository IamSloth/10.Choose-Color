using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewRecordUI : MonoBehaviour
{
    Text myText;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("Score"))
            return;

        myText = GetComponent<Text>();
        myText.text = "최고점수 " + string.Format("{0:F0}", PlayerPrefs.GetFloat("Score"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
