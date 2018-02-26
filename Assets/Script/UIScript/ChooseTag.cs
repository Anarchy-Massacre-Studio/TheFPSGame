using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseTag : MonoBehaviour
{
    public Button LeftButton;
    public Button RightButton;
    public Text ShowContext;

    public string[] ShowWord;

    private int index = 0;

    private void Start()
    {
        ShowContext.text = ShowWord[index];

        LeftButton.onClick.AddListener(() =>
        {
            if(index == 0)
            {
                ShowContext.text = ShowWord[ShowWord.Length - 1];
                index = ShowWord.Length - 1;
            }
            else
            {
                ShowContext.text = ShowWord[index - 1];
                index--;
            }
        });
        RightButton.onClick.AddListener(() =>
        {
            if(index == ShowWord.Length - 1)
            {
                ShowContext.text = ShowWord[0];
                index = 0;
            }
            else
            {
                ShowContext.text = ShowWord[index + 1];
                index++;
            }
        });
    }
}
