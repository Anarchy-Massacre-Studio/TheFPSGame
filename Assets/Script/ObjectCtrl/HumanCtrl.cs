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
    public bool gorundFrom_check = false;         //在地面？
    public Transform walls_start_Check;           //墙壁检测器官起点
    public Transform walls_1end_Check;           //墙壁检测器官终点1
    public Transform ground_start_Check;           //地面检测器官起点
    public Transform ground_end_Check;           //地面检测器官终点
    public CapsuleCollider cc;              //角色碰撞器
    public ConstantForce cf;                //角色力
    public Transform bodyLookObj;           //上半身看向的目标 
    public Transform eyesHome;              //眼睛
    public Transform Carama_tf;              //相机
    public Transform gunLeftHandHome;          //枪上左手位置
    public Transform gunRightHandHome;         //枪上右手位置
    public Transform Gun_1;                 //来复枪位置
    public Transform gun_rifleHome;         //来复枪家的位置
    public bool Gun_1_have=true;                 //当前有枪?
    public bool leftHandIK_state = true;     //左手ik开启?
    public bool rightHandIK_state = true;     //左手ik开启?
    public bool bodyHandIK_state = true;     //肚子ik开启?
    public GameObject leftHandFrom;            //左手
    public GameObject Gun_1Obj;            //来复枪
    public bool Move_state = true;     //角色移动方法开启?
    public float run_Input_speed;        //跑步动画速度参数



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
            climb_1_check = ((Physics.Linecast(walls_start_Check.position, walls_1end_Check.position, 1 << LayerMask.NameToLayer("Wall_1"))) ? climb_1_check =true : climb_1_check = false);
            //地面检测
            gorundFrom_check = ((Physics.Linecast(ground_start_Check.position, ground_end_Check.position, 1 << LayerMask.NameToLayer("Ground"))) ? gorundFrom_check = true : gorundFrom_check = false);
            //人物动画控制
            #region humanAnimation
            //移动动画 w，a，s，d,shift
            Animator.SetFloat("MoveX", move_x_Input);
            //输入的shift给shift速度
            if (move_y_Input!=0&&!squat_state)
            {
                run_Input_speed = run_Input;
            }
            else
            {
                run_Input_speed = 0;
            }
            Animator.SetFloat("MoveY", move_y_Input+ run_Input_speed);
            //跑步时
            if (run_Input_speed != 0&& move_y_Input!=0)
            {

                //关闭上半身部分ik 
                leftHandIK_state = false;
                //rightHandIK_state = false;
                bodyHandIK_state = false;
 
                Gun_1Obj.transform.parent = leftHandFrom.transform;         //让枪成为手Home的子物体
                Animator.applyRootMotion = true;                            //开启动画控制
                Move_state = false;                                         //关闭人物移动方法
            }
            else
            {
                //开启上半身部分ik 
                leftHandIK_state = true;
               // rightHandIK_state = true;
                bodyHandIK_state = true;
                //让枪成为枪Home的子物体
                Gun_1Obj.transform.parent = gun_rifleHome.transform;
                Gun_1Obj.transform.position = gun_rifleHome.transform.position;
                Gun_1Obj.transform.rotation = gun_rifleHome.transform.rotation;
                Animator.applyRootMotion = false;      //关闭动画控制
                Move_state = true;                                         //开启人物移动方法
            }
            //蹲起+移动动画  w，a，s，d,c
            Animator.SetBool("Squat", squat_state);
            if (squat_state)
            {
                Animator.SetFloat("SquatX", move_x_Input);
                Animator.SetFloat("SquatY", move_y_Input);
            }
            //跳跃动画判断
            if (jump_Input && climb_1_check&& gorundFrom_check)
            {
                Animator.SetFloat("JumpY", 3);//执行翻墙
            }
            else if (jump_Input && run_Input_speed != 0 && !climb_1_check && gorundFrom_check)
            {
                Animator.SetFloat("JumpY", 2);//执行向前大跳
            }
            else if (jump_Input && run_Input_speed == 0 && !climb_1_check && gorundFrom_check)
            {
                Animator.SetFloat("JumpY", 1);//小跳
            }
            //如果角色在空中就施加重力
            cf.force = ((!gorundFrom_check) ? new Vector3(0, cf.force.y - 50, 0) : cf.force = new Vector3(0, 0, 0));//返回重力或初始化重力
            //角色落地判断
            Animator.SetBool("GroundFrom", gorundFrom_check);
            //摄像机位置等于眼睛位置
            Carama_tf.position = eyesHome.position;
            //当前有枪？
            if (Gun_1_have)
            {

            }
            #endregion

            //人物移动
            #region humanMove
            if (Move_state)
            {
                //移动w，a，s，d,shift
                float squat_y_move_speed = ((squat_state) ? move_y_Input * 0.7f : move_y_Input);//返回蹲或起的前后移动速度
                float squat_x_move_speed = ((squat_state) ? move_x_Input * 0.7f : move_x_Input);//返回蹲或起的左右移动速度
                //float run_speed = ((run_Input_speed == 1) ? 20 : 0);
                transform.Translate(new Vector3(0, 0, squat_y_move_speed ) * Time.deltaTime * move_speed );     //前后移动
                transform.Translate(new Vector3(squat_x_move_speed, 0, 0) * Time.deltaTime * move_speed);                //左右移动
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

    /// <summary>
    /// 跳跃事件开始
    /// </summary>
    /// <param name="start"></param>
    public void event_jump1_start(string start)
    {
        IsCtrl = false;         //关闭控制
        Animator.applyRootMotion = true;        //开启动画控制
        //jumps们的事件
        if (start == "jumps_start")
        {
            cc.enabled = false;
        }
        //jumpdown的事件
        if (start == "jumpdown_start")
        {
            
        }
    }
    /// <summary>
    /// 跳跃事件结束
    /// </summary>
    /// <param name="end"></param>
    public void event_jump1_end(string end)
    {
        IsCtrl = true;         //开启控制
        //jumps们的事件
        if (end == "jumps_end")
        {
            cc.enabled = true;
        }
        //jumpdown的事件
        if (end == "jumpdown_end")
        {
            
        }
        Animator.applyRootMotion = false;      //关闭动画控制
        Animator.SetFloat("JumpY", 0);      //重新赋值没有跳跃类型
    }

    //动画ik。
    private void OnAnimatorIK(int layerIndex)
    {
        if(Animator)
        {
            if(isActive)
            {

                //设置权重
                Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
                Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
                Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
                Animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
                //左手ik
                if (leftHandIK_state)
                {
                    Animator.SetIKPosition(AvatarIKGoal.LeftHand, gunLeftHandHome.position);
                    Animator.SetIKRotation(AvatarIKGoal.LeftHand, gunLeftHandHome.rotation);
                }
                //右手ik
                if (rightHandIK_state)
                {
                    Animator.SetIKPosition(AvatarIKGoal.RightHand, gunRightHandHome.position);
                    Animator.SetIKRotation(AvatarIKGoal.RightHand, gunRightHandHome.rotation);
                }
                //上半身ik
                if (bodyHandIK_state)
                {
                    Animator.SetLookAtWeight(1, 1, 0, 0);                  //肚子权重
                    Animator.SetLookAtPosition(bodyLookObj.position);       //看向的目标
                }

            }
            else
            {
                //重置骨骼坐标
                Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                Animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                Animator.SetLookAtWeight(0, 0, 0, 0);                  //上半身权重
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
