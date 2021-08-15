﻿using System;
using System.Diagnostics;
using System.IO;
using RobloxFiles.DataTypes;

namespace Source2Roblox.Models
{
    [Flags]
    public enum ModelFlags
    {
        AutoGeneratedHitbox = 0x1,
        UsesEnvCubemap = 0x2,
        ForceOpaque = 0x4,
        TranslucentTwoPass = 0x8,
        StaticProp = 0x10,
        UsesFBTexture = 0x20,
        HasShadowLOD = 0x40,
        UsesBumpMapping = 0x80,
        UsesShadowLODMaterials = 0x100,
        Obsolete = 0x200,
        Unused = 0x400,
        NoForcedFade = 0x800,
        ForcePhonemeCrossfade = 0x1000,
        ConstantDirectionalLightDot = 0x2000,
        FlexesConverted = 0x4000,
        BuiltInPreviewMode = 0x8000,
        AmbientBoost = 0x10000,
        DoNotCastShadows = 0x20000,
        CastTextureShadows = 0x40000
    }

    public class ModelHeader
    {
        public readonly string ID;
        public readonly int Version;
        public readonly int Checksum;

        public readonly string Name;
        public readonly int DataLength;

        public readonly Vector3 EyePosition;
        public readonly Vector3 IllumPosition;

        public readonly Vector3 HullMin;
        public readonly Vector3 HullMax;

        public readonly Vector3 ViewBBMin;
        public readonly Vector3 ViewBBMax;

        public readonly ModelFlags Flags;

        public readonly int BoneCount;
        public readonly int BoneIndex;

        public readonly int BoneControllerCount;
        public readonly int BoneControllerIndex;

        public readonly int HitboxCount;
        public readonly int HitboxIndex;

        public readonly int LocalAnimCount;
        public readonly int LocalAnimIndex;

        public readonly int LocalSequenceCount;
        public readonly int LocalSequenceIndex;

        public readonly int ActivityListVersion;
        public readonly int EventsIndexed;

        public readonly int TextureCount;
        public readonly int TextureIndex;

        public readonly int TextureDirCount;
        public readonly int TextureDirIndex;

        public readonly int SkinReferenceCount;
        public readonly int SkinFamilyCount;
        public readonly int SkinRefIndex;

        public readonly int BodyPartCount;
        public readonly int BodyPartIndex;

        public readonly int AttachmentCount;
        public readonly int AttachmentIndex;

        public readonly int LocalNodeCount;
        public readonly int LocalNodeIndex;
        public readonly int LocalNodeNameIndex;

        public readonly int FlexDescCount;
        public readonly int FlexDescIndex;

        public readonly int FlexControllerCount;
        public readonly int FlexControllerIndex;

        public readonly int FlexRulesCount;
        public readonly int FlexRulesIndex;

        public readonly int IKChainCount;
        public readonly int IKChainIndex;

        public readonly int MouthsCount;
        public readonly int MouthsIndex;

        public readonly int LocalPoseParamCount;
        public readonly int LocalPoseParamIndex;

        public readonly int SurfacePropIndex;
        public readonly int KeyValueIndex;
        public readonly int KeyValueCount;

        public readonly int IKLockCount;
        public readonly int IKLockIndex;

        public readonly float Mass;
        public readonly int Contents;

        public readonly int IncludeModelCount;
        public readonly int IncludeModelIndex;

        public readonly int VirtualModel;

        public readonly int AnimBlocksNameIndex;
        public readonly int AnimBlocksCount;
        public readonly int AnimBlocksIndex;

        public readonly int AnimBlockModel;
        public readonly int BoneTableNameIndex;

        public readonly int VertexBase;
        public readonly int OffsetBase;

        public readonly byte DirectionalDotProduct;
        public readonly byte NumAllowedRootLODs;
        public readonly byte RootLOD;

        public readonly int FlexControllerUICount;
        public readonly int FlexControllerUIIndex;

        //////////////////////////////////////
        // StudioHDR2
        //////////////////////////////////////
        
        public readonly int StudioHDR2Index;

        public readonly int SrcBoneTransformCount;
        public readonly int SrcBoneTransformIndex;

        public readonly int IllumPositionAttachmentIndex;
        public readonly float MaxEyeDeflection;
        public readonly int LinearBoneIndex;
        
        //////////////////////////////////////
        // Constructor
        //////////////////////////////////////

