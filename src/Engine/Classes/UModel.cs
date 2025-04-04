using UELib.Engine;
using UELib.ObjectModel.Annotations;
using UELib.Types;

namespace UELib.Core
{
    /// <summary>
    ///     Implements UModel/Engine.Model
    /// </summary>
    [Output("Brush")]
    [UnrealRegisterClass]
    public class UModel : UPrimitive
    {
        public int Int1;

        public long Long1;
        public long Long2;
        public long Long3;
        public long Long4;
        public long Long5;
        public long Long6;

        public UObject Object1;
        public UObject Object2;

        public UVector Vector1;

        public USphere Sphere1;

        [Output]
        public UPolys Polys;
        
        public UModel()
        {
            ShouldDeserializeOnDemand = true;
        }

        protected override void Deserialize()
        {
            Properties = new DefaultPropertiesCollection();
            DeserializeNetIndex();

            Long1 = _Buffer.ReadInt64();
            Record(nameof(Long1), Long1);

            _Buffer.ReadStruct(out Vector1);
            Record(nameof(Vector1), Vector1);

            _Buffer.ReadStruct(out Sphere1);
            Record(nameof(Sphere1), Sphere1);

            Long2 = _Buffer.ReadInt64();
            Record(nameof(Long2), Long2);
            Long3 = _Buffer.ReadInt64();
            Record(nameof(Long3), Long3);
            Long4 = _Buffer.ReadInt64();
            Record(nameof(Long4), Long4);

            Object1 = _Buffer.ReadObject();
            Record(nameof(Object1), Object1);

            Int1 = _Buffer.ReadInt32();
            Record(nameof(Int1), Int1);

            Long5 = _Buffer.ReadInt64();
            Record(nameof(Long5), Long5);
            Long6 = _Buffer.ReadInt64();
            Record(nameof(Long6), Long6);

            var property = new UDefaultProperty(this);
            property.Type = PropertyType.ObjectProperty;
            property.Name = new UName($"Polys");
            property._PropertyValuePosition = _Buffer.Position;
            Properties.Add(property);
            Object2 = _Buffer.ReadObject();
            Record(nameof(Object2), Object2);
        }
    }
}
