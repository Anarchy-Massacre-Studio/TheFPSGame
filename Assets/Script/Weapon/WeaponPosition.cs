using UnityEngine;

[ExecuteInEditMode]
public class WeaponPosition : MonoBehaviour
{
    public Transform LongRefer;
    public Transform ShortRefer;

    public Transform Long;
    public Transform Short;
	
	void Update ()
    {
        //Long.position = LongRefer.position + new Vector3(-2.5f, 0, 1.2f);
        //Short.position = ShortRefer.position + new Vector3(1, 0, 1);

        //Long.localEulerAngles = LongRefer.eulerAngles + new Vector3(-90, 0, -90);
        //Short.localEulerAngles = ShortRefer.eulerAngles + new Vector3(160, 20, 0);
    }
}
