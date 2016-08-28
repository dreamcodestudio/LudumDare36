using UnityEngine;
using UnityEngine.UI;
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
    private MeshRenderer _playerMesh;
    private AudioSource _audioSource;

    #region UNITY_EVENTS

    private void Awake()
    {
        _explosionParticels = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
        _explosionParticels.gameObject.SetActive(false);
        _player = GetComponent<Player>();
        _playerMesh = GetComponentInChildren<MeshRenderer>();
        _audioSource = GetComponent<AudioSource>();
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

    public void TakeEnergy(int amount, bool energyPickup = false)
    {
        if (_isDead)
            return;
        if (energyPickup)
        {
            _audioSource.PlayOneShot(_player.pickupAudio);
        }
        _currenEnergy += amount;
        if (_currenEnergy <= 0f)
        {
            GameThreadManager.Instance.uiHudManadger.userMsg.text = "Don't waste all energy";
            GameThreadManager.Instance.uiHudManadger.userMsg.DOFade(2f, 1f);
            StartCoroutine(OnDeath());
        }
        else if (_currenEnergy > maxEnergy)
        {
            GameThreadManager.Instance.uiHudManadger.userMsg.text = "You are overflowing energy";
            GameThreadManager.Instance.uiHudManadger.userMsg.DOFade(2f, 1f);
            StartCoroutine(OnDeath());
        }
        GameThreadManager.Instance.uiHudManadger.playerEnergyLabel.text = "Energy: " + _currenEnergy;
    }

    private IEnumerator OnDeath(float delay = 0.5f)
    {
        _isDead = true;
        _audioSource.PlayOneShot(_player.explosionAudio);
        _explosionParticels.transform.position = transform.position;
        _explosionParticels.gameObject.SetActive(true);
        _playerMesh.enabled = false;
        gameCam.GetComponent<CameraController>().enabled = false;
        gameCam.DOShakePosition(0.7f, new Vector3(4f, 0f, 3f));
        DOTween.To(value => GameThreadManager.Instance.vignetteEffect.intensity = value, 0.03f, 0.25f, 1f);
        DOTween.To(value => GameThreadManager.Instance.vignetteEffect.blur = value, 0.4f, 1f, 1f);
        GameThreadManager.Instance.uiHudManadger.restartBtn.gameObject.SetActive(true);
        DOTween.To(value => GameThreadManager.Instance.uiHudManadger.restartBtn.GetComponent<CanvasGroup>().alpha = value, 0.0f, 1f, 1.25f);
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
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
        _audioSource.PlayOneShot(_player.mineInstallAudio);
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
