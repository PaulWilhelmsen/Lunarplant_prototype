using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class SeedManager : MonoBehaviour
{

    public Text greenLeafText;
    public Text purpleLeafText;
    [SerializeField]
    private int amountGreenLeaf;
    [SerializeField]
    private int amountPurpleLeaf;
    // Use this for initialization
    void Start()
    {
        Assert.IsNotNull(greenLeafText, "Text not set in seedmanager");
        Assert.IsNotNull(purpleLeafText, "Text not set in seedmanager");
        greenLeafText.text = amountGreenLeaf.ToString();
        purpleLeafText.text = amountPurpleLeaf.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addSeed(int seedType, int addAmount)
    {
        switch (seedType)
        {
            case 0:
                {
                    amountGreenLeaf += addAmount;
                    greenLeafText.text = amountGreenLeaf.ToString();
                    break;
                }
            case 1:
                {
                    amountPurpleLeaf += addAmount;
                    purpleLeafText.text = amountPurpleLeaf.ToString();
                    break;
                }
        }
    }

    public void subSeed(int seedType, int subAmount)
    {
        switch (seedType)
        {
            case 0:
                {
                    amountGreenLeaf -= subAmount;
                    greenLeafText.text = amountGreenLeaf.ToString();
                    break;
                }
            case 1:
                {
                    amountPurpleLeaf -= subAmount;
                    purpleLeafText.text = amountPurpleLeaf.ToString();
                    break;
                }
        }
    }

    public int getGreenSeed()
    {
        return amountGreenLeaf;
    }
    public int getPurpleSeed()
    {
        return amountPurpleLeaf;
    }

}
