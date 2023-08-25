
public abstract class BaseHandler
{
    protected int _value;
    public abstract void Init();
    public abstract void UnSubscribe();

    protected abstract void Handle();
}
