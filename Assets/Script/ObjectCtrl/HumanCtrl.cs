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
    public float move_speed=15;            //角色移动速度
    public bool climb_1_check = false;        //检测前方是否是小墙
    public bool jump = false;               //跳跃中？
    public Transform wall_Check;           //墙壁检测空物体
    public CapsuleCollider cc;              //角色碰撞器

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
            //人物按键输入
            #region humanKeyInput
            //获取按键 w,a,s,d,shift,c，space
            float move_x_Input = Input.GetAxis("Horizontal");
            float move_y_Input = Input.GetAxis("Vertical");
            float run_Input = ((move_y_Input >= 0.5 && Input.GetKey(KeyCode.LeftShift)&& squat_state==false) ? Input.GetAxis("Shift") : Input.GetAxis("Shift"));//返回shift
            squat_state = ((Input.GetKeyDown(KeyCode.C)) ? squat_state = !squat_state : squat_state);//返回squat_state
            bool jump_Input = Input.GetKeyDown(KeyCode.Space);
            #endregion

            //Debug.Log(CharacterController.isGrounded);

            //墙壁检测
            climb_1_check = ((Physics.Linecast(transform.position, wall_Check.position, 1 << LayerMask.NameToLayer("Wall_1"))) ? climb_1_check =true : climb_1_check = false); 

            //人物动画控制
            #region humanAnimation
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
            //跳跃按键执行
            if (jump_Input)
            {
                Animator.SetBool("Jump", true);     //跳跃
            }
            //跳跃动画判断
            if (jump_Input && climb_1_check == true)
            {
                Animator.SetFloat("JumpY", 3);//执行翻墙
            }
            else if (jump_Input && run_Input != 0 && climb_1_check == false)
            {
                Animator.SetFloat("JumpY", 2);//执行向前大跳
            }
            else if (jump_Input && run_Input == 0 && climb_1_check == false)
            {
                Animator.SetFloat("JumpY", 1);//小跳
            }
            #endregion

            //人物移动
            #region humanMove
            //移动w，a，s，d,shift
            float squat_y_move_speed = ((squat_state) ? move_y_Input * 0.5f : move_y_Input);//返回蹲或起的前后移动速度
            float squat_x_move_speed = ((squat_state) ? move_x_Input * 0.5f : move_x_Input);//返回蹲或起的左右移动速度
            transform.Translate(new Vector3(0, 0, squat_y_move_speed + run_Input) * Time.deltaTime * (move_speed+20));     //前后移动
            transform.Translate(new Vector3(squat_x_move_speed, 0, 0) * Time.deltaTime * move_speed);                //左右移动
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

    /// <summary>
    /// 跳跃事件开始
    /// </summary>
    /// <param name="start"></param>
    public void event_jump1_start(string collide_close)
    {
        Animator.applyRootMotion = true;        //开启动画控制
        if (collide_close=="ok")
        {
            cc.enabled = false;
        }
    }
    /// <summary>
    /// 跳跃事件结束
    /// </summary>
    /// <param name="end"></param>
    public void event_jump1_end(string collide_close)
    {
        if (collide_close == "no")
        {
            cc.enabled = true;
        }
        Animator.applyRootMotion = false;      //关闭动画控制
        Animator.SetFloat("JumpY", 0);      //重新赋值没有跳跃类型
        Animator.SetBool("Jump", false);     //重新赋值没有跳跃
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
