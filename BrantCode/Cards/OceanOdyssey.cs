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
using MegaCrit.Sts2.Core.ValueProps;

namespace Brant.Cards;

public sealed class OceanOdyssey : BrantCard
{
    public OceanOdyssey() : base(0, CardType.Attack, CardRarity.Quest, TargetType.AnyEnemy)
    {
    }

    public override bool GainsBlock => true;

    public override HashSet<CardKeyword> CanonicalKeywords => new HashSet<CardKeyword> { CardKeyword.Ethereal, CardKeyword.Exhaust };

    protected override IEnumerable<DynamicVar> CanonicalVars => new List<DynamicVar> { new DamageVar(10M, ValueProp.Move), new BlockVar(5M, ValueProp.Move), new CardsVar(0) };

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

    protected override bool IsPlayable
    {
        get
        {
            // Cannot be used when gauge not full (max 4)
            int gauge = ERSystem.GetGauge(this.Owner?.PlayerCombatState);
            return gauge >= ERSystem.MaxGauge;
        }
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, nameof(cardPlay.Target));
        int er = ERSystem.GetER(this.Owner.PlayerCombatState);
        int damage = er / 10;
        int block = er / 20;

        await DamageCmd.Attack((decimal)damage).FromCard(this).Targeting(cardPlay.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        if (block > 0)
        {
            await CreatureCmd.GainBlock(this.Owner.Creature, new BlockVar((decimal)block, ValueProp.Move), cardPlay);
        }
        
        // Reset gauge after using Ocean Odyssey
        ERSystem.SetGauge(this.Owner.PlayerCombatState, 0);
    }
}
