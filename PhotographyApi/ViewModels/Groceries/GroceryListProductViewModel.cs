namespace PhotographyApi.ViewModels.Groceries;

public record GroceryListProductViewModel(long? Id, long? RowVersion, string Name, bool RecurringProduct, bool Sale, bool AlbertHeijn);