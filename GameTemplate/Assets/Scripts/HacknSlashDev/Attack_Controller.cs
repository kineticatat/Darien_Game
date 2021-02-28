using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Controller : MonoBehaviour
{
    #region Singleton

    public static Attack_Controller instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public bool canAttack;
    public bool isAttacking;

    public int currentWeaponIndex;
    private Animator animator;
    private Movement_Controller movement;

    public bool didChainAttacks;

    public WeaponType[] allWeaponTypes;

    public MeshFilter weaponMesh;
    public MeshCollider weaponCollider;
    public GameObject duelSword;

    public int currentWeaponDamage;

    public Transform currentTarget;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<Movement_Controller>();

        canAttack = true;
        EquipWeapon(1);
    }

    private void Update()
    {
        CheckAttack();
        CheckWeaponSelection();

    }

    private void CheckAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canAttack)
            {
                StartAttack();
            }
            else if (isAttacking)
            {
                didChainAttacks = true;
            }
        } 
    }

    private void CheckWeaponSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(01);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipWeapon(02);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EquipWeapon(03);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            EquipWeapon(04);

        }
    }

    public void EquipWeapon(int weaponTypeIndex)
    {
        currentWeaponIndex = weaponTypeIndex;
        animator.SetInteger("EquipedWeapon", weaponTypeIndex);

        WeaponType newWeapon = allWeaponTypes[currentWeaponIndex];

        weaponMesh.sharedMesh = newWeapon.weaponMesh;
        weaponCollider.sharedMesh = newWeapon.weaponMesh;

        weaponMesh.transform.localPosition = newWeapon.locationOffset;
        weaponMesh.transform.localEulerAngles = newWeapon.EulerRotationOffset;
        weaponMesh.transform.localScale = newWeapon.localScale;

        currentWeaponDamage = newWeapon.weaponDamage;

        if (allWeaponTypes[currentWeaponIndex].doesDuelWeild)
        {
            duelSword.SetActive(true);
        }
        else
        {
            duelSword.SetActive(false);
        }
    }

    public void StartAttack()
    {
        animator.SetTrigger("Attack");
        canAttack = false;
        isAttacking = true;
    }

    public void CheckAttackContinue(int proficiencyCheck)
    {
        allWeaponTypes[currentWeaponIndex].ResetSwing();

        if (proficiencyCheck > allWeaponTypes[currentWeaponIndex].playerProficiency)
        {
            animator.SetTrigger("AttackStop");
            canAttack = true;
            didChainAttacks = false;
            isAttacking = false;
        }
        else
        {
            if (!didChainAttacks)
            {
                animator.SetTrigger("AttackStop");
                canAttack = true;
                isAttacking = false;
            }
            else
            {
                didChainAttacks = false;
            }
        }
    }
}

[System.Serializable]
public class WeaponType{
    public string name;
    public bool doesDuelWeild;
    public float playerProficiency;
    public Mesh weaponMesh;
    public Vector3 locationOffset;
    public Vector3 EulerRotationOffset;
    public Vector3 localScale;
    public int weaponDamage;

    public List<Health> targetsDamagedThisSwing = new List<Health>();

    public bool DamageTarget(Health target)
    {
        if (targetsDamagedThisSwing.Contains(target))
        {
            return false;
        }

        targetsDamagedThisSwing.Add(target);
        return true;
    }

    public void ResetSwing()
    {
        targetsDamagedThisSwing.Clear();
    }
}
