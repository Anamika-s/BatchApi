using BatchApi.Context;
using BatchApi.IRepo;
using BatchApi.Models;

namespace RepositoryPatternDemo.Repo
{
    public class BatchRepo : IBatchRepo
    {
        AppDbContext _context;
        public BatchRepo(AppDbContext context)
        {
            _context = context;
        }
        public void AddBatch(Batch batch)
        {
           _context.Batches.Add(batch);
            _context.SaveChanges();
        }

        public void DeleteBatch(int id)
        {
            Batch user = GetBatch(id);
            _context.Batches.Remove(user);
            _context.SaveChanges();
        }

        public void EditBatch(int id, Batch user)
        {
            Batch temp = GetBatch(id);
            temp.StartDate = user.StartDate;
            _context.SaveChanges();
        }

        public Batch GetBatch(int id)
        {
            return _context.Batches.FirstOrDefault(x => x.Id == id);
        }

        public List<Batch> GetBatches()
        {
            return _context.Batches.ToList();
        }
    }
}
