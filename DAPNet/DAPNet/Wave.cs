using System;
using System.IO;

namespace DAPNet
{
    /// <summary>
    /// Represents a WAV file.
    /// <a href="https://ccrma.stanford.edu/courses/422/projects/WaveFormat/">A brief documentation.</a>.
    /// </summary>
    public class Wave
    {
        /// <summary>
        /// Contains the letters "RIFF" in ASCII form (0x52494646 big-endian form).
        /// </summary>
        private const int RiffchunkIDValue = 0x46464952;
        
        /// <summary>
        /// Contains the letters "WAVE" (0x57415645 big-endian form).
        /// </summary>
        private const int WaveFormatValue = 0x45564157;

        /// <summary>
        /// Contains the letters "fmt " (0x666d7420 big-endian form).
        /// </summary>
        private const int FmtSubchunk1IDValue = 0x20746D66;

        /// <summary>
        /// The size of the rest of the Subchunk1 (Subchunk1Size).
        /// </summary>
        private const int PcmSubchunk1SizeValue = 16;

        /// <summary>
        /// PCM = 1 (i.e. Linear quantization).
        /// </summary>
        private const Int16 PcmAudioFormatValue = 1;

        /// <summary>
        /// Minimal number of channel allowed.
        /// </summary>
        private const int MinNumChannels = 1;

        /// <summary>
        /// Minimal sample rate allowed.
        /// </summary>
        private const int MinSampleRate = 1;
        
        /// <summary>
        /// Contains the letters "data" (0x64617461 big-endian form).
        /// </summary>
        private const int DataSubchunk2IDValue = 0x61746164;

        /// <summary>
        /// Minimal Subchunk2 size allowed.
        /// </summary>
        private const int MinSubchunk2Size = 0;

        /// <summary>
        /// Number of bits in one byte.
        /// </summary>
        private const int BitsInByte = 8;


        /// <summary>
        /// Sample factory for conversion of individual samples while 
        /// reading or writing a WAV file.
        /// </summary>
        private SampleConverterFactory sampleConverterFactory;

        /// <summary>
        /// Holds the value representing wheter the represented WAV is
        /// in consistent state.
        /// </summary>
        private Boolean isConsistent;

        /// <summary>
        /// Holds the size of the Chunk ("ChunkSize").
        /// </summary>
        private int chunkSize;

        /// <summary>
        /// Holds the number of channels ("NumChannels").
        /// </summary>
        private Int16 numChannels;

        /// <summary>
        /// Holds the sample rate ("SampleRate").
        /// </summary>
        private int sampleRate;

        /// <summary>
        /// Holds the byte rate ("ByteRate").
        /// </summary>
        private int byteRate;

        /// <summary>
        /// Holds the align size of blocks ("BlockAlign").
        /// </summary>
        private Int16 blockAlign;

        /// <summary>
        /// Holds the bit depth.
        /// </summary>
        private Int16 bitDepth;
        
        /// <summary>
        /// Holds the number of samples in each channel.
        /// </summary>
        private int numSamplesPerChannel;

        /// <summary>
        /// Holds the size of the Subchunk2 (Subchunk2Size).
        /// </summary>
        private int subchunk2Size;

        /// <summary>
        /// Holds the channles.
        /// </summary>
        private SampleVector[] channels;


        /// <summary>
        /// Creates a new WAV file representation with 
        /// default sample converter factory.
        /// </summary>
        public Wave() : this(new DefaultSampleConverterFactory())
        { 
        }

        /// <summary>
        /// Creates a new WAV file representation with sample converter factory.
        /// </summary>
        /// <param name="sampleConverterFactory">Factory providing sample converters.</param>
        public Wave(SampleConverterFactory sampleConverterFactory)
        {
            this.sampleConverterFactory = sampleConverterFactory;
            isConsistent = false;
            chunkSize = 0;
            numChannels = 0;
            sampleRate = 0;
            byteRate = 0;
            blockAlign = 0;
            bitDepth = 0;
            numSamplesPerChannel = 0;
            subchunk2Size = 0;
        }


        /// <summary>
        /// The number of channles ("NumChannels").
        /// </summary>
        public int NumChannels
        {
            get 
            { 
                return numChannels; 
            }
        }

        /// <summary>
        /// The sample rate ("SampleRate").
        /// </summary>
        public int SampleRate
        {
            get 
            { 
                return sampleRate; 
            }
        }

