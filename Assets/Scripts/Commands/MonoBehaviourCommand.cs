using MZTATest.Services;
using System.Linq;
using UnityEngine;
using Zenject;

namespace MZTATest.Commands
{
    public abstract class MonoBehaviourCommand : MonoBehaviour, ICommand
    {
        public abstract bool CanExecute();
        public abstract void Execute();
    }
}
