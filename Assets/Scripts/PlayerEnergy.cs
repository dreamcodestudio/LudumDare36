using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerEnergy : MonoBehaviour
{
    public int startEnergy = 1;
    public int maxEnergy = 4;
    public GameObject explosionPrefab;
    public GameObject minePrefab;
    public Camera gameCam;

    private float _currenEnergy;
    private bool _isDead;
    private ParticleSystem _explosionParticels;
    private Player _player;

    #region UNITY_EVENTS

    private void Awake()
    {
        _explosionParticels = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
        _explosionParticels.gameObject.SetActive(false);
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        TakeEnergy(startEnergy);
        //StartCoroutine(TestDeath());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (_currenEnergy > 0)
            {
                SpawnMine();
            }
        }
    }

    #endregion

    public void TakeEnergy(int amount)
    {
        if (_isDead)
            return;
        _currenEnergy += amount;
        if (_currenEnergy <= 0f)
        {
            GameThreadManager.Instance.uiHudManadger.userMsg.text = "Don't waste all energy";
            GameThreadManager.Instance.uiHudManadger.userMsg.DOFade(2f, 1f);
            OnDeath();
        }
        else if (_currenEnergy > maxEnergy)
        {
            GameThreadManager.Instance.uiHudManadger.userMsg.text = "You are overflowing energy";
            GameThreadManager.Instance.uiHudManadger.userMsg.DOFade(2f, 1f);
            OnDeath();
        }
        GameThreadManager.Instance.uiHudManadger.playerEnergyLabel.text = "Energy: " + _currenEnergy;
    }

    private void OnDeath()
    {
        _isDead = true;
        _explosionParticels.transform.position = transform.position;
        _explosionParticels.gameObject.SetActive(true);
        gameObject.SetActive(false);
        gameCam.GetComponent<CameraController>().enabled = false;
        gameCam.DOShakePosition(0.7f, new Vector3(4f, 0f, 3f));
        DOTween.To(value => GameThreadManager.Instance.vignetteEffect.intensity = value, 0.03f, 0.25f, 2f);
        DOTween.To(value => GameThreadManager.Instance.vignetteEffect.blur = value, 0.4f, 1f, 2f);
    }

    private void SpawnMine()
    {
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray rayToMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        float dist;
        plane.Raycast(rayToMouse, out dist);
        Vector3 targetPos = rayToMouse.GetPoint(dist);
        var mine = Instantiate(minePrefab, targetPos, Quaternion.identity);
        TakeEnergy(-1);
        if (_player.LastCosmicEnergies.Count > 0)
        {
            _player.LastCosmicEnergies[0].SetTargetEnergyAgent(_player.LastCosmicEnergies[0].sourceEnergyAgent);
            _player.LastCosmicEnergies.RemoveAt(0);
        }
    }

    private IEnumerator TestDeath()
    {
        yield return new WaitForSeconds(2.0f);
        TakeEnergy(-100);
    }
}
