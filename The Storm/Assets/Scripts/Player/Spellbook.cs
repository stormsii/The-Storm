using UnityEngine;
using System.Collections.Generic;

public class Spellbook : MonoBehaviour
{

    [SerializeField] private AllSpells allSpells;

    public List<Spell> knownSpells = new();

    private PlayerStats playerStats;

    void InitializeSpells()
    {
        foreach (Spell spell in allSpells.everySpell)
        {
            if (IsEligible(spell))
                knownSpells.Add(spell);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();

        InitializeSpells();
    }

    private bool IsEligible(Spell spell)
    {
        if (spell.minLevel <= playerStats.CurrentLevel)
        {
            return true;
        }
        return false;
    }
}
