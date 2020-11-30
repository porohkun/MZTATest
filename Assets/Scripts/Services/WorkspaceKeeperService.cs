using MZTATest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZTATest.Services
{
    public class WorkspaceKeeperService
    {
        public event Action CurrentWorkspaceChanged;

        private Workspace _currentWorkspace;

        public Workspace GetWorkspace()
        {
            if (_currentWorkspace == null)
                CreateWorkspace();
            return _currentWorkspace;
        }

        public void CreateWorkspace()
        {
            _currentWorkspace = new Workspace();
            CurrentWorkspaceChanged?.Invoke();
        }
    }
}
