namespace Test.Helpers;

public static class ObjectExtensions
{
    public static T With<T>(this T subject, Action action)
    {
        action();
        return subject;
    }
}
