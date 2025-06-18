using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.GridLayoutGroup;

public class Projectile : MonoBehaviour
{
    private Spell spell;
    private Vector3 spawnPos;
    private Outputter outputter;
    private GameObject target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize(Spell currentSpell, GameObject targetT, Outputter o)
    {
        spell = currentSpell;
        target = targetT;
        outputter = o;
        spawnPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = target.transform.position;
        Vector3 direction = (targetPos - transform.position).normalized;
        transform.position += direction * spell.projectileSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.transform.position) < 0.5f)
        {
            var damageable = target.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.ChangeHealth((int)spell.damage);
            }

            Destroy(gameObject);
        }
    }
}