        /// <summary>
        /// The number of samples in each channel.
        /// </summary>
        public int Length
        {
            get 
            { 
                return numSamplesPerChannel; 
            }
        }

        /// <summary>
        /// The channels.
        /// </summary>
        public SampleVector[] Channels
        {
            get
            {
                return channels;
            }
        }

        /// <summary>
        /// Reverses order of samples in each channels.
        /// </summary>
        public void Reverse()
        {
            foreach (SampleVector channel in channels)
            {
                channel.Reverse();
            }
        }

        /// <summary>
        /// Creates a copy of the WAV file representation.
        /// </summary>
        /// <returns>A new WAV file representation that is a copy of this instance.</returns>
        public Wave Clone()
        {
            Wave clone = new Wave();
            clone.isConsistent = isConsistent;
            clone.chunkSize = chunkSize;
            clone.numChannels = numChannels;
            clone.sampleRate = sampleRate;
            clone.byteRate = byteRate;
            clone.blockAlign = blockAlign;
            clone.bitDepth = bitDepth;
            clone.numSamplesPerChannel = numSamplesPerChannel;
            clone.subchunk2Size = subchunk2Size;

            clone.channels = new SampleVector[numChannels];
            for (int i = 0; i < channels.Length; i++)
            {
                clone.channels[i] = channels[i].Clone();
            }
            return clone;
        }

        /// <summary>
        /// Reads a WAV file from file path into representation.
        /// </summary>
        /// <param name="path">File path of WAV file to read.</param>
        public void Read(string path)
        {
            isConsistent = false;
            BinaryReader waveReader = new BinaryReader(File.Open(path, FileMode.Open));
            ReadRiffChunkDescriptor(waveReader);
            ReadFmtSubchunk(waveReader);
            ReadDataSubchunk(waveReader);
            waveReader.Close();
            isConsistent = true;
        }

        /// <summary>
        /// Writes a WAV file from file path into representation.
        /// </summary>
        /// <param name="path">File path where to write a WAV representation.</param>
        public void Write(string path)
        {
            if (!isConsistent)
            {
                throw new InvalidOperationException("Represented wave is not valid.");
            }

            BinaryWriter waveWriter = new BinaryWriter(File.Open(path, FileMode.Create));
            WriteRiffChunkDescriptor(waveWriter);
            WriteFmtSubchunk(waveWriter);
            WriteDataSubchunk(waveWriter);
            waveWriter.Close();
        }

        /// <summary>
        /// Resets the channels by actual settings.
        /// </summary>
        private void ResetChannels()
        {
            channels = new SampleVector[numChannels];
            for (int i = 0; i < numChannels; i++)
            {
                channels[i] = new SampleVector(numSamplesPerChannel, SampleRate);
            }
        }


        #region Chunk read methods
        /// <summary>
        /// Reads the "RIFF" chunk descriptor of the WAV file from the wave reader.
        /// </summary>
        /// <param name="waveReader">The WAV reader to read the "RIFF" chunk descriptor from.</param>
        private void ReadRiffChunkDescriptor(BinaryReader waveReader)
        {
            ReadChunkID(waveReader);
            ReadChunkSize(waveReader);
            ReadFormat(waveReader);
        }

        /// <summary>
        /// Reads the "fmt" sub-chunk of the WAV file from the wave reader.
        /// </summary>
        /// <param name="waveReader">The WAV reader to read the "fmt" sub-chunk from.</param>
        private void ReadFmtSubchunk(BinaryReader waveReader)
        {
            ReadSubchunk1ID(waveReader);
            ReadSubchunk1Size(waveReader);
            ReadAudioFormat(waveReader);
            ReadNumChannels(waveReader);
            ReadSampleRate(waveReader);
            ReadByteRate(waveReader);
            ReadBlockAllign(waveReader);
            ReadBitsPerSample(waveReader);
        }

        /// <summary>
        /// Reads the "data" sub-chunk of the WAV file from the wave reader.
        /// </summary>
        /// <param name="waveReader">The WAV reader to read the "data" sub-chunk from.</param>
        private void ReadDataSubchunk(BinaryReader waveReader)
        {
            ReadSubchunk2ID(waveReader);
            ReadSubchunk2Size(waveReader);
            ReadSamplesData(waveReader);
        }
        #endregion

