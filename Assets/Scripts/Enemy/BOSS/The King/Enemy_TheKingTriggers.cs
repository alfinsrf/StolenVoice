using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_TheKingTriggers : Enemy_AnimationTriggers
{
    private Enemy_TheKing enemyTheKing => GetComponentInParent<Enemy_TheKing>();

    private void Relocate() => enemyTheKing.FindPosition();

    private void MakeInvisible() => enemyTheKing.fx.MakeTransparent(true);
    private void MakeVisible() => enemyTheKing.fx.MakeTransparent(false);

    private void AttackSound() => AudioManager.instance.PlaySFX(1, enemyTheKing.transform);
}
