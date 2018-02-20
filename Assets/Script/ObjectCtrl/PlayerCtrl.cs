using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCtrl : HumanCtrl
{
    public override void Action()
    {
        //被玩家控制时的动作。
        base.Action();
    }

    public override void Die()
    {
        
    }

    protected override void Start()
    {
        base.Start();
        IsCtrl = true;
        transform.GetChild(1).gameObject.AddComponent<PlayerGetData>();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
