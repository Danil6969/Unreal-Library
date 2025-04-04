using UELib.Annotations;
using UELib.Branch;
using UELib.Core;
using UELib.Flags;
using UELib.ObjectModel.Annotations;
using UELib.Types;

namespace UELib.Engine
{
    /// <summary>
    ///     Implements UPolys/Engine.Polys
    /// </summary>
    [Output("PolyList")]
    [UnrealRegisterClass]
    public class UPolys : UObject
    {
        [CanBeNull] public UObject ElementOwner;

        [Output]
        public UArray<Poly> Element;

        public UPolys()
        {
            ShouldDeserializeOnDemand = true;
        }

        protected override void Deserialize()
        {
            Properties = new DefaultPropertiesCollection();
            DeserializeNetIndex();

            long long1 = _Buffer.ReadInt64();
            Record(nameof(long1), long1);

            int num, max;

            num = _Buffer.ReadInt32();
            Record(nameof(num), num);
            max = _Buffer.ReadInt32();
            Record(nameof(max), max);

            if (_Buffer.Version >= (uint)PackageObjectLegacyVersion.ElementOwnerAddedToUPolys)
            {
                ElementOwner = _Buffer.ReadObject();
                Record(nameof(ElementOwner), ElementOwner);
            }

            Element = new UArray<Poly>(num);
            for (int i = 0; i < num; ++i)
            {
                UDefaultProperty property = new UDefaultProperty(this);
                property.Type = PropertyType.Poly;
                //property.Name = new UName($"Polygon[{i:D}]");
                property._PropertyValuePosition = _Buffer.Position;
                Properties.Add(property);
                _Buffer.ReadClass(out Poly poly);
                Record(nameof(poly), poly);
                Element.Add(poly);
            }
        }
    }
}
