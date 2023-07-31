using System.Collections.Generic;

namespace SingerApp.MultiLingualObjects
{
    public interface IMultiLingualObject<TTranslation>
    where TTranslation : class, IObjectTranslation
    {
        ICollection<TTranslation> Translations { get; set; }
    }
}
