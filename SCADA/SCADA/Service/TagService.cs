using Microsoft.AspNetCore.SignalR;
using SCADA.DTOS;
using SCADA.Model;
using SCADA.Repository.IRepository;
using static SCADA.Model.Alarm;

namespace SCADA.Service
{
    public class TagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IServiceScopeFactory _serviceScope;

        public TagService(ITagRepository tagRepository, IServiceScopeFactory serviceScope)
        {
            _tagRepository = tagRepository;
            _serviceScope = serviceScope;
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
            inputsDTO.AnalogInputList = _tagRepository.GetAnalogInputs().OrderBy(x => int.Parse(x.IOAddress)).ToList();
            inputsDTO.DigitalInputList = _tagRepository.GetDigitalInputs().OrderBy(x => int.Parse(x.IOAddress)).ToList();
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
    }
}

