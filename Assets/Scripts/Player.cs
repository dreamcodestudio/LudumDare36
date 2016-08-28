using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public EnergyAgent energyAgent;
    public List<CosmicEnergy> LastCosmicEnergies = new List<CosmicEnergy>();
    public PlayerEnergy unitEnergy;

    void Start()
    {
        transform.DOScale(Vector3.one, 2f);
    }
}
