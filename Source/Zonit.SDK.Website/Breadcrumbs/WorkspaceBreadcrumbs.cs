using Zonit.Extensions.Website;

namespace Zonit.SDK.Website;

public class WorkspaceBreadcrumbs : BreadcrumbsModel
{
    public WorkspaceBreadcrumbs() : base("Workspace", "Workspace")
    {
        Template = "workspace";
    }
}