using UnityEngine;
using System.Collections;
using System;

public class Casting : MonoBehaviour
{

    [Header("Components")]
    private PlayerStats _ps;
    private Targeting _t;
    private Outputter _o;
    private CastbarScript _cb;

    [Header("Variables")]
    [SerializeField] private bool isCasting; public bool IsCasting { get => isCasting; }
    [SerializeField] private float currentCastingTime; public float CurrentCastingTime { get => currentCastingTime; }
    [SerializeField] private float castingTime; public float CastingTime { get => castingTime; }
    [SerializeField] private Spell currentSpell; public Spell CurrentSpell { get => currentSpell; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _t = GetComponent<Targeting>();
        _ps = GetComponent<PlayerStats>();
        _o = GetComponent<Outputter>();
        _cb = GetComponentInChildren<CastbarScript>(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cast(Spell spell)
    {
        if (isCasting){ return; }
        if (_t.CurrentTarget == null ) { return; }

        currentSpell = Instantiate(spell);
        if (currentSpell.castTime > 0 && StartCast(currentSpell))
        {
            isCasting = true;
            castingTime = currentSpell.castTime;
            _cb.ToggleCastbar();
            StartCoroutine(MidCast(spell));
        }
    }

    void EndCast(Spell spell)
    {
        _cb.ToggleCastbar();
        isCasting = false;
        currentCastingTime = 0;
        castingTime = 0;

        if (_t == null)
        {
            Debug.Log("t null");
            return;
        }
        
        if (_t.CurrentTarget.gameObject == null)
        {
            Debug.Log("current target game object");
            return;
        }

        if (spell == null)
        {
            Debug.Log("spell object");
            return;
        }

        if (spell.hasProjectile)
        {
            GameObject projectile = Instantiate(spell.projectilePrefab, transform.position, Quaternion.identity);
            Projectile proj = projectile.GetComponent<Projectile>();
            proj.Initialize(spell, _t.CurrentTarget.gameObject, _o);
        }
        else
        {
            HealDamage(spell);
        }
    }

    private void HealDamage(Spell spell)
    {
        _o.ChangeHealth(_t.CurrentTarget.gameObject, spell);
    }

    private bool StartCast(Spell spell)
    {
        Debug.Log("StartCast good!");
        return true;
    }

    public IEnumerator MidCast(Spell spell)
    {
        currentCastingTime = 0f;
        castingTime = spell.castTime;

        while (currentCastingTime < castingTime)
        {
            currentCastingTime += Time.deltaTime;

            yield return null; // wait until next frame
        }

        EndCast(spell);
    }
}
