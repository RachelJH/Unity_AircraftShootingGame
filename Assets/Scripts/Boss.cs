using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState { MoveToAppearPoint = 0, Phase01, }

public class Boss : MonoBehaviour
{
    [SerializeField]
    private float bossAppearPoint = 2.5f;
    private BossState bossState = BossState.MoveToAppearPoint;
    private Movement2D movement2D;
    private BossWeapon bossWeapon;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();    
        bossWeapon = GetComponent<BossWeapon>();
    }

    public void ChangeState(BossState newState)
    {
        StopCoroutine(bossState.ToString());
        bossState = newState;
        StartCoroutine(bossState.ToString());
    }

    private IEnumerator MoveToAppearPoint()
    {
        movement2D.MoveTo(Vector3.down);

        while(true)
        {
            if(transform.position.y <= bossAppearPoint)
            {
                movement2D.MoveTo(Vector3.zero);
                ChangeState(BossState.Phase01);
            }
            yield return null;
        }
    }

    private IEnumerator Phase01()
    {
        bossWeapon.StartFiring(AttackType.CircleFire);

        while(true)
        {
            yield return null;
        }
    }

}
