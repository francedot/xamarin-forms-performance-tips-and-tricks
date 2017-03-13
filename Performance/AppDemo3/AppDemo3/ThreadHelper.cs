using System;

namespace AppDemo3
{
    public delegate bool IsAction();
    public static class ThreadHelper
    {
        public static object MainThread;

        public static IsAction IsMainThread;
    }
}