using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Fogueira : MonoBehaviour
{
    public GameObject[] enemyPreFabList;
    public GameObject[] enemySpawns;
    List<GameObject> enemyInstanceList = new List<GameObject>();

    Transform player;
    private int _level;
    private int _oldLevel;
    public bool canUseFireplace;
    EntityStats playerStats;

    // Text Interaction
    public GameObject textInteraction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _oldLevel = player.gameObject.GetComponent<EntityStats>().level;
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>();
        Spawn();
    }

    void Update()
    {
        if(player != null) GetDistance();

        ShowInteraction();
    }


    public void Interact()
    {
        Spawn();
        RestoreHP();
        LevelUpScreen(); //Testar em casa
    }

    private void LevelUpScreen()
    {
        _level = player.gameObject.GetComponent<EntityStats>().level;
        if(_level != _oldLevel) _oldLevel = _level;
        
    }

    private void Spawn()
    {
        // Se ja existir inimigos spawnados, eles serao destruidos antes de spawnar novos
        if(enemyInstanceList != null) 
        {
            enemyInstanceList.Clear();
            GameObject[] enemiesToDestroy = GameObject.FindGameObjectsWithTag("Enemy");

            foreach(GameObject enemy in enemiesToDestroy) Destroy(enemy);
        }

        // Instanciando todos os prefabs de inimigos e colocando eles em uma nova lista
        int i = 0;
        foreach(GameObject enemy in enemyPreFabList)
        {
            GameObject enemyInstance = Instantiate(enemy, enemySpawns[i].transform.position, Quaternion.identity);
            i++;
            enemyInstanceList.Add(enemyInstance);
        }
    }

    private void RestoreHP()
    {
        playerStats.hp = playerStats.maxHp;
    }

    void GetDistance()
    {
        Vector2 myDist = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 playerDist = new Vector2(player.transform.position.x, player.transform.position.y);

        float dist = Vector2.Distance(myDist, playerDist);
        
        canUseFireplace = dist <= 0.615f;
        HUD.Instance.distFogPlayer = canUseFireplace;
    }

    void ShowInteraction()
    {
        if(canUseFireplace)
        {
            textInteraction.SetActive(true);
            return;
        }

        textInteraction.SetActive(false);    
    }

}
