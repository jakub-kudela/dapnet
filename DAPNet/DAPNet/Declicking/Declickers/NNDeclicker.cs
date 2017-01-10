using System;
using System.Collections.Generic;
using NeuronDotNet.Core;
using NeuronDotNet.Core.Backpropagation;
using System.Diagnostics;

namespace DAPNet
{
    /// <summary>
    /// Represent neural network declicker.
    /// </summary>
    public class NNDeclicker : Declicker
    {
        /// <summary>
        /// Holds the default order.
        /// </summary>
        public const int DefaultOrder = 20;

        /// <summary>
        /// Holds the default block size.
        /// </summary>
        public const int DefaultBlockSize = 512;

        /// <summary>
        /// Holds the default number of training epochs.
        /// </summary>
        public const int DefaultTrainingEpochs = 100;

        /// <summary>
        /// Holds the default hidden neuron count.
        /// </summary>
        public const int DefaultHiddenNeuronCount = 5;

        /// <summary>
        /// Holds the default learning rate.
        /// </summary>
        public const double DefaultLearningRate = 0.1d;


        /// <summary>
        /// Holds the output neuron count.
        /// </summary>
        private const int OutputNeuronCount = 1;

        /// <summary>
        /// Holds the minimal value in terms of neural netowrk.
        /// </summary>
        private const double MinNNValue = 0.0d;

        /// <summary>
        /// Holds the maximal value in terms of neural netowrk.
        /// </summary>
        private const double MaxNNValue = 1.0d;


        /// <summary>
        /// Holds the backpropagation neural network.
        /// </summary>
        private BackpropagationNetwork network;

        /// <summary>
        /// Holds the order.
        /// </summary>
        private int order;

        /// <summary>
        /// Holds the number of training epochs.
        /// </summary>
        private int trainingEpochs;

        /// <summary>
        /// Holds the number of hidden neuron count.
        /// </summary>
        private int hiddenNeuronCount;

        /// <summary>
        /// Holds the learning rate.
        /// </summary>
        private double learningRate;
        
        /// <summary>
        /// Holds the block size.
        /// </summary>
        private int blockSize;


        /// <summary>
        /// Creates a new neural network declicker with default settings.
        /// </summary>
        public NNDeclicker() : this(DefaultOrder, DefaultTrainingEpochs, DefaultHiddenNeuronCount, DefaultLearningRate, DefaultBlockSize)
        { 
        }

        /// <summary>
        /// Creates a new neural network declicker.
        /// </summary>
        /// <param name="order">Order of the new neural network declicker.</param>
        public NNDeclicker(int order) : this(order, DefaultTrainingEpochs, DefaultHiddenNeuronCount, DefaultLearningRate, DefaultBlockSize)
        { 
        }
        
        /// <summary>
        /// Creates a new neural network declicker.
        /// </summary>
        /// <param name="order">Order of the new neural network declicker.</param>
        /// <param name="blockSize">Block size of the new neural network declicker.</param>
        public NNDeclicker(int order, int blockSize) : this(order, DefaultTrainingEpochs, DefaultHiddenNeuronCount, DefaultLearningRate, blockSize)
        {
        }

        /// <summary>
        /// Creates a new neural network declicker.
        /// </summary>
        /// <param name="order">Order of the new neural network declicker.</param>
        /// <param name="trainingEpochs">Number of training epochs of the new neural network declicker.</param>
        /// <param name="hiddenNeuronCount">Number of hidden neurons of the new neural network declicker.</param>
        /// <param name="learningRate">Learning rate of the new neural network declicker.</param>
        /// <param name="blockSize">Block size of the new neural network declicker.</param>
        public NNDeclicker(int order, int trainingEpochs, int hiddenNeuronCount, double learningRate, int blockSize)
        {
            this.order = order;
            this.trainingEpochs = trainingEpochs;
            this.hiddenNeuronCount = hiddenNeuronCount;
            this.learningRate = learningRate;
            this.blockSize = blockSize;
            ResetNetwork();
        }

