using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFade : PostEffectBase 
{
	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit (src, dest, _Material);
	}
}
