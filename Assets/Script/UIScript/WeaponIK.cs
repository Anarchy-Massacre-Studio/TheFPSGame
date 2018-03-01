using UnityEngine;
using DG.Tweening;

public class WeaponIK : MonoBehaviour
{
    public WeaponPosition weaponPosition;

    public bool isActive = false;

    public Transform BackRefer;
    public Transform ForwardRefer;

    public GameObject FirstWeapon;
    public GameObject SecondWeapon;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        //FirstWeapon = PlayerData.FirstWeapon;
        //SecondWeapon = PlayerData.SecondWeapon;
    }

    private void Update()
    {
        if(!isActive)
        {
            FirstWeapon.transform.position = weaponPosition.Long.position;
            FirstWeapon.transform.rotation = weaponPosition.Long.rotation;
        }
        SecondWeapon.transform.position = weaponPosition.Short.position;
        SecondWeapon.transform.rotation = weaponPosition.Short.rotation;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(animator)
        {
            if (isActive)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

                animator.SetIKPosition(AvatarIKGoal.LeftHand, ForwardRefer.position);
                animator.SetIKPosition(AvatarIKGoal.RightHand, BackRefer.position);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, ForwardRefer.rotation);
                animator.SetIKRotation(AvatarIKGoal.RightHand, BackRefer.rotation);
            }
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
            }
        }
    }
}
