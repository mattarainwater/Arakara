using Arakara.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Battle.Phases
{
    public abstract class Phase
    {
        public bool IsFinished { get; set; }
        public bool GoBack { get; set; }
        public BattleActor Actor { get; set; }

        public Phase(BattleActor actor)
        {
            Actor = actor;
        }

        public void Initialize()
        {
            Console.Out.WriteLine($"Started - Actor: {Actor.Name} Phase: {GetType().ToString()}");
            IsFinished = false;
            GoBack = false;
            initialize();
        }

        public void Update()
        {
            update();
        }

        public void Finish()
        {
            Console.Out.WriteLine($"Finished - Actor: {Actor.Name} Phase: {GetType().ToString()}");
            finish();
        }

        protected virtual void initialize() { }
        protected abstract void update();
        protected virtual void finish() { }
    }
}
