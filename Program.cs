namespace TopKFrequentElements
{
    internal class Program
    {
        public class TopKFrequentElements
        {
            #region MaxHeap Solution (with custom max heap)
            private class MaxHeap
            {
                private class Node
                {
                    public readonly int value;
                    public readonly int frequence;

                    public Node(int value, int frequence)
                    {
                        this.value = value;
                        this.frequence = frequence;
                    }
                }

                private readonly List<Node> heap;
                private void Swap(int i, int j)
                {
                    (heap[i], heap[j]) = (heap[j], heap[i]);
                }

                private void SiftDown(int currentIdx, int endIdx)
                {
                    int childOneIdx = currentIdx * 2 + 1;
                    while(childOneIdx <= endIdx)
                    {
                        int swapIdx = childOneIdx;
                        int childTwoIdx = currentIdx * 2 + 2;
                        if (childTwoIdx <= endIdx && heap[childTwoIdx].frequence > heap[childOneIdx].frequence)
                        {
                            swapIdx = childTwoIdx;
                        }
                        if (heap[swapIdx].frequence > heap[currentIdx].frequence)
                        {
                            Swap(swapIdx, currentIdx);
                            currentIdx = swapIdx;
                            childOneIdx = currentIdx * 2 + 1;
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                private void SiftUp(int currentIdx)
                {
                    int parentIdx = (currentIdx - 1) / 2;
                    while(parentIdx >= 0 && heap[parentIdx].frequence < heap[currentIdx].frequence)
                    {
                        Swap(currentIdx, parentIdx);
                        currentIdx = parentIdx;
                        parentIdx = (currentIdx - 1) / 2;
                    }
                }

                public MaxHeap()
                {
                    heap = new List<Node>();
                }

                public void Push(int value, int frequence)
                {
                    heap.Add(new Node(value, frequence));
                    SiftUp(heap.Count - 1);
                }

                public int GetMaxFrequenceValue()
                {
                    Swap(0, heap.Count - 1);
                    Node node = heap[^1];
                    heap.RemoveAt(heap.Count - 1);
                    SiftDown(0, heap.Count - 1);
                    return node.value;
                }
            }
            public int[] TopKFrequentHeap(int[] nums, int k)
            {
                int[] frequence = new int[k];
                Dictionary<int, int> frequencies = new();
                foreach (int num in nums)
                {
                    frequencies[num] = frequencies.GetValueOrDefault(num) + 1;
                }
                MaxHeap maxHeap = new();
                foreach (var freq in frequencies)
                {
                    maxHeap.Push(freq.Key, freq.Value);
                }
                for (int i = 0; i < k; ++i)
                {
                    frequence[i] = maxHeap.GetMaxFrequenceValue();
                }
                return frequence;
            }
            #endregion

            #region Bucket Sort Solution
            public int[] TopKFrequent(int[] nums, int k)
            {
                List<int> topKFrequent = new();
                List<int>[] buckets = new List<int>[nums.Length + 1];
                Dictionary<int, int> frequents = new();
                foreach (int num in nums)
                {
                    frequents[num] = frequents.GetValueOrDefault(num, 0) + 1;
                }
                foreach(var frequent in frequents)
                {
                    if (buckets[frequent.Value] is null)
                    {
                        buckets[frequent.Value] = new List<int>();
                    }
                    buckets[frequent.Value].Add(frequent.Key);
                }
                for (int i = buckets.Length - 1; i >= 0; --i)
                {
                    for (int j = 0; topKFrequent.Count < k && j < buckets[i]?.Count; ++j)
                    {
                        topKFrequent.Add(buckets[i][j]);
                    }
                    if (topKFrequent.Count == k)
                    {
                        break;
                    }
                }
                return topKFrequent.ToArray();
            }
            #endregion
        }

        static void Main(string[] args)
        {
            TopKFrequentElements topKFrequentElements = new();
            Console.WriteLine(string.Join(", ", topKFrequentElements.TopKFrequent(new int[] { 1, 1, 1, 2, 2, 3 }, 2)));
            Console.WriteLine(string.Join(", ", topKFrequentElements.TopKFrequent(new int[] { 1 }, 1)));
            Console.WriteLine(string.Join(", ", topKFrequentElements.TopKFrequentHeap(new int[] { 1, 1, 1, 2, 2, 3 }, 2)));
            Console.WriteLine(string.Join(", ", topKFrequentElements.TopKFrequentHeap(new int[] { 1 }, 1)));
            Console.WriteLine(string.Join(", ", topKFrequentElements.TopKFrequentHeap(new int[] { 5, 3, 1, 1, 1, 3, 73, 1 }, 2)));
            Console.WriteLine(string.Join(", ", topKFrequentElements.TopKFrequentHeap(new int[] { 2, 3, 4, 1, 4, 0, 4, -1, -2, -1 }, 2)));
        }
    }
}