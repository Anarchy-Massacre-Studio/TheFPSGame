using UnityEngine;
using UnityEngine.UI;

public class SliderTableEvent : MonoBehaviour
{
    public static bool isMetallic;

    public Text Text;
    // Use this for initialization
    void Start ()
    {
        GetComponent<Slider>().onValueChanged.AddListener(OnValueChanged);
	}

    void OnValueChanged(float f)
    {
        Text.text = f.ToString("0.00");
        if (isMetallic) PlayerData.PlayerMaterial.SetFloat("_Metallic", f);
        else PlayerData.PlayerMaterial.SetFloat("_Glossiness", f);
    }
}
