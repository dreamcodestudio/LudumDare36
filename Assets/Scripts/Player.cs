using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public EnergyAgent energyAgent;
    [HideInInspector]
    public List<CosmicEnergy> LastCosmicEnergies = new List<CosmicEnergy>();
    public PlayerEnergy unitEnergy;
    [Header("Audio")]
    public AudioClip explosionAudio;
    public AudioClip pickupAudio;
    public AudioClip mineInstallAudio;

    void Start()
    {
        transform.DOScale(Vector3.one, 2f);
    }
}
