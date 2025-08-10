using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyColorData", menuName = "EnemyColorData")]
public class EnemyColor : ScriptableObject
{
    public WizardColor color;
    public Sprite sprite;

}
