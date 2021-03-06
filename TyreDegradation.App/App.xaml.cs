using System.IO.Abstractions;
using System.Windows;
using Prism.Ioc;
using TyreDegradation.App.Views;
using TyreDegradation.Contract.Interfaces;
using TyreDegradation.Data.Parsers;
using TyreDegradation.Services.Results;

namespace TyreDegradation.App
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ResultsService>();
            containerRegistry.Register(typeof(IFileSystem), typeof(FileSystem));
            containerRegistry.Register(typeof(ITyreInformation), typeof(TyreInformationParser));
            containerRegistry.Register(typeof(ITrackInformation), typeof(TrackInformationParser));
        }
    }
}