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
    public Transform Refer;

    public bool isActive = false; //是否开启ik动画。
    public Transform RightHandRefer; //右手ik标记物。
    public Transform LeftHandRefer; //左手ik标记物。

    public int Speed = 50;

    public float sensitivityX = 10F;
    public float sensitivityY = 10F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationY = 0F;

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
            //Debug.Log(CharacterController.isGrounded);
            //人物控制。
            #region human
            //移动 w，a，s，d
            CharacterController.Move(transform.forward * Time.deltaTime * Input.GetAxis("Vertical") * Speed);
            Animator.SetFloat("MoveX", Input.GetAxis("Vertical"));

            CharacterController.Move(transform.right * Time.deltaTime * Input.GetAxis("Horizontal") * Speed);
            Animator.SetFloat("MoveY", Input.GetAxis("Horizontal"));

            //快跑 shift
            if (Input.GetAxis("Shift") > 0 && Input.GetAxis("Vertical") > 0)
            {
                CharacterController.Move(transform.forward * Time.deltaTime * Input.GetAxis("Vertical") * (Speed + 20) * Input.GetAxis("Shift"));
                Animator.SetFloat("MoveX", Input.GetAxis("Vertical") + Input.GetAxis("Shift"));
            }

            //跳跃 空格
            if(Input.GetKeyDown(KeyCode.Space) && CharacterController.isGrounded)
            {
                Debug.Log("Jump");
                CharacterController.Move(transform.up * 5);
            }

            //重力
            if(!CharacterController.isGrounded)
            {
                CharacterController.Move(CharacterController.velocity - Vector3.up * 9.8f);
            }
            #endregion

            #region camera
            //相机控制。
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
                transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX, 
                transform.localEulerAngles.z);

 
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            
            CameraPosition.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);

            #endregion

            Refer.transform.localEulerAngles = CameraPosition.localEulerAngles;

        }
        //不受玩家控制时的AI。
        else
        {

        }
    }

    //动画ik。
    private void OnAnimatorIK(int layerIndex)
    {
        if(Animator)
        {
            if(isActive)
            {
                Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
                Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
                Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
                Animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
                if(RightHandRefer != null)
                {
                    Animator.SetIKPosition(AvatarIKGoal.RightHand, RightHandRefer.position);
                    Animator.SetIKRotation(AvatarIKGoal.RightHand, RightHandRefer.rotation);
                }
            }
            else
            {
                Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                Animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            }
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
