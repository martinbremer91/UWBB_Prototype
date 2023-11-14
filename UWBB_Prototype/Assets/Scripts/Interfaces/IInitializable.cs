namespace UWBB.Interfaces
{
    public interface IInitializable
    {
        public void Init();
    }
    
    public interface IInitializable<T>
    {
        public void Init(T init);
    }

    public interface IDeinitializable
    {
        public void Deinit();
    }
}