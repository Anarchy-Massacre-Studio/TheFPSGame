using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GunScreen : MonoBehaviour
{
    public Camera Camera;
    public MeshRenderer Screen;

	void Start ()
    {
        RenderTexture renderTexture = new RenderTexture(Camera.pixelWidth, Camera.pixelHeight, 24);
        Camera.targetTexture = renderTexture;

        Material material = new Material(Shader.Find("Standard"));
        material.mainTexture = renderTexture;
        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", Color.white);
        material.SetTexture("_EmissionMap", renderTexture);
        material.SetFloat("_Metallic", 0.7f);
        material.SetFloat("_Glossiness", 0.8f);

        Screen.material = material;
	}
}
