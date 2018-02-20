using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public static class PlayerData
{
    public static Material PlayerMaterial;

    static PlayerData()
    {
        PlayerMaterial = new Material(Shader.Find("Standard"));
        XElement playerData;
        try
        {
            playerData = XElement.Load("PlayerData.xml");
        }
        catch(FileNotFoundException)
        {
            XElement element = new XElement("PlayerData",
                new XElement("Color",
                new XElement("MainColor",
                    new XElement("R", "1"),
                    new XElement("G", "1"),
                    new XElement("B", "1")),
                new XElement("Metallic", "0"),
                new XElement("Smoothness", "0.5"),
                new XElement("Emission", 
                    new XElement("R", "0"),
                    new XElement("G", "0"),
                    new XElement("B", "0"))));

            playerData = element;
            element.Save("PlayerData.xml");

        }
        catch(Exception ex)
        {
            File.WriteAllText("Log", ex.Message);
            Application.Quit();
        }
        finally
        {
            PlayerMaterial.color = new Color(1, 1, 1);
        }
    }
}
