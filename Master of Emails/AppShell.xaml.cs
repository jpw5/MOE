using practice.Pages;

namespace Master_of_Emails;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(PriorityOneMafPage), typeof(PriorityOneMafPage));
        Routing.RegisterRoute(nameof(InconAlertPage), typeof(InconAlertPage));
        Routing.RegisterRoute(nameof(ZfoPage), typeof(ZfoPage));
        Routing.RegisterRoute(nameof(DuressAlarmPage), typeof(DuressAlarmPage));
        Routing.RegisterRoute(nameof(ScadaPage), typeof(ScadaPage));
        Routing.RegisterRoute(nameof(FiberAlertPage), typeof(FiberAlertPage));
        Routing.RegisterRoute(nameof(Settings), typeof(Settings));
    }
}
