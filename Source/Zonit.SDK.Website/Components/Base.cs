using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Cultures;
using Zonit.Extensions.Identity;
using Zonit.Extensions.Organizations;
using Zonit.Extensions.Projects;

namespace Zonit.SDK.Website;

public abstract class Base : ComponentBase, IDisposable
{
    private static bool UseCulture => true;
    private static bool UseWorkspace => true;
    private static bool UseCatalog => true;
    private bool _disposed;

    [Inject]
    protected ICultureProvider Culture { get; set; } = default!;

    [Inject]
    protected IWorkspaceProvider Workspace { get; set; } = default!;

    [Inject]
    protected ICatalogProvider Catalog { get; set; } = default!;

    [Inject]
    protected IAuthenticatedProvider Authenticated { get; set; } = default!;

    [Inject]
    protected IOrganizationManager OrganizationManager { get; set; } = default!;

    [Inject]
    protected Lazy<IUserProvider> UserManager { get; set; } = default!;

    private Lazy<ICultureProvider>? _lazyCulture;
    private Lazy<IWorkspaceProvider>? _lazyWorkspace;
    private Lazy<ICatalogProvider>? _lazyCatalog;

    protected override void OnInitialized()
    {
        if (UseCulture && Culture is not null)
        {
            _lazyCulture = new Lazy<ICultureProvider>(() => Culture);
            _lazyCulture.Value.OnChange += OnCultureChange;
        }

        if (UseWorkspace && Workspace is not null)
        {
            _lazyWorkspace = new Lazy<IWorkspaceProvider>(() => Workspace);
            _lazyWorkspace.Value.OnChange += OnRefreshChangeAsync;
        }

        if (UseCatalog && Catalog is not null)
        {
            _lazyCatalog = new Lazy<ICatalogProvider>(() => Catalog);
            _lazyCatalog.Value.OnChange += OnRefreshChangeAsync;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            if (_lazyCulture?.Value is not null)
                _lazyCulture.Value.OnChange -= OnCultureChange;

            if (_lazyWorkspace?.Value is not null)
                _lazyWorkspace.Value.OnChange -= OnRefreshChangeAsync;

            if (_lazyCatalog?.Value is not null)
                _lazyCatalog.Value.OnChange -= OnRefreshChangeAsync;
        }

        _disposed = true;
    }

    ~Base() 
        => Dispose(false);

    protected virtual void OnCultureChange()
        => StateHasChanged();
    

    /// <summary>
    /// Klasa do ponownego przeładowania treści na stronie, danych np z bazy danych
    /// </summary>
    protected virtual async void OnRefreshChangeAsync()
    {
        try
        {
            await OnInitializedAsync();
            await InvokeAsync(StateHasChanged);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in OnRefreshChange: {ex.Message}");
        }
    }
}
