using UnityEngine;

interface ITargetable
{

    void OnTargeted();
    void OnUntargeted();

}

interface IDamageable
{

    public void ChangeHealth(int healthChanged);

}
