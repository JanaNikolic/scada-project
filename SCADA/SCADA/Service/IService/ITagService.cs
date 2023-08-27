﻿using SCADA.DTOS;
using SCADA.Model;

namespace SCADA.Service.IService
{
    public interface ITagService
    {
        public InputListDTO GetInputTags();
        public OutputListDTO GetOutputTags();
        public void AddTag(TagDTO tagDTO);
        public void AddOutputValue(OutputDTO dto);
        public void RemoveTag(int id);
    }
}