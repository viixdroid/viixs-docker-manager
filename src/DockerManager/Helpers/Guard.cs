namespace DockerManager.Helpers;

public static class Guard
{
    public static class Against
    {
        public static TValue Null<TValue>(TValue? value, string parameterName)
            where TValue : class
        {
            ArgumentNullException.ThrowIfNull(value, parameterName);
            return value;
        }
    }
}
