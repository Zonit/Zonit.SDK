using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;
using Zonit.SDK.Website;

namespace $rootnamespace$;

[Authorize]
[Route("/" + Route)]
public sealed partial class $safeitemname$ : PageBase // IAreaWeb, IAreaManager, IAreaManagement, IAreaDiagnostic
{
    public const string Route = "$safeitemname$";

    protected override List<BreadcrumbsModel>? Breadcrumbs =>
    [
        new("Home", "Home"),
    ];

    protected override void OnInitialized()
    {
        base.OnInitialized();
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
    }
}