using SCADA.Data;
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
    }
}
