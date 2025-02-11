using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace Zonit.SDK.Website;

public abstract class PageBase<TViewModel> : PageBase where TViewModel : class, new()
{
    [SupplyParameterFromForm]
    protected TViewModel? Model { get; set; }
    protected EditContext? EditContext { get; private set; }
    protected ValidationMessageStore? ValidationMessages { get; private set; }
    protected bool Processing { get; set; }
    public bool IsValid => EditContext?.Validate() ?? false;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Model ??= new TViewModel();

        EditContext = new EditContext(Model);
        EditContext.OnFieldChanged += OnModelChanged;
        EditContext.OnValidationRequested += HandleValidationRequested;

        ValidationMessages = new ValidationMessageStore(EditContext);
    }

    private void HandleValidationRequested(object? sender, ValidationRequestedEventArgs e)
    {
        if (Model is null || EditContext is null)
            return;

        ValidationMessages?.Clear();

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(Model);

        bool isValid = Validator.TryValidateObject(Model, validationContext, validationResults, true);

        if (!isValid)
        {
            foreach (var validationResult in validationResults)
            {
                foreach (var memberName in validationResult.MemberNames)
                {
                    var field = EditContext.Field(memberName);
                    ValidationMessages?.Add(field, Culture.Translate(validationResult.ErrorMessage!));      // Translate
                }
            }
        }

        EditContext.NotifyValidationStateChanged();
    }

    protected virtual void OnModelChanged(object? sender, FieldChangedEventArgs e)
        => StateHasChanged();

    protected virtual void HandleInvalidSubmit(string message) { }
    protected virtual async Task HandleValidSubmitAsync() { await Task.CompletedTask; }

    public async Task HandleValidSubmit(EditContext editContext)
    {
        if (editContext.Validate() is false)
            return;

        Processing = true;

        await HandleValidSubmitAsync();

        Processing = false;
    }

    public void HandleInvalidSubmit()
    {
        foreach (var error in GetValidationErrors())
        {
            if (error is null)
                continue;

            var message = Culture.Translate(error);

            HandleInvalidSubmit(message);
        }
    }

    public void ResetModel()
    {
        Model = new();
        EditContext = new EditContext(Model);
        ValidationMessages = new ValidationMessageStore(EditContext);
    }

    public void AddValidationMessage(string fieldName, string message)
    {
        if (EditContext is null)
            return;

        var field = EditContext.Field(fieldName);
        ValidationMessages?.Add(field, message);
    }

    private IEnumerable<string?> GetValidationErrors()
    {
        if (Model is null)
            return [];

        var validationContext = new ValidationContext(Model, serviceProvider: null, items: null);
        var validationResults = new List<ValidationResult>();

        Validator.TryValidateObject(Model, validationContext, validationResults, validateAllProperties: true);

        return validationResults.Select(result => result.ErrorMessage);
    }


    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (EditContext is not null)
        {
            EditContext.OnFieldChanged -= OnModelChanged;
            EditContext.OnValidationRequested -= HandleValidationRequested;
            ValidationMessages?.Clear();
            ValidationMessages = null;
        }
    }
}