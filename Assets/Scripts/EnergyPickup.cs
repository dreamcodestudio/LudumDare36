using UnityEngine;
using System.Collections;

public class EnergyPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            var playerCom = target.GetComponentInParent<Player>();
            playerCom.unitEnergy.TakeEnergy(1, true);
            GameThreadManager.Instance.availablePickEnergies.Remove(this);
            if (GameThreadManager.Instance.availablePickEnergies.Count == 1)
            {
                for (var i = 0; i < 2; i++)
                {
                    GameThreadManager.Instance.SpawnEnergyRandom();
                }
            }
            gameObject.SetActive(false);
        }
    }
}
