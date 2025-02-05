using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;

namespace Zonit.SDK.Website;

public abstract class PageComponent : BaseComponent
{
    [Inject]
    protected IBreadcrumbsProvider BreadcrumbsProvider { get; set; } = default!;

    public List<BreadcrumbsModel>? Breadcrumbs { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        BreadcrumbsProvider.Initialize(Breadcrumbs);
    }
}
