using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MZTATest.Commands
{
    public class AppCloseCommand : MonoBehaviourCommand
    {
        public override bool CanExecute()
        {
            if (Application.isEditor)
                return false;
            return true;
        }

        public override void Execute()
        {
            Application.Quit();
        }
    }
}
