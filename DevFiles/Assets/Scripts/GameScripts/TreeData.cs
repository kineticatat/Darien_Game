using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeData : MonoBehaviour
{
    public Tree_ScriptableObject treeType;

    public Text[] titleDesc;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        titleDesc[0].text = treeType.name;
        titleDesc[1].text = treeType.Description;
    }
}
