using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 人形生物的控制，包括动画。
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class HumanCtrl : ObjectCtrl
{
    public CharacterController CharacterController;
    public Animator Animator;

    public int Speed = 50;

    protected virtual void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
    }
    /// <summary>
    /// MachineCtrl,HumanCtrl,BuildingCtrl的注释一样，PlayerCtrl的例外。
    /// </summary>
    public override void Action()
    {
        //被玩家控制时的动作。
        if (IsCtrl)
        {
            CharacterController.Move(transform.forward * Time.deltaTime * Input.GetAxis("Vertical") * Speed);
            Animator.SetFloat("MoveX", Input.GetAxis("Vertical"));

            CharacterController.Move(transform.right * Time.deltaTime * Input.GetAxis("Horizontal") * Speed);
            Animator.SetFloat("MoveY", Input.GetAxis("Horizontal"));

            if (Input.GetAxis("Shift") > 0 && Input.GetAxis("Vertical") > 0)
            {
                CharacterController.Move(transform.forward * Time.deltaTime * Input.GetAxis("Vertical") * (Speed + 20) * Input.GetAxis("Shift"));
                Animator.SetFloat("MoveX", Input.GetAxis("Vertical") + Input.GetAxis("Shift"));
            }

        }
        //不受玩家控制时的AI。
        else
        {

        }
    }

    public override void Die()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
