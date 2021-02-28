using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationPing : MonoBehaviour
{
    Movement_Controller movement_Controller;
    Attack_Controller attack_Controller;
    private void Start()
    {
        movement_Controller = Movement_Controller.instance;
        attack_Controller = Attack_Controller.instance;
    }

    public void EndRoll()
    {
        movement_Controller.EndRoll();
    }

    public void Jump()
    {
        movement_Controller.Jump();
    }

    public void SetMeshHeight(float height)
    {
        movement_Controller.SetMeshHeight(height);
    }

    public void EndAttack(int proficiencyCheck)
    {
        attack_Controller.CheckAttackContinue(proficiencyCheck);
    }

}
