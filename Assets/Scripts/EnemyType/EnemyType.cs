using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTypeData", menuName = "EnemyTypeData")]
public class EnemyType : ScriptableObject
{
    public AnimatorOverrideController blueAnimator;
    public AnimatorOverrideController redAnimator;
    public AnimatorOverrideController yellowAnimator;
    public AnimatorOverrideController greenAnimator;
    public AnimatorOverrideController orangeAnimator;
    public AnimatorOverrideController purpleAnimator;

    public float minSpeed;
    public float maxSpeed;

    public int pointsWorth;
    public int hitsRequired;

    public Vector2 colliderOffset; // 1: 0.03/-0.03     2:  0/0     3: -0.02/-0.15
    public Vector2 colliderSize; //   1: 0.56/0.56      2: 0.63/11  3: 1.18/1.68
}