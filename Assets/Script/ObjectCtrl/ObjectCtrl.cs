using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 可被控制的游戏物体的基类；
/// 所有可被玩家控制的游戏物体都继承该类；
/// 该类不需要拖到任何游戏物体上。
/// </summary>
public abstract class ObjectCtrl : MonoBehaviour
{
    /// <summary>
    /// 游戏物体的生命值。
    /// </summary>
    public int Health = 100;
    /// <summary>
    /// 控制该物体时，摄像机的位置；
    /// 设为CameraPosition的子物体，相对位置和旋转为(0,0,0)。
    /// </summary>
    public Transform CameraPosition;

    /// <summary>
    /// 该游戏物体是否被控制。
    /// </summary>
    public bool IsCtrl = false;

    /// <summary>
    /// 游戏物体死亡时的动作。
    /// </summary>
    public abstract void Die();

    /// <summary>
    /// 游戏物体活着时所执行的动作。
    /// Action()会在Update中执行。
    /// </summary>
    public abstract void Action();

    protected virtual void Update()
    {
        Action();
    }
}
