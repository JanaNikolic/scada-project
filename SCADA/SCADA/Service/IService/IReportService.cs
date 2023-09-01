using SCADA.DTOS;
using SCADA.Model;

namespace SCADA.Service.IService
{
    public interface IReportService
    {
        public List<AlarmActivated> getAllAlarmsInTimeRange(TimeRange timeRange);

        public List<AlarmActivated> getAllAlarmsByPriority(AlarmPriority priority);

        public List<TagRecord> getAllRecordsInTimeRange(TimeRange timeRange);

        public List<TagRecord> getAllRecordsByTag(int id);
    }
}
