using SingerApp.MultiLingualObjects;
using Volo.Abp.Domain.Entities;

namespace SingerApp.Singers;
public class SingerTranslation : Entity<int>, IObjectTranslation
{
    public int SingerId{get;set;}
    public string Name { get; set; }
    public string Language { get; set; }

    public override object[] GetKeys()
    { 
        return new object[] { SingerId, Language }; 
    }
}