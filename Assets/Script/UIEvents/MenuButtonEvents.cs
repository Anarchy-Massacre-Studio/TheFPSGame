using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

[RequireComponent(typeof(MenuHumanAnimation))]
public partial class MenuButtonEvents : MonoBehaviour
{
    public MenuHumanAnimation MenuHumanAnimation;

    public GameObject Camera;

    public RectTransform BackButton;

    public RectTransform MainMenu;
    public RectTransform EquipMenu;
    public RectTransform EquipMenu_Color;
    public RectTransform EquipMenu_Weapon;
    public RectTransform EquipMenu_Equip;
    public RectTransform ColorTable;
    public RectTransform ValueSlider;

    public Image ColorTableMask;

    public GameObject Loading;

    private enum MainMenuButtons : int { play, equip, course, setting, about, exit }
    private enum EquipMenuButtons : int { color, weapon, equip }
    private enum EquipColorMenuButtons : int { color, metallic, smooth, shine }

    public void MainMenuEvents(int i) 
    {
        switch(i)
        {
            case (int)MainMenuButtons.play:
                Play();
                break;

            case (int)MainMenuButtons.equip:
                Equip();
                MenuHumanAnimation.Equip();
                break;

            case (int)MainMenuButtons.course:
                Course();
                break;

            case (int)MainMenuButtons.setting:
                Setting();
                break;

            case (int)MainMenuButtons.about:
                About();
                break;

            case (int)MainMenuButtons.exit:
                Exit();
                break;
        }
    }
    public void EquipMenuEvents(int i)
    {
        switch(i)
        {
            case (int)EquipMenuButtons.color:
                parent_name = "EquipMenu_Color";

                doAnchorPosXShowAndHide(EquipMenu_Color, EquipMenu);
                MenuHumanAnimation.Equip_Color();
                break;

            case (int)EquipMenuButtons.weapon:
                parent_name = "EquipMenu_Weapon";

                doAnchorPosXShowAndHide(EquipMenu_Weapon, EquipMenu);
                MenuHumanAnimation.Equip_Weapon();
                break;

            case (int)EquipMenuButtons.equip:
                parent_name = "EquipMenu_Equip";

                doAnchorPosXShowAndHide(EquipMenu_Equip, EquipMenu);
                MenuHumanAnimation.Equip_Equip();
                break;
        }
    }
    public void EquipColorMenuEvents(int i)
    {
        RectTransform color_color = EquipMenu_Color.transform.GetChild(0).GetComponent<RectTransform>();
        RectTransform color_metallic = EquipMenu_Color.transform.GetChild(1).GetComponent<RectTransform>();
        RectTransform color_smooth = EquipMenu_Color.transform.GetChild(2).GetComponent<RectTransform>();
        RectTransform color_shine = EquipMenu_Color.transform.GetChild(3).GetComponent<RectTransform>();

        RectTransform[] color_buttons = new RectTransform[4] { color_color, color_metallic, color_smooth, color_shine };

        switch (i)
        {
            case (int)EquipColorMenuButtons.color:
                foreach(var c in color_buttons)
                {
                    if(c == color_color)
                    {
                        c.DOLocalRotate(new Vector3(0, 5f, 0), 0.3f);
                        c.DOAnchorPos3D(new Vector3(c.anchoredPosition3D.x, c.anchoredPosition3D.y, -40f), 0.3f);
                    }
                    else
                    {
                        c.DOLocalRotate(new Vector3(0, 20f, 0), 0.3f);
                        c.DOAnchorPos3D(new Vector3(c.anchoredPosition3D.x, c.anchoredPosition3D.y, 0f), 0.3f);
                    }
                }
                break;

            case (int)EquipColorMenuButtons.metallic:
                foreach (var c in color_buttons)
                {
                    if (c == color_metallic)
                    {
                        c.DOLocalRotate(new Vector3(0, 5f, 0), 0.3f);
                        c.DOAnchorPos3D(new Vector3(c.anchoredPosition3D.x, c.anchoredPosition3D.y, -40f), 0.3f);
                    }
                    else
                    {
                        c.DOLocalRotate(new Vector3(0, 20f, 0), 0.3f);
                        c.DOAnchorPos3D(new Vector3(c.anchoredPosition3D.x, c.anchoredPosition3D.y, 0f), 0.3f);
                    }
                }
                break;

            case (int)EquipColorMenuButtons.smooth:
                foreach (var c in color_buttons)
                {
                    if (c == color_smooth)
                    {
                        c.DOLocalRotate(new Vector3(0, 5f, 0), 0.3f);
                        c.DOAnchorPos3D(new Vector3(c.anchoredPosition3D.x, c.anchoredPosition3D.y, -40f), 0.3f);
                    }
                    else
                    {
                        c.DOLocalRotate(new Vector3(0, 20f, 0), 0.3f);
                        c.DOAnchorPos3D(new Vector3(c.anchoredPosition3D.x, c.anchoredPosition3D.y, 0f), 0.3f);
                    }
                }
                break;

            case (int)EquipColorMenuButtons.shine:
                foreach (var c in color_buttons)
                {
                    if (c == color_shine)
                    {
                        c.DOLocalRotate(new Vector3(0, 5f, 0), 0.3f);
                        c.DOAnchorPos3D(new Vector3(c.anchoredPosition3D.x, c.anchoredPosition3D.y, -40f), 0.3f);
                    }
                    else
                    {
                        c.DOLocalRotate(new Vector3(0, 20f, 0), 0.3f);
                        c.DOAnchorPos3D(new Vector3(c.anchoredPosition3D.x, c.anchoredPosition3D.y, 0f), 0.3f);
                    }
                }
                break;
        }
    }

