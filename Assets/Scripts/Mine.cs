using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Mine : MonoBehaviour
{
    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            var playerCom = target.GetComponentInParent<Player>();
            playerCom.unitEnergy.TakeEnergy(-100);

            GameThreadManager.Instance.uiHudManadger.userMsg.text = "Be careful";
            GameThreadManager.Instance.uiHudManadger.userMsg.DOFade(2f, 1f);
        }
        else if(target.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            var enemyCom = target.GetComponentInParent<Enemy>();
            enemyCom.unitEnergy.TakeEnergy(-100);
        }
    }
}
