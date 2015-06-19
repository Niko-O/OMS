using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnUtils;
using OnUtils.Wpf;

namespace TennisPlugin.Scoring
{
    public class UndoStateList : NotifyPropertyChanged
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

        private Stack<IScoringState> States = new Stack<IScoringState>();

        public UndoStateList(IScoringState InitialState)
        {
            if (InitialState == null)
            {
                throw new ArgumentNullException("InitialState");
            }
            States.Push(InitialState);
        }

        public void Process(ScoringStrategyAction Action)
        {
            States.Push(States.Peek().Process(Action));
            OnPropertyChanged("CanUndo", "CurrentState");
        }

        public bool CanProcess(ScoringStrategyAction Action)
        {
            return States.Peek().CanProcess(Action);
        }

        public void Undo()
        {
            if (!CanUndo) throw new InvalidOperationException("Kann nicht rückgängig machen.");
            States.Pop();
            OnPropertyChanged("CanUndo", "CurrentState");
        }

    }
}
