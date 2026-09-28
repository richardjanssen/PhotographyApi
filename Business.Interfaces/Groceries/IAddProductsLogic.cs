namespace Business.Interfaces.Groceries;

public interface IAddProductsLogic
{
    Task AddProducts(string[] names);
}