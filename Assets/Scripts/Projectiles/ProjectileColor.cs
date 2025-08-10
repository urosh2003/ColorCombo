using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileColorData", menuName = "ProjectileColorData")]
public class ProjectileColor : ScriptableObject
{
    public Sprite sprite;
    public WizardColor color;
}