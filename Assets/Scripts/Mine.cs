using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour
{
    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player on mine");
            gameObject.SetActive(false);
            var playerCom = target.GetComponentInParent<Player>();
            playerCom.unitEnergy.TakeEnergy(-100);
        }
    }
}
