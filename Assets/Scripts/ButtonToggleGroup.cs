using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToggleGroup : MonoBehaviour
{
    public Color defaultTint;
    public Color selectedTint;
    public List<Image> buttons;

    int selectedIndex;

    // Start is called before the first frame update
    void Start()
    {
        selectedIndex = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().skinIndex;
        SetUI();
    }

    public void SetCurrentIndex(int index)
    {
        selectedIndex = index;
        SetUI();
    }

    void SetUI()
    {
        ResetColors();
        buttons[selectedIndex].color = selectedTint;
    }

    void ResetColors()
    {
        foreach (Image image in buttons)
        {
            image.color = defaultTint;
        }
    }
}
