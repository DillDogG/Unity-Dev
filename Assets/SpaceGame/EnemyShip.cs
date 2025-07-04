using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Enemy, IDamagable
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private float minFireRate;
    [SerializeField] private float maxFireRate;

    private void Start()
    {
        weapon.Equip();
        StartCoroutine(FireTimerCR());
    }

    private void Update()
    {
        
    }

    IEnumerator FireTimerCR()
    {
        float time = Random.Range(minFireRate, maxFireRate);
        yield return new WaitForSeconds(time);
        weapon.Use();
        StartCoroutine(FireTimerCR());
    }
}
