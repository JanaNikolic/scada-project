using SCADA.Model;

namespace SCADA.Repository.IRepository
{
    public interface ITagRecordRepository
    {
        public List<TagRecord> GetAllById(int id);
        public List<TagRecord> GetAll();
        public List<TagRecord> GetByDate(DateTime start, DateTime end);
    }
}