        /// <summary>
        /// Order.
        /// </summary>
        public int Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
                ResetNetwork();
            }
        }
 
        /// <summary>
        /// Number of traning epochs.
        /// </summary>
        public int TrainingEpochs
        {
            get
            {
                return trainingEpochs;
            }
            set
            {
                trainingEpochs = value;
            }
        }

        /// <summary>
        /// Number of hidden neurons.
        /// </summary>
        public int HiddenNeuronCount
        {
            get
            {
                return hiddenNeuronCount;
            }
            set
            {
                hiddenNeuronCount = value;
                ResetNetwork();
            }
        }

        /// <summary>
        /// Learning rate.
        /// </summary>
        public double LearningRate
        {
            get
            {
                return learningRate;
            }
            set
            {
                learningRate = value;
                ResetNetwork();
            }
        }

        /// <summary>
        /// Processing block size.
        /// </summary>
        public int BlockSize
        {
            get
            {
                return blockSize;
            }
            set
            {
                blockSize = value;
            }
        }

        /// <summary>
        /// Gets excitations out of specified samples.
        /// </summary>
        /// <param name="samples">Samples to get excitations of.</param>
        /// <returns>Excitations of specified samples.</returns>
        public override SampleVector GetExcitations(SampleVector samples)
        {
            SampleVector excitations = new SampleVector(samples.Count);
            for (int i = 0; i < samples.Count; i += BlockSize)
            {
                int actualBlockSize = Math.Min(BlockSize, samples.Count - i);
                SampleVector blockExcitations = GetBlockExcitations(samples, i, actualBlockSize);
                excitations.Replace(blockExcitations, i, actualBlockSize);
            }
            return excitations;
        }

        /// <summary>
        /// Corrects specified degraded samples.
        /// </summary>
        /// <param name="samples">Degraded samples to correct.</param>
        /// <param name="detection">Detection of degraded samples to correct.</param>
        public override void Correct(SampleVector samples, DetectionVector detection)
        {
            int[] g = GetG(detection);
            int[] o = GetO(detection);
            SampleVector f = GetF(samples, detection);
            SampleVector r = GetR(samples, detection);
            for (int i = 0; i < samples.Count; i++)
            {
                if (detection[i])
                {
                    double v_i = (double)o[i] / (g[i] + 1);
                    samples[i] = (1 - v_i) * f[i] + v_i * r[i];
                }
            }
        }

        /// <summary>
        /// Gets a forward correction.
        /// </summary>
        /// <param name="samples">Degraded samples to correct.</param>
        /// <param name="detection">Detection of degraded samples.</param>
        /// <returns>Forward corrected samples.</returns>
        internal SampleVector GetF(SampleVector samples, DetectionVector detection)
        {
            SampleVector f = samples.Clone();
            for (int i = 0; i < samples.Count; i += BlockSize)
            {
                int actualBlockSize = Math.Min(BlockSize, samples.Count - i);
                if (detection.ContainsFlag(true, i, actualBlockSize))
                {
                    SampleVector blockCorrections = GetBlockCorrections(f, detection, i, actualBlockSize);
                    f.Replace(blockCorrections, i, actualBlockSize);
                }
            }
            return f;
        }

        /// <summary>
        /// Gets a backward correction.
        /// </summary>
        /// <param name="samples">Degraded samples to correct.</param>
        /// <param name="detection">Detection of degraded samples.</param>
        /// <returns>Backward corrected samples.</returns>
        internal SampleVector GetR(SampleVector samples, DetectionVector detection)
        {
            samples.Reverse();
            detection.Reverse();
            SampleVector r = GetF(samples, detection);
            r.Reverse();
            samples.Reverse();
            detection.Reverse();
            return r;
        }

        /// <summary>
        /// Gets excitations for specified block of samples of a specified interval.
        /// </summary>
        /// <param name="samples">Degraded samples including a block to get excitations of.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>Excitations for the specified block.</returns>
        internal SampleVector GetBlockExcitations(SampleVector samples, int index, int count)
        {
            TryTrainNetwork(samples, index, count);
            List<double> blockExcitations = new List<double>();
            for (int i = index; i < index + count; i++)
            {
                double[] inputVector = BlockToNNValues(samples, i - Order, Order);
                double[] outputVector = network.Run(inputVector);
                blockExcitations.Add(samples[i] - SampleConverter.ToSample(outputVector[0], MinNNValue, MaxNNValue));
                
            }
            return new SampleVector(blockExcitations.ToArray());
        }

        /// <summary>
        /// Gets excitations for specified block of samples of a specified interval.
        /// </summary>
        /// <param name="samples">Degraded samples including a block to get excitations of.</param>
        /// <param name="detection">Detection of degraded samples.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>Excitations for the specified block.</returns>
        internal SampleVector GetBlockExcitations(SampleVector samples, DetectionVector detection, int index, int count)
        {
            TryTrainNetwork(samples, detection, index, count);
            List<double> blockExcitation = new List<double>();
            for (int i = index; i < index + count; i++)
            {
                double[] inputVector = BlockToNNValues(samples, i - Order, Order);
                double[] outputVector = network.Run(inputVector);
                blockExcitation.Add(samples[i] - SampleConverter.ToSample(outputVector[0], MinNNValue, MaxNNValue));
            }
            return new SampleVector(blockExcitation.ToArray());
        }

        /// <summary>
        /// Gets corrections for specified block of samples of a specified interval.
        /// </summary>
        /// <param name="samples">Degraded samples including a block to get corrections of.</param>
        /// <param name="detection">Detection of degraded samples.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>Corrections for the specified block.</returns>
        internal SampleVector GetBlockCorrections(SampleVector samples, DetectionVector detection, int index, int count)
        {
            TryTrainNetwork(samples, detection, index, count);
            SampleVector blockCorrections = samples.Take(index - Order, count + Order);
            DetectionVector blockDetection = detection.Take(index - Order, count + Order);
            for (int i = Order; i < count + Order; i++)
            {
                if (blockDetection[i])
                {
                    double[] inputVector = BlockToNNValues(blockCorrections, i - Order, Order);
                    double[] outputVector = network.Run(inputVector);
                    blockCorrections[i] = SampleConverter.ToSample(outputVector[0], MinNNValue, MaxNNValue);      
                }
            }
            return blockCorrections.Take(Order, count);
        }

        /// <summary>
        /// Tries to trains the neural network for the block.
        /// </summary>
        /// <param name="samples">Degraded samples including block upon which to train.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>true if training was succesful, false if not.</returns>
        private bool TryTrainNetwork(SampleVector samples, int index, int count)
        {
            TrainingSet trainingSet = new TrainingSet(Order, OutputNeuronCount);
            for (int i = index; i < index + count; i++)
            {
                double[] inputVector = BlockToNNValues(samples, i - Order, Order);
                double[] outputVector = BlockToNNValues(samples, i, OutputNeuronCount);
                trainingSet.Add(new TrainingSample(inputVector, outputVector));
            }

            if (trainingSet.TrainingSampleCount == 0)
            {
                return false;
            }

            network.Learn(trainingSet, TrainingEpochs);
            return true;
        }

        /// <summary>
        /// Tries to the neural network for the block.
        /// </summary>
        /// <param name="samples">Degraded samples including block upon which to train.</param>
        /// <param name="detection">Detection of degraded samples.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>true if training was succesful, false if not.</returns>
        private bool TryTrainNetwork(SampleVector samples, DetectionVector detection, int index, int count)
        {
            TrainingSet trainingSet = new TrainingSet(Order, OutputNeuronCount);
            for (int i = index; i < index + count; i++)
            {
                if (!detection.ContainsFlag(true, i - Order, Order + OutputNeuronCount))
                {
                    double[] inputVector = BlockToNNValues(samples, i - Order, Order);
                    double[] outputVector = BlockToNNValues(samples, i, OutputNeuronCount);
                    trainingSet.Add(new TrainingSample(inputVector, outputVector));
                }
            }

            if (trainingSet.TrainingSampleCount == 0)
            {
                return false;
            }

            network.Learn(trainingSet, TrainingEpochs);
            return true;
        }

        /// <summary>
        /// Resets the neural network.
        /// </summary>
        private void ResetNetwork()
        {
            LinearLayer inputLayer = new LinearLayer(Order);
            SigmoidLayer hiddenLayer = new SigmoidLayer(hiddenNeuronCount);
            SigmoidLayer outputLayer = new SigmoidLayer(OutputNeuronCount);
            new BackpropagationConnector(inputLayer, hiddenLayer);
            new BackpropagationConnector(hiddenLayer, outputLayer);
            network = new BackpropagationNetwork(inputLayer, outputLayer);
            network.SetLearningRate(LearningRate);
        }


        /// <summary>
        /// Converts a block of samples to an array of corresponding 
        /// values in terms of neural network.
        /// </summary>
        /// <param name="samples">Samples to convert.</param>
        /// <param name="index">Starting index of the block.</param>
        /// <param name="count">Number of samples of the block.</param>
        /// <returns>Array of corresponding values in terms of neural network.</returns>
        private double[] BlockToNNValues(SampleVector samples, int index, int count)
        {
            List<double> nnValues = new List<double>();
            for (int i = index; i < index + count; i++)
            {
                nnValues.Add(SampleConverter.ToValue(samples[i], MinNNValue, MaxNNValue));
            }
            return nnValues.ToArray();
        }
    }
}
