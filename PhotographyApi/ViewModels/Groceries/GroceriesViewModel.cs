namespace PhotographyApi.ViewModels.Groceries;

public record GroceriesViewModel(
    IReadOnlyCollection<GroceryListProductViewModel> Products,
    IReadOnlyCollection<GroceryListRecurringProductViewModel> RecurringProducts);