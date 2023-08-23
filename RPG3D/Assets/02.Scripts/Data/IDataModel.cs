namespace _02.Scripts.Data
{
    public interface IDataModel
    {
        int id { get; set; }

        IDataModel ResetWithDefaults();
        void Init();
    }
}

