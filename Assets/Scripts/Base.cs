using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Base : MonoBehaviour
{
    public EnergyAgent energyAgent;
    public UnityEngine.UI.Slider energySlider;

    private int _currentEnergy;

    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player on base");
            var playerCom = target.GetComponentInParent<Player>();
            for (var i = 0; i < playerCom.LastCosmicEnergies.Count; i++)
            {
                playerCom.LastCosmicEnergies[i].SetTargetEnergyAgent(energyAgent);
                TakeEnergy(10);
                playerCom.unitEnergy.TakeEnergy(-1);
            }
            playerCom.LastCosmicEnergies.Clear();
        }
    }

    public void TakeEnergy(int amount)
    {
        _currentEnergy += amount;
        energySlider.DOValue(_currentEnergy, 0.5f);
    }
}
