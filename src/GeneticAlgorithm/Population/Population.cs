using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GeneticAlgorithm.FitnessFunctions;
using GeneticAlgorithm.Individuals;
using GeneticAlgorithm.Operators.Crossover;
using GeneticAlgorithm.Operators.Mutation;
using GeneticAlgorithm.Operators.Selection;

namespace GeneticAlgorithm.Population
{
    public class Population : IPopulation
    {
        private readonly int _populationSize;
        private readonly int _matingPoolSize;
        private readonly float _mutationRate;
        private readonly int _offspringSize;
        private readonly int _generationsCount;

        private int _generation = 1;
        
        private readonly ICrossoverOperator _crossoverOperator;
        private readonly ISelectionOperator _selectionOperator;
        private readonly IMutationOperator _mutationOperator;
        
        public List<Individual> Individuals { get; private set; }
        public List<Individual> MatingPool { get; private set; }
        
        public float MaxScore => Individuals.Select(i => i.Score).Max();

        public event Func<PopulationState, Task> GenerationCompleted;  
        
        public Population(PopulationSettings settings, ICrossoverOperator crossoverOperator, ISelectionOperator selectionOperator, IMutationOperator mutationOperator, Func<Individual> createIndividualDelegate)
        {
            _ = settings ?? throw new ArgumentNullException(nameof(settings));
            
            _mutationRate = settings.MutationRate;
            _matingPoolSize = settings.MatingPoolSize;
            _populationSize = settings.PopulationSize;
            _offspringSize = settings.OffspringSize;
            _generationsCount = settings.GenerationsCount;
            
            _crossoverOperator = crossoverOperator;
            _selectionOperator = selectionOperator;
            _mutationOperator = mutationOperator;

            MatingPool = new List<Individual>();
            Individuals = new List<Individual>();
            
            for (int i = 0; i < _populationSize; i++)
            {
                var individual = createIndividualDelegate.Invoke();
                Individuals.Add(individual);
            }
        }

        private PopulationState State => new()
            {
                Generation = _generation,
                HighScore = MaxScore,
                HighRatedIndividuals = MatingPool
            }; 

        public async Task EvolveAsync(IFitnessFunction mainFitnessFunction, IFitnessFunction offspringFitnessFunction, CancellationToken token)
        {
            for (; _generation <= _generationsCount; _generation++)
            {
                AdjustIdentifiers();
                
                if(token.IsCancellationRequested)
                    return;
                
                await ProcessForIndividualsAsync(Individuals, mainFitnessFunction);
                var offspring = Breed();
                await ProcessForIndividualsAsync(offspring, offspringFitnessFunction);
                Update(offspring);
                await OnGenerationCompleted();
            }
        }
        
        private List<Individual> Breed()
        {
            var parentPairs = _selectionOperator.SelectParents(Individuals, _matingPoolSize);
            MatingPool = parentPairs.Select(p => p.Item1).ToList();
            var offspring = _crossoverOperator.ReproduceOffspring(parentPairs, _offspringSize);
            _mutationOperator.Mutate(offspring, _mutationRate);
            return offspring;
        }
        
        private void Update(List<Individual> offspring)
        {
            Individuals.AddRange(offspring);
            Individuals = Individuals
                .OrderByDescending(i => i.Score)
                .Take(_populationSize)
                .ToList();
        }

        private async Task ProcessForIndividualsAsync(List<Individual> individuals, IFitnessFunction function)
        {
            var tasks = new List<Task>();
            for (int i = 0; i < individuals.Count; i++)
            {
                int id = i;
                tasks.Add(Task.Run(async () =>
                {
                    var score = await function.EvaluateAsync(individuals[id]);
                    individuals[id].Score = score;
                }));
            }
            await Task.WhenAll(tasks);
        }

        private async Task OnGenerationCompleted()
        {
            await GenerationCompleted?.Invoke(State);
        }
        
        private void AdjustIdentifiers()
        {
            for (var i = 0; i < Individuals.Count; i++)
            {
                Individuals[i].Id = i;
            }
        }
    }
}
