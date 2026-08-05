namespace Common.Common;

public class ApplicationRoles
{
    public const string PhotographyApi_Admin = "PhotographyApi_Admin";
    public const string RiesjApi_Admin = "RiesjApi_Admin";
    public const string RiesjApi_RecipeEditor = "RiesjApi_RecipeEditor";

    public static readonly string[] AllRoles = [
        PhotographyApi_Admin,
        RiesjApi_Admin,
        RiesjApi_RecipeEditor
    ];
}
