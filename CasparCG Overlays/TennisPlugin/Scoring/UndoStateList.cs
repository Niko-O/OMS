using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TennisPlugin.Scoring
{
    public class UndoStateList : OnUtils.Wpf.NotifyPropertyChanged
    {

        public bool CanUndo
        {
            get
            {
                return States.Count > 1;
            }
        }

        public IScoringState CurrentState
        {
            get
            {
                return States.Peek();
            }
        }

        private IScoringStrategy Strategy;
        private Stack<IScoringState> States = new Stack<IScoringState>();

        public UndoStateList(IScoringStrategy NewStrategy)
        {
            if (NewStrategy == null)
            {
                throw new ArgumentNullException("NewStrategy");
            }
            Strategy = NewStrategy;
            States.Push(Strategy.InitialState);
        }

        public void Process(ScoringStrategyAction Action)
        {
            States.Push(Strategy.Process(States.Peek(), Action));
            OnPropertyChanged("CanUndo", "CurrentState");
        }

        public void Undo()
        {
            if (!CanUndo) throw new InvalidOperationException("Kann nicht rückgängig machen.");
            States.Pop();
            OnPropertyChanged("CanUndo", "CurrentState");
        }

    }
}
