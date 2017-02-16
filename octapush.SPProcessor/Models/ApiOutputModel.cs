using System.Collections.Generic;
using octapush.SPProcessor.Enums;

namespace octapush.SPProcessor.Models
{
    public class ApiOutputModel
    {
        public EnumSpProcessorCallResult Result { get; set; }
        public object Supplement { get; set; }
    }
}