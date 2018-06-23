using Arakara.BattleEngine.Models;
using Tenswee.Common.Containers;

namespace Arakara.BattleEngine.Interfaces
{
    public interface IAbilityLoader
    {
        void Load(IContainer game, Ability ability);
    }
}