    private string parent_name = null;
    public void Back_Equip()
    {
        switch(parent_name)
        {
            case "EquipMenu":
                doAnchorPosXShowAndHide(MainMenu, EquipMenu, BackButton);

                Camera.transform.DOMove(new Vector3(0, 1f, -20f), 0.5f);
                Camera.transform.DORotate(Vector3.zero, 0.5f);

                MenuHumanAnimation.Equip_Back();
                break;

            case "EquipMenu_Color":
                parent_name = "EquipMenu";

                doAnchorPosXShowAndHide(EquipMenu, EquipMenu_Color);
                MenuHumanAnimation.Equip_Color_Back();
                break;

            case "EquipMenu_Weapon":
                parent_name = "EquipMenu";

                doAnchorPosXShowAndHide(EquipMenu, EquipMenu_Weapon);
                MenuHumanAnimation.Equip_Weapon_Back();
                break;

            case "EquipMenu_Equip":
                parent_name = "EquipMenu";

                doAnchorPosXShowAndHide(EquipMenu, EquipMenu_Equip);
                MenuHumanAnimation.Equip_Equip_Back();
                break;

            default: break;
        }
    }

    public void Back(int i)
    {

    }


    #region api

    #region DoTweeningBox

    void doAnchorPosXShowAndHide(RectTransform show, params RectTransform[] hide)
    {
        if (show != null)
        {
            show.DOAnchorPosX(0, 0.5f);
            show.DORotate(new Vector3(0, 20, 0), 0.3f);
        }
        if (hide != null)
        {
            foreach (var h in hide)
            {
                h.DOAnchorPosX(500, 0.5f);
                h.DORotate(new Vector3(0, 180, 0), 0.3f);
            }
        }
    }

    void doAnchorPosXShowAndHide(bool moreShow, RectTransform hide, params RectTransform[] show)
    {
        if (moreShow)
        {
            if (hide != null)
            {
                hide.DOAnchorPosX(500, 0.5f);
                hide.DORotate(new Vector3(0, 180, 0), 0.3f);
            }
            if (show != null)
            {
                foreach (var s in show)
                {
                    s.DOAnchorPosX(0, 0.5f);
                    s.DORotate(new Vector3(0, 20, 0), 0.3f);
                }
            }
        }
    }

    #endregion

    void Play()
    {

    }

    void Equip()
    {
        parent_name = "EquipMenu";

        MainMenu.DOAnchorPosX(500, 0.5f);
        MainMenu.DORotate(new Vector3(0, 180, 0), 0.3f);

        Camera.transform.DOMove(new Vector3(0, -2.5f, -9.5f), 1f);
        Camera.transform.DORotate(new Vector3(0, -8, 0), 1f).OnComplete(() =>
        {
            doAnchorPosXShowAndHide(true, null, EquipMenu, BackButton);
        });
    }

    void Course()
    {
        MenuHumanAnimation.Course();

        MainMenu.DOAnchorPosX(500, 0.5f);
        MainMenu.DORotate(new Vector3(0, 180, 0), 0.3f).OnComplete(() => { Loading.SetActive(true); });

        Camera.transform.DOMove(new Vector3(-5f, -6.5f, -7f), 1f);
        Camera.transform.DORotate(new Vector3(-7, -10, 0), 1f).OnComplete(() => { StartCoroutine(load()); });
    }

    void Setting()
    {

    }

    void About()
    {

    }

    void Exit()
    {
        Application.Quit();
    }

    IEnumerator load()
    {
        yield return new WaitForSecondsRealtime(1f);
        yield return SceneManager.LoadSceneAsync("Course");
    }

    #endregion
}

public partial class MenuButtonEvents : MonoBehaviour
{
    public Image Mask;

    private void Start()
    {
        Camera.transform.DOMoveZ(-20, 1f).OnComplete(() => { doAnchorPosXShowAndHide(MainMenu, null); });
        Mask.DOColor(new Color(0, 0, 0, 0), 1f).OnComplete(() => { Mask.gameObject.SetActive(false); });

        GameObject color_table_button = Resources.Load<GameObject>("ColorTableButton");

        foreach(var c in PlayerData.GetColorTable())
        {
            var c_t_b = Instantiate(color_table_button);
            c_t_b.transform.SetParent(ColorTable.transform.GetChild(0));

            var c_t_b_r = c_t_b.GetComponent<RectTransform>();

            c_t_b_r.anchoredPosition3D = new Vector3(c_t_b_r.anchoredPosition3D.x, c_t_b_r.anchoredPosition3D.y, 0);
            c_t_b_r.localScale = Vector3.one;

            c_t_b.GetComponent<Image>().color = c;
            c_t_b.GetComponent<ColorTableButton>().Target = ColorTable.transform.GetChild(1).GetComponent<RectTransform>();
        }
    }
}
