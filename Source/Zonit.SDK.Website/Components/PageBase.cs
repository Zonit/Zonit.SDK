using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;

namespace Zonit.SDK.Website;

public abstract class PageBase : Base
{
    [Inject]
    protected IBreadcrumbsProvider BreadcrumbsProvider { get; set; } = default!;

    protected virtual List<BreadcrumbsModel>? Breadcrumbs { get; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        BreadcrumbsProvider.Initialize(Breadcrumbs);
    }
}