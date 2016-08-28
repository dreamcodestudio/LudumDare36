using UnityEngine;
using System.Collections;

public class EnergyPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            var playerCom = target.GetComponentInParent<Player>();
            playerCom.unitEnergy.TakeEnergy(1);
            GameThreadManager.Instance.availablePickEnergies.Remove(this);
            gameObject.SetActive(false);
        }
    }
}
