using Plugins.ReactiveProperty;

namespace Sorter.Features
{
    public class GameplayData
    {
        public ReactiveProperty<int> Score { get; private set; } = new();
        public ReactiveProperty<int> Health { get; private set; } = new();
    }
}