using Microsoft.AspNetCore.SignalR;
using SCADA.Hubs.IHubs;
using SCADA.Hubs;
using SCADA.Model;
using SCADA.Repository.IRepository;
using SCADA.Service.IService;

namespace SCADA.Service
{
    public class ReportService : IReportService
    {

        private readonly ITagRepository _tagRepository;
        private readonly IAlarmRepository _alarmRepository;
        private readonly ITagRecordRepository _tagRecordRepository;
        private readonly IServiceScopeFactory _serviceScope;

        public ReportService(ITagRepository tagRepository, IAlarmRepository alarmRepository, ITagRecordRepository tagRecordRepository)
        {
            _tagRepository = tagRepository;
            _alarmRepository = alarmRepository;
            _tagRecordRepository = tagRecordRepository;
        }
        public List<AlarmActivated> getAllAlarmsByPriority(AlarmPriority priority)
        {
            return this._alarmRepository.GetActivatedAlarmsByPriority(priority);
        }

        public List<AlarmActivated> getAllAlarmsInTimeRange(TimeRange timeRange)
        {
            return this._alarmRepository.GetActivatedAlarmsByDate(timeRange.StartTime, timeRange.EndTime);
        }

        public List<TagRecord> getAllRecordsByTag(int id)
        {
            return this._tagRecordRepository.GetAllById(id);
        }

        public List<TagRecord> getAllRecordsInTimeRange(TimeRange timeRange)
        {
            return this._tagRecordRepository.GetByDate(timeRange.StartTime, timeRange.EndTime);
        }
    }
}
