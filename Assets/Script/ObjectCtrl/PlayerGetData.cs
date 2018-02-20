using UnityEngine;

public class PlayerGetData : MonoBehaviour
{
    Material material;

	void Awake ()
    {
        material = Resources.Load<Material>("Player");
        GetComponent<SkinnedMeshRenderer>().material = PlayerData.PlayerMaterial;
	}
}
