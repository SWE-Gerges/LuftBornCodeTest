namespace LuftBornCodeTest.Server.Services
{
    public interface IMoviesService
    {
        public Task<IEnumerable<Movie>> GetAll();
        public Task<Movie> FindById(int id);
        public Task<Movie> Add(Movie movie);
        public Movie Update(Movie movie);

        public Movie Remove(Movie movie);

        

    }
}
