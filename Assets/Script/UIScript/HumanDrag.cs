using UnityEngine;

public class HumanDrag : MonoBehaviour
{
    public OutlinePostEffect OutlinePostEffect;

    public bool canDrag = true;

    private void OnMouseEnter() => OutlinePostEffect.samplerScale = 0.8f;

    private void OnMouseExit() => OutlinePostEffect.samplerScale = 0;

    private float mouseX;
    private bool isUp = false;

    private void OnMouseDrag()
    {
        if (canDrag)
        {
            isUp = false;
            mouseX = Input.GetAxis("Mouse X");
            transform.localEulerAngles -= new Vector3(0, mouseX * 5, 0);
        }
    }

    private void OnMouseUp() { if(canDrag) isUp = true; }

    private void Update()
    {
        if (canDrag) if (isUp) transform.localEulerAngles = new Vector3(0, Mathf.Lerp(transform.localEulerAngles.y, 150, Time.deltaTime * 4), 0);
    }
}
