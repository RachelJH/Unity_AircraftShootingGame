using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float attackRate = 0.1f;
    private int maxAttackLevel = 3;
    private int attackLevel = 1;

    [SerializeField]
    private GameObject boomPrefab;
    private int boomCount = 3;

    public int AttackLevel
    {
        set => attackLevel = Mathf.Clamp(value, 1, maxAttackLevel);
        get => attackLevel;
    }
    public int BoomCount
    {
        set => boomCount = Mathf.Max(0, value);
        get => boomCount;   
    }

    public void StartFiring()
    {
        StartCoroutine("TryAttack");
    }
    public void StopFiring()
    {
        StopCoroutine("TryAttack");
    }

    public void StartBoom()
    {
        if(boomCount >0)
        {
            boomCount--;
            Instantiate(boomPrefab, transform.position, Quaternion.identity);
        }
    }

    private IEnumerator TryAttack()
    {
        while (true)
        {
            //Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            AttackByLevel();

            yield return new WaitForSeconds(attackRate);
        }
    }

    private void AttackByLevel()
    {
        GameObject clonProjectile = null;

        switch (attackLevel)
        {
            case 1:
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(projectilePrefab, transform.position + Vector3.left * 0.2f, Quaternion.identity);
                Instantiate(projectilePrefab, transform.position + Vector3.right * 0.2f, Quaternion.identity);
                break;
            case 3:
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                clonProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                clonProjectile.GetComponent<Movement2D>().MoveTo(new Vector3(-0.2f, 1, 0));

                clonProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                clonProjectile.GetComponent<Movement2D>().MoveTo(new Vector3(0.2f, 1, 0));
                break;
        }
        
    }
}
