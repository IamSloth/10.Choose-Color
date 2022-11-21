using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Color highRate;
    public Color midRate;
    public Color lowRate;

    public Image fill;
    
    Slider mySlider;
    // Start is called before the first frame update
    void Start()
    {
        mySlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float rate = GameManager.playTime / GameManager.maxTime;
        mySlider.value = 1 - rate;

        if (rate >= 0.7f)
        {
            fill.color = lowRate;
        }
        
        else if (rate >= 0.4f)
        {
            fill.color  = midRate;
        }

        else
        {
            fill.color  = highRate;
        }
        
        
    }
}
