using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Relics;
using Brant.Character;

namespace Brant.Relics;

[Pool(typeof(BrantRelicPool))]
public abstract class BrantRelic : CustomRelicModel
{
    // Implementation of relic icons/paths as in Drifter example
}