using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 人形生物的控制，包括动画。
/// </summary>
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class HumanCtrl : ObjectCtrl
{
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

    public bool squat_state = false;    //角色蹲站状态，默认站

    protected virtual void Start()
    {
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
            //获取按键 w,a,s,d,shift,c
            float move_x_Input = Input.GetAxis("Horizontal");
            float move_y_Input = Input.GetAxis("Vertical");
            float run_Input = ((move_y_Input >= 0.5 && Input.GetKey(KeyCode.LeftShift)) ? 1 : 0);//返回1:0
            squat_state = ((Input.GetKeyDown(KeyCode.C)) ? squat_state = !squat_state : squat_state);//返回squat_state

            //移动动画 w，a，s，d,shift
            Animator.SetFloat("MoveX", move_x_Input);
            Animator.SetFloat("MoveY", move_y_Input+ run_Input);

            //蹲起+移动动画  w，a，s，d,c
            Animator.SetBool("Squat", squat_state);
            if (squat_state)
            {
                Animator.SetFloat("SquatX", move_x_Input);
                Animator.SetFloat("SquatY", move_y_Input);
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
