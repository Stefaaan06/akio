using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class playerGun : MonoBehaviour
{
    [Header("Gun Logic")]
    public gun[] playerGuns = new gun[0];
    private int _currentGun = -1;
    public int maxGuns = 3;

    [Header("References")]
    public Camera playerCamera;
    public Transform cameraTransform;
    public Rigidbody2D playerRigidbody;
    public TMP_Text ammoText;
    public TMP_Text gunNameText;
    public LayerMask hitLayer;
    
    private GameObject[] _gunObjects = new GameObject[0];
    private float _lastShotTime;

    public GameObject debug;

    private void Update()
    {
        RotateGunTowardsMouse();
        Shoot();

        for (int i = 1; i <= 3; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                EquipGun(i - 1);
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
        {
            _currentGun += scroll > 0 ? 1 : -1;
            if (_currentGun >= playerGuns.Length)
            {
                _currentGun = 0;
            }
            else if (_currentGun < 0)
            {
                _currentGun = playerGuns.Length - 1;
            }
            EquipGun(_currentGun);
        }
    }

    private void RotateGunTowardsMouse()
    {
        if (_currentGun < 0 || _currentGun >= playerGuns.Length) return;

        Vector3 mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; 

        Vector3 direction = mousePosition - _gunObjects[_currentGun].transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //flip gun so its not upside down
        if (angle > 90 || angle < -90)
        {
            _gunObjects[_currentGun].transform.localScale = new Vector3(-1, 1, 1);
            angle += 180;
        }
        else
        {
            _gunObjects[_currentGun].transform.localScale = new Vector3(1, 1, 1);
        }

        _gunObjects[_currentGun].transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void AddGun(gun newGun)
    {
        if (playerGuns.Length >= maxGuns)
        {
            RemoveGun(_currentGun);
        }

        if (playerGuns.Contains(newGun)) return;

        Array.Resize(ref playerGuns, playerGuns.Length + 1);
        playerGuns[playerGuns.Length - 1] = newGun;

        Array.Resize(ref _gunObjects, _gunObjects.Length + 1);
        _gunObjects[_gunObjects.Length - 1] = Instantiate(newGun.gunPrefab, transform.position, transform.rotation, this.transform);
        _gunObjects[_gunObjects.Length - 1].layer = 9;  // Ensure the gun is on the correct layer
        foreach (Transform child in _gunObjects[_gunObjects.Length - 1].GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = 9;  // Ensure child objects are also on the correct layer
        }

        newGun.currentClipSize = newGun.maxClipSize;

        EquipGun(playerGuns.Length - 1);
    }

    void RemoveGun(int gunIndex)
    {
        if (gunIndex < 0 || gunIndex >= playerGuns.Length) return;

        gun[] newGuns = new gun[playerGuns.Length - 1];
        GameObject[] newGunObjects = new GameObject[_gunObjects.Length - 1];

        int newIndex = 0;
        for (int i = 0; i < playerGuns.Length; i++)
        {
            if (i == gunIndex) continue;

            newGuns[newIndex] = playerGuns[i];
            newGunObjects[newIndex] = _gunObjects[i];
            newIndex++;
        }

        playerGuns = newGuns;
        _gunObjects = newGunObjects;
    }

    public void EquipGun(int gunIndex)
    {
        if (gunIndex < 0 || gunIndex >= playerGuns.Length) return;

        foreach (GameObject gun in _gunObjects)
        {
            gun.SetActive(false);
        }
        _gunObjects[gunIndex].SetActive(true);
        _currentGun = gunIndex;

        UpdateNameText();
        UpdateAmmoText();

        StopAllCoroutines();
    }

    void Shoot()
    {
        if (this._currentGun < 0 || this._currentGun >= playerGuns.Length) return;

        if (Time.time - _lastShotTime < 1f / playerGuns[this._currentGun].fireRate) return;

        gun _currentGun = playerGuns[this._currentGun];

        if (_currentGun.allowAutoFire)
        {
            if (!Input.GetMouseButton(0)) return;
        }
        else if (!Input.GetMouseButtonDown(0)) return;

        if (_currentGun.currentClipSize <= 0) return;
        _currentGun.currentClipSize--;
        UpdateAmmoText();

        Debug.Log("shoot");

        Vector3 mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - _gunObjects[this._currentGun].transform.position).normalized;


        RaycastHit2D hit = Physics2D.Raycast(_gunObjects[this._currentGun].transform.position, direction, playerGuns[this._currentGun].range, hitLayer);
        if (hit.collider != null)
        {
            playerRigidbody.AddForce(-direction * (_currentGun.feedback * 2), ForceMode2D.Impulse);
            Instantiate(debug, hit.point, transform.rotation);
            health hitHealth;
            if (hit.transform.TryGetComponent<health>(out hitHealth))
            {
                hitHealth.removeHP(_currentGun.damage);
            }
        }
        else
        {
            playerRigidbody.AddForce(-direction * _currentGun.feedback, ForceMode2D.Impulse);
        }

        _lastShotTime = Time.time;
    }

    public IEnumerator Reload()
    {
        if (this._currentGun < 0 || this._currentGun >= playerGuns.Length) yield break;

        gun _currentGun = playerGuns[this._currentGun];

        UpdateAmmoReloadText();

        yield return new WaitForSeconds(_currentGun.reloadTime);

        _currentGun.currentClipSize = _currentGun.maxClipSize;

        UpdateAmmoText();
    }

    void UpdateAmmoText()
    {
        if (this._currentGun < 0 || this._currentGun >= playerGuns.Length) return;

        gun _currentGun = playerGuns[this._currentGun];

        ammoText.text = _currentGun.currentClipSize + "/" + _currentGun.maxClipSize;
    }

    void UpdateAmmoReloadText()
    {
        if (this._currentGun < 0 || this._currentGun >= playerGuns.Length) return;

        gun _currentGun = playerGuns[this._currentGun];

        ammoText.text = _currentGun.currentClipSize + "/" + _currentGun.maxClipSize + " (R)";
    }

    void UpdateNameText()
    {
        if (this._currentGun < 0 || this._currentGun >= playerGuns.Length) return;

        gun _currentGun = playerGuns[this._currentGun];

        gunNameText.text = _currentGun.gunName;
    }
}
