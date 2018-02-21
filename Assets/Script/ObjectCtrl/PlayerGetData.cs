using UnityEngine;
using System;
using System.Xml.Linq;

public class PlayerGetData : MonoBehaviour
{
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
    }
}