        #region Chunk-part read methods
        /// <summary>
        /// Reads the "ChunkID" of the WAV file from the wave reader.
        /// </summary>
        /// <param name="waveReader">The WAV reader to read the "ChunkID" from.</param>
        private void ReadChunkID(BinaryReader waveReader)
        {
            int chunkIDValue = waveReader.ReadInt32();
            if (chunkIDValue != RiffchunkIDValue)
            {
                throw new FormatException("The ChunkID does not contain 'RIFF'.");
            }
        }

        /// <summary>
        /// Reads the "ChunkSize" of the WAV file from the wave reader.
        /// </summary>
        /// <param name="waveReader">The WAV reader to read the "ChunkSize" from.</param>
        private void ReadChunkSize(BinaryReader waveReader)
        {
            chunkSize = waveReader.ReadInt32();
        }

        /// <summary>
        /// Reads the "Format" of the WAV file from the wave reader.
        /// </summary>
        /// <param name="waveReader">The WAV reader to read the "Format" from.</param>
        private void ReadFormat(BinaryReader waveReader)
        {
            int formatValue = waveReader.ReadInt32();
            if (formatValue != WaveFormatValue)
            {
                throw new FormatException("The Format does not contain 'WAVE'.");
            }
        }

        /// <summary>
        /// Reads the "Subchunk1ID" of the WAV file from the wave reader.
        /// </summary>
        /// <param name="waveReader">The WAV reader to read the "Subchunk1ID" from.</param>
        private void ReadSubchunk1ID(BinaryReader waveReader)
        {
            int subchunk1ID = waveReader.ReadInt32();
            if (subchunk1ID != FmtSubchunk1IDValue)
            {
                throw new FormatException("The Subchunk1ID does not contain 'fmt '.");
            }
        }

        /// <summary>
        /// Reads the "Subchunk1Size" of the WAV file from the wave reader.
        /// </summary>
        /// <param name="waveReader">The WAV reader to read the "Subchunk1Size" from.</param>
        private void ReadSubchunk1Size(BinaryReader waveReader)
        {
            int subchunk1Size = waveReader.ReadInt32();
            if (subchunk1Size != PcmSubchunk1SizeValue)
            {
                throw new FormatException("The 'fmt' sub-chunk has not 24 byte size.");
            }
        }

        /// <summary>
        /// Reads the "AudioFormat" of the WAV file from the wave reader.
        /// </summary>
        /// <param name="waveReader">The WAV reader to read the "AudioFormat" from.</param>
        private void ReadAudioFormat(BinaryReader waveReader)
        {
            int audioFormat = waveReader.ReadInt16();
            if (audioFormat != PcmAudioFormatValue)
            {
                throw new FormatException("The audio format is not PCM.");
            }
        }

        /// <summary>
        /// Reads the "NumChannels" of the WAV file from the wave reader.
        /// </summary>
        /// <param name="waveReader">The WAV reader to read the "NumChannels" from.</param>
        private void ReadNumChannels(BinaryReader waveReader)
        {
            numChannels = waveReader.ReadInt16();
            if (numChannels < MinNumChannels)
            {
                throw new FormatException(String.Format("The NumChannels is less than {0}.", MinNumChannels));
            }
        }

        /// <summary>
        /// Reads the "SampleRate" of the WAV file from the wave reader.
        /// </summary>
        /// <param name="waveReader">The WAV reader to read the "SampleRate" from.</param>
        private void ReadSampleRate(BinaryReader waveReader)
        {
            sampleRate = waveReader.ReadInt32();
            if (sampleRate < MinSampleRate)
            {
                throw new FormatException(String.Format("The SampleRate is less than {0}.", MinSampleRate));
            }
        }

        /// <summary>
        /// Reads the "ByteRate" of the WAV file from the wave reader.
        /// </summary>
        /// <param name="waveReader">The WAV reader to read the "ByteRate" from.</param>
        private void ReadByteRate(BinaryReader waveReader)
        {
            byteRate = waveReader.ReadInt32();
        }

        /// <summary>
        /// Reads the "ByteRate" of the WAV file from the wave reader.
        /// </summary>
        /// <param name="waveReader">The WAV reader to read the "ByteRate" from.</param>
        private void ReadBlockAllign(BinaryReader waveReader)
        {
            blockAlign = waveReader.ReadInt16();
        }

