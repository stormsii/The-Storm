using UnityEngine;

public enum SpellType { Fire, Shock, Frost }

[CreateAssetMenu(fileName = "New Spell", menuName = "Spells/Spell")]
public class Spell : ScriptableObject
{

    [Header("Basic Info")]
    public string spellName;
    [TextArea] public string description;
    public Sprite spellImage;
    public SpellType type;
    public int minLevel;

    [Header("Stats")]
    public float powerCost;
    public float castTime;
    public float cooldown;
    public float range;
    public float damage;

    public bool hasProjectile;
    [ShowIf("hasProjectile")] public float projectileSpeed;

    [Header("FX")]
    [ShowIf("hasProjectile")] public GameObject projectilePrefab;
    public GameObject castEffectPrefab;
    public GameObject hitEffectPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
