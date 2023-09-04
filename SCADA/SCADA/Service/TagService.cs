using Azure;
using Microsoft.AspNetCore.SignalR;
using SCADA.DTOS;
using SCADA.Hubs;
using SCADA.Hubs.IHubs;
using SCADA.Model;
using SCADA.Repository.IRepository;
using SCADA.Service.IService;
using static SCADA.Model.Alarm;

namespace SCADA.Service
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IServiceScopeFactory _serviceScope;
        private readonly IHubContext<RTUHubClient, IRTUHubClient> _rtuHubContext;
        private readonly IHubContext<SimulationHubClient, ISimulationHubClient> _simulationHubContext;

        public TagService(ITagRepository tagRepository, IServiceScopeFactory serviceScope, IHubContext<RTUHubClient, IRTUHubClient> rtHubContext,
         IHubContext<SimulationHubClient, ISimulationHubClient> simulationHubContext)
        {
            _tagRepository = tagRepository;
            _serviceScope = serviceScope;
            _rtuHubContext = rtHubContext;
            _simulationHubContext = simulationHubContext;
        }

        public void AddTag(TagDTO dto)
        {
            switch (dto.Type)
            {
                case "Analog Input":
                    this.AddAnalogInputTag(dto);
                    break;
                case "Analog Output":
                    this.AddAnalogOutputTag(dto);
                    break;
                case "Digital Input":
                    this.AddDigitalInputTag(dto);
                    break;
                case "Digital Output":
                    this.AddDigitalOutputTag(dto);
                    break;
                default:
                    throw new Exception("Tag type provided is not supported.");
            }
        }

        private void AddAnalogInputTag(TagDTO dto)
        {
            if (GetByAddress(dto.IOAddress) != null)
            {
                throw new Exception("This Address is already taken");
            }
            AnalogInput newAI = new AnalogInput
            {
                IOAddress = dto.IOAddress,
                Description = dto.Description,
                Driver = dto.Driver,
                Name = dto.Name,
                Value = -1,
                HighLimit = (double)dto.HighLimit,
                LowLimit = (double)dto.LowLimit,
                Units = dto.Units,
                ScanTime = (int)dto.ScanTime,
                IsScanOn = (bool)dto.IsScanOn

            };
            _tagRepository.AddAnalogInputTag(newAI);
        }

        public void AddAnalogOutputTag(TagDTO dto)
        {
            if (GetByAddress(dto.IOAddress) != null)
            {
                throw new Exception("This Address is already taken");
            }
            AnalogOutput newAO= new AnalogOutput
            {
                IOAddress = dto.IOAddress,
                Description = dto.Description,
                Name = dto.Name,
                Value = (double)dto.InitialValue,
                HighLimit = (double)dto.HighLimit,
                LowLimit = (double)dto.LowLimit,
                Units = dto.Units
            };
            _tagRepository.AddAnalogOutputTag(newAO);
        }

        public void AddDigitalInputTag(TagDTO dto)
        {
            if (GetByAddress(dto.IOAddress) != null)
            {
                throw new Exception("This Address is already taken");
            }
            DigitalInput newDI = new DigitalInput
            {
                IOAddress = dto.IOAddress,
                Description = dto.Description,
                Name = dto.Name,
                Driver = dto.Driver,
                Value = -1,
                ScanTime = (int)dto.ScanTime,
                IsScanOn = (bool)dto.IsScanOn
            };
            _tagRepository.AddDigitalInputTag(newDI);
        }

        public void AddDigitalOutputTag(TagDTO dto)
        {
            if (GetByAddress(dto.IOAddress) != null)
            {
                throw new Exception("This Address is already taken");
            }
            DigitalOutput newDO = new DigitalOutput
            {
                IOAddress = dto.IOAddress,
                Description = dto.Description,
                Value = (double)dto.InitialValue,
                Name = dto.Name
            };
            _tagRepository.AddDigitalOutputTag(newDO);
        }

        public void AddOutputValue(OutputDTO dto)
        {
            Tag tag = GetByAddress(dto.IOAddress);

            if (tag == null)
            {
                throw new Exception("This Tag does not exist");
            }

            _tagRepository.AddOutputValue(dto);
        }

        public InputListDTO GetInputTags()
        {
            InputListDTO inputsDTO = new InputListDTO();
            inputsDTO.AnalogInputList = _tagRepository.GetAnalogInputs().ToList();
            inputsDTO.DigitalInputList = _tagRepository.GetDigitalInputs().ToList();
            return inputsDTO;
        }

        public OutputListDTO GetOutputTags()
        {
            OutputListDTO outputsDTO = new OutputListDTO();
            outputsDTO.AnalogOutputList = _tagRepository.GetAnalogOutputs().OrderBy(x => int.Parse(x.IOAddress)).ToList();
            outputsDTO.DigitalOutputList = _tagRepository.GetDigitalOutputs().OrderBy(x => int.Parse(x.IOAddress)).ToList();
            return outputsDTO;
        }

        
        public void RemoveTag(int id)
        {
            Tag? tag = _tagRepository.GetById(id);
            if (tag == null)
            {
                throw new Exception("Tag not found");
            }
            _tagRepository.RemoveTag(tag);
        }

        public Tag? GetByAddress(string address)
        {
            return _tagRepository.GetAll().Find(a => a.IOAddress == address);
        }

        public void UpdateScanStatus(int id)
        {
            try
            {
                Tag? tag = _tagRepository.GetById(id);

                if (tag is DigitalInput digitalInput)
                {
                    _tagRepository.UpdateDigitalInputScan(id);
                }
                else if (tag is AnalogInput analogInput) 
                { 
                    _tagRepository.UpdateAnalogInputScan(id);
                }
                else
                {
                    throw new Exception("This Address doesn't exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Scan couldn't be toggled");
            }
        }

        public void StartSimulation()
        {
            var inputs = _tagRepository.GetInputs();

            foreach (var input in inputs)
            {
                if (input is AnalogInput analog) { StartAnalogThread(analog); }
                else if(input is DigitalInput digital) { StartDigitalThread(digital); }
            }
        }

        private void StartDigitalThread(DigitalInput digital)
        {
            new Thread(async () =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    using (var scope = _serviceScope.CreateScope())
                    {

                        if (digital.IsScanOn)
                        {
                            var repo = scope.ServiceProvider.GetRequiredService<ITagRepository>();
                            TagRecord? value = await repo.GetTagRecordByAddress(digital.IOAddress);
                            if (value == null) { return; }
                            SendValue(value);
                        }
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(digital.ScanTime));
                }
            }).Start();
        }

        private void StartAnalogThread(AnalogInput analog)
        {
            new Thread(async () =>
            {
                Thread.CurrentThread.IsBackground = true;

                while (true)
                {
                    using (var scope = _serviceScope.CreateScope())
                    {
                        if (analog.IsScanOn)
                        {
                            var repo = scope.ServiceProvider.GetRequiredService<ITagRepository>();
                            TagRecord? value = await repo.GetTagRecordByAddress(analog.IOAddress);
                            if (value == null) { return; }
                            SendValue(value);
                            var alarmService = scope.ServiceProvider.GetRequiredService<IAlarmService>();
                            CheckIfAlarmExists(value, analog, alarmService);
                        }
                    }

                    Thread.Sleep(TimeSpan.FromSeconds(analog.ScanTime));
                }
            }).Start();
        }

        private void CheckIfAlarmExists(TagRecord value, AnalogInput analog, IAlarmService alarmService)
        {
            foreach(var alarm in analog.Alarms)
            {
                Alarm a = alarmService.GetById(alarm.Id);
                if ((alarm.Type == AlarmType.ABOVE && alarm.Threshold >= value.Value) || (alarm.Type == AlarmType.BELOW && alarm.Threshold <= value.Value))
                {
                    alarmService.AddAlarmValue(new AlarmActivated(a), analog);
                    SendAlarm(a);
                }
            }
        }

        private void SendValue(TagRecord value)
        {
            _rtuHubContext.Clients.All.SendRTUData(value);
        }
        
        private void SendAlarm(Alarm alarm)
        {
            _simulationHubContext.Clients.All.SendSimulationData(alarm);
        }
    }
}

