using MZTATest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using MZTATest.ViewModels;
using MZTATest.Models;
using UnityEngine;
using MZTATest.Views;

namespace MZTATest
{
    public class Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<WorkspaceKeeperService>().AsSingle();
            Container.Bind<BlocksSelectionService>().AsSingle();
            Container.Bind<WorkspaceViewModel>().AsTransient();
            Container.BindFactory<Block, BlockViewModel, BlockViewModel.Factory>().AsTransient();
            Container.BindFactory<BlockViewModel, BlockView, BlockView.Factory>().FromComponentInNewPrefabResource("Views/BlockView");
            Container.Bind<CursorService>().FromComponentInNewPrefabResource("Services/CursorService").AsSingle();
        }
    }
}
