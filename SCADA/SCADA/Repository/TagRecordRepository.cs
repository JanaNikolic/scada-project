using SCADA.Data;
using SCADA.Model;
using SCADA.Repository.IRepository;

namespace SCADA.Repository
{
    public class TagRecordRepository : ITagRecordRepository
    {
        private readonly DataContext _dataContext;

        public TagRecordRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<TagRecord> GetAll()
        {
            return _dataContext.TagRecords.OrderByDescending(r => r.Timestamp).ToList();
        }

        public List<TagRecord> GetAllById(int id)
        {
            return _dataContext.TagRecords.Where(r => r.TagId == id).OrderByDescending(r => r.Timestamp).ToList();
        }

        public List<TagRecord> GetByDate(DateTime start, DateTime end)
        {
            return _dataContext.TagRecords.Where(a => a.Timestamp >= start && a.Timestamp <= end).ToList();
        }
    }
}
