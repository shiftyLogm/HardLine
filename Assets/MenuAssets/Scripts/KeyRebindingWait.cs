using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KeyRebindingWait : MonoBehaviour
{
    public static bool setRebindingKeys = true;
    [SerializeField] private GameObject screen;

    public void OnClick() => setRebindingKeys = false;

    private IEnumerator rebindEsc()
    {
        yield return new WaitForSeconds(0.1f);
        if (!screen.activeSelf) setRebindingKeys = true;
    }

    void Update()
    {
        if (!setRebindingKeys && Input.anyKeyDown) StartCoroutine(rebindEsc());
    }
}