        public ModelHeader(BinaryReader reader)
        {
            ID = reader.ReadString(4);
            Debug.Assert(ID == "IDST", "Not a MDL file!");

            Version = reader.ReadInt32();
            Checksum = reader.ReadInt32();

            const int minVersion = ModelConstants.MinModelVersion;
            Debug.Assert(Version >= minVersion, $"Unsupported MDL version! ({Version} < {minVersion})");

            const int maxVersion = ModelConstants.MaxModelVersion;
            Debug.Assert(Version <= maxVersion, $"Unsupported MDL version! ({Version} > {maxVersion})");

            Name = reader.ReadString(64);
            DataLength = reader.ReadInt32();

            EyePosition = reader.ReadVector3();
            IllumPosition = reader.ReadVector3();

            HullMin = reader.ReadVector3();
            HullMax = reader.ReadVector3();

            ViewBBMin = reader.ReadVector3();
            ViewBBMax = reader.ReadVector3();

            Flags = (ModelFlags)reader.ReadUInt32();

            BoneCount = reader.ReadInt32();
            BoneIndex = reader.ReadInt32();

            BoneControllerCount = reader.ReadInt32();
            BoneControllerIndex = reader.ReadInt32();

            HitboxCount = reader.ReadInt32();
            HitboxIndex = reader.ReadInt32();

            LocalAnimCount = reader.ReadInt32();
            LocalAnimIndex = reader.ReadInt32();

            LocalSequenceCount = reader.ReadInt32();
            LocalSequenceIndex = reader.ReadInt32();

            ActivityListVersion = reader.ReadInt32();
            EventsIndexed = reader.ReadInt32();

            TextureCount = reader.ReadInt32();
            TextureIndex = reader.ReadInt32();

            TextureDirCount = reader.ReadInt32();
            TextureDirIndex = reader.ReadInt32();

            SkinReferenceCount = reader.ReadInt32();
            SkinFamilyCount = reader.ReadInt32();
            SkinRefIndex = reader.ReadInt32();

            BodyPartCount = reader.ReadInt32();
            BodyPartIndex = reader.ReadInt32();

            AttachmentCount = reader.ReadInt32();
            AttachmentIndex = reader.ReadInt32();

            LocalNodeCount = reader.ReadInt32();
            LocalNodeIndex = reader.ReadInt32();
            LocalNodeNameIndex = reader.ReadInt32();

            FlexDescCount = reader.ReadInt32();
            FlexDescIndex = reader.ReadInt32();

            FlexControllerCount = reader.ReadInt32();
            FlexControllerIndex = reader.ReadInt32();

            FlexRulesCount = reader.ReadInt32();
            FlexRulesIndex = reader.ReadInt32();

            IKChainCount = reader.ReadInt32();
            IKChainIndex = reader.ReadInt32();

            MouthsCount = reader.ReadInt32();
            MouthsIndex = reader.ReadInt32();

            LocalPoseParamCount = reader.ReadInt32();
            LocalPoseParamIndex = reader.ReadInt32();

            reader.JumpTo(0x134);
            SurfacePropIndex = reader.ReadInt32();

            KeyValueIndex = reader.ReadInt32();
            KeyValueCount = reader.ReadInt32();

            IKLockCount = reader.ReadInt32();
            IKLockIndex = reader.ReadInt32();

            Mass = reader.ReadSingle();
            Contents = reader.ReadInt32();

            IncludeModelCount = reader.ReadInt32();
            IncludeModelIndex = reader.ReadInt32();

            VirtualModel = reader.ReadInt32();
            AnimBlocksNameIndex = reader.ReadInt32();

            AnimBlocksCount = reader.ReadInt32();
            AnimBlocksIndex = reader.ReadInt32();

            AnimBlockModel = reader.ReadInt32();
            BoneTableNameIndex = reader.ReadInt32();

            VertexBase = reader.ReadInt32();
            OffsetBase = reader.ReadInt32();
            DirectionalDotProduct = reader.ReadByte();

            RootLOD = reader.ReadByte();
            NumAllowedRootLODs = reader.ReadByte();

            reader.Skip(5);

            FlexControllerUICount = reader.ReadInt32();
            FlexControllerUIIndex = reader.ReadInt32();

            // Read StudioHDR2 Data.
            StudioHDR2Index = reader.ReadInt32();
            
            if (StudioHDR2Index > 0)
            {
                reader.JumpTo(StudioHDR2Index);

                SrcBoneTransformCount = reader.ReadInt32();
                SrcBoneTransformIndex = reader.ReadInt32();

                IllumPositionAttachmentIndex = reader.ReadInt32();
                MaxEyeDeflection = reader.ReadSingle();

                LinearBoneIndex = reader.ReadInt32();
            }
            else
            {
                IllumPositionAttachmentIndex = -1;

                SrcBoneTransformCount = 0;
                SrcBoneTransformIndex = 0;

                MaxEyeDeflection = 0;
                LinearBoneIndex = -1;
            }
        }
    }
}