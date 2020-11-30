using MZTATest.Models;
using System;

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

        public void SetWorkspace(Workspace workspace)
        {
            _currentWorkspace = workspace;
            CurrentWorkspaceChanged?.Invoke();
        }
    }
}
