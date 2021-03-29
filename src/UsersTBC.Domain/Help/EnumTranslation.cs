using System;
using System.Web;
using System.ComponentModel;
using System.Resources;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Threading;
using UsersTBC.Domain.Resources;

namespace UsersTBC.Domain.Help
{
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private readonly string _resourceKey;
        private readonly ResourceManager _resource;
        private readonly string CurrentLang = Thread.CurrentThread.CurrentCulture.Name;

        IHttpContextAccessor _httpContextAccessor;
        public LocalizedDescriptionAttribute(string resourceKey)
        {
            if (CurrentLang == "en-US")
            {
                _resource = new ResourceManager(typeof(LanguageEn));
            }
            else if (CurrentLang == "ka")
            {
                _resource = new ResourceManager(typeof(LanguageKa));
            }
            else
            {
                _resource = new ResourceManager(typeof(LanguageEn));
            }
           
            _resourceKey = resourceKey;
        }

        public override string Description
        {
            get
            {
                string displayName = _resource.GetString(_resourceKey);

                return string.IsNullOrEmpty(displayName)
                    ? string.Format("[[{0}]]", _resourceKey)
                    : displayName;
            }
        }
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumValue)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return enumValue.ToString();
        }
    }
}
