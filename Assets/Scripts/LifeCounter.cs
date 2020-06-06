using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{
    public Text lifeCountText;

    //for using in another script
    public static LifeCounter lc;

    private void Start()
    {
        lc = gameObject.GetComponent<LifeCounter>();
    }

    public void SetLifeCount(int lifeCount)
    {
        lifeCountText.text = "x" + lifeCount.ToString();
    }
}
