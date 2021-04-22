using System;
using System.Linq;
using Network.Model;
using Network.Model.Layers;
using Network.Model.WeightsInitializers;
using Network.NeuralMath;
using Network.NeuralMath.Cpu;

namespace GeneticAlgorithm.Individuals
{
    public class NeuralNetworkComponent
    {
        private readonly NeuralLayeredNetwork _network;
        private readonly TensorBuilder _inputBuilder;
        
        public NeuralNetworkComponent(int inputSize)
        {
            _inputBuilder = new CpuBuilder();
            _network = CreateNetwork(inputSize);
        }

        public float[] NeuronsValues
        {
            get
            {
                return _network.Layers
                    .OfType<IParameterizedLayer>()
                    .SelectMany(pl => pl.ParametersStorage.Weights.Storage.Data)
                    .ToArray();
            }
            set
            {
                if (value is null)
                    throw new ArgumentNullException(nameof(value));
                
                int offset = 0;
                foreach (var layer in _network.Layers.OfType<IParameterizedLayer>())
                {
                    var weightsTensor = layer.ParametersStorage.Weights;
                    for (int i = 0; i < weightsTensor.Size; i++)
                    {
                        weightsTensor[i] = value[offset + i];
                    }
                    offset += weightsTensor.Size;
                }
            }
        }

        private NeuralLayeredNetwork CreateNetwork(int inputSize)
        {
            var init = new XavierInitializer();
            return new NeuralLayeredNetwork(Shape.ForVector(inputSize))
                .Fully(64, init)
                .Relu()
                .Fully(4, init)
                .Softmax();
        }

        public float[] Process(float[] inputData)
        {
            if (inputData is null)
                throw new ArgumentNullException(nameof(inputData));
            
            var inputTensor = _inputBuilder.OfStorage(new CpuStorage(Shape.ForVector(inputData.Length), inputData));
            var outputTensor = _network.Forward(inputTensor);
            return outputTensor.Storage.Data;
        }
        
    }
}