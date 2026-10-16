using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brant.Cards;
using Brant.Hooks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Brant.Cards;

public sealed class FinalBow : BrantCard
{
    public FinalBow() : base(2, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
    {
    }

    public override bool GainsBlock => true;

    protected override IEnumerable<DynamicVar> CanonicalVars => new List<DynamicVar>
    {
        new DamageVar(35M, ValueProp.Move),
        new BlockVar(20M, ValueProp.Move),
        new CardsVar(2)
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
        int er = ERSystem.GetER(this.Owner?.PlayerCombatState);
        if (er >= 200)
        {
            if (this.CombatState != null)
            {
                await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(this.CombatState).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
            }
            await CreatureCmd.GainBlock(this.Owner.Creature, this.DynamicVars.Block, cardPlay);
            CombatManager.Instance.SetReadyToEndTurn(this.Owner, false);
        }
        else
        {
            ERSystem.AddER(this.Owner.PlayerCombatState, 15);
            await CreatureCmd.GainBlock(this.Owner.Creature, 12M, ValueProp.Move, cardPlay);
        }
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(10M);
        this.DynamicVars.Block.UpgradeValueBy(5M);
    }
}