        /// <summary>
        /// Reads the "BitsPerSample" of the WAV file from the wave reader.
        /// </summary>
        /// <param name="waveReader">The WAV reader to read the "BitsPerSample" from.</param>
        private void ReadBitsPerSample(BinaryReader waveReader)
        {
            bitDepth = waveReader.ReadInt16();
            SampleConverter sampleConverter = sampleConverterFactory.GetSampleConverter(bitDepth);
            if (sampleConverter == null)
            {
                throw new FormatException("The sample converter for given BitsPerSample is not implemented.");
            }

            float requiredByteRate = sampleRate * numChannels * bitDepth / BitsInByte;
            if (requiredByteRate != byteRate)
            {
                throw new FormatException("The ByteRate is not equal to 'SampleRate * NumChannels * BitsPerSample/8'.");
            }

            float requiredBlockAllign = numChannels * bitDepth / BitsInByte;
            if (requiredBlockAllign != blockAlign)
            {
                throw new FormatException("The BlockAllign is not equal to 'NumChannels * BitsPerSample/8'.");
            }
        }

        /// <summary>
        /// Reads the "Subchunk2ID" of the WAV file from the wave reader.
        /// </summary>
        /// <param name="waveReader">The WAV reader to read the "Subchunk2ID" from.</param>
        private void ReadSubchunk2ID(BinaryReader waveReader)
        {
            int subchunk2IDValue = waveReader.ReadInt32();
            if (subchunk2IDValue != DataSubchunk2IDValue)
            {
                throw new FormatException("The Subchunk2ID does not contain 'data'.");
            }
        }

        /// <summary>
        /// Reads the "Subchunk2Size" of the WAV file from the wave reader.
        /// </summary>
        /// <param name="waveReader">The WAV reader to read the "Subchunk2Size" from.</param>
        private void ReadSubchunk2Size(BinaryReader waveReader)
        {
            subchunk2Size = waveReader.ReadInt32();
            if (subchunk2Size < MinSubchunk2Size)
            {
                throw new FormatException(String.Format("The Subchunk2Size is less than {0}.", MinSubchunk2Size));
            }

            int bytesPerSample = bitDepth / BitsInByte;
            float numSamples = subchunk2Size / bytesPerSample;
            if (numSamples != Math.Floor(numSamples))
            {
                throw new FormatException("The Subchunk2Size is not divisible by bytes per sample.");
            }

            float numSamplesPerChannel = numSamples / numChannels;
            if (numSamplesPerChannel != Math.Floor(numSamplesPerChannel))
            {
                throw new FormatException("Number of samples is not divisible by NumChannels.");
            }
            this.numSamplesPerChannel = (int)numSamplesPerChannel;
        }

        /// <summary>
        /// Reads samples data of the WAV file from the wave reader.
        /// </summary>
        /// <param name="waveReader">The WAV reader to read samples data from.</param>
        private void ReadSamplesData(BinaryReader waveReader)
        {
            SampleConverter sampleConverter = sampleConverterFactory.GetSampleConverter(bitDepth);
            ResetChannels();
            for (int i = 0; i < numSamplesPerChannel; i++)
            {
                for (int j = 0; j < numChannels; j++)
                {
                    channels[j][i] = sampleConverter.ReadSample(waveReader);
                }
            }
        }
        #endregion

        #region Chunk write methods
        /// <summary>
        /// Writes the "RIFF" chunk descriptor of the WAV file to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The WAV writer to write the "RIFF" chunk descriptor to.</param>
        private void WriteRiffChunkDescriptor(BinaryWriter waveWriter)
        {
            WriteChunkID(waveWriter);
            WriteChunkSize(waveWriter);
            WriteFormat(waveWriter);
        }

        /// <summary>
        /// Writes the "fmt" sub-chunk of the WAV file to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The WAV writer to write the "fmt" sub-chunk to.</param>
        private void WriteFmtSubchunk(BinaryWriter waveWriter)
        {
            WriteSubchunk1ID(waveWriter);
            WriteSubchunk1Size(waveWriter);
            WriteAudioFormat(waveWriter);
            WriteNumChannels(waveWriter);
            WriteSampleRate(waveWriter);
            WriteByteRate(waveWriter);
            WriteBlockAlign(waveWriter);
            WriteBitsPerSample(waveWriter);
        }

