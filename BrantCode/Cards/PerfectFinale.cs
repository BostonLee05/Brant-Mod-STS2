using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brant.Cards;
using Brant.Hooks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Brant.Cards;

public sealed class PerfectFinale : BrantCard
{
    public PerfectFinale() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies)
    {
    }

    public override HashSet<CardKeyword> CanonicalKeywords => new HashSet<CardKeyword> { CardKeyword.Exhaust };

    protected override IEnumerable<DynamicVar> CanonicalVars => new List<DynamicVar>
    {
        new PowerVar<WeakPower>(1M),
        new PowerVar<VulnerablePower>(1M),
        new CardsVar(1)
    };

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            return new IHoverTip[]
            {
                new HoverTip(new LocString("keywords", "BRANT-ER.title"), new LocString("keywords", "BRANT-ER.description"))
            };
        }
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (this.CombatState != null)
        {
            foreach (var enemy in this.CombatState.Enemies)
            {
                if (enemy.IsAlive)
                {
                    await PowerCmd.Apply<WeakPower>(choiceContext, enemy, this.DynamicVars.Weak.BaseValue, this.Owner.Creature, this);
                    await PowerCmd.Apply<VulnerablePower>(choiceContext, enemy, this.DynamicVars.Vulnerable.BaseValue, this.Owner.Creature, this);
                }
            }
        }
        ERSystem.RemoveER(this.Owner?.PlayerCombatState, 10);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Weak.UpgradeValueBy(1M);
        this.DynamicVars.Vulnerable.UpgradeValueBy(1M);
    }
}
