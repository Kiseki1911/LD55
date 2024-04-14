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
    public List<string> parameters = new List<string>();
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
    public void ShowResult()
    {
        panel.SetActive(true);
        float val = GameManager.Instance.randomHSV / 36f;
        itemIndex = (int)val;
        resultValue = val - itemIndex;
        sprite.sprite = items[itemIndex].sprite;
        itemName.text = items[itemIndex].name;
        itemDes.text = items[itemIndex].des;
        int a = Random.Range(0, parameters.Count);
        int b = Random.Range(0, parameters.Count);
        while (a == b) b = Random.Range(0, parameters.Count);
        itemValues.text = parameters[a] + ": " + (resultValue*100).ToString("F2") + "\n" + parameters[b] + ": " + (GameManager.Instance.finalAngles / 9).ToString("F2");
    }
}

[System.Serializable]
public class Items
{
    public Sprite sprite;
    public string name;
    public string des;
}