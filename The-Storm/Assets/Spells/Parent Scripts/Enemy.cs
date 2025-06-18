using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Regular Enemy")]
public class Enemies : ScriptableObject
{

    public AnimationClip walkingAnimation;
    public AnimationClip damageAnimation;
    public AnimationClip dodgeAnimation;
    public AnimationClip deathAnimation;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void WalkingAnimation()
    {

    }

    void DamagedAnimation()
    {

    }

    void DodgeAnimation()
    {

    }

    void DeathAnimation()
    {

    }
}