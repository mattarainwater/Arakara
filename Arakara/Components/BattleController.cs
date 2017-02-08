using Arakara.Battle;
using Arakara.Common;
using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public class BattleController
    {
        public List<BattleActor> Actors { get; set; }
        public BattleActor CurrentActor { get; set; }

        private List<Entity> _targetableEntities;

        public bool IsActive { get; set; }

        public BattleController()
        {
            Actors = new List<BattleActor>();
            _targetableEntities = new List<Entity>();
        }

        public void Update()
        {
            if(IsActive)
            {
                if (CurrentActor != null)
                {
                    CurrentActor.ProcessTurn(this);
                    if (CurrentActor.State == BattleState.NotTurn)
                    {
                        CurrentActor.TimeUntilTurn = CurrentActor.Delay;
                        while (Actors.Any(x => x.TimeUntilTurn == CurrentActor.TimeUntilTurn && x != CurrentActor))
                        {
                            CurrentActor.TimeUntilTurn++;
                        }
                        CurrentActor = Actors.OrderBy(x => x.TimeUntilTurn).First();
                        var timeUntilTurn = CurrentActor.TimeUntilTurn;
                        foreach (var actor in Actors)
                        {
                            actor.TimeUntilTurn -= timeUntilTurn;
                        }
                        _targetableEntities.ForEach(entity => entity.destroy());
                        _targetableEntities = new List<Entity>();
                        CurrentActor.State = BattleState.StartOfTurn;
                    }
                }
                else
                {
                    CurrentActor = Actors.First();
                    CurrentActor.State = BattleState.StartOfTurn;
                }
            }
        }

        public void MakeTargetables(BattleActor targerter, Targeting targeting)
        {
            _targetableEntities.ForEach(entity => entity.destroy());
            _targetableEntities = new List<Entity>();
            var targetableActors = GetTargetableActors(targerter, targeting);
            for (var i = 0; i < targetableActors.Count(); i++)
            {
                var actor = targetableActors[i];
                var actorEntity = actor.entity;
                var targetableEntity = targerter.entity.scene.createEntity("target " + i, new Vector2(actorEntity.transform.position.X, actorEntity.transform.position.Y - 20));
                targetableEntity.tag = EntityTags.TARGETABLE_TAG;
                var verts = new Vector2[3]
                {
                    new Vector2(DimensionConstants.CHARACTER_WIDTH_HALVED - 10, 0),
                    new Vector2(DimensionConstants.CHARACTER_WIDTH_HALVED + 10, 0),
                    new Vector2(DimensionConstants.CHARACTER_WIDTH_HALVED, 10),
                };
                targetableEntity.addComponent(new SimplePolygon(verts, Color.LightPink));
                targetableEntity.addCollider(new BoxCollider(new Rectangle(0, 20, DimensionConstants.CHARACTER_WIDTH, DimensionConstants.CHARACTER_HEIGHT)));
                targetableEntity.addComponent(new Targetable(actor, targerter));
                _targetableEntities.Add(targetableEntity);
            }
        }

        private List<BattleActor> GetTargetableActors(BattleActor targerter, Targeting targeting)
        {
            switch (targeting)
            {
                case Targeting.Allies:
                    return Actors.Where(x => x.Faction.Id == targerter.Faction.Id).ToList();
                case Targeting.Enemies:
                    return Actors.Where(x => x.Faction.Id != targerter.Faction.Id).ToList();
                case Targeting.Self:
                    return Actors.Where(x => x == targerter).ToList();
                default:
                    return null;
            }
        }

        public void AddActor(BattleActor actor)
        {
            Actors.Add(actor);
        }
    }
}
