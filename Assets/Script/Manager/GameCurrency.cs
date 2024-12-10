using System;
using UnityEngine;

[System.Serializable]
public class GameCurrency
{
    public int Gold;
    public int Dia;
    public int Acts;
    public int maxActs;

    public GameCurrency(int acts, int maxacts, int dia, int gold)
    {
        Gold = gold;
        Dia = dia;
        Acts = acts;
        maxActs = maxacts;
    }

    public void AddGold(int amount)
    {
        Gold += amount;
        Debug.Log($"Gold added: {amount}. Current Gold: {Gold}");
    }

    public bool SpendGold(int amount)
    {
        if (Gold >= amount)
        {
            Gold -= amount;
            Debug.Log($"Gold spent: {amount}. Current Gold: {Gold}");
            return true;
        }
        Debug.LogWarning("Not enough gold to spend.");
        return false;
    }

    public void AddDia(int amount)
    {
        Dia += amount;
        Debug.Log($"Dia added: {amount}. Current Dia: {Dia}");
    }

    public bool SpendDia(int amount)
    {
        if (Dia >= amount)
        {
            Dia -= amount;
            Debug.Log($"Dia spent: {amount}. Current Dia: {Dia}");
            return true;
        }
        Debug.LogWarning("Not enough dia to spend.");
        return false;
    }

    public void AddActs(int amount)
    {
        Acts += amount;
        if (Acts > maxActs)
        {
            Acts = maxActs;
            Debug.Log($"Acts set to maxActs: {maxActs}");
        }
    }

    public bool SpendActs(int amount)
    {
        if (Acts >= amount)
        {
            Acts -= amount;
            Debug.Log($"Acts spent: {amount}. Current Acts: {Acts}");
            return true;
        }
        Debug.LogWarning("Not enough acts to spend.");
        return false;
    }
}