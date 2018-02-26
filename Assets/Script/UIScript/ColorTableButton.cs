using UnityEngine;
using UnityEngine.UI;

public class ColorTableButton : MonoBehaviour
{
    public static bool isColor;

    public RectTransform Target;
	// Use this for initialization
	void Start ()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
	}

    void OnClick()
    {
        Target.anchoredPosition = GetComponent<RectTransform>().anchoredPosition + new Vector2(4.5f,-9.5f);
        if (isColor) PlayerData.PlayerMaterial.color = GetComponent<Image>().color;
        else PlayerData.PlayerMaterial.SetColor("_EmissionColor", GetComponent<Image>().color);
    }
}