        /// <summary>
        /// Writes the "data" sub-chunk of the WAV file to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The WAV writer to write the "data" sub-chunk to.</param>
        private void WriteDataSubchunk(BinaryWriter waveWriter)
        {
            WriteSubchunk2ID(waveWriter);
            WriteSubchunk2Size(waveWriter);
            WriteSamplesData(waveWriter);
        }
        #endregion

        #region Chunk-part write methods
        /// <summary>
        /// Writes the "ChunkID" of the WAV file to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The WAV writer to write the "ChunkID" to.</param>
        private void WriteChunkID(BinaryWriter waveWriter)
        {
            waveWriter.Write(RiffchunkIDValue);
        }

        /// <summary>
        /// Writes the "ChunkSize" of the WAV file to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The WAV writer to write the "ChunkSize" to.</param>
        private void WriteChunkSize(BinaryWriter waveWriter)
        {
            waveWriter.Write(chunkSize);
        }

        /// <summary>
        /// Writes the "Format" of the WAV file to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The WAV writer to write the "Format" to.</param>
        private void WriteFormat(BinaryWriter waveWriter)
        {
            waveWriter.Write(WaveFormatValue);
        }


        /// <summary>
        /// Writes the "Subchunk1ID" of the WAV file to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The WAV writer to write the "Subchunk1ID" to.</param>
        private void WriteSubchunk1ID(BinaryWriter waveWriter)
        {
            waveWriter.Write(FmtSubchunk1IDValue);
        }

        /// <summary>
        /// Writes the "Subchunk1Size" of the WAV file to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The WAV writer to write the "Subchunk1Size" to.</param>
        private void WriteSubchunk1Size(BinaryWriter waveWriter)
        {
            waveWriter.Write(PcmSubchunk1SizeValue);
        }

        /// <summary>
        /// Writes the "AudioFormat" of the WAV file to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The WAV writer to write the "AudioFormat" to.</param>
        private void WriteAudioFormat(BinaryWriter waveWriter)
        {
            waveWriter.Write(PcmAudioFormatValue);
        }

        /// <summary>
        /// Writes the "NumChannels" of the WAV file to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The WAV writer to write the "NumChannels" to.</param>
        private void WriteNumChannels(BinaryWriter waveWriter)
        {
            waveWriter.Write(numChannels);
        }

        /// <summary>
        /// Writes the "SampleRate" of the WAV file to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The WAV writer to write the "SampleRate" to.</param>
        private void WriteSampleRate(BinaryWriter waveWriter)
        {
            waveWriter.Write(sampleRate);
        }

        /// <summary>
        /// Writes the "ByteRate" of the WAV file to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The WAV writer to write the "ByteRate" to.</param>
        private void WriteByteRate(BinaryWriter waveWriter)
        {
            waveWriter.Write(byteRate);
        }

        /// <summary>
        /// Writes the "BlockAlign" of the WAV file to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The WAV writer to write the "BlockAlign" to.</param>
        private void WriteBlockAlign(BinaryWriter waveWriter)
        {
            waveWriter.Write(blockAlign);
        }

        /// <summary>
        /// Writes the "BitsPerSample" of the WAV file to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The WAV writer to write the "BitsPerSample" to.</param>
        private void WriteBitsPerSample(BinaryWriter waveWriter)
        {
            waveWriter.Write(bitDepth);
        }


        /// <summary>
        /// Writes the "Subchunk2ID" of the WAV file to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The WAV writer to write the "Subchunk2ID" to.</param>
        private void WriteSubchunk2ID(BinaryWriter waveWriter)
        {
            waveWriter.Write(DataSubchunk2IDValue);
        }

        /// <summary>
        /// Writes the "Subchunk2Size" of the WAV file to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The WAV writer to write the "Subchunk2Size" to.</param>
        private void WriteSubchunk2Size(BinaryWriter waveWriter)
        {
            waveWriter.Write(subchunk2Size);
        }

        /// <summary>
        /// Writes samples data of the WAV file to the wave writer.
        /// </summary>
        /// <param name="waveWriter">The WAV writer to write samples data to.</param>
        private void WriteSamplesData(BinaryWriter waveWriter)
        {
            SampleConverter sampleFilter = sampleConverterFactory.GetSampleConverter(bitDepth);
            for (int i = 0; i < numSamplesPerChannel; i++)
            {
                for (int j = 0; j < numChannels; j++)
                {
                    sampleFilter.WriteSample(waveWriter, channels[j][i]);
                }
            }
        }
        #endregion
    }
}
