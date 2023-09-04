using SCADA.DTOS;
using SCADA.Model;

namespace SCADA.Service.IService
{
    public interface ITagService
    {
        public List<Tag> GetTags();
        public InputListDTO GetInputTags();
        public OutputListDTO GetOutputTags();
        public void AddTag(TagDTO tagDTO);
        public void AddOutputValue(OutputDTO dto);
        public void RemoveTag(int id);
        public void UpdateScanStatus(int id);

    }
}
