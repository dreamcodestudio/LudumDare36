using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public EnergyAgent energyAgent;
    public List<CosmicEnergy> LastCosmicEnergies = new List<CosmicEnergy>();
    public UnitEnergy unitEnergy;
}
