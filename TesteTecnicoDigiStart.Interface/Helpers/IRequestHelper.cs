namespace TesteTecnicoDigiStart.Interface
{
    public interface IRequestHelper
    {
        string PostT(string url, string body, string token);
        string GetT(string url, string token);
    }
}
