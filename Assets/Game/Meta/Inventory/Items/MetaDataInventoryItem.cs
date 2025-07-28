using Sirenix.OdinInspector;
using System;
using System.Text;
using UnityEngine;

namespace Game.Meta
{
    [Serializable]
    public class MetaDataInventoryItem
    {
        [field: SerializeField]
        public string Name_LocalizationKey { get; set; }

        [field: SerializeField]
        public string Description_LocalizationKey { get; set; }

        [field: SerializeField]
        public string Info_LocalizationKey { get; set; }

        [field: SerializeField]
        public string Effect_LocalizationKey { get; set; }

        [field: SerializeField]
        public string[] Locations_LocalizationKey { get; set; }
        
        [PreviewField(200)]
        public Sprite Icon;

        public MetaDataInventoryItem Clone()
        {
            Locations_LocalizationKey ??= Array.Empty<string>();
            
            var copiedArray = new string[Locations_LocalizationKey.Length];
            Array.Copy(Locations_LocalizationKey, copiedArray, Locations_LocalizationKey.Length);
            
            return new MetaDataInventoryItem()
            {
                Name_LocalizationKey = Name_LocalizationKey,
                Description_LocalizationKey = Description_LocalizationKey,
                Info_LocalizationKey = Info_LocalizationKey,
                Effect_LocalizationKey = Effect_LocalizationKey,
                Icon = Icon,
                Locations_LocalizationKey = copiedArray,
            };
        }
        
        public string GetFormattedDescriptionBlock()
        {
            var builder = new StringBuilder();

            AppendLineIfNotNullOrEmpty(builder, Description_LocalizationKey);
            builder.AppendLine();

            AppendLineIfNotNullOrEmpty(builder, Info_LocalizationKey);
            builder.AppendLine();

            AppendLineIfNotNullOrEmpty(builder, Effect_LocalizationKey);

            return builder.ToString().TrimEnd(); // Убираем последний перевод строки, если не нужно
        }

        private static void AppendLineIfNotNullOrEmpty(StringBuilder builder, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                builder.AppendLine(value);
            }
        }
    }
}