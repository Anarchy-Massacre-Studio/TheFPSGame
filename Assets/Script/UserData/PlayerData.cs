using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public static class PlayerData
{
    public static Material PlayerMaterial;
    public static Color[] ColorTable = null;

    static XDocument playerData;

    static PlayerData()
    {
        PlayerMaterial = Resources.Load<Material>("Player");
        
        try
        {
            playerData = XDocument.Load("PlayerData.xml");
        }
        catch(FileNotFoundException)
        {
            XDocument xDocument = new XDocument(
                new XElement("PlayerData",
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
                            new XElement("B", "0")))));   

            playerData = xDocument;
            xDocument.Save("PlayerData.xml");

        }
        catch(Exception ex)
        {
            File.WriteAllText("Error_Log.txt", ex.Message);
            Application.Quit();
        }

        XElement x = playerData.Element("PlayerData").Element("Color");

        float m_r = Convert.ToSingle(x.Element("MainColor").Element("R").Value);
        float m_g = Convert.ToSingle(x.Element("MainColor").Element("G").Value);
        float m_b = Convert.ToSingle(x.Element("MainColor").Element("B").Value);

        float metallic = Convert.ToSingle(x.Element("Metallic").Value);
        float smoothness = Convert.ToSingle(x.Element("Smoothness").Value);

        float e_r = Convert.ToSingle(x.Element("Emission").Element("R").Value);
        float e_g = Convert.ToSingle(x.Element("Emission").Element("G").Value);
        float e_b = Convert.ToSingle(x.Element("Emission").Element("B").Value);

        PlayerMaterial.color = new Color(m_r, m_g, m_b);
        PlayerMaterial.SetFloat("_Metallic", metallic);
        PlayerMaterial.SetFloat("_Glossiness", smoothness);
        PlayerMaterial.SetColor("_EmissionColor", new Color(e_r, e_g, e_b));
    }

    public static Color[] GetColorTable()
    {
        if (ColorTable == null)
        {
            XDocument xDocument = null;
            try
            {
                xDocument = XDocument.Load("Color.xml");
            }
            catch (FileNotFoundException ex)
            {
                File.WriteAllText("Color_Error_Log.txt", ex.Message);
                Application.Quit();
            }
            catch (Exception ex)
            {
                File.WriteAllText("Error_Log.txt", ex.Message);
                Application.Quit();
            }

            var query = from q in xDocument.Descendants("Data")
                        select q.Value;

            String[] color_hex_string = new String[136];

            for(int i = 0; i < query.Count(); i++)
            {
                color_hex_string[i] = query.ElementAt(i);
                char[] c = new char[7];
                c = color_hex_string[i].ToCharArray();

                int[] c_int = new int[6];
                for(int j = 0; j < c_int.Length; j++)
                {
                    c_int[j] = Convert.ToInt32(c[j+1].ToString(), 16);
                }

                float r = (c_int[0] * 16 + c_int[1]) / 255f;
                float g = (c_int[2] * 16 + c_int[3]) / 255f;
                float b = (c_int[4] * 16 + c_int[5]) / 255f;

                ColorTable = new Color[136];
                ColorTable[i] = new Color(r, g, b);
            }

            return ColorTable;
        }
        else
        {
            return ColorTable;
        }
    }
}
