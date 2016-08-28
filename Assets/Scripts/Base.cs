using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class Base : MonoBehaviour
{
    public EnergyAgent energyAgent;
    public UnityEngine.UI.Slider energySlider;
    public GameObject cosmicEnergyGo;
    public CameraController camController;
    [HideInInspector]
    public List<CosmicEnergy> LastCosmicEnergies = new List<CosmicEnergy>();

    private int _currentEnergy;
    private bool _isDead;

    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player on base");
            var playerCom = target.GetComponentInParent<Player>();
            for (var i = 0; i < playerCom.LastCosmicEnergies.Count; i++)
            {
                playerCom.LastCosmicEnergies[i].SetTargetEnergyAgent(energyAgent);
                TakeEnergy(1);
                playerCom.unitEnergy.TakeEnergy(-1); 
                LastCosmicEnergies.Add(playerCom.LastCosmicEnergies[i]);
            }
            playerCom.LastCosmicEnergies.Clear();
        }
        else if (target.gameObject.CompareTag("Enemy"))
        {
            var enemyCom = target.GetComponentInParent<Enemy>();
            enemyCom.unitEnergy.TakeEnergy(-100);
            TakeEnergy(-1);
            if (LastCosmicEnergies.Count > 0)
            {
                LastCosmicEnergies[0].SetTargetEnergyAgent(LastCosmicEnergies[0].sourceEnergyAgent);
                LastCosmicEnergies.RemoveAt(0);
            }
        }
    }

    public void TakeEnergy(int amount)
    {
        if (_isDead)
            return;
        _currentEnergy += amount;
        if (_currentEnergy <= 0)
        {
            GameThreadManager.Instance.uiHudManadger.userMsg.text = "Don't forget protect base";
            GameThreadManager.Instance.uiHudManadger.userMsg.DOFade(2f, 1f);
            OnDeath();
        }
        else
        {
            if (_currentEnergy >= 10)
            {
                GameThreadManager.Instance.uiHudManadger.userMsg.text = "You win, to be continued...";
                GameThreadManager.Instance.uiHudManadger.userMsg.DOFade(2f, 1f);
                Camera.main.DOOrthoSize(100f, 15f);
            }
        }
        energySlider.DOValue(_currentEnergy * 10f, 0.5f);
    }

    private void OnDeath()
    {
        _isDead = true;
        camController.target = transform;
        cosmicEnergyGo.SetActive(false);
        Camera.main.DOOrthoSize(50f, 10f);
        GameThreadManager.Instance.uiHudManadger.restartBtn.gameObject.SetActive(true);
        DOTween.To(value => GameThreadManager.Instance.uiHudManadger.restartBtn.GetComponent<CanvasGroup>().alpha = value, 0.0f, 1f, 1.25f);
    }
}
