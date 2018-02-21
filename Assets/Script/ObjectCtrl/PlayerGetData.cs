using UnityEngine;
using System;
using System.Xml.Linq;

public class PlayerGetData : MonoBehaviour
{
    Material material;

	void Awake ()
    {
        GetComponent<SkinnedMeshRenderer>().material = PlayerData.PlayerMaterial;
	}

    XDocument xDocument;

    private void OnApplicationQuit()
    {
        try
        {
            xDocument = XDocument.Load("PlayerData.xml");
        }
        catch(Exception ex)
        {
            System.IO.File.WriteAllText("Error_Log", ex.Message);
        }
        XElement xe = xDocument.Element("PlayerData").Element("Color");

        material = GetComponent<SkinnedMeshRenderer>().material;

        xe.Element("MainColor").Element("R").SetValue(material.color.r.ToString());
        xe.Element("MainColor").Element("G").SetValue(material.color.g.ToString());
        xe.Element("MainColor").Element("B").SetValue(material.color.b.ToString());

        xe.Element("Metallic").SetValue(material.GetFloat("_Metallic").ToString());
        xe.Element("Smoothness").SetValue(material.GetFloat("_Glossiness").ToString());

        xe.Element("Emission").Element("R").SetValue(material.GetColor("_EmissionColor").r.ToString());
        xe.Element("Emission").Element("G").SetValue(material.GetColor("_EmissionColor").g.ToString());
        xe.Element("Emission").Element("B").SetValue(material.GetColor("_EmissionColor").b.ToString());

        xDocument.Save("PlayerData.xml");

        PlayerData.GetColorTable();
    }
}
