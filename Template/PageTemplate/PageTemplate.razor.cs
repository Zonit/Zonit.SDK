using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;
using Zonit.SDK.Website;

namespace $rootnamespace$;

[Authorize]
[Route("/" + Route)]
public sealed partial class $safeitemname$ : PageComponent // IAreaWeb, IAreaManager, IAreaManagement, IAreaDiagnostics
{
    public const string Route = "$safeitemname$";

    public override List<BreadcrumbsModel>? Breadcrumbs =>
    [
        new("Home", "/"),
    ];

    protected override void OnInitialized()
    {
        base.OnInitialized();
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
}