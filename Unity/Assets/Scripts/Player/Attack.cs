using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float range;
    public int Damage;
    public float power;
    public Animator animator;
    public float nextHit;
    public float attackRate = 0.4f;
    public AudioSource woosh;
    public bool Prot;
    public Movement movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (GameObject.Find("Options").GetComponent<OptionsEnJeu>().menuOpen)
        {
            return;
        }

        if (Time.time > nextHit && Input.GetButtonDown("Fire2") && !movement.torched) 
        {
            woosh.Play();
            nextHit = Time.time + attackRate;
            Collider[] enemies = Physics.OverlapSphere(transform.position, range);
            int tmp = Damage;
            if (GetComponentInParent<CharacterThings>().dard)
            {
                tmp = 9999;
            }
            if (GetComponentInParent<CharacterThings>().Souffrance)
            {
                tmp += GetComponentInParent<CharacterThings>().MaxHP - GetComponentInParent<CharacterThings>().HP;
            }
            if (GetComponentInParent<CharacterThings>().killer)
            {
                tmp *= 3;
            }

            if (GetComponentInParent<CharacterThings>().bloodLove)
            {
                tmp *= 2;
            }
            for (int i = 0; i < enemies.Length; i++)
            {
                // enemies[i].GetComponent<ShootingAI>().TakeDamage(explosionDamage);
                if (enemies[i].GetComponent<Rigidbody>() && !enemies[i].CompareTag("Player") && !enemies[i].CompareTag("Ennemy"))
                    enemies[i].GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, range);
                if (enemies[i].CompareTag("Ennemy") && enemies[i].TryGetComponent(typeof(Rigidbody), out _))
                {
                    if (Prot)
                    {
                        enemies[i].attachedRigidbody.AddExplosionForce(4000f, transform.position, range);
                    }
                    EnnemyLife IA = enemies[i].GetComponent<EnnemyLife>();
                    IA.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, tmp, GetComponentInParent<CharacterThings>().toile);
                }
                if (enemies[i].CompareTag("Player"))
                {
                    CharacterThings victim = enemies[i].GetComponent<CharacterThings>();
                    
                    if (victim != enemies[i].GetComponentInParent<CharacterThings>())
                    {
                        if (Prot)
                        {
                            enemies[i].attachedRigidbody.AddExplosionForce(4000f, transform.position, range);
                        }
                        victim.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All,GetComponentInParent<CharacterThings>().toile, GetComponentInParent<CharacterThings>().purulence);
                    }
                    
                    
                }
            }
        }
    }
}
