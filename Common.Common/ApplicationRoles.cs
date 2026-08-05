namespace Common.Common;

public class ApplicationRoles
{
    public const string Riesj_Admin = "Riesj_Admin";
    public const string Riesj_RecipeEdit = "Riesj_RecipeEdit";
    public const string Riesj_ShoppingListEdit = "Riesj_ShoppingListEdit";

    public static readonly string[] AllRoles = [
        Riesj_Admin,
        Riesj_RecipeEdit,
        Riesj_ShoppingListEdit
    ];
}
