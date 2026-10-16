using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brant.Cards;
using Brant.Hooks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Brant.Cards.Basics;

public class ToTheHorizonPower : TemporaryStrengthPower
{
    public override AbstractModel OriginModel => ModelDb.Card<ToTheHorizon>();

    public ToTheHorizonPower() : base()
    {
    }
}

public sealed class ToTheHorizon : BrantCard
{
    public ToTheHorizon() : base(2, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
    {
    }

    protected override IEnumerable<DynamicVar> CanonicalVars => new List<DynamicVar>
    {
        new DamageVar(10M, ValueProp.Move),
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
        ArgumentNullException.ThrowIfNull(cardPlay.Target, nameof(cardPlay.Target));
        int er = ERSystem.GetER(this.Owner.PlayerCombatState);
        int damage = er / 10;
        await DamageCmd.Attack((decimal)damage).FromCard(this).Targeting(cardPlay.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        
        int strength = er / 50;
        if (strength > 0)
        {
            await PowerCmd.Apply<ToTheHorizonPower>(choiceContext, this.Owner.Creature, (decimal)strength, this.Owner.Creature, this);
        }

        ERSystem.SetToTheHorizonActive(this.Owner.PlayerCombatState, true);
    }
}