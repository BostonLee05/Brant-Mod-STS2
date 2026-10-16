using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Brant.Cards.Basics;

public sealed class StrikeBrant : BrantCard
{
  public StrikeBrant()
    : base(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
  {
  }

  protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag>() { CardTag.Strike };

  protected override IEnumerable<DynamicVar> CanonicalVars => new List<DynamicVar> { new DamageVar(5M, ValueProp.Move), new CardsVar(1) };

  protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
  {
    ArgumentNullException.ThrowIfNull(cardPlay.Target, nameof(cardPlay.Target));
    await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
  }

  protected override void OnUpgrade() => this.DynamicVars.Damage.UpgradeValueBy(3M);
}
