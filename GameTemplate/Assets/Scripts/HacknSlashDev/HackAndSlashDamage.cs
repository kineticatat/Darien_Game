using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HackAndSlashDamage : MonoBehaviour
{
    Health health;
    public Transform spawnPoint;
    public GameObject textObject;
    private void Start()
    {
        health = GetComponent<Health>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            Attack_Controller playerDamage = collision.gameObject.GetComponentInParent<Attack_Controller>();

                
            if (playerDamage != null && playerDamage.isAttacking && playerDamage.allWeaponTypes[playerDamage.currentWeaponIndex].DamageTarget(health))
            {
                health.TakeDamage(playerDamage.currentWeaponDamage);


                GameObject textDamagePopup = Instantiate(textObject, spawnPoint);
                textDamagePopup.GetComponentInChildren<Text>().text = playerDamage.currentWeaponDamage.ToString();
                textDamagePopup.GetComponent<SingleParticleEject>().EjectParticle();
                textDamagePopup.transform.localScale = Vector3.one * playerDamage.currentWeaponDamage / 20f;

            }
        }
    }
}
