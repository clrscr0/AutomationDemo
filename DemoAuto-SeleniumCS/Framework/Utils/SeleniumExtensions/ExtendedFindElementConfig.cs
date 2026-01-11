public class ExtendedFindElementConfig
{
    public ElementState State;
    public int Retries;
    public TimeSpan Timeout;
    public bool ThrowError;

    /// <summary>
    /// Configuration for ExtendedFindElement
    /// </summary>
    /// <param name="state">Wait for ElementState, default=ElementState.Visible</param>
    /// <param name="retries">Number of retries. Default = 1</param>
    /// <param name="timeout">Wait time before throwing an exception or doing another retry, default=TestSetting.LongWait</param>
    /// <param name="throwError">Will throw null if false, default=true</param>
    public ExtendedFindElementConfig(ElementState state = ElementState.Visible, int retries = -1, TimeSpan? timeout = null, bool throwError = true)
    {
        State = state;
        Retries = (retries < 1) ? 3 : retries;
        Timeout = (TimeSpan)((timeout is null) ? TimeSpan.FromMicroseconds(10000) : timeout);
        ThrowError = throwError;
    }
}