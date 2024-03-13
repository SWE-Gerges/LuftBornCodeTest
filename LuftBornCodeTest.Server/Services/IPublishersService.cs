namespace LuftBornCodeTest.Server.Services
{
    public interface IPublishersService
    {
        Task <IEnumerable<Publisher>> GetAll ();
        Task<Publisher> FindById (byte id);
        Task<Publisher> Add(Publisher publisher);
        Publisher Update(Publisher publisher);
        Publisher Remove(Publisher publisher);
        

    }
}
