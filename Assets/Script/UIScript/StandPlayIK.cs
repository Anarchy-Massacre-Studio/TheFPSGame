using UnityEngine;

public class StandPlayIK : MonoBehaviour
{
    public Transform RightRefer;
    public Transform LeftRefer;
    public Transform LookAtRefer;

    Animator animator;

	void Start ()
    {
        animator = GetComponent<Animator>();
	}

    private void OnAnimatorIK(int layerIndex)
    {
        if(animator)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.9f);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.9f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.9f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.9f);

            animator.SetIKPosition(AvatarIKGoal.RightHand, RightRefer.position);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftRefer.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, RightRefer.rotation);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, LeftRefer.rotation);

            animator.SetLookAtWeight(1f);
            animator.SetLookAtPosition(LookAtRefer.position);
        }
    }
}
