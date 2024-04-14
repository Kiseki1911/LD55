using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : Singleton<ResultManager>
{
    public GameObject panel;
    public Image sprite;
    public TMP_Text itemName, itemDes, itemValues;

    public List<Items> items = new List<Items>();
    int itemIndex;
    float resultValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowResult(){
        panel.SetActive(true);
        float val = GameManager.Instance.randomHSV/36f;
        itemIndex = (int)val;
        resultValue = val - itemIndex;
        sprite.sprite = items[itemIndex].sprite;
        itemName.text = items[itemIndex].name;
        itemDes.text = items[itemIndex].des;
        itemValues.text = resultValue.ToString();
    }
}

[System.Serializable]
public class Items{
    public Sprite sprite;
    public string name;
    public string des;
}