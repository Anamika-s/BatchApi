using BatchApi.Models;
namespace BatchApi.IRepo
{
    public interface IBatchRepo
    {
        public List<Batch> GetBatches();
        public Batch GetBatch(int id);
        void AddBatch(Batch batch);
        void DeleteBatch(int id);
        void EditBatch(int id, Batch batch);
    }
}
