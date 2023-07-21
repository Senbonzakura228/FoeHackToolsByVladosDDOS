using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

public static class RequestQueueService
{
    private static readonly Queue<TaskCompletionSource<bool>> RequestQueue = new Queue<TaskCompletionSource<bool>>();

    public static async Task<bool> AddRequestToQueue(TaskCompletionSource<bool> requestTask = null)
    {
        RequestQueue.Enqueue(requestTask);
        Console.WriteLine(RequestQueue.Count);
        if (RequestQueue.Count == 1)
        {
            return true;
        }
        else
        {
            await WaitPreviousTasks(RequestQueue);
            RequestQueue.Dequeue();
            Console.WriteLine("Dequeue" + RequestQueue.Count);
            return true;
        }
    }

    private static async Task WaitPreviousTasks(Queue<TaskCompletionSource<bool>> currentQueue)
    {
        var queueArray = currentQueue.ToArray().ToList();
        for (var i = 0; i < queueArray.Count - 1; i++)
        {
            Task.Delay(100000).ContinueWith((t) =>
            {
                var index = i;
                if (queueArray.Count <= 0) return;
                if (!queueArray[i].Task.IsCompleted)
                {
                    queueArray[i].SetResult(true);
                }
            });
            Console.WriteLine("Wait" + i);
            await queueArray[i].Task;
        }
    }
}