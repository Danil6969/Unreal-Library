﻿using System.Collections.Generic;

namespace UELib.Core
{
    /// <summary>
    /// Represents a unreal enum.
    /// </summary>
    [UnrealRegisterClass]
    public partial class UEnum : UField
    {
        #region Serialized Members

        /// <summary>
        /// Names of each element in the UEnum.
        /// </summary>
        public IList<UName> Names;

        #endregion

        #region Constructors

        protected override void Deserialize()
        {
            base.Deserialize();

            int count = _Buffer.ReadLength();
            Record(nameof(count), count);
            Names = new List<UName>(count);
            for (var i = 0; i < count; ++i)
            {
                UName enumField = _Buffer.ReadNameReference();
                Names.Add(enumField);
                Record(nameof(enumField), enumField);
            }
#if SPELLBORN
            if (_Buffer.Package.Build == UnrealPackage.GameBuild.BuildName.Spellborn
                && 145 < _Buffer.Version)
            {
                uint unknownEnumFlags = _Buffer.ReadUInt32();
                Record(nameof(unknownEnumFlags), unknownEnumFlags);
            }
        }
#endif

        #endregion
    }
}
