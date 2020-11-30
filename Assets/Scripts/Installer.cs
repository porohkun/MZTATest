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
            Container.Bind<ISerializationService>().To<JsonSezializationService>().AsSingle();
            Container.Bind<WorkspaceViewModel>().AsTransient();
            Container.BindFactory<Block, BlockViewModel, BlockViewModel.Factory>().AsTransient();
            Container.BindFactory<BlockViewModel, BlockView, BlockView.Factory>().FromComponentInNewPrefabResource("Views/BlockView");
            Container.Bind<CursorService>().FromComponentInNewPrefabResource("Services/CursorService").AsSingle();
            Container.Bind<ColorPickerService>().FromComponentInNewPrefabResource("Services/ColorPickerService").AsSingle();
            Container.Bind<MessageBoxService>().FromComponentInNewPrefabResource("Services/MessageBoxService").AsSingle();

            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                    Container.Bind<ISaveLoadService>().To<WindowsSaveLoadService>().FromComponentInNewPrefabResource("Services/WindowsSaveLoadService").AsSingle();
                    break;
                case RuntimePlatform.WebGLPlayer:
                    Container.Bind<ISaveLoadService>().To<BrowserSaveLoadService>().FromComponentInNewPrefabResource("Services/BrowserSaveLoadService").AsSingle();
                    break;
                default:
                    Container.Bind<ISaveLoadService>().To<DummySaveLoadService>().FromComponentInNewPrefabResource("Services/DummySaveLoadService").AsSingle();
                    break;
            }

        }
    }
}
