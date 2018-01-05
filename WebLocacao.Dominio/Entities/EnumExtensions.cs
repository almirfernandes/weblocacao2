using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLocacao.Dominio.Entities
{
    public class EnumExtensions
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}