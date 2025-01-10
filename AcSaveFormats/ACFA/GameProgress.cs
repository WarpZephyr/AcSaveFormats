using BinaryMemory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace AcSaveFormats.ACFA
{
    public class GameProgress
    {
        #region Constants

        public const int MissionCount = 42;
        public const int CollaredMatchCount = 30;
        public const int OrcaMatchCount = 12;
        public const int MatchCount = 48;
        public const int DataPackCount = 16;
        public const int ReservedPartUnlockCount = 1024;
        public const int ReservedDesignUnlockCount = 100;
        public const int ReservedEmblemUnlockCount = 64;
        public const int ReservedFrsUnlockCount = 248;

        #endregion

        #region Properties

        public ushort GameCompletions { get; set; }
        public bool ShowIntro { get; set; }
        public byte Unk03 { get; set; }
        public List<ushort> StoryMissionIDs { get; set; }
        public List<ushort> FreeMissionIDs { get; set; }
        public MissionRank[] NormalRankings { get; private set; }
        public MissionRank[] HardRankings { get; private set; }
        public bool[] MatchesWon { get; private set; }
        public bool[] DataPacksUnlocked { get; private set; }
        public List<PartUnlock> PartUnlocks { get; set; }
        public List<ushort> DesignUnlocks { get; set; }
        public List<byte> EmblemUnlocks { get; set; }
        public int FrsAmount { get; set; }
        public List<byte> FrsUnlocks { get; set; }
        public int Unk1B5C { get; set; }

        #endregion

        #region Constructors

        public GameProgress()
        {
            GameCompletions = 0;
            ShowIntro = true;
            Unk03 = 0;
            StoryMissionIDs = new List<ushort>();
            FreeMissionIDs = new List<ushort>();
            NormalRankings = new MissionRank[MissionCount];
            HardRankings = new MissionRank[MissionCount];
            MatchesWon = new bool[MatchCount];
            DataPacksUnlocked = new bool[DataPackCount];
            PartUnlocks = new List<PartUnlock>();
            DesignUnlocks = new List<ushort>();
            EmblemUnlocks = new List<byte>();
            FrsAmount = 0;
            FrsUnlocks = new List<byte>();
            Unk1B5C = 0x63743E0D; // No idea.
        }

        internal GameProgress(BinaryStreamReader br)
        {
            GameCompletions = br.ReadUInt16();
            ShowIntro = br.ReadBoolean();
            Unk03 = br.ReadByte();

            // Story Mission IDs
            int storyCount = br.ReadInt32();
            if ((uint)storyCount > MissionCount)
                throw new InvalidDataException($"Invalid story mission count: {storyCount}; Max: {MissionCount}");
            StoryMissionIDs = new List<ushort>(storyCount);
            for (int i = 0; i < storyCount; i++)
                StoryMissionIDs.Add(br.ReadUInt16());
            int skip = MissionCount - storyCount;
            if (skip > 0)
                br.Position += skip * sizeof(ushort); // Skip unused ID space

            // Free Mission IDs
            int freeCount = br.ReadInt32();
            if ((uint)freeCount > MissionCount)
                throw new InvalidDataException($"Invalid free mission count: {freeCount}; Max: {MissionCount}");
            FreeMissionIDs = new List<ushort>(freeCount);
            for (int i = 0; i < freeCount; i++)
                FreeMissionIDs.Add(br.ReadUInt16());
            skip = MissionCount - freeCount;
            if (skip > 0)
                br.Position += skip * sizeof(ushort); // Skip unused ID space

            // Normal Rankings
            NormalRankings = new MissionRank[MissionCount];
            for (int i = 0;i < MissionCount; i++)
                NormalRankings[i] = br.ReadEnumSByte<MissionRank>();

            // Hard Rankings
            HardRankings = new MissionRank[MissionCount];
            for (int i = 0; i < MissionCount; i++)
                HardRankings[i] = br.ReadEnumSByte<MissionRank>();

            // Matches
            MatchesWon = br.ReadBooleans(MatchCount);

            // Data Packs
            DataPacksUnlocked = br.ReadBooleans(DataPackCount);

            // Part Unlocks
            int partUnlockCount = br.ReadInt32();
            if ((uint)partUnlockCount > ReservedPartUnlockCount)
                throw new InvalidDataException($"Invalid unlocked part count: {partUnlockCount}; Max: {ReservedPartUnlockCount}");
            PartUnlocks = new List<PartUnlock>(partUnlockCount);
            for (int i = 0; i < partUnlockCount; i++)
                PartUnlocks.Add(new PartUnlock(br));
            skip = ReservedPartUnlockCount - partUnlockCount;
            if (skip > 0)
                br.Position += skip * Unsafe.SizeOf<PartUnlock>(); // Skip unused part unlock space

            // Design Unlocks
            int designCount = br.ReadInt32();
            if ((uint)designCount > ReservedDesignUnlockCount)
                throw new InvalidDataException($"Invalid unlocked design count: {designCount}; Max: {ReservedDesignUnlockCount}");
            DesignUnlocks = new List<ushort>(ReservedDesignUnlockCount);
            for (int i = 0; i < designCount; i++)
                DesignUnlocks.Add(br.ReadUInt16());
            skip = ReservedDesignUnlockCount - designCount;
            if (skip > 0)
                br.Position += skip * sizeof(ushort); // Skip unused ID space

            // Emblem Unlocks
            int emblemCount = br.ReadInt32();
            if ((uint)emblemCount > ReservedEmblemUnlockCount)
                throw new InvalidDataException($"Invalid unlocked emblem count: {emblemCount}; Max: {ReservedEmblemUnlockCount}");
            EmblemUnlocks = new List<byte>(ReservedEmblemUnlockCount);
            for (int i = 0; i < emblemCount; i++)
                EmblemUnlocks.Add(br.ReadByte());
            skip = ReservedEmblemUnlockCount - emblemCount;
            if (skip > 0)
                br.Position += skip; // Skip unused ID space

            // FRS Amount
            FrsAmount = br.ReadInt32();

            // FRS Unlocks
            int frsUnlockCount = br.ReadInt32();
            if ((uint)frsUnlockCount > ReservedFrsUnlockCount)
                throw new InvalidDataException($"Invalid unlocked FRS count: {frsUnlockCount}; Max: {ReservedFrsUnlockCount}");
            FrsUnlocks = new List<byte>(ReservedFrsUnlockCount);
            for (int i = 0; i < frsUnlockCount; i++)
                FrsUnlocks.Add(br.ReadByte());
            skip = ReservedFrsUnlockCount - frsUnlockCount;
            if (skip > 0)
                br.Position += skip; // Skip unused ID space

            // Unknown
            Unk1B5C = br.ReadInt32();
        }

        #endregion

        #region Read

        public static GameProgress Read(string path)
        {
            using var br = new BinaryStreamReader(path, true);
            return new GameProgress(br);
        }

        public static GameProgress Read(byte[] bytes)
        {
            using var br = new BinaryStreamReader(bytes, true);
            return new GameProgress(br);
        }

        #endregion

        #region Write

        internal void Write(BinaryStreamWriter bw)
        {
            bw.WriteUInt16(GameCompletions);
            bw.WriteBoolean(ShowIntro);
            bw.WriteByte(Unk03);

            // Story Mission IDs
            if ((uint)StoryMissionIDs.Count > MissionCount)
                throw new InvalidOperationException($"Invalid story mission count: {StoryMissionIDs.Count}; Max: {MissionCount}");
            bw.WriteInt32(StoryMissionIDs.Count);
            foreach (ushort id in StoryMissionIDs)
                bw.WriteUInt16(id);
            int remaining = MissionCount - StoryMissionIDs.Count;
            if (remaining > 0)
                bw.WriteBytePattern(remaining * sizeof(ushort), 0); // Write unused ID space

            // Free Mission IDs
            if ((uint)FreeMissionIDs.Count > MissionCount)
                throw new InvalidOperationException($"Invalid free mission count: {FreeMissionIDs.Count}; Max: {MissionCount}");
            bw.WriteInt32(FreeMissionIDs.Count);
            foreach (ushort id in FreeMissionIDs)
                bw.WriteUInt16(id);
            remaining = MissionCount - FreeMissionIDs.Count;
            if (remaining > 0)
                bw.WriteBytePattern(remaining * sizeof(ushort), 0); // Write unused ID space

            // Normal Rankings
            for (int i = 0; i < MissionCount; i++)
                bw.WriteSByte((sbyte)NormalRankings[i]);

            // Hard Rankings
            for (int i = 0; i < MissionCount; i++)
                bw.WriteSByte((sbyte)HardRankings[i]);

            // Matches
            bw.WriteBooleans(MatchesWon);

            // Data Packs
            bw.WriteBooleans(DataPacksUnlocked);

            // Part Unlocks
            if ((uint)PartUnlocks.Count > ReservedPartUnlockCount)
                throw new InvalidOperationException($"Invalid unlocked part count: {PartUnlocks.Count}; Max: {ReservedPartUnlockCount}");
            bw.WriteInt32(PartUnlocks.Count);
            foreach (PartUnlock part in PartUnlocks)
                part.Write(bw);
            remaining = ReservedPartUnlockCount - PartUnlocks.Count;
            if (remaining > 0)
                bw.WriteBytePattern(remaining * Unsafe.SizeOf<PartUnlock>(), 0); // Write unused ID space

            // Design Unlocks
            if ((uint)DesignUnlocks.Count > ReservedDesignUnlockCount)
                throw new InvalidOperationException($"Invalid unlocked design count: {DesignUnlocks.Count}; Max: {ReservedDesignUnlockCount}");
            bw.WriteInt32(DesignUnlocks.Count);
            foreach (ushort id in DesignUnlocks)
                bw.WriteUInt16(id);
            remaining = ReservedDesignUnlockCount - DesignUnlocks.Count;
            if (remaining > 0)
                bw.WriteBytePattern(remaining * sizeof(ushort), 0); // Write unused ID space

            // Emblem Unlocks
            if ((uint)EmblemUnlocks.Count > ReservedEmblemUnlockCount)
                throw new InvalidOperationException($"Invalid unlocked emblem count: {EmblemUnlocks.Count}; Max: {ReservedEmblemUnlockCount}");
            bw.WriteInt32(EmblemUnlocks.Count);
            foreach (byte id in EmblemUnlocks)
                bw.WriteByte(id);
            remaining = ReservedEmblemUnlockCount - EmblemUnlocks.Count;
            if (remaining > 0)
                bw.WriteBytePattern(remaining, 0); // Write unused ID space

            // FRS Amount
            bw.WriteInt32(FrsAmount);

            // FRS Unlocks
            if ((uint)FrsUnlocks.Count > ReservedFrsUnlockCount)
                throw new InvalidOperationException($"Invalid unlocked FRS count: {FrsUnlocks.Count}; Max: {ReservedFrsUnlockCount}");
            bw.WriteInt32(FrsUnlocks.Count);
            foreach (byte id in FrsUnlocks)
                bw.WriteByte(id);
            remaining = ReservedFrsUnlockCount - FrsUnlocks.Count;
            if (remaining > 0)
                bw.WriteBytePattern(remaining, 0); // Write unused ID space

            // Unknown
            bw.WriteInt32(Unk1B5C);
        }

        public void Write(string path)
        {
            using var bw = new BinaryStreamWriter(path, true);
            Write(bw);
        }

        public byte[] Write()
        {
            using var bw = new BinaryStreamWriter(true);
            Write(bw);
            return bw.ToArray();
        }

        #endregion

        #region Enums

        public enum MissionRank : sbyte
        {
            None = -1,
            E = 0,
            D = 1,
            C = 2,
            B = 3,
            A = 4,
            S = 5
        }

        #endregion

        #region Structs

        public struct PartUnlock
        {
            public PartCategory Category { get; set; }
            public byte Unk01 { get; set; }
            public ushort PartID { get; set; }
            public PartUnlockStatus UnlockStatus { get; set; }

            public PartUnlock()
            {
                
            }

            internal PartUnlock(BinaryStreamReader br)
            {
                Category = br.ReadEnumByte<PartCategory>();
                Unk01 = br.ReadByte();
                PartID = br.ReadUInt16();
                UnlockStatus = br.ReadEnumUInt16<PartUnlockStatus>();
            }

            internal readonly void Write(BinaryStreamWriter bw)
            {
                bw.WriteByte((byte)Category);
                bw.WriteByte(Unk01);
                bw.WriteUInt16(PartID);
                bw.WriteUInt16((ushort)UnlockStatus);
            }

            #region Enums

            public enum PartUnlockStatus : ushort
            {
                Hidden = 0,
                InShop = 4096,
                Owned = 8192
            }

            public enum PartCategory : byte
            {
                Head = 0,
                Core = 1,
                Arms = 2,
                Legs = 3,
                FCS = 4,
                Generator = 5,
                MainBooster = 6,
                BackBooster = 7,
                SideBooster = 8,
                OveredBooster = 9,
                RightArmUnit = 10,
                LeftArmUnit = 11,
                RightBackUnit = 12,
                LeftBackUnit = 13,
                ShoulderUnit = 14,
                StabilizerHeadTop = 17,
                StabilizerHeadRight = 18,
                StabilizerHeadLeft = 19,
                StabilizerCoreUpperRight = 20,
                StabilizerCoreUpperLeft = 21,
                StabilizerCoreLowerRight = 22,
                StabilizerCoreLowerLeft = 23,
                StabilizerArmRight = 24,
                StabilizerArmLeft = 25,
                StabilizerLegsBack = 26,
                StabilizerLegsUpperRight = 27,
                StabilizerLegsUpperLeft = 28,
                StabilizerLegsUpperRightBack = 29,
                StabilizerLegsUpperLeftBack = 30,
                StabilizerLegsMiddleRight = 31,
                StabilizerLegsMiddleLeft = 32,
                StabilizerLegsMiddleRightBack = 33,
                StabilizerLegsMiddleLeftBack = 34,
                StabilizerLegsLowerRight = 35,
                StabilizerLegsLowerLeft = 36,
                StabilizerLegsLowerRightBack = 37,
                StabilizerLegsLowerLeftBack = 38,
            }

            #endregion
        }

        #endregion
    }
}
