using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    int r = 0;
    bool _isAttackEnd = false;
    private void Update()
    {
        if(_isAttackEnd)
        {
            r = Random.Range(0, 4);
        }
        if (r == 0)
        {
            StartCoroutine(Attack(5));
        }
    }

    IEnumerator Attack(float time)
    {
        _isAttackEnd = false;
        yield return new WaitForSeconds(time);
        _isAttackEnd = true;
    }
}
