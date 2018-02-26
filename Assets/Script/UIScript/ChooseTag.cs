using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ChooseTag : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static string MapName;
    public static string Mode;
    public static string Difficulty;

    public Button LeftButton;
    public Button RightButton;
    public Text ShowContext;
    public Image TipFrame;

    public string[] ShowWord;
    public string Tip;

    private int index = 0;

    private void Start()
    {
        TipFrame.GetComponentInChildren<Text>().text = Tip;

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        TipFrame.DOFade(129f/255f, 0.5f);
        TipFrame.GetComponentInChildren<Text>().DOFade(1, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TipFrame.DOFade(0, 0.5f);
        TipFrame.GetComponentInChildren<Text>().DOFade(0, 0.5f);
    }
}